Imports DevExpress.XtraEditors

Public Class Menu

    Private formCount As Integer = 0

    Private Sub AccordionControlElement2_Click(sender As Object, e As EventArgs) Handles AccordionControlElement2.Click
        Try
            AddNewForm(New DisenoEtiqueta())
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "AXC")
        End Try
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

    Private Sub FluentDesignFormContainer1_Click(sender As Object, e As EventArgs) Handles FluentDesignFormContainer1.Click

    End Sub
End Class
