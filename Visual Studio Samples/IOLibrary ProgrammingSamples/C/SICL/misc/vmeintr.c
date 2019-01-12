/* vmeintr.c
   This example uses SICL to cause a VME interrupt from an
   Agilent E1361 register-based relay card at logical address 136. */
#include <stdio.h>
#include <sicl.h>

static void vmeint (INST, unsigned short);
static void int_setup (INST, unsigned long);
static void int_hndlr (INST, long, long);
int intr = 0;

void main() {
  INST id_intf1;
  unsigned long mask = 1;

  ionerror (I_ERROR_EXIT);
  iintroff ();
  id_intf1 = iopen ("vxi,136");
  int_setup (id_intf1, mask);
  vmeint (id_intf1, 136);

  /* wait for SRQ or interrupt condition */
  iwaithdlr (0);

  iintron ();
  iclose (id_intf1);
}

static void int_setup(INST id, unsigned long mask) {
  ionintr(id, int_hndlr);
  isetintr(id, I_INTR_VXI_SIGNAL, mask);
}

static void vmeint (INST id, unsigned short laddr) {
  int reg;
  unsigned long a16map = 0;

  reg = 8;
  a16map = imapx (id, I_MAP_A16, 0, 1);

  /* Cause relay card to interrupt: */
  ipokex16(id, a16map, 0xc000 + (laddr * 64) + reg, 0x0);

  iunmapx(id, a16map, I_MAP_A16, 0, 1);
}

static void int_hndlr (INST id, long reason, long sec) {
  printf ("VME interrupt: reason: 0x%lx, sec: 0x%lx\n", reason, sec);
  intr = 1;
}
