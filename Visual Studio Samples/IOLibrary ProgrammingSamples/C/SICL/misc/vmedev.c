/* vmedev.c
   This example program opens a VXI interface session and sets
   up an interrupt handler.  When the specified interrupt occurs,
   the procedure defined in the interrupt handler is called. */
#include <stdio.h>
#include <stdlib.h>
#include <sicl.h>

#define ADDR "vxi"

void handler (INST id, long reason, long secval){
  printf ("Got the interrupt\n");
}

void main ()
{
  unsigned short reg;
  unsigned long a24map;
  INST id;

  /* install error handler */
  ionerror (I_ERROR_EXIT);

  /* open an interface communications session */
  id = iopen (ADDR);

  /* install interrupt handler */
  ionintr (id, handler);
  isetintr (id, I_INTR_VXI_VME, 1);

  /* turn off interrupt notification */
  iintroff ();

  /* map into user memory space */
  a24map = imapx (id, I_MAP_A24, 0x40, 1);

  /* read a register */
  ipeekx16(id, a24map, 0x00, &reg);
 
  /* print contents */
  printf ("The registers contents were as follows:  0x%04X\n", reg);

  /* write to a register causing interrupt */
  ipokex16 (id, a24map, 0x00, reg);

  /* wait for interrupt */
  iwaithdlr (10000);

  /* turn on interrupt notification */
  iintron ();

  /* unmap memory space */
  iunmapx (id, a24map, I_MAP_A24, 0x40, 1);

  /* close session */
  iclose (id);
}
