VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3195
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3195
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton cmdOpen 
      Caption         =   "Open Relay"
      Height          =   615
      Left            =   2400
      TabIndex        =   1
      Top             =   1080
      Width           =   1815
   End
   Begin VB.CommandButton cmdClose 
      Caption         =   "Close Relay"
      Height          =   615
      Left            =   2400
      TabIndex        =   0
      Top             =   240
      Width           =   1815
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim mgr As New VisaComLib.ResourceManager
Dim Switch As New FormattedIO488

Private Sub cmdClose_Click()
    
    Switch.WriteString "Close (@112)"
    
End Sub

Private Sub cmdOpen_Click()
    
    Switch.WriteString "Open (@112)"
    
End Sub

Private Sub Form_Load()


    Set Switch.IO = mgr.Open("GPIB::9")


End Sub
