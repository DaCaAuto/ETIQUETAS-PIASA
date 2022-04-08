Imports DevExpress.Skins
Imports DevExpress.Skins.XtraForm
Imports DevExpress.Utils
Imports DevExpress.XtraEditors
Public Class FrmMenuPrincipal

    Protected Overrides ReadOnly Property ExtendNavigationControlToFormTitle As Boolean
        Get
            Return False
        End Get
    End Property

    Private Sub FrmMenuPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            WindowsFormsSettings.DefaultFont = New System.Drawing.Font("Arial", 13) ', FontStyle.Bold)
            'DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = New Font("Tahoma", 13)
            'WindowsFormsSettings.DefaultPrintFont = New System.Drawing.Font("Tahoma", 13)
            'AccordionControl1.OptionsMinimizing.State = DevExpress.XtraBars.Navigation.AccordionControlState.Minimized
            'AccordionControl1.OptionsMinimizing.State = DevExpress.XtraBars.Navigation.AccordionControlState.Normal


            'WindowsFormsSettings.DefaultMenuFont = New System.Drawing.Font("Tahoma", 15)
            FrmLogin.Close()
            'AccordionControl1.Font = New Font(AccordionControl1.Font.FontFamily, 20)

            'AccordionControl1.CollapseAll()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "AXC")
        End Try

    End Sub

    Private Sub FrmMenuPrincipal_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try

            FrmLogin.Show()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message)
        End Try
        FrmLogin.Show()
    End Sub


End Class


