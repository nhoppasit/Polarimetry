/*formatio.c
  This example program makes a multimeter measurement with a comma
  separated list passed with formatted I/O and prints the results. 
  Note that you must change the device address. */

#include <visa.h>
#include <stdio.h>

void main () {

  ViSession defaultRM, vi;
  double res;
  double list [2] = {1,0.001};

  /* Open session to GPIB device at address 22 */
  viOpenDefaultRM (&defaultRM);
  viOpen (defaultRM, "GPIB0::22::INSTR", VI_NULL,VI_NULL, &vi);

  /* Initialize device */
  viPrintf (vi, "*RST\n");

  /* Set up device and send comma separated list */
  viPrintf (vi, "CALC:DBM:REF 50\n");
  viPrintf (vi, "MEAS:VOLT:AC? %,2f\n", list);

  /* Read results */
  viScanf (vi, "%lf", &res);

  /* Print results */
  printf ("Mesurement Results: %lf\n", res);

  /* Close session */
  viClose (vi);
  viClose (defaultRM);
}
