/* ser_dev.c
   This example program takes a measurement from a DVM
   using a SICL device session.
*/

#include <sicl.h>
#include <stdio.h>
#include <stdlib.h>

#if !defined(WIN32)
   #define LOADDS __loadds
#else
   #define LOADDS
#endif

void SICLCALLBACK LOADDS error_handler (INST id, int error) {

   printf ("Error: %s\n", igeterrstr (error));
   exit (1);
}

main()
{
   INST dvm;
   double res;

   #if defined(__BORLANDC__) && !defined(__WIN32__)
      _InitEasyWin();   // required for Borland EasyWin programs
   #endif

   /* Log message and terminate on error */
   ionerror (error_handler);

   /* Open the multimeter session */
   dvm = iopen ("COM1,488");
   itimeout (dvm, 10000);

   /* Prepare the multimeter for measurements */
   iprintf (dvm,"*RST\n");
   iprintf (dvm,"SYST:REM\n");

   /* Take a measurement */
   iprintf (dvm,"MEAS:VOLT:DC?\n");

   /* Read the results */
   iscanf (dvm,"%lf",&res);

   /* Print the results */
   printf ("Result is %f\n",res);

   /* Close the voltmeter session */
   iclose (dvm);

/* For WIN16 programs, call _siclcleanup before exiting to release
   resources allocated by SICL for this application.  This call
   is a no-op for WIN32 programs. */
   _siclcleanup();

   return 0;
}
