/*nonfmtio.c
  This example program measures the AC voltage on a multimeter and
  prints the results. Note that you must change the device address. */

#include <visa.h>
#include <stdio.h>

void main () {

  ViSession defaultRM, vi;
  char strres [20];
  unsigned long actual;

  /* Open session to GPIB device at address 22 */
  viOpenDefaultRM (&defaultRM);
  viOpen (defaultRM, "GPIB0::22::INSTR", VI_NULL,VI_NULL, &vi);

  /* Initialize device */
  viWrite (vi, (ViBuf)"*RST\n", 5, &actual);

  /* Set up device and take measurement */
  viWrite (vi, (ViBuf)"CALC:DBM:REF 50\n", 16, &actual);
  viWrite (vi, (ViBuf)"MEAS:VOLT:AC? 1, 0.001\n", 23, &actual);

  /* Read results */
  viRead (vi, (ViBuf)strres, 20, &actual);
  
  /* NULL terminate the string */
  strres [actual]=0;
  
  /* Print results */
  printf ("Mesurement Results: %s\n", strres);

  /* Close session */
  viClose (vi);
  viClose (defaultRM);
}
