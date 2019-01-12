/* errhand2.c
   This program shows how you can install your own
   error handler.
*/

#include <sicl.h>
#include <stdio.h>
#include <stdlib.h>

#if !defined(WIN32)
   #define LOADDS __loadds
#else
   #define LOADDS
#endif

void SICLCALLBACK LOADDS err_handler (INST id, int error) {

   fprintf (stderr, "Error: %s\n", igeterrstr (error));
   exit (1);
}

main () {
   INST dvm;
   double res;

   #if defined(__BORLANDC__) && !defined(__WIN32__)
      _InitEasyWin();   // required for Borland EasyWin programs
   #endif

   ionerror (err_handler);
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
