/* vximesdev.c
   This example program measures AC voltage on a multimeter and 
   prints out the results */
#include <sicl.h>
#include <stdio.h>

void main()
{
   INST dvm;
   char strres[20];

   /* Print message and terminate on error */
   ionerror (I_ERROR_EXIT);

   /* Open the multimeter session */
   dvm = iopen ("vxi,24");
   itimeout (dvm, 10000);
   
   /* Initialize dvm */
   iwrite (dvm, "*RST\n", 5, 1, NULL);

   /* Take measurement */
   iwrite (dvm,"MEAS:VOLT:AC? 1, 0.001\n", 23, 1, NULL);
  
   /* Read measurements */
   iread (dvm, strres, 20, NULL, NULL);

   /* Print the results */
   printf("Result is %s\n", strres);

   /* Close the multimeter session */
   iclose(dvm);

}

