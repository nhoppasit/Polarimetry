/* formatio.c
   This example program makes a multimeter mesurement with a comma  
   separated list passed with formatted I/O and prints the results
*/

#include <sicl.h>
#include <stdio.h>

main()
{
   INST dvm;

   double res;
   double list[2] = {1,0.001};

   #if defined(__BORLANDC__) && !defined(__WIN32__)
      _InitEasyWin();    /* required for Borland EasyWin programs */
   #endif

   /* Log message and terminate on error */
   ionerror (I_ERROR_EXIT);

   /* Open the multimeter session */
   dvm = iopen ("gpib0,16");
   itimeout (dvm, 10000);

   /*Initilize dvm*/
   iprintf (dvm, "*RST\n");

   /*Set up multimeter and send comma separated list*/
   iprintf (dvm, "CALC:DBM:REF 50\n");
   iprintf (dvm, "MEAS:VOLT:AC? %,2lf\n", list);

   /* Read the results */
   iscanf (dvm,"%lf", &res);

   /* Print the results */
   printf ("Result is %f\n", res);

   /* Close the mulitmeter session */
   iclose (dvm);

/* For WIN16 programs, call _siclcleanup before exiting to release
   resources allocated by SICL for this application.  This call
   is a no-op for WIN32 programs. */
   _siclcleanup();

   return 0;
}
