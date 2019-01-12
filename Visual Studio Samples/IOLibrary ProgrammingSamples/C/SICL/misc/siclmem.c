/*
   siclmem.c
   This example program demonstrates the use of
   simple and block memory I/O methods in SICL.
*/

#include <sicl.h>
#include <stdlib.h>
#include <stdio.h>

#define VXI_INST "vxi,24"

void main () {
   INST           id;
   unsigned long  memPtr16;
   unsigned short id_reg;
   unsigned short devtype_reg;
   unsigned short memArray[2];
   int            err;

   /* Open a session to our instrument */
   id = iopen(VXI_INST);

   /* Map into memory space */
   memPtr16 = imapx(id, I_MAP_VXIDEV, 0, 1);

   /* ================== using peek ================================
      Read instrument id register contents */
   ipeekx16(id, memPtr16, 0x0, &id_reg);

   /*
      Read device type register contents
   */
   ipeekx16(id, memPtr16, 0x2, &devtype_reg);

   /* Print results */
   printf(" ipeekx16: ID Register = 0x%04X\n", id_reg);
   printf(" ipeekx16: Device Type Register = 0x%04X\n", devtype_reg);

   /*
   ==================================================================
   ======================== block memory I/O ========================
      = iblockmovex

   This command offers the best performance for reading and writing
   large data blocks on the VXI backplane.  Note that for this
   example we are only moving 2 words at a time.  Normally this
   function would be used to move much larger blocks of data.
   ==================================================================
   ==================================================================
   */
   
   /*
   ================== Demonstrate block read ========================
   Read the instrument id register and device type register into
   an array.
   */
   err = iblockmovex(id, memPtr16, 0, 16, 1, 0, (unsigned long)memArray, 16, 1, 2, 1);

   /* Print results */
   printf(" iblockmovex: ID Register = 0x%04X\n", memArray[0]);
   printf(" iblockmovex: Device Type Register = 0x%04X\n", memArray[1]);

   /*
   ==================== Demonstrate popfifo =========================
   */
   
   /* Do a popfifo of the Id Register */
   err = iblockmovex(id, memPtr16, 0, 16, 0, 0, (unsigned long)memArray, 16, 1, 2, 1);
   
   /* Print results */
   printf(" iblockmovex(fifo): 1 ID Register = 0x%04X\n", memArray[0]);
   printf(" iblockmovex(fifo): 2 ID Register = 0x%04X\n", memArray[1]);

   /*
   ===================== Cleanup and exit ===========================
   */

   /* Unmap memory space */
   iunmapx(id, memPtr16, I_MAP_VXIDEV, 0, 1);

   /* Close instrument session */
   iclose(id);
}
