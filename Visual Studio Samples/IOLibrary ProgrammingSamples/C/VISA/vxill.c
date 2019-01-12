/*vxill.c
  This example program uses the low level functions to
  read the id and device type registers of the device 
  at VXI0::24.  Change this address if necessary.  The 
  register contents are then displayed.*/

#include <visa.h>
#include <stdlib.h>
#include <stdio.h>

void main () {

  ViSession defaultRM, dmm;
  ViAddr address;
  unsigned short id_reg, devtype_reg;

  /* Open session to VXI device at address 24 */
  viOpenDefaultRM (&defaultRM);
  viOpen (defaultRM, "VXI0::24::INSTR", VI_NULL,VI_NULL, &dmm);

  /* Map into memory space */
  viMapAddress (dmm, VI_A16_SPACE, 0x00, 0x10, VI_FALSE, VI_NULL, &address);

  /* Read instrument id register contents */
  viPeek16 (dmm, address, &id_reg); 

  /* Read device type register contents */
  /* ViAddr is defined as a void * so we must cast it to something else */
  /* in order to do pointer arithmetic */
  viPeek16 (dmm, (ViAddr)((ViUInt16 *)address + 0x01), &devtype_reg);

  /* Unmap memory space */
  viUnmapAddress (dmm);

  /* Print results */
  printf ("ID Register = 0x%4X\n", id_reg);
  printf ("Device Type Register = 0x%4X\n", devtype_reg);

  /* Close sessions */
  viClose (dmm);
  viClose (defaultRM);
}
