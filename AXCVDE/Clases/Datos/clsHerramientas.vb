Public Class ClsHerramientas
    Function ListaTransacciones(ByVal prmUsuarioBuscar As String, ByVal prmEstacionBuscar As String, ByVal prmFechaDesde As String, ByVal prmFechaHasta As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmUsuarioBuscar") = prmUsuarioBuscar
            dato.Parametro("prmEstacionBuscar") = prmEstacionBuscar
            dato.Parametro("prmFechaDesde") = prmFechaDesde
            dato.Parametro("prmFechaHasta") = prmFechaHasta
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryRegTran")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaTransaccionesRPT(ByVal prmUsuarioBuscar As String, ByVal prmEstacionBuscar As String, ByVal prmFechaDesde As String, ByVal prmFechaHasta As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmUsuarioBuscar") = prmUsuarioBuscar
            dato.Parametro("prmEstacionBuscar") = prmEstacionBuscar
            dato.Parametro("prmFechaDesde") = prmFechaDesde
            dato.Parametro("prmFechaHasta") = prmFechaHasta
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spRptQryRegTran")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaReferencias(ByVal prmReferencia As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryInfo", prmReferencia, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar referencias"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar referencias "
            Return pRespuesta
        End Try
    End Function

    Function ActualizaEstatusTransfer(ByVal prmReferencia As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim dato As New clsDato
        Dim res As String = ""
        Try
            dato.Parametro("prmReferencia") = prmReferencia
            dato.Parametro("prmIdStatus") = 0
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = CMetodos.ConfigLeer("BaseDatosBI") + ".dbo.spUpdActualizaEstatusTransfer"

            Dim wpEstado As String = ""
            Dim wpTextoERR As String = ""

            While dato.DataReader.Read

                wpEstado = dato.DataReader.Item("Estado")
                wpTextoERR = dato.DataReader.Item("Texto")

            End While

            If wpEstado = "OK" Then
                Return wpEstado
            Else
                Return wpTextoERR
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return res
    End Function

    Function ActualizaEstatusRecibo(ByVal prmOrdenCompra As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim dato As New clsDato
        Dim res As String = ""
        Try
            dato.Parametro("prmOrdenCompra") = prmOrdenCompra
            dato.Parametro("prmIdStatus") = 5
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = CMetodos.ConfigLeer("BaseDatosBI") + ".dbo.spUpdActualizaEstatusReciboAdmin"

            MsgBox(CMetodos.ConfigLeer("BaseDatosBI") + ".dbo.spUpdActualizaEstatusReciboAdmin")

            Dim wpEstado As String = ""
            Dim wpTextoERR As String = ""

            While dato.DataReader.Read

                wpEstado = dato.DataReader.Item("Estado")
                wpTextoERR = dato.DataReader.Item("Texto")

            End While

            If wpEstado = "OK" Then
                Return wpEstado
            Else
                Return wpTextoERR
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return res
    End Function

    Function ListaMovimientos(ByVal prmNumParte As String, ByVal prmMovimiento As String, ByVal prmTipoAjuste As String, ByVal prmFechaInicio As String, ByVal prmFechaFin As String, ByVal prmLote As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryRegMovimientos", prmNumParte, prmMovimiento, prmTipoAjuste, prmFechaInicio, prmFechaFin, prmLote, prmEstacion, prmUsuario)

            If pResultado.Estado Then
                ds = CType(pResultado.Resultado, DataSet)

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy
                            pRespuesta.Estado = True
                            pRespuesta.Texto = ""
                            pRespuesta.Tablas = ds
                        Case "ER"
                            pRespuesta.Estado = False
                            pRespuesta.Texto = dr("Texto").ToString
                        Case Else
                            pRespuesta.Estado = False
                            pRespuesta.Texto = "Error al tratar de listar movimientos"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar movimientos "
            Return pRespuesta
        End Try
    End Function
    Function ListaTransaccionesSurtidor(ByVal prmUsuarioBuscar As String, ByVal prmEstacionBuscar As String, ByVal prmFechaDesde As String, ByVal prmFechaHasta As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmUsuarioBuscar") = prmUsuarioBuscar
            dato.Parametro("prmEstacionBuscar") = prmEstacionBuscar
            dato.Parametro("prmFechaDesde") = prmFechaDesde
            dato.Parametro("prmFechaHasta") = prmFechaHasta
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryTransSurtidor")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Public Function ListaTipoAjuste(ByVal prmAjuste As String, ByVal prmTipoMovimiento As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Dim dt As DataTable
        Dim ds As DataSet
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado

            pResultado = db.SPToDataSet("spQryListaTiposAjuste", prmAjuste, prmTipoMovimiento, prmEstacion, prmUsuario)

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
                            pRespuesta.Estado = True
                            pRespuesta.Texto = "No se puden listar los tipos de ajuste"
                            pRespuesta.Resultado = Nothing
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta
        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "COrdenProd.ListaTipoAjuste")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar los tipos de ajuste [SP1]" + ex.Message

            Return pRespuesta
        End Try
    End Function

End Class
