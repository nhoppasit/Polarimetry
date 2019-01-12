/* evntqueu.c
   This example program illustrates enabling an event queue 
   using viWaitOnEvent. Note that you must change the address. */

#include <visa.h>
#include <stdio.h>


void main(){
   ViSession defaultRM,vi;
   ViEventType eventType;
   ViEvent eventVi;
   ViStatus err;
   ViInt16 trigId;

   /* open session to VXI device */
   viOpenDefaultRM(&defaultRM);
   viOpen(defaultRM, "VXI0::24::INSTR", VI_NULL, VI_NULL, &vi);

   /* select trigger line TTL0 */
   viSetAttribute(vi, VI_ATTR_TRIG_ID, VI_TRIG_TTL0);
   
   /* enable the event */
   viEnableEvent(vi, VI_EVENT_TRIG, VI_QUEUE, VI_NULL);
   
   /* fire trigger line, twice */
   viAssertTrigger(vi, VI_TRIG_PROT_SYNC);
   viAssertTrigger(vi, VI_TRIG_PROT_SYNC);

   /* Wait for the event to occur */
   err=viWaitOnEvent(vi, VI_EVENT_TRIG, 10000, &eventType, &eventVi);
   if(err==VI_ERROR_TMO){
      printf("Timeout Occurred! Event not received.\n");
      return;
   }
   
   /* print the event information */
   printf("Trigger Event Occurred!\n");
   printf("...Original Device Session = %ld\n", vi);

   /* get trigger that fired */
   viGetAttribute(eventVi, VI_ATTR_RECV_TRIG_ID, &trigId);
   printf("Trigger that fired: ");
   switch(trigId){
      case VI_TRIG_TTL0:
         printf("TTL0");
         break;
      default:
         printf("<other 0x%x>",trigId);
         break;
   }
   printf("\n");

   /* close the context before continuing */
   viClose(eventVi);

   /* get second event */
   err=viWaitOnEvent(vi, VI_EVENT_TRIG, 10000, &eventType, &eventVi);
   if(err==VI_ERROR_TMO){
      printf("Timeout Occurred! Event not received.\n");
      return;
   }
   printf("Got second event\n");

   /* close the context before continuing */
   viClose(eventVi);
   

   /* unenable event */
   viDisableEvent(vi, VI_EVENT_TRIG, VI_QUEUE);

   /* close the sessions */
   viClose(vi);
   viClose(defaultRM);
}
