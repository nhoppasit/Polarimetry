/* evnthdlr.c
   This example program illustrates installing an event handler to
   be called when a trigger interrupt occurs. Note that you must
   change the address. */

#include <visa.h>
#include <stdio.h>


/* trigger event handler */
ViStatus _VI_FUNCH myHdlr(ViSession vi, ViEventType eventType,
                          ViEvent ctx, ViAddr userHdlr){
   ViInt16 trigId;

   /* make sure it is a trigger event */
   if(eventType!=VI_EVENT_TRIG){
      /* Stray event, so ignore */
      return VI_SUCCESS;
   }

   /* print the event information */
   printf("Trigger Event Occurred!\n");
   printf("...Original Device Session = %ld\n", vi);

   /* get the trigger that fired */
   viGetAttribute(ctx, VI_ATTR_RECV_TRIG_ID, &trigId);
   printf("Trigger that fired: ");
   switch(trigId){
      case VI_TRIG_TTL0:
         printf("TTL0");
         break;
      default:
         printf("<other 0x%x>", trigId);
         break;
   }
   printf("\n");

   return VI_SUCCESS;
}

void main(){
   ViSession defaultRM,vi;

   /* open session to VXI device */
   viOpenDefaultRM(&defaultRM);
   viOpen(defaultRM, "VXI0::24::INSTR", VI_NULL, VI_NULL, &vi);

   /* select trigger line TTL0 */
   viSetAttribute(vi, VI_ATTR_TRIG_ID, VI_TRIG_TTL0);
   
   /* install the handler and enable it */
   viInstallHandler(vi, VI_EVENT_TRIG, myHdlr, (ViAddr)10);
   viEnableEvent(vi, VI_EVENT_TRIG, VI_HNDLR, VI_NULL);
   
   /* fire trigger line, twice */
   viAssertTrigger(vi, VI_TRIG_PROT_SYNC);
   viAssertTrigger(vi, VI_TRIG_PROT_SYNC);

   /* unenable and uninstall the handler */
   viDisableEvent(vi, VI_EVENT_TRIG, VI_HNDLR);
   viUninstallHandler(vi, VI_EVENT_TRIG, myHdlr, (ViAddr)10);

   /* close the sessions */
   viClose(vi);
   viClose(defaultRM);
}
