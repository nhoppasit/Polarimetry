/* vxidev.c
   The following example prompts the user for an instrument
   address and then reads the id register and device type
   register.  The contents of the register are then displayed. */

#include <stdio.h>
#include <stdlib.h>
#include <sicl.h>

void main ()
{
  char inst_addr[80];
  unsigned long a16map;
  unsigned short id_reg, devtype_reg;
  INST id;

  /* get instrument address */
  puts ("Please enter the logical address of the register-based instrument,               for example, vxi,24 :  \n");
  gets (inst_addr);

  /* install error handler */
  ionerror (I_ERROR_EXIT);

  /* open communications session with instrument */
  id  =  iopen (inst_addr);
  itimeout (id, 10000);

  /* map into user memory space */
  a16map = imapx (id, I_MAP_VXIDEV, 0, 1);

  /* read registers */
  ipeekx16 (id, a16map, 0x00, &id_reg);
  ipeekx16 (id, a16map, 0x02, &devtype_reg);

  /* print results */
  printf ("Instrument at address %s\n", inst_addr);
  printf ("ID Register = 0x%04X\n  Device Type Register = 0x%04X\n", id_reg, devtype_reg);

  /* unmap memory space */
  iunmapx (id, a16map, I_MAP_VXIDEV, 0, 1);

  /* close session */
  iclose (id);
}
