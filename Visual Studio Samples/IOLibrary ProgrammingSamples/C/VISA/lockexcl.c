/* lockexcl.c
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

  /* Make sure no other process or thread does anything to this resource
     between the viPrintf() and the viScanf() calls */
  viLock (vi, VI_EXCLUSIVE_LOCK, 2000, VI_NULL, VI_NULL);

  /* Send an *IDN? string to the device */
  viPrintf (vi, "*IDN?\n");
  
  /* Read results */
  viScanf (vi, "%t", &buf);

  /* unlock this session so other processes and threads can use it */
  viUnlock (vi);

  /* Print results */
  printf ("Instrument identification string: %s\n", buf);

  /* Close session */
  viClose (vi);
  viClose (defaultRM);
}
