/* gpibstat.c
   The following example retrieves and displays
   GPIB bus status information.
*/

#include <stdio.h>
#include <sicl.h>

main()
{
   INST id;                 /* session id        */
   int rem;                 /* remote enable     */
   int srq;                 /* service request   */
   int ndac;                /* not data accepted */
   int sysctlr;             /* system controller */
   int actctlr;             /* active controller */
   int talker;              /* talker            */
   int listener;            /* listener          */
   int addr;                /* bus address       */

   #if defined(__BORLANDC__) && !defined(__WIN32__)
      _InitEasyWin();   // required for Borland EasyWin programs
   #endif

   /* exit process if SICL error detected */
   ionerror(I_ERROR_EXIT);

   /* open GPIB interface session */
   id = iopen("gpib0");
   itimeout (id, 10000);

   /* retrieve GPIB bus status */
   igpibbusstatus(id, I_GPIB_BUS_REM,      &rem);
   igpibbusstatus(id, I_GPIB_BUS_SRQ,      &srq);
   igpibbusstatus(id, I_GPIB_BUS_NDAC,     &ndac);
   igpibbusstatus(id, I_GPIB_BUS_SYSCTLR,  &sysctlr);
   igpibbusstatus(id, I_GPIB_BUS_ACTCTLR,  &actctlr);
   igpibbusstatus(id, I_GPIB_BUS_TALKER,   &talker);
   igpibbusstatus(id, I_GPIB_BUS_LISTENER, &listener);
   igpibbusstatus(id, I_GPIB_BUS_ADDR,     &addr);

   /* display bus status */
   printf("%-5s%-5s%-5s%-5s%-5s%-5s%-5s%-5s\n", "REM", "SRQ",
         "NDC", "SYS", "ACT", "TLK", "LTN", "ADDR");
   printf("%2d%5d%5d%5d%5d%5d%5d%6d\n", rem, srq, ndac, 
          sysctlr, actctlr, talker, listener, addr);

/* For WIN16 programs, call _siclcleanup before exiting to release
   resources allocated by SICL for this application.  This call
   is a no-op for WIN32 programs. */
   _siclcleanup();

   return 0;
}
