/* vxiintr.c
   The following example gets information about a specific
   vxi device and prints it out. */
#include <stdio.h>
#include <sicl.h>

void main () {
  int laddr;
  struct vxiinfo info;
  INST id;

  /* get instrument logical address */
  printf ("Please enter the logical address of the register-based                          instrument, for example, 24 :  \n");
  scanf ("%d", &laddr);

  /* install error handler */
  ionerror (I_ERROR_EXIT);

  /* open a vxi interface session */
  id  =  iopen ("vxi");
  itimeout (id, 10000);

  /* read resource manager information for specified device */
  ivxirminfo (id, laddr, &info);

  /* print results */
  printf ("Instrument at address %d\n", laddr);
  printf ("Manufacturer's Id = %s\n  Model = %s\n",
               info.manuf_name, info.model_name);

  /* close session */
  iclose (id);
}
