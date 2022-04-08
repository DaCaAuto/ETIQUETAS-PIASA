Imports DevExpress.Skins
Imports DevExpress.XtraEditors

Public Class FrmMenuPrincipal
    Dim closingPending As Boolean = False
    Private formCount As Integer = 0

    Private Sub XtraForm2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "AXC")
        End Try
    End Sub

    Private Sub XtraForm2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            'If closingPending Then
            '    Return
            'End If

            If XtraMessageBox.Show("Se cerrará la aplicación y las pestañas, ¿Desea continuar?", "AXC", MessageBoxButtons.YesNo) = DialogResult.No Then
                e.Cancel = True
                Return
            End If
            '  closingPending = True
            Me.Dispose()
            Application.Exit()


        Catch ex As Exception
            XtraMessageBox.Show(ex.Message)
        End Try
        'FrmLogin.Show()
    End Sub
    Public Sub AddNewForm(ByVal frm As Form)
        Try

            XtraTabbedMdiManager1.MdiParent = Me
            formCount += 1
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "AXC")
        End Try
    End Sub

    Private Sub XtraTabbedMdiManager1_PageRemoved(sender As Object, e As DevExpress.XtraTabbedMdi.MdiTabPageEventArgs) Handles XtraTabbedMdiManager1.PageRemoved
        Try
            formCount -= 1

            If formCount = 0 Then

                XtraTabbedMdiManager1.MdiParent = Nothing

                Me.IsMdiContainer = False

            End If
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "AXC")
        End Try
    End Sub

    Private Sub XtraForm2_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Try
            Me.Refresh()
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "AXC")
        End Try
    End Sub


    Private Sub AccordionControlElement1_Click(sender As Object, e As EventArgs) Handles AccordionControlElement1.Click
        Try
            AddNewForm(New DisenoEtiqueta)
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "AXC")
        End Try
    End Sub

    Private Sub AccordionControlElement3_Click(sender As Object, e As EventArgs) Handles AccordionControlElement3.Click
        Try
            AddNewForm(New FrmImprimirEtiqueta)
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "AXC")
        End Try
    End Sub
End Class

