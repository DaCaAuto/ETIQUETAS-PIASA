Public Class ClsPlanta
    'variables
    'Dim dato As New clsDato

    Function ListaPlantas(ByVal prmPlanta As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("@prmPlanta") = prmPlanta
            'dato.Parametro("prmEstacion") = prmEstacion
            ds = dato.Tabla("spQryListadoPlanta")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function CreaPlanta(ByVal prmPlanta As String, ByVal prmDescripcion As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim wpEstado As String = ""
        Dim wpTextoERR As String = ""
        Try
            Dim dato As New clsDato
            dato.Parametro("prmPlanta") = prmPlanta
            dato.Parametro("prmDescripcion") = prmDescripcion
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = "spInsCreaPlanta"

            While dato.DataReader.Read

                wpEstado = dato.DataReader.Item("Estado")
                wpTextoERR = dato.DataReader.Item("Texto")

            End While

            If wpEstado = "ER" Then
                Return wpTextoERR
            ElseIf wpEstado = "OK" Then
                Return wpTextoERR
            End If
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function

    Friend Function ListaPlantas(prmPlanta As String, prmEstacion As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListadoPlantas", prmPlanta, prmEstacion)

            If pResultado.Estado Then
                ds = CType(pResultado.Resultado, DataSet)

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy
                            pRespuesta.Estado = True
                            pRespuesta.Texto = ""
                            pRespuesta.Tabla = dt
                        Case "ER"
                            pRespuesta.Estado = False
                            pRespuesta.Texto = dr("Texto").ToString
                        Case Else
                            pRespuesta.Estado = False
                            pRespuesta.Texto = "Error al tratar de listar plantas"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar plantas "
            Return pRespuesta
        End Try
    End Function
End Class
