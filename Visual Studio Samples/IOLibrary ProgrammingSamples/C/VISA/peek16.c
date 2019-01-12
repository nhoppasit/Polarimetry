#include <stdio.h>  // for printf()
#include <stdlib.h> // for exit()
#include "visa.h"

#define EXIT    1
#define NO_EXIT 0
//
// This function simplifies checking for VISA errors.
//
void checkError( ViSession vi, ViStatus status, char *errStr, int doexit ) {
   char buf[256];
   if (status >= VI_SUCCESS)
      return;
   buf[0] = 0;
   viStatusDesc( vi, status, buf );
   printf( "ERROR 0x%lx (%s)\n  '%s'\n", status, errStr, buf );
   if ( doexit == EXIT )
      exit( 1 );
}

void main() {
   ViSession drm;
   ViSession vi;
   ViUInt16  inData16  = 0;
   ViUInt16  peekData16= 0;
   ViUInt8   *addr;
   ViUInt16  *addr16;
   ViStatus  status;
   ViUInt16  offset;

   status = viOpenDefaultRM( &drm );
   checkError( 0, status, "viOpenDefaultRM", EXIT );
   //
   // Open a session to the vxi memacc resource
   //
   status = viOpen( drm, "vxi0::memacc", VI_NULL, VI_NULL, &vi );
   checkError( drm, status, "viOpen", EXIT );
   //
   // Calculate the A16 offset of the VXI registers for the device
   // at VXI logical address 8
   //
   offset = 0xc000 + 64 * 8;
   //
   // Open a map to all of A16 space
   //
   status = viMapAddress( vi, VI_A16_SPACE, 0, 0x10000, VI_FALSE, 0, (ViPAddr)(&addr) );
   checkError( vi, status, "viMapAddress", EXIT );
   //
   // Offset the address pointer returned from viMapAddress for use with viPeek16
   //
   addr16 = (ViUInt16 *)(addr + offset );
   //
   // Peek the contents of the card's ID register (Offset 0 from card's base address)
   // Note that viPeek does not return a status code.
   //
   viPeek16( vi, addr16, &peekData16 );
   //
   // Now use viIn16 and read the contents of the same register
   //
   status = viIn16( vi, VI_A16_SPACE, (ViBusAddress)offset, &inData16 );
   checkError( vi, status, "viIn16", NO_EXIT );
   //
   // Print the results
   //
   printf( "inData16  : 0x%04hx\n", inData16 );
   printf( "peekData16: 0x%04hx\n", peekData16 );

   viClose( vi );
   viClose( drm );
}
