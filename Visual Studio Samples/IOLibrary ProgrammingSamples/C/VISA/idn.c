/*idn.c
  This example program queries a GPIB device for an identification string 
  and prints the results. Note that you must change the address. */

#include <visa.h>
#include <stdio.h>

void main () {

  ViSession defaultRM, vi;
  char buf [256] = {0};
  

  /* Open session to GPIB device at address 22 */
  viOpenDefaultRM (&defaultRM);
  viOpen (defaultRM, "GPIB0::22::INSTR", VI_NULL,VI_NULL, &vi);

  /* Initialize device */
  viPrintf (vi, "*RST\n");

  /* Send an *IDN? string to the device */
  viPrintf (vi, "*IDN?\n");
  
  /* Read results */
  viScanf (vi, "%t", &buf);

  /* Print results */
  printf ("Instrument identification string: %s\n", buf);

  /* Close session */
  viClose (vi);
  viClose (defaultRM);
}
