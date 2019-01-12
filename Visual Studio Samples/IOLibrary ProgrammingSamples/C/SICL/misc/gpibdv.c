/* gpibdv.c
   This example program sends a scan list to a switch and while
   looping closes channels and takes measurements.
*/

#include <sicl.h>
#include <stdio.h>

main()
{
   INST dvm;
   INST sw;

   double res;
   int i;

   #if defined(__BORLANDC__) && !defined(__WIN32__)
      _InitEasyWin();   // required for Borland EasyWin programs
   #endif

   /* Log message and terminate on error */
   ionerror (I_ERROR_EXIT);

   /* Open the multimeter and switch sessions*/
   dvm = iopen ("gpib0,9,3");
   sw = iopen ("gpib0,9,14");
   itimeout (dvm, 10000);
   itimeout (sw, 10000);

   /*Set up trigger*/
   iprintf (sw, "TRIG:SOUR BUS\n");

   /*Set up scan list*/
   iprintf (sw,"SCAN (@100:103)\n");
   iprintf (sw,"INIT\n");

   for (i=1;i<=4;i++)
   {
      /* Take a measurement */
      iprintf (dvm,"MEAS:VOLT:DC?\n");

      /* Read the results */
      iscanf (dvm,"%lf",&res);

      /* Print the results */
      printf ("Result is %f\n",res);

      /*Trigger to close channel*/
      iprintf (sw, "TRIG\n");
   }
   /* Close the multimeter and switch sessions */
   iclose (dvm);
   iclose (sw);

/* For WIN16 programs, call _siclcleanup before exiting to release
   resources allocated by SICL for this application.  This call
   is a no-op for WIN32 programs. */
   _siclcleanup();

   return 0;
}
