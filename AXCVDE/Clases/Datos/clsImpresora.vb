Imports AXC

Public Class clsImpresora

    Private _listaImpresoras As DataSet


    Function ListaImpresoras(ByVal prmPlanta As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmPlanta") = prmPlanta
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListadoImpresora")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function AgregaImpresora(ByVal prmImpresora As String, ByVal prmPlanta As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Try
            Dim dato As New clsDato
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            dato.SP(True) = "spInsCreaKanban"

            Dim wpEstado As String = ""
            Dim wpTextoERR As String = ""

            While dato.DataReader.Read

                wpEstado = dato.DataReader.Item("Estado")
                wpTextoERR = dato.DataReader.Item("Texto")

            End While

            If wpEstado = "ER" Then
                Return wpTextoERR
            ElseIf wpEstado = "OK" Then
                Return wpEstado
            End If
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function


    Function ListaProcesosImpresoras(ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryProcesosImpresoras", "@", prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar procesos"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar procesos "
            Return pRespuesta
        End Try
    End Function

    Friend Function CargarImpresora(prmEstacion As String, prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryImpresorasAdmin", prmEstacion, prmUsuario)

            If pResultado.Estado Then
                ds = pResultado.Resultado

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
                            pRespuesta.Texto = "Error al tratar de listar información"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar información"
            Return pRespuesta
        End Try
    End Function

    Function AsociaProcesoImpresora(ByVal prmProceso As String, ByVal prmImpresora As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdImpresoraProceso", prmProceso, prmImpresora, prmEstacion, prmUsuario)

            If CResultado.Estado Then
                dr = CType(CResultado.Resultado, DataRow)


                If dr("Estado") = "ER" Then
                    cRespuesta.Estado = False
                    cRespuesta.Texto = dr("Texto").ToString
                    cRespuesta.Resultado = Nothing
                Else
                    cRespuesta.Estado = True
                    cRespuesta.Texto = ""
                    cRespuesta.Resultado = Nothing
                End If
            Else
                cRespuesta.Estado = False
                cRespuesta.Texto = CResultado.Texto
                cRespuesta.Resultado = Nothing
            End If
        Catch ex As Exception
            cRespuesta.Estado = False
            cRespuesta.Texto = ex.Message
        End Try
        Return cRespuesta
    End Function
End Class
