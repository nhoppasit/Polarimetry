/* srqhdlr.c
   This example program illustrates installing an event handler to
   be called when an SRQ interrupt occurs. Note that you must
   change the address. */

#include <visa.h>
#include <stdio.h>
#if defined (_WIN32)
   #include <windows.h> /* for Sleep() */
   #define YIELD   Sleep( 10 )
#elif defined (__BORLANDC__)
   #include <windows.h>  /* for Yield() */
   #define YIELD Yield()
#elif defined (_WINDOWS)
   #include <io.h>      /* for _wyield */
   #define YIELD   _wyield()
#else
   #include <unistd.h>
   #define YIELD sleep (1)
#endif

int srqOccurred;

/* trigger event handler */
ViStatus _VI_FUNCH mySrqHdlr(ViSession vi, ViEventType eventType,
                          ViEvent ctx, ViAddr userHdlr){

   ViUInt16 statusByte;
   
   /* make sure it is an SRQ event */
   if(eventType!=VI_EVENT_SERVICE_REQ){
      /* Stray event, so ignore */
      printf( "\nStray event of type 0x%lx\n", eventType );
      return VI_SUCCESS;
   }

   /* print the event information */
   printf("\nSRQ Event Occurred!\n");
   printf("...Original Device Session = %ld\n", vi);

   /* get the status byte */
   viReadSTB(vi, &statusByte);
   printf("...Status byte is 0x%x\n", statusByte);

   srqOccurred = 1;
   return VI_SUCCESS;
}

void main(){
   ViSession defaultRM,vi;
   long      count;

   /* open session to message based VXI device */
   viOpenDefaultRM(&defaultRM);
   viOpen(defaultRM, "GPIB-VXI0::24::INSTR", VI_NULL, VI_NULL, &vi);

   /* Enable command error events */
   viPrintf( vi, "*ESE 32\n" );

   /* Enable event register interrupts */
   viPrintf( vi, "*SRE 32\n" );

   /* install the handler and enable it */
   viInstallHandler(vi, VI_EVENT_SERVICE_REQ, mySrqHdlr, (ViAddr)10);
   viEnableEvent(vi, VI_EVENT_SERVICE_REQ, VI_HNDLR, VI_NULL);
   
   srqOccurred = 0;
   
   /* Send a bogus command to the message based device to cause an SRQ */
   /* Note: 'IDN' causes the error -- '*IDN?' is the correct syntax */
   viPrintf( vi, "IDN\n" );
   
   /* Wait a while for the SRQ to be generated and for the handler */
   /* to be called. Print something while we wait */
   printf( "Waiting for an SRQ to be generated ." );
   for ( count = 0 ; (count < 10) && (srqOccurred == 0) ; count++ ) {
      long count2 = 0;
      printf( "." );
      while ( (count2++ < 100) && (srqOccurred ==0) ){
         YIELD;
      }
   }
   printf( "\n" );
    
   /* disable and uninstall the handler */
   viDisableEvent(vi, VI_EVENT_SERVICE_REQ, VI_HNDLR);
   viUninstallHandler(vi, VI_EVENT_SERVICE_REQ, mySrqHdlr, (ViAddr)10);
   
   /* Clean up after ourselves - don't leave device in error state */
   viPrintf( vi, "*CLS\n" );
   
   /* close the sessions */
   viClose(vi);
   viClose(defaultRM);
   
   printf( "End of program\n" );
}
