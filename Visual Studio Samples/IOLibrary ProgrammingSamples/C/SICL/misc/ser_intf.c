/* ser_intf.c
   This program does the following:
   1) gets the current configuration of the serial port,
   2) sets it to 9600 baud, no parity, 8 data bits, and
      1 stop bit, and
   3) Prints the old configuration.
*/

#include <stdio.h>
#include <sicl.h>

main()
{
   INST intf;                 /* interface session id */
   unsigned long baudrate, parity, databits, stopbits;
   char *parity_str;

   #if defined(__BORLANDC__) && !defined(__WIN32__)
      _InitEasyWin();   // required for Borland EasyWin programs
   #endif

   /* Log message and exit program on error */
   ionerror (I_ERROR_EXIT);

   /* open RS-232 interface session */
   intf = iopen ("COM1");
   itimeout (intf, 10000);

   /* get baud rate, parity, data bits, and stop bits */
   iserialstat (intf, I_SERIAL_BAUD,   &baudrate);
   iserialstat (intf, I_SERIAL_PARITY, &parity);
   iserialstat (intf, I_SERIAL_WIDTH,  &databits);
   iserialstat (intf, I_SERIAL_STOP,   &stopbits);

   /* determine string to display for parity */
   if      (parity == I_SERIAL_PAR_NONE)   parity_str = "NONE";
   else if (parity == I_SERIAL_PAR_ODD)    parity_str = "ODD";
   else if (parity == I_SERIAL_PAR_EVEN)   parity_str = "EVEN";
   else if (parity == I_SERIAL_PAR_MARK)   parity_str = "MARK";
   else   /*parity == I_SERIAL_PAR_SPACE*/ parity_str = "SPACE";

   /* set to 9600,NONE,8,1 */
   iserialctrl (intf, I_SERIAL_BAUD,   9600);
   iserialctrl (intf, I_SERIAL_PARITY, I_SERIAL_PAR_NONE);
   iserialctrl (intf, I_SERIAL_WIDTH,  I_SERIAL_CHAR_8);
   iserialctrl (intf, I_SERIAL_STOP,   I_SERIAL_STOP_1);

   /* Display previous settings */
   printf("Old settings:  %5ld,%s,%ld,%ld\n",
      baudrate, parity_str, databits, stopbits);

   /* close port */
   iclose (intf);

/* For WIN16 programs, call _siclcleanup before exiting to release
   resources allocated by SICL for this application.  This call
   is a no-op for WIN32 programs. */
   _siclcleanup();

   return 0;
}
