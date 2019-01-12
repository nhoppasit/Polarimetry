/* locking.c
   This example shows how device locking can be
   used to gain exclusive access to a device*/

#include <sicl.h>
#include <stdio.h>

main()
{
   INST dvm;

   char strres[20];
   unsigned long actual;

   #if defined(__BORLANDC__) && !defined(__WIN32__)
      _InitEasyWin();   // required for Borland EasyWin programs 
   #endif
   
   /* Log message and terminate on error */
   ionerror (I_ERROR_EXIT);

   /* Open the multimeter session */
   dvm = iopen ("gpib0,16");
   itimeout (dvm, 10000);

   /* Lock the multimeter device to prevent access from
       other applications*/
   ilock(dvm);

   /* Take a measurement  */
   iwrite (dvm, "MEAS:VOLT:DC?\n", 14, 1, NULL);

   /* Read the results */
   iread (dvm, strres, 20, NULL, &actual);

   /* Release the multimeter device for use by others */
   iunlock(dvm);

   /* NULL terminate result string and print the results */
   /* This technique assumes the last byte sent was a line-feed */
   if (actual) {
     strres[actual - 1] = (char) 0;
     printf("Result is %s\n", strres);
   }

   /* Close the multimeter session */
   iclose(dvm);

   // For WIN16 programs, call _siclcleanup before exiting to release
   // resources allocated by SICL for this application.  This call
   // is a no-op for WIN32 programs.
   _siclcleanup();

   return 0;
}

