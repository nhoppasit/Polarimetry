/////////////////////////////////////////////////////////////////////
//
// The following simple demonstration program uses the Standard 
// Instrument Control Library to query an GPIB instrument for
// an identification string and then prints the result.
//
// Edit the DEVICE_ADDRESS line below to specify the address of the 
// device you want to talk to.  For example:
//
//     gpib0,0    - refers to an GPIB device at bus address 0 
//                  connected to an interface named "gpib0" by the 
//                  I/O Config utility.
//
//     gpib0,9,0  - refers to an GPIB device at bus address 9, 
//                  secondary address 0, connected to an interface 
//                  named "gpib0" by the I/O Config utility.
//
// Note that this program is meant to be built either as a WIN16 
// QuickWin or EasyWin program on 16 bit Windows 95, or as a WIN32
// console application on 32 bit Windows 95 or Windows NT.  Also 
// note that WIN16 programs must be compiled with the Large memory 
// model.
//
/////////////////////////////////////////////////////////////////////

#include <stdio.h>              // for printf()
#include "sicl.h"		// Standard Instrument Control Library routines

#define DEVICE_ADDRESS "gpib0,0"   // Modify this line to match your setup

void main(void)
{
   INST id;                 	// device session id
   char buf[256] = { 0 };   	// read buffer for idn string

   #if defined(__BORLANDC__) && !defined(__WIN32__)
     _InitEasyWin();		// required for Borland EasyWin programs.
   #endif

   // Install a default SICL error handler that logs an error message and
   // exits.  On Windows 95 view messages with the SICL Message Viewer,
   // and on Windows NT use the Windows NT Event Viewer.
   ionerror(I_ERROR_EXIT);	

   // Open a device session using the DEVICE_ADDRESS
   id = iopen(DEVICE_ADDRESS);

   // Set the I/O timeout value for this session to 1 second
   itimeout(id, 1000);
  
   // Write the *RST string (and send an EOI indicator) to put the instrument
   // in a known state.
   iprintf(id, "*RST\n");

   // Write the *IDN? string and send an EOI indicator, then read
   // the response into buf.  
   // For WIN16 programs, this will only work with the Large memory model 
   // since ipromptf expects to receive far pointers to the format strings.

   ipromptf(id, "*IDN?\n", "%t", buf);
   printf("%s\n", buf);

   iclose(id);

   // For WIN16 programs, call _siclcleanup before exiting to release 
   // resources allocated by SICL for this application.  This call is a
   // no-op for WIN32 programs.
   _siclcleanup();
}
