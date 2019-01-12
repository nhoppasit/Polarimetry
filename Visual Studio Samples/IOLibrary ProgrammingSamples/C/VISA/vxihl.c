/*vxihl.c
  This example program uses the high level functions to
  read the id and device type registers of the device at
  VXI0::24.  Change this address if necessary.  The 
  register contents are then displayed.*/

#include <visa.h>
#include <stdlib.h>
#include <stdio.h>

void main () {

  ViSession defaultRM, dmm;
  unsigned short id_reg, devtype_reg;

  /* Open session to VXI device at address 24 */
  viOpenDefaultRM (&defaultRM);
  viOpen (defaultRM, "VXI0::24::INSTR", VI_NULL,VI_NULL, &dmm);

  /* Read instrument id register contents */
  viIn16 (dmm, VI_A16_SPACE, 0x00, &id_reg); 

  /* Read device type register contents */
  viIn16 (dmm, VI_A16_SPACE, 0x02, &devtype_reg);

  /* Print results */
  printf ("ID Register = 0x%4X\n", id_reg);
  printf ("Device Type Register = 0x%4X\n", devtype_reg);

  /* Close sessions */
  viClose (dmm);
  viClose (defaultRM);
}
