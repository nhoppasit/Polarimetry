/* errhand.c
   This example demonstrates how a SICL error handler
   can be installed.
*/

#include <sicl.h>
#include <stdio.h>

main ()
{
   INST dvm;
   double res;

   #if defined(__BORLANDC__) && !defined(__WIN32__)
      _InitEasyWin();   // required for Borland EasyWin programs
   #endif

   ionerror (I_ERROR_EXIT);
   dvm = iopen ("gpib0,16");
   itimeout (dvm, 10000);
   iprintf (dvm, "%s\n", "MEAS:VOLT:DC?");
   iscanf (dvm, "%lf", &res);
   printf ("Result is %f\n", res);
   iclose (dvm);

/* For WIN16 programs, call _siclcleanup before exiting to release
   resources allocated by SICL for this application.  This call
   is a no-op for WIN32 programs. */
   _siclcleanup();

   return 0;
}
