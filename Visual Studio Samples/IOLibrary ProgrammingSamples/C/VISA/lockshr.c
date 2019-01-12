/* lockshr.c
  This example program queries a GPIB device for an identification string 
  and prints the results. Note that you must change the address. */

#include <visa.h>
#include <stdio.h>

void main () {

  ViSession defaultRM, vi;
  char buf [256] = {0};
  char lockkey [256] = {0};
  

  /* Open session to GPIB device at address 22 */
  viOpenDefaultRM (&defaultRM);
  viOpen (defaultRM, "GPIB0::22::INSTR", VI_NULL,VI_NULL, &vi);

  /* acquire a shared lock so only this process and processes that
     we know about can access this resource */
  viLock (vi, VI_SHARED_LOCK, 2000, VI_NULL, lockkey);

  /* at this time, we can make 'lockkey' available to other processes
     that we know about.  This can be done with shared memory or other
     inter-process communication methods.  These other processes can
     then call "viLock(vi, VI_SHARED_LOCK, 2000, lockkey, lockkey)"
     and they will also have access to this resource.
  */

  /* Initialize device */
  viPrintf (vi, "*RST\n");

  /* Make sure no other process or thread does anything to this resource
     between the viPrintf() and the viScanf() calls
     NOTE:  this also locks out the processes with which we shared our
     'shared lock' key.
  */
  viLock (vi, VI_EXCLUSIVE_LOCK, 2000, VI_NULL, VI_NULL);

  /* Send an *IDN? string to the device */
  viPrintf (vi, "*IDN?\n");
  
  /* Read results */
  viScanf (vi, "%t", &buf);

  /* unlock this session so other processes and threads can use it */
  viUnlock (vi);

  /* Print results */
  printf ("Instrument identification string: %s\n", buf);

  /* release the shared lock too */
  viUnlock (vi);

  /* Close session */
  viClose (vi);
  viClose (defaultRM);
}
