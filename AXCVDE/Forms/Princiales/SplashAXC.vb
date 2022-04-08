Public Class SplashAXC
    Sub New
        InitializeComponent()
    End Sub

    Public Overrides Sub ProcessCommand(ByVal cmd As System.Enum, ByVal arg As Object)
        MyBase.ProcessCommand(cmd, arg)
    End Sub

    Public Enum SplashScreenCommand
        SomeCommandId
    End Enum

    Private Sub peLogo_EditValueChanged(sender As Object, e As EventArgs) Handles peLogo.EditValueChanged

    End Sub
End Class
