////////////////////////////////////////////////////////////////////////////////
//
// Filename: scope.c
//
// This program demonstrates the Standard Instrument Control
// Library within a Windows Program.
//
// This program requires two devices to run:
//      54601A digitizing oscilloscope (or compatible scope)
//      a printer capable of printing in HP RASTER GRAPHICS STANDARD
//      (e.g. ThinkJet printers)
// An optional multimeter such as an Agilent 34401A may also be connected.
//
// The windows application contains an "Action" menu with four
// commands:
//    "get_voltage"    Take voltage reading from multimeter
//    "perform_meas"   Program scope to make measurement, then upload results
//    "show_header"    Show scope settings
//    "print_disp"     Command scope to print its display.
//
// The commands that are sent to the scope are device dependent, and
// are found in the manual for the scope.
//
// This program is designed to illustrate several individual SICL
// commands and is not meant to be an example of a robust Windows
// application.
////////////////////////////////////////////////////////////////////////////////

#include <string.h>        // strlen()
#include <stdlib.h>        // exit()
#include <stdio.h>         // sprintf()
#include <windows.h>       // Windows API declarations
#include "sicl.h"          // Standard Instrument Control Library routines
#include "scope.h"         // message defines

// function prototypes
long FAR PASCAL _export WndProc(HWND hWnd, unsigned iMessage, unsigned wParam, long lParam);
void enable_io_menu_items(BOOL);
void SICLCALLBACK my_err_handler(INST, int);
void SICLCALLBACK my_srq_handler(INST);
void init_scope_io (void);
void close_scope (void);
void get_data (INST);
void get_voltage (void);
void show_scope_settings (void);
void print_disp (INST);

// defines
#define R_ELEMENTS       5000
#define TIMEOUT          5000     // 5 second timeout value (in ms)
#define MAX_LINES        20       // max number of lines printed on screen

// global data
HWND   hWnd;                      // application window handle
HWND   hInst;
INST   scope;                     // session id for scope
unsigned 
int    readings [R_ELEMENTS];
int    num_lines=0;               // number of text lines to output
int    ioerror = 0;
unsigned char scopeidn[50];
unsigned char probe[8];
float  pre [20];
char   text_buf[MAX_LINES][80];   // used for displaying text output

/////////////////////////////////////////////////////////////////////
//
// WinMain:
//
// Parameters:
//   hInstance - number that uniquely identifies this program
//   hPrevInstance - handle of previous instance of this program
//   lpszCmdLine - pointer to command line argument string
//   nCmdShow - indicates how the window will be displayed
//
// Description:
//   This routine implements the Windows event loop for the application.
//
/////////////////////////////////////////////////////////////////////
int PASCAL WinMain (HANDLE hInstance, HANDLE hPrevInstance, LPSTR lpszCmdLine,
                    int nCmdShow)
{
    static char szAppName[] = "scope";
    MSG            msg;
    WNDCLASS    wndclass;
    lpszCmdLine = lpszCmdLine;

    if (!hPrevInstance) {
        wndclass.style          = CS_HREDRAW | CS_VREDRAW;
        wndclass.lpfnWndProc    = WndProc;
        wndclass.cbClsExtra     = 0;
        wndclass.cbWndExtra     = 0;
        wndclass.hInstance      = hInstance;
        wndclass.hIcon          = LoadIcon (hInstance, "scopeicon");
        wndclass.hCursor        = LoadCursor ((HMODULE)NULL, IDC_ARROW);
        wndclass.hbrBackground  = GetStockObject (WHITE_BRUSH);
        wndclass.lpszMenuName   = "scopemenu";
        wndclass.lpszClassName  = szAppName;

        if (!RegisterClass (&wndclass))
            return FALSE;
    }


    hWnd = CreateWindow (szAppName,
                         "scope",
                         WS_OVERLAPPEDWINDOW,
                         CW_USEDEFAULT,
                         CW_USEDEFAULT,
                         800,
                         400,
                         (HWND)NULL,
                         (HMENU)NULL,
                         hInstance,
                         NULL);

    ShowWindow (hWnd, nCmdShow);

    UpdateWindow (hWnd);

    init_scope_io();

    while(GetMessage (&msg, (HWND)NULL, 0, 0)) {
        TranslateMessage (&msg);
        DispatchMessage (&msg);
    }

    return msg.wParam;
}

/////////////////////////////////////////////////////////////////////
//
// WndProc:
//
// Parameters:
//   hWnd - identifies the window receiving the message
//   iMessage - identifies the message
//   wParam - provides more information about the message
//   lParam - provides more information about the message
//
// Description:
//   The following routine processes window messages.
//
/////////////////////////////////////////////////////////////////////
long FAR PASCAL _export WndProc (HWND hWnd, unsigned iMessage, unsigned wParam, long lParam)
{
    static int      xChar;
    static int      yChar;
    HDC             hDC;
    PAINTSTRUCT     ps;
    TEXTMETRIC      tm;
    int             i;

    switch (iMessage) {
        case WM_CREATE:
            hInst = ((LPCREATESTRUCT) lParam) -> hInstance;

            hDC = GetDC (hWnd);
            GetTextMetrics (hDC, &tm);
            xChar = tm.tmAveCharWidth;
            yChar = tm.tmHeight;
            ReleaseDC (hWnd, hDC);

            num_lines = 0;  // set message output to top of window

            break;

        case WM_DESTROY:

            close_scope();

            PostQuitMessage (0);
            break;

        case WM_COMMAND:
            if (!LOWORD (lParam)) {
                switch (wParam) {
                    case SCOPE_EXIT:
                        SendMessage (hWnd, WM_CLOSE, 0, 0L);
                        break;

                    case SCOPE_READ_VOLT:
                        get_voltage();
                        break;

                    case SCOPE_GET_DATA:
                        get_data(scope);
                        break;

                    case SCOPE_HEADER:
                        show_scope_settings();
                        break;

                    case SCOPE_PRINT:
                        print_disp(scope);
                        break;

                    default:
                        return DefWindowProc (hWnd, iMessage, wParam, lParam);
                }
            } else {
                return DefWindowProc (hWnd, iMessage, wParam, lParam);
            }

            break;

        case WM_PAINT:
            hDC = BeginPaint (hWnd, &ps);

            for (i = 0; i <num_lines; i++) {
                TextOut(hDC, xChar, yChar * i, text_buf[i], strlen(text_buf[i]));
            }

            EndPaint (hWnd, &ps);
            break;

        default:
            return DefWindowProc (hWnd, iMessage, wParam, lParam);
    }
    return 0L;
}

/////////////////////////////////////////////////////////////////////
//
// enable_io_menu_items:
//
// Parameters:
//   enable_menus - TRUE to enable menus items, FALSE to disable
//
// Description:
//   This routine is used to disable I/O menu selections while
//   calls to the Standard Instrument Control Library are in
//   progress.
//
/////////////////////////////////////////////////////////////////////
void enable_io_menu_items(BOOL enable_menus)
{
    HMENU hMenu;
    unsigned  io_menu_state;

    hMenu = GetMenu(hWnd);

    if (enable_menus && !ioerror)
        io_menu_state = MF_ENABLED;
    else
        io_menu_state = MF_GRAYED;

    EnableMenuItem(hMenu, SCOPE_READ_VOLT, io_menu_state);
    EnableMenuItem(hMenu, SCOPE_GET_DATA, io_menu_state);
    EnableMenuItem(hMenu, SCOPE_PRINT, io_menu_state);

}


/////////////////////////////////////////////////////////////////////
//
// my_err_handler:
//
// Parameters:
//   id - identifies the I/O session in which an error occured.
//   err - the error that occured
//
// Description:
//   This routine is installed with ionerror() and is called when an
//   error occurs in a call to the Standard Instrument Control Library.
//   It will cause a message to be written to the application
//   window and will prevent any further actions.
//
/////////////////////////////////////////////////////////////////////
void SICLCALLBACK my_err_handler(INST id, int error)
{
    HMENU hMenu;

    sprintf(text_buf[num_lines++],
             "session id=%d, error = %d:%s", id, error, igeterrstr(error));
    sprintf(text_buf[num_lines++], "Select `File | Exit' to exit program!");

    InvalidateRect(hWnd, NULL, TRUE);

    // If error is from scope, disable I/O actions by graying out menu picks.
    if (id == scope) {
        hMenu = GetMenu(hWnd);

        EnableMenuItem(hMenu, SCOPE_READ_VOLT, MF_GRAYED);
        EnableMenuItem(hMenu, SCOPE_GET_DATA, MF_GRAYED);
        EnableMenuItem(hMenu, SCOPE_HEADER, MF_GRAYED);
        EnableMenuItem(hMenu, SCOPE_PRINT, MF_GRAYED);

        // set flag indicating that a scope error occurred.
        ioerror = 1;
    }

    // set flag indicating that a scope error occurred.
    ioerror = 1;
}

/////////////////////////////////////////////////////////////////////
//
// my_srq_handler:
//
// Parameters:
//   id - identifies the I/O session for the SRQ.
//
// Description:
//   This routine is called when an SRQ is generated by a device.
//
/////////////////////////////////////////////////////////////////////
void SICLCALLBACK my_srq_handler(INST id)
{
    unsigned char status;

    // make sure it was the scope requesting service
    ireadstb(id,&status);

    if (status &= 64) {
        // clear the status byte so the scope can assert SRQ again if needed.
        iprintf(id,"*CLS\n");

        sprintf(text_buf[num_lines++],
                 "SRQ received!, stat=0x%x",status);
    } else {
        sprintf(text_buf[num_lines++],
                 "SRQ received, but not from the scope");
    }
    InvalidateRect(hWnd, NULL, TRUE);
}

/////////////////////////////////////////////////////////////////////
//
// init_scope_io:
//
// Parameters:
//   none
//
// Description:
//   This routine installs a SICL error handler, opens a device
//   session for the scope and puts the scope in a known state.
//
/////////////////////////////////////////////////////////////////////
void init_scope_io(void)
{
    // install custom error handler
    ionerror(my_err_handler);

    // open a device session to the scope
    scope = iopen("gpib0,7");

    if (scope == 0) {
        sprintf(text_buf[num_lines++],"Oscilloscope iopen failed!");
    } else {
        sprintf(text_buf[num_lines++],"Oscilloscope session initialized!");
        // set a timeout value for the session
        itimeout(scope, TIMEOUT);

        // put the scope in a known state
        iclear(scope);
        iremote(scope);
    }
    InvalidateRect(hWnd, NULL, TRUE);  // print output
}

/////////////////////////////////////////////////////////////////////
//
// close_scope:
//
// Parameters:
//   none
//
// Description:
//   This routine cleans up, closes the scope session and calls
//   _siclcleanup.
//
/////////////////////////////////////////////////////////////////////
void close_scope(void)
{
    // give local control back to the scope
    ilocal(scope);

    // close the session
    iclose(scope);

    // Call _siclcleanup before exiting to release resources allocated
    // by SICL for this application (required for Windows 3.1).
    _siclcleanup();
}


/////////////////////////////////////////////////////////////////////
//
// get_voltage:
//
// Parameters:
//   none
//
// Description:
//   The following routine gets voltage readings from the Digital
//   MultiMeter specified by the symbolic name "dmm" in SICL.INI
//
/////////////////////////////////////////////////////////////////////
void get_voltage(void)
{
    INST dmm;
    double voltage;
    unsigned char cmd[50];

    enable_io_menu_items(FALSE);  // do this before making sicl calls

    dmm = iopen("gpib0,22");
    itimeout(dmm, 500);

    // put the meter in a known state and get its IDN string.
    iprintf(dmm,"*RST;*CLS\n");
    ipromptf(dmm,"*IDN?\n","%s",cmd);

    sprintf(text_buf[num_lines++],"DMM is %s",cmd);

    // make a DC voltage reading and get the result.
    iprintf(dmm,"measure:volt:dc?\n");
    iscanf(dmm,"%lf",&voltage);

    sprintf(text_buf[num_lines++],"  Current voltage: %lf V",voltage);

    // close the session
    iclose(dmm);

    InvalidateRect(hWnd, NULL, TRUE);  // print output

    enable_io_menu_items(TRUE);  // do this after making all sicl calls
}

/////////////////////////////////////////////////////////////////////
//
// get_data:
//
// Parameters:
//   INST id for the scope device session.
//
// Description:
//   The following routine gets settings and waveform data from the
//   scope.  The device is locked during the measurement to prevent
//   outside access.
//
/////////////////////////////////////////////////////////////////////
void get_data (INST id)
{
    long   elements;

    enable_io_menu_items(FALSE);  // do this before making sicl calls

    // lock the device to prevent access from other applications
    ilock(scope);

    // initialize scope
    //iprintf(id,"*RST\n");

    // retrieve the scope's ID string
    ipromptf(id,"*IDN?\n","%s",scopeidn);

    // setup up the waveform source

    //iprintf(id,":autoscale\n");
    iprintf(id,":waveform:format word\n");

    // input waveform preamble to controller

    iprintf(id,":digitize channel1\n");
    iprintf(id,":waveform:preamble?\n");

    // read the scope preamble - 20 comma-separated values.
    iscanf(id,"%,20f\n",pre);

    // command scope to send the data
    iprintf(id,":waveform:data?\n");

    // enter the data
    elements = R_ELEMENTS;
    iscanf(id,"%#wb\n", &elements, readings);

    if ( elements == R_ELEMENTS ) {
        sprintf (text_buf[num_lines++],"Readings buffer full: May not have received all points");
        InvalidateRect(hWnd, NULL, TRUE);
    }

    // Get probe attenuation
    ipromptf(id,":chan1:probe?\n","%s",probe);

    // release the scope for use by others
    iunlock(scope);

    sprintf(text_buf[num_lines++]," Oscilloscope waveform upload complete!");
    InvalidateRect(hWnd, NULL, TRUE);  // print output

    enable_io_menu_items(TRUE);  // do this after making all sicl calls
}

/////////////////////////////////////////////////////////////////////
//
// show_scope_settings:
//
// Parameters:
//   none
//
// Description:
//   This routine formats and prints the settings of the scope read
//   in the get_data routine.  It does no I/O.
//
/////////////////////////////////////////////////////////////////////
void show_scope_settings (void)
{
    float vdiv;
    float off;
    float sdiv;
    float delay;

    num_lines = 0;  // set message output to top of window

    vdiv  = 32 * pre [7];
    off   = -1.0 * ((128 - pre [9]) * pre [7] + pre [8]);
    sdiv  = pre [2] * pre [4] / 10;
    delay = (pre [2] / 2 - pre [6]) * pre [4] + pre [5];

    //    print the statistics about the data
    //
    sprintf (text_buf[num_lines++],"Oscilloscope ID:  %s", scopeidn);
    sprintf (text_buf[num_lines++]," ------  Current Channel 1 Settings  ------");
    sprintf (text_buf[num_lines++],"      Volts/Div = %f V", vdiv);
    sprintf (text_buf[num_lines++],"         Offset = %f V", off);
    sprintf (text_buf[num_lines++],"          Probe = %s"    , probe);
    sprintf (text_buf[num_lines++],"          S/Div = %f S", sdiv);
    sprintf (text_buf[num_lines++],"          Delay = %f S", delay);
    sprintf (text_buf[num_lines++]," ");
    InvalidateRect(hWnd, NULL, TRUE);
}

/////////////////////////////////////////////////////////////////////
//
// print_disp:
//
// Parameters:
//   INST id for the scope device session.
//
// Description:
//   Commands the scope to print it's display and send SRQ when
//   complete.  The iwaithdlr function is used to suspend operation
//   until the SRQ is received.
//
/////////////////////////////////////////////////////////////////////
void print_disp (INST id)
{
    INST gpibintf;
    unsigned char cmd[16];
    int length;

    enable_io_menu_items(FALSE);  // do this before making all sicl calls
    num_lines = 0;  // set message output to top of window

    // get the interface session for this device
    gpibintf = igetintfsess(id);

    // disable interrupt events
    iintroff();

    // install the SRQ handler
    ionsrq(id,my_srq_handler);   // Not supported on Agilent 82335

    // tell the scope to SRQ on 'operation complete'
    iprintf(id,"*CLS\n");
    iprintf(id,"*SRE 32; *ESE 1\n");

    // tell the scope to print
    iprintf(id,":print?; *OPC\n");

    // tell the scope to talk and printer to listen
    //    the listen command is formed by adding 32 to the device address
    //        of the device to be a listener
    //    the talk command is formed by adding 64 to the device address of
    //        the device to be a talker

    cmd[0] = (unsigned char)63;      // 63 is unlisten
    cmd[1] = (unsigned char)(32+1);  // printer at addr 1, make it a listener
    cmd[2] = (unsigned char)(64+7);  // scope at addr 7, make it a talker
    cmd[3] = '\0';                    // terminate the string

    length = strlen (cmd);

    // Use GPIB specific commands to control the bus and interface
    igpibsendcmd(gpibintf,cmd,length);
    igpibatnctl(gpibintf,0);

    sprintf (text_buf[num_lines++],"Waiting for print to complete...");
    InvalidateRect(hWnd, NULL, TRUE);

    // wait for SRQ  or 30 seconds before continuing.
    iwaithdlr(30000L);

    // Re-enable interrupt events.
    iintron();

    sprintf (text_buf[num_lines++],"Printing complete!");
    InvalidateRect(hWnd, NULL, TRUE);

    enable_io_menu_items(TRUE);  // do this after making all sicl calls
}
