/* nonfmt.c
   This example program measures AC voltage on a multimeter and 
   prints out the results*/ 

#include <sicl.h>
#include <stdio.h>

main()
{
   INST dvm;
   char strres[20];
   unsigned long actual;

   #if defined(__BORLANDC__) && !defined(__WIN32__)
      _InitEasyWin();   /* required for Borland EasyWin programs */
   #endif
   
   /* Log message and terminate on error */
   ionerror (I_ERROR_EXIT);

   /* Open the multimeter session */
   dvm = iopen ("gpib0,16");
   itimeout (dvm, 10000);

   /*Initialize dvm*/
   iwrite (dvm, "*RST\n", 5, 1, NULL);

   /*Set up multimeter and take measurements*/ 
   iwrite (dvm,"CALC:DBM:REF 50\n",16,1,NULL); 
   iwrite (dvm,"MEAS:VOLT:AC? 1, 0.001\n",23,1,NULL);

   /* Read measurements */
   iread (dvm, strres, 20, NULL, &actual);

   /* NULL terminate result string and print the results */
   /* This technique assumes the last byte sent was a line-feed */
   if (actual) {
     strres[actual - 1] = (char) 0;
     printf("Result is %s\n", strres);
   }

  /* Close the multimeter session */
  iclose(dvm);
  
  /* For WIN16 programs, call _siclcleanup before exiting to release
     resources allocated by SICL for this application.  This call
     is a no-op for WIN32 programs.  */
  _siclcleanup();
 
  return 0; 
}
