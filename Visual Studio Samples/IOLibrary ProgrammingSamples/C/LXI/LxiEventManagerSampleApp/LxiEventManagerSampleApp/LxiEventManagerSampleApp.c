/* ----------------------------------------------------------------------------
 * 
 * Copyright 2007 Agilent Technologies Inc. All Rights reserved.
 *
 * You have a royalty-free right to use, modify, reproduce, and distribute
 * the sample application files (and/or any modified version) in any way
 * you find useful, provided that you agree Agilent has no warranty, obligation
 * or liability for any sample application files.
 *
 * Agilent Technologies provides programming examples for illustration only.
 * This sample program assumes you are familiar with the programming language
 * being demonstrated and the tools used to create and debug procedures.
 * Agilent support engineers can help explain functionality of Agilent software
 * components and associated commands, but they will not modify tests samples
 * to provide added functionality or construct procedures to meet your specific needs.
 *
 * ----------------------------------------------------------------------------
 *
 * This program uses the LxiEventManagerC Api to listen for LXI Event LAN messages
 * and to send two such messages.
 * 
 * NOTE: this sample assumes IO Libaries and VISA are installed in the default location.
 * Custom locations for IO Libraries require changing the sample Visual Studio project.
 * (C/C++ 'Additional Include Directories' and Linker 'Additional Library Directories')
 *
 * ----------------------------------------------------------------------------  */

#include <windows.h>
#include <stdio.h>
#include <tchar.h>
#include <string.h>

#include "LxiEventManagerC.h"
int receiveCount;
DWORD waitResult;

ViStatus _VI_FUNC MessageArrivedHandler(ViString eventId, 
    ViReal64 receiptSeconds, 
    ViReal64 receiptFractionalSeconds,
	ViString eventSource,
	ViBoolean receivedViaUdp,
	LxiEventMessage message);

int IsError(ViStatus status, char * functionName);

int _tmain(int argc, _TCHAR* argv[])
{
	ViStatus result;
	ViSession lxiConnection;
	ViUInt32 seq;
	LxiEventMessage msg;
	ViReal64 secNow;
	ViReal64 fracSecNow;
	unsigned char dataBuf[] = {1,2,3};

	receiveCount = 0;

	// Install our MessageArrived handler
	result = lximgrInstallHandler(MessageArrivedHandler);
	if (IsError(result, "lximgrInstallHandler")) return 1;

	// Start notification of messages arriving on the default LXI network port
	result = lximgrStartMessageNotification(LXI_MGR_EVENT_PATH_LXIPORT);
	if (IsError(result, "lximgrStartMessageNotification")) return 1;

	// Open a connection to send LXI event messages to the default LXI multicast address
	result = lximgrOpen(LXI_MGR_EVENT_PATH_DEFAULT, &lxiConnection);
	if (IsError(result, "lximgrOpen")) return 1;

	// Send a simple LXI Event message
	result = lximgrSendEvent(lxiConnection, LXI_MGR_EVENT_ID_LAN0);
	if (IsError(result, "lximgrSendEvent")) return 1;

	// Create a custom LXI event and send that

	// Get the next sequence number to put in the message
	result = lximgrGetSequenceNumber(lxiConnection, LXI_MGR_DEFAULT_DOMAIN, &seq);
	if (IsError(result, "lximgrGetSequenceNumber")) return 1;

	// Create an Event Message, here using the Error ID.  
	// Could have used any of the LXI_MGR_EVENT_ID_xxx standard definitions
	// or any custom string up to 16 characters
	result = lximgrCreateEventMessage(LXI_MGR_EVENT_ID_ERROR, seq, &msg);
	if (IsError(result, "lximgrCreateEventMessage")) return 1;

	// Error ID Events should have this attribute flag set
	// (this demonstrates setting attributes)
	result = lximgrSetAttribute(msg, LXI_ATTR_EVENT_FLAGS, LXI_ATTR_EVENT_FLAG_ERROR);
	if (IsError(result, "lximgrSetAttribute")) return 1;
	
	// Add a custom data field with and ID of 99 (the data is defined above)
	result = lximgrAddDataField(msg, 99, 3, dataBuf);
	if (IsError(result, "lximgrAddDataField")) return 1;
	
	// Get the time now to use as the timestamp for the event
	result = lximgrTimeNow(&secNow, &fracSecNow);
	if (IsError(result, "lximgrTimeNow")) return 1;

	// Set the timestamp seconds  and fractional secondsin the Event message
	result = lximgrSetTimeAttribute(msg, LXI_ATTR_EVENT_SECONDS, secNow);
	if (IsError(result, "lximgrSetTimeAttribute")) return 1;
	result = lximgrSetTimeAttribute(msg, LXI_ATTR_EVENT_FRACTIONALSECONDS,fracSecNow);
	if (IsError(result, "lximgrSetTimeAttribute")) return 1;

	// Now send the custom Event message
	result = lximgrSendEventMessage(lxiConnection, msg);
	if (IsError(result, "lximgrSendEventMessage")) return 1;

	// Wait for the messages to be received
	do
	{
		Sleep(100);	
	} while (receiveCount < 2);

	// Now clean up before stopping the program

	// Delete the custom event message
	result = lximgrDeleteEventMessage(msg);
	if (IsError(result, "lximgrDeleteEventMessage")) return 1;

	// Close the connection for sending
	result = lximgrClose(lxiConnection);
	if (IsError(result, "lximgrClose")) return 1;

	// Stop listening for new event messages
	result = lximgrStopMessageNotification();
	if (IsError(result, "lximgrStopMessageNotification")) return 1;

	// Uninstall our message arrived handler
	result = lximgrUninstallHandler(MessageArrivedHandler);
	if (IsError(result, "lximgrUninstallHandler")) return 1;

	// Shutdown LxiEventManagerC
	result = lximgrShutdown();
	if (IsError(result, "lximgrShutdown")) return 1;

	printf("Press 'Enter' to stop this program\r\n");
	getchar();

	return 0;
}

ViStatus _VI_FUNC MessageArrivedHandler(ViString eventId, 
    ViReal64 receiptSeconds, 
    ViReal64 receiptFractionalSeconds,
	ViString eventSource,
	ViBoolean receivedViaUdp,
	LxiEventMessage message)
{
	ViStatus result;
	char msgEventID[20];
	char receiveTime[32];
	ViUInt8 domain;
	ViUInt16 flags;
	ViUInt32 seq;

	// The message parameter is the received message. It is only valid for the duration of this call.

	// Get the EventID (note this is actually provided by the 'eventID' parameter, too)
	result = lximgrGetAttribute(message, LXI_ATTR_EVENT_EVENTID, msgEventID);
	if (IsError(result, "lximgrGetAttribute(EventID)")) return result;

	// Get a few more fields
	result = lximgrGetAttribute(message, LXI_ATTR_EVENT_DOMAIN, &domain);
	if (IsError(result, "lximgrGetAttribute(Domain)")) return result;

	result = lximgrGetAttribute(message, LXI_ATTR_EVENT_FLAGS, &flags);
	if (IsError(result, "lximgrGetAttribute(Flags)")) return result;

	result = lximgrGetAttribute(message, LXI_ATTR_EVENT_SEQUENCE, &seq);
	if (IsError(result, "lximgrGetAttribute(Sequence)")) return result;

	lximgrTimeToString(receiveTime, 32, receiptSeconds, receiptFractionalSeconds);

	printf("Received Event '%s' at %s, domain %d, flags %x, sequence %d\r\n",
		msgEventID, receiveTime, domain, flags, seq);

	receiveCount++;

	return result;
}

int IsError(ViStatus status, char * functionName)
{
	char errorDesc[1024];
	if (status != VI_SUCCESS)
	{
		printf("Error %d returned by '%s'\r\n", status, functionName);

		lximgrGetLastStatusDesc(errorDesc, 1024);

		printf("Description: %s", errorDesc);

		printf("Press 'Enter' to stop this program\r\n");

		getchar();

		return 1;		
	}
	return 0;
}


