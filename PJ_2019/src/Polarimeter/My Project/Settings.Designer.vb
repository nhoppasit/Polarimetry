﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.1
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property IsDemo() As Boolean
            Get
                Return CType(Me("IsDemo"),Boolean)
            End Get
            Set
                Me("IsDemo") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Red")>  _
        Public Property ReferenceColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("ReferenceColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("ReferenceColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Blue")>  _
        Public Property Color1() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color1"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color1") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("192, 0, 192")>  _
        Public Property Color2() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color2"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color2") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0, 192, 192")>  _
        Public Property Color3() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color3"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color3") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("64, 64, 64")>  _
        Public Property Color4() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color4"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color4") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("192, 0, 192")>  _
        Public Property Color5() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color5"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color5") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0, 0, 192")>  _
        Public Property Color6() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color6"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color6") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0, 192, 192")>  _
        Public Property Color7() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color7"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color7") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0, 192, 0")>  _
        Public Property Color8() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color8"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color8") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("192, 192, 0")>  _
        Public Property Color9() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color9"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color9") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("192, 64, 0")>  _
        Public Property Color10() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color10"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color10") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("192, 0, 0")>  _
        Public Property Color11() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color11"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color11") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Gray")>  _
        Public Property Color12() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color12"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color12") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("255, 128, 255")>  _
        Public Property Color13() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color13"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color13") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("128, 128, 255")>  _
        Public Property Color14() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color14"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color14") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("128, 255, 255")>  _
        Public Property Color15() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color15"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color15") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("128, 255, 128")>  _
        Public Property Color16() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color16"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color16") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("255, 255, 128")>  _
        Public Property Color17() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color17"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color17") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("255, 192, 128")>  _
        Public Property Color18() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color18"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color18") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("255, 128, 128")>  _
        Public Property Color19() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color19"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color19") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Silver")>  _
        Public Property Color20() As Global.System.Drawing.Color
            Get
                Return CType(Me("Color20"),Global.System.Drawing.Color)
            End Get
            Set
                Me("Color20") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("15")>  _
        Public Property VoltageRange() As Double
            Get
                Return CType(Me("VoltageRange"),Double)
            End Get
            Set
                Me("VoltageRange") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0.0001")>  _
        Public Property VoltageResolution() As Double
            Get
                Return CType(Me("VoltageResolution"),Double)
            End Get
            Set
                Me("VoltageResolution") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.Polarimeter.My.MySettings
            Get
                Return Global.Polarimeter.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace