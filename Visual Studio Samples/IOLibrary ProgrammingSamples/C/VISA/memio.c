/*
   memio.c
   This example program demonstrates the use of various memory I/O
   methods in VISA.
*/

#include <visa.h>
#include <stdlib.h>
#include <stdio.h>

#define VXI_INST "VXI0::24::INSTR"

void main () {
   ViSession defaultRM, vi;
   ViAddr         address;
   ViUInt16       accessMode;
   unsigned short *memPtr16;
   unsigned short id_reg;
   unsigned short devtype_reg;
   unsigned short memArray[2];

   /* Open the default resource manager and a session to our instrument */
   viOpenDefaultRM (&defaultRM);
   viOpen (defaultRM, VXI_INST, VI_NULL,VI_NULL, &vi);

/* ==================================================================
   ====================== Low level memory I/O ======================
      = viPeek16
      = direct memory dereference (when allowed)
   ================================================================== */
   
   /* Map into memory space */
   viMapAddress (vi, VI_A16_SPACE, 0x00, 0x10, VI_FALSE, VI_NULL, &address);

   /* ================== using viPeek ================================
   /* Read instrument id register contents */
   viPeek16 (vi, address, &id_reg); 

   /*
      Read device type register contents
      ViAddr is defined as a (void *) so we must cast it to something
      else in order to do pointer arithmetic.
   */
   viPeek16 (vi, (ViAddr)((ViUInt16 *)address + 0x01), &devtype_reg);

   /* Print results */
   printf ("   viPeek16: ID Register = 0x%4X\n", id_reg);
   printf ("   viPeek16: Device Type Register = 0x%4X\n", devtype_reg);

   /* Use direct memory dereferencing if it is supported */
   viGetAttribute( vi, VI_ATTR_WIN_ACCESS, &accessMode );
   if ( accessMode == VI_DEREF_ADDR ) {

      /* assign the pointer to a variable of the correct type */
      memPtr16 = (unsigned short *)address;

      /* do the actual memory reads */
      id_reg =      *memPtr16;
      devtype_reg = *(memPtr16+1);

      /* Print results */
      printf ("dereference: ID Register = 0x%4X\n", id_reg);
      printf ("dereference: Device Type Register = 0x%4X\n", devtype_reg);
   }

   /* Unmap memory space */
   viUnmapAddress (vi);

/* ==================================================================
   ====================== High Level memory I/O =====================
      = viIn16
   ================================================================== */

  /* Read instrument id register contents */
  viIn16 (vi, VI_A16_SPACE, 0x00, &id_reg); 

  /* Read device type register contents */
  viIn16 (vi, VI_A16_SPACE, 0x02, &devtype_reg);

  /* Print results */
  printf ("     viIn16: ID Register = 0x%4X\n", id_reg);
  printf ("     viIn16: Device Type Register = 0x%4X\n", devtype_reg);

/* ==================================================================
   ================== High Level block memory I/O ===================
      = viMoveIn16
   The viMoveIn/viMoveOut commands do both block read/write and FIFO
   read write.

   These commands offer the best performance for reading and writing
   large data blocks on the VXI backplane.  Note that for this
   example we are only moving 2 words at a time.  Normally these
   functions would be used to move much larger blocks of data.
   ==================================================================

   If the value of VI_ATTR_SRC_INCREMENT is 1 (the default), then
   viMoveIn does a block read.
   If the value of VI_ATTR_SRC_INCREMENT is 0 then viMoveIn does a
   FIFO read.

   If the value of VI_ATTR_DEST_INCREMENT is 1 (the default), then
   viMoveOut does a block write.
   If the value of VI_ATTR_DEST_INCREMENT is 0 then viMoveOut does a
   FIFO write.

   ================================================================== */
   
  /*
     ================ Demonstrate block read ========================
     Read the instrument id register and device type register into
     an array.
  */
  viMoveIn16 (vi, VI_A16_SPACE, 0x00, 2, memArray); 

  /* Print results */
  printf (" viMoveIn16: ID Register = 0x%4X\n", memArray[0]);
  printf (" viMoveIn16: Device Type Register = 0x%4X\n", memArray[1]);

  /*
     ================== Demonstrate FIFO read ========================
     First set the source increment to 0 so we will repetatively read
     from the same memory location.
  */
  viSetAttribute( vi, VI_ATTR_SRC_INCREMENT, 0 );
  
  /* Do a FIFO read of the Id Register */
  viMoveIn16 (vi, VI_A16_SPACE, 0x00, 2, memArray);
   
  /* Print results */
  printf (" viMoveIn16: 1 ID Register = 0x%4X\n", memArray[0]);
  printf (" viMoveIn16: 2 ID Register = 0x%4X\n", memArray[1]);

  /* Close sessions */
  viClose (vi);
  viClose (defaultRM);

}