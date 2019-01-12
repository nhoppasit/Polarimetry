/*' """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
'    Copyright © 2002 Agilent Technologies Inc. All rights
'    reserved.
'
' You have a royalty-free right to use, modify, reproduce and distribute
' the Sample Application Files (and/or any modified version) in any way
' you find useful, provided that you agree that Agilent has no
' warranty,  obligations or liability for any Sample Application Files.
'
' Agilent Technologies provides programming examples for illustration only,
' This sample program assumes that you are familiar with the programming
' language being demonstrated and the tools used to create and debug
' procedures. Agilent support engineers can help explain the
' functionality of Agilent software components and associated
' commands, but they will not modify these samples to provide added
' functionality or construct procedures to meet your specific needs.
' """"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""

' This program finds the active clocks, then logs the synchronization history for those clocks to stdout.
'
' NOTE: This sample assumes IO Libraries is installed in the default location.
' Custom locations for either require changing the PtpSyncLogger project reference
' for Agilent.TMFramework.Lxi.Timing.PtpManager, which can be found in the
' IO Libraries Suite install directory.
'
' The format of the output is a comma-separated-value format suitable for consumption by Excel.
'
'  "SyncTime", <ClockUUID1>,, <ClockUUID2>,, ...
'  , "Offset", "Delay", "Offset", "Delay", ...
'  '<SyncSeconds>.<SyncNanoseconds>', <UUID1 Offset>, <UUID1 1WayDelay>, <UUID2 Offset>, <UUID2 1WayDelay>
'  ...
'
' Where:
'    <SyncSeconds>.<SyncNanoseconds> indicates the sync PTP time when the offsets and delays were valid
'    <UUIDx Offset> and <UUIDx 1WayDelay> are the Offset from master and One way delay as reals in seconds 
' */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Agilent.TMFramework.Lxi.Timing;

namespace PtpSyncLogger
{
    class Program
    {
        static PtpManager m_ptpMgr;
        static StreamWriter m_logFile;
        static PtpTime m_lastSyncTime;

        static void Main(string[] args)
        {
            string logFileName = "PtpSyncLog.csv";
            if (args.Length == 2)
                logFileName = args[1];
            else
            {
                Console.WriteLine("Command line syntax: PtpSyncLogger <logfilename>");
                Console.WriteLine("Logging to 'PtpSyncLog.csv'");
            }

            m_logFile = new StreamWriter(logFileName);
            m_lastSyncTime = null;

            m_ptpMgr = new PtpManager();

            FindActiveClocks();

            LogSynchronizationHeaders();

            // Register a handler for Sync messages (the handler does the writing to the log file)
            m_ptpMgr.MessageReceived += new MessageReceivedEventHandler(MessageReceivedHandler);

            m_ptpMgr.CaptureClockHistory = true;

            Console.WriteLine("Press 'Enter' to stop logging.");

            Console.Read();

            // Stop monitoring Sync messages
            m_ptpMgr.MessageReceived -= new MessageReceivedEventHandler(MessageReceivedHandler);

            m_logFile.Close();
        }

        static void FindActiveClocks()
        {
            // Search all Ptp domains for active clocks
            // (Note: this code could have used FindActiveDomains to do this)
            for (byte i = 0; i <= 127; i++)
                m_ptpMgr.AddSearchDomain(i);
            m_ptpMgr.UpdateClocks();
        }

        static void MessageReceivedHandler(object sender, MessageReceivedEventArgs args)
        {
            if (args.Message.MessageType == PtpMessageType.SyncMessage)
            {
                LogLastSynchronizationInfo();

                PtpSyncMessage syncMessage = (PtpSyncMessage)args.Message;

                m_lastSyncTime = syncMessage.OriginTimeStamp;
            }
        }

        static void LogSynchronizationHeaders()
        {
            string header1 = "SyncTime";
            string header2 = string.Empty;
            string clockSeparator = ", ";

            for (int clkInx = 0; clkInx < m_ptpMgr.Clocks.Count; clkInx++)
            {
                header1 += clockSeparator + m_ptpMgr.Clocks[clkInx].DefaultData.ClockIdentity;
                header2 += ", Offset, Delay";
                clockSeparator = ",, ";
            }

            Console.WriteLine(header1);
            Console.WriteLine(header2);

            m_logFile.WriteLine(header1);
            m_logFile.WriteLine(header2);
        }

        static void LogLastSynchronizationInfo()
        {
            if (m_lastSyncTime == null)
                return;

            // Start with time of last sync message (quoted to keep Excel from interpreting it as a double)
            string syncLine = string.Format("'{0}'",m_lastSyncTime.ToString());

            for (int clkInx = 0; clkInx < m_ptpMgr.Clocks.Count; clkInx++)
            {
                PtpClock clock = m_ptpMgr.Clocks[clkInx];
                PtpClockHistory history = null;

                for (int histInx = 0; histInx < clock.ClockHistory.Count; histInx++)
                {
                    if (clock.ClockHistory[histInx].TimeStamp.Seconds == m_lastSyncTime.Seconds &&
                        clock.ClockHistory[histInx].TimeStamp.NanoSeconds == m_lastSyncTime.NanoSeconds)
                    {
                        history = clock.ClockHistory[histInx];
                        break;
                    }
                }
                if (history != null)
                {
                    syncLine += string.Format(", {0}, {1}",
                        history.CurrentData.OffsetFromMaster,
                        history.CurrentData.MeanPathDelay);
                }
                else
                {
                    syncLine += ",, ";
                }
            }

            Console.WriteLine(syncLine);
            m_logFile.WriteLine(syncLine);
        }
    }
}
