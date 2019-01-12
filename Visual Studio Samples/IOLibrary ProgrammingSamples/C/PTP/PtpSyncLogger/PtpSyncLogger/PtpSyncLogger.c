/*----------------------------------------------------------------------------------------------
 *
 * This program finds the active clocks, then logs the synchronization history for those clocks to stdout.
 *
 * NOTE: This sample assumes IO Libraries and VISA are installed in the default location.
 * Custom locations for either require changing the PtpSyncLogger project
 * (C/C++ 'Additional Include Directories' and Linker 'Additional Library Directories')
 *
 * The format of the output is a comma-separated-value format suitable for consumption by Excel.
 *
 *  "SyncTime", <ClockUUID1>,, <ClockUUID2>,, ...
 *  , "Offset", "Delay", "Offset", "Delay", ...
 *  '<SyncSeconds>.<SyncNanoseconds>', <UUID1 Offset>, <UUID1 1WayDelay>, <UUID2 Offset>, <UUID2 1WayDelay>
 *  ...
 *
 * Where:
 *    <SyncSeconds>.<SyncNanoseconds> indicates the sync PTP time when the offsets and delays were valid
 *    <UUIDx Offset> and <UUIDx 1WayDelay> are the Offset from master and One way delay as reals in seconds 
 *    
 *----------------------------------------------------------------------------------------------*/

#include <stdio.h>
#include <tchar.h>

#include <string.h>
#include "PtpManagerC.h"

ViUInt64 m_lastSyncSeconds;
ViUInt32 m_lastSyncNanoseconds;

FILE * m_logFile;


ViStatus FindActiveClocks()
{
	ViStatus result;
	ViUInt8 i;
	ViInt32 clockCount;
	ViInt32 clkInx;
    ViBoolean updateRetry;

	/* Set to search for clock info in all domains */
	for (i = 0; i <= 127; i++)
		ptpAddSearchDomain(i);

	/* Look for all active clocks */
    updateRetry = VI_FALSE;
    while (1) {
        result = ptpUpdateClocks();
	    if (result != VI_SUCCESS)
        {
            /* Try a longer clock-query timeout */
            if (updateRetry != VI_TRUE)
            {
                updateRetry = VI_TRUE;
                /* Set the query-timeout to 1500 mSec's */
                ptpSetAttribute(PTP_ATTR_MANAGER_QUERY_TIMEOUT, 1500);
                continue;
            }
            else 
            {
                /* Update still failed when using a longer query timeout */
                return result;
            }
        }
        break;
    }

	/* Set the search domains to ones with active clocks */
	ptpResetSearchDomains();

	ptpGetClockCount(&clockCount);
	for (clkInx = 0; clkInx < clockCount; clkInx++)
	{
		ViUInt8 domain;
		ptpClock clock;

		result = ptpGetClock(clkInx, &clock);
		if (result == VI_SUCCESS)
		{
			ptpGetClockAttribute(clock, PTP_ATTR_DEFAULTDS_DOMAIN_NUMBER, &domain);
			ptpAddSearchDomain(domain);
		}
	}

	return VI_SUCCESS;
}

void LogSynchronizationHeaders()
{
	char header1[1024] = "SyncTime";
	char header2[1024]= "";
	char * clockSeparator;
	char clockID[256];
	ViStatus result;
	ViInt32 clockCount;
	ViInt32 clkInx;

	clockSeparator  = ", ";

	ptpGetClockCount(&clockCount);

	for (clkInx = 0; clkInx < clockCount; clkInx++)
	{
		ptpClock clock;

		result = ptpGetClock(clkInx, &clock);
		if (result == VI_SUCCESS)
		{
			/* Get the clock ID, being sure the clockID buffer is large enough */
			ptpGetClockAttribute(clock, PTP_ATTR_DEFAULTDS_CLOCK_IDENTITY, clockID);
			if (strlen(clockID) + 2 + strlen(header1) < 1024)
			{
				strcat(header1, clockSeparator);
				strcat(header1, clockID);
				strcat(header2, ", Offset, Delay");
				clockSeparator = ",, ";
			}
		}

	}

	printf("%s\r\n", header1);
	printf("%s\r\n", header2);

	fprintf(m_logFile, "%s\n", header1);
	fprintf(m_logFile, "%s\n", header2);
}

void LogLastSynchronizationInfo()
{
	ViStatus result;
	char syncLine[1024];
	char clockEntry[256];
	char lastSyncTime[256];
	ViInt32 clockCount;
	ViInt32 clkInx;
	ViInt32 histInx;

	if (m_lastSyncSeconds == 0) return;

	ptpGetClockCount(&clockCount);

	/* Get the PTP time of the last sync as a string */
	result = ptpTimeToString(lastSyncTime, 256, m_lastSyncSeconds, m_lastSyncNanoseconds, 0);

	/* Quote the time so Excel won't try to convert it to a float, losing precision */
	sprintf(syncLine, "'%s'", lastSyncTime);

	for (clkInx = 0; clkInx < clockCount; clkInx++)
	{
		ptpClock clock;
		ViInt32 histCount;
		int histEntryFound;
		histEntryFound = 0;

		result = ptpGetClock(clkInx, &clock);
		if (result != VI_SUCCESS)
			continue;
		
		ptpGetClockHistoryCount(clock, &histCount);
		
		/* Find the clock history associated with the last sync */
		for (histInx = 0; histInx < histCount; histInx++)
		{
			ViUInt64 histSec;
			ViUInt32 histNsec;
			ptpGetClockHistoryAttribute(clock, histInx, PTP_ATTR_HISTORY_TIMESTAMP_SECONDS, &histSec);
			ptpGetClockHistoryAttribute(clock, histInx, PTP_ATTR_HISTORY_TIMESTAMP_NANOSECONDS, &histNsec);
			if (histSec == m_lastSyncSeconds && histNsec == m_lastSyncNanoseconds)
			{
				ViReal64 offset;
				ViReal64 delay;
				ptpGetClockHistoryAttribute(clock, histInx, PTP_ATTR_HISTORY_OFFSET_FROM_MASTER, &offset);
				ptpGetClockHistoryAttribute(clock, histInx, PTP_ATTR_HISTORY_PATH_DELAY, &delay);
				sprintf(clockEntry,  ", %.9f, %.9f", offset, delay);
				strcat(syncLine, clockEntry);
				histEntryFound = 1;
				break;
			}
		}
		if (histEntryFound == 0)
			strcat(syncLine, ",,");
	}

	printf("%s\r\n", syncLine);
	fprintf(m_logFile, "%s\n", syncLine);
}

/* Message Received callback for PTP Sync messages */
ViStatus _VI_FUNC PtpMessageHandler(ViUInt32 receiptSecondsUpper, ViUInt32 receiptSecondsLower, ViUInt32 receiptNanoseconds, ViString msgSource)
{
	/* For each sync message (ignoring other messages) */
	ViUInt8 msgType;
	ViStatus result = ptpGetMsgAttribute(PTP_MSG_MESSAGE_TYPE, &msgType);
	if (result != VI_SUCCESS || msgType != PTP_MSG_MESSAGE_TYPE_SYNC)
		return result;

	/* Log the synchronization info from the previous sync interval. */
	LogLastSynchronizationInfo();

	/* Get the time stamp for this sync interval in preparation for logging the relevant sync info next time */
	ptpGetMsgAttribute(PTP_MSG_ORIGIN_TIMESTAMP_SECONDS, &m_lastSyncSeconds);
	ptpGetMsgAttribute(PTP_MSG_ORIGIN_TIMESTAMP_NANOSECONDS, &m_lastSyncNanoseconds);

	return VI_SUCCESS;
}

int _tmain(int argc, _TCHAR* argv[])
{
	ViStatus result;
	result = FindActiveClocks();
	if (result != VI_SUCCESS)
	{
		printf("Error finding clocks, error number %d\r\n", result);
		return 0;
	}

	if (argc != 2)
	{
		printf("Command line syntax: PtpSyncLogger <logfilename>\r\n");
		printf("Logging to 'PtpSyncLog.csv'\r\n");
		m_logFile = fopen("PtpSyncLog.csv", "w");
	}
	else
		m_logFile = fopen(argv[1], "w");

	printf("Press Enter to terminate sync logging.\r\n");

	LogSynchronizationHeaders();

	ptpSetAttribute(PTP_ATTR_MANAGER_CAPTURE_HISTORY, 1);

	ptpInstallSyncMsgHandler(PtpMessageHandler);

	getchar();

	ptpSetAttribute(PTP_ATTR_MANAGER_CAPTURE_HISTORY, 0);
	ptpUninstallSyncMsgHandler(PtpMessageHandler);
	ptpShutdown();

	fclose(m_logFile);

	return 0;
}

