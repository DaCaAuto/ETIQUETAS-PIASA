Public Class clsExistencias

    Function ListaExistenciasAlamacen(ByVal prmPosicion As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("@prmNumParte") = "@"
            dato.Parametro("@prmPosicion") = prmPosicion
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaExistencia")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaExistenciasGeneral(ByVal prmNumParte As String, ByVal prmPosicion As String, ByVal prmEstatus As String, ByVal prmLote As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            If prmNumParte = "" Then
                prmNumParte = "@"
            End If
            If prmPosicion = "" Then
                prmPosicion = "@"
            End If
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmPosicion") = prmPosicion
            dato.Parametro("@prmEstatus") = prmEstatus
            dato.Parametro("@prmLote") = prmLote
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaExistencia")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Friend Function ResultadoConsultaPalletCliente(prmTipo As Integer, prmBusqueda As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryPalletsCliente", prmTipo, prmBusqueda, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar pallets"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar pallets "
            Return pRespuesta
        End Try
    End Function

    Function ListaExistenciasGeneralRPT(ByVal prmNumParte As String, ByVal prmAlmacen As String, ByVal prmLote As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            If prmNumParte = "TODOS" Then
                prmNumParte = "@"
            End If
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmAlmacen") = prmAlmacen
            dato.Parametro("@prmLote") = prmLote
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spRptQryListaExistenciaPallets")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaDiferenciasAXCGP(ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryDifNumParteAXCGP")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaDiferenciasAXCGPRPT(ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spRptQryDifNumParteAXCGP")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaPalletsSinColocar(ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaPalletsSinColocar")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function
    Function ListaExistenciasEmbalaje(ByVal prmNumParte As String, ByVal prmPosicion As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            If prmNumParte = "" Then
                prmNumParte = "@"
            End If
            If prmPosicion = "" Then
                prmPosicion = "@"
            End If
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmPosicion") = prmPosicion
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaExistenciaEmbalaje")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function
    Function ListaExistenciasEmbalajeRPT(ByVal prmNumParte As String, ByVal prmPosicion As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            If prmNumParte = "" Then
                prmNumParte = "@"
            End If
            If prmPosicion = "" Then
                prmPosicion = "@"
            End If
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmPosicion") = prmPosicion
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spRptQryListaExistenciaEmbalaje")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaExistenciasTotalRPT(ByVal prmNumParte As String, ByVal prmAlmacen As String, ByVal prmPosicion As String, ByVal prmLote As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            If prmNumParte = "TODOS" Then
                prmNumParte = "@"
            End If
            If prmPosicion = "" Then
                prmPosicion = "@"
            End If
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmLote") = prmLote
            dato.Parametro("@prmAlmacen") = prmAlmacen
            dato.Parametro("@prmPosicion") = prmPosicion
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spRptQryExistenciaTotalAlmacen")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Public Function ConsultaNumParte(ByVal prmNumParte As String, ByVal prmEstatus As String) As CResultado
        Dim pRespuesta As New CResultado
        Dim dt As DataTable
        Dim ds As DataSet
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado

            pResultado = db.SPToDataSet("spQryListaNumParte", prmNumParte, prmEstatus)

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
                            pRespuesta.Texto = "No se puden listar los números de parte"
                            pRespuesta.Resultado = Nothing
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta
        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "COrdenProd.ListaNumParte")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar los números de parte [SP1]" + ex.Message

            Return pRespuesta
        End Try
    End Function


    Public Function CargarEstatusColocados(ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Dim dt As DataTable
        Dim ds As DataSet
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado

            pResultado = db.SPToDataSet("spQryListaEstatusColocados", prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "No se puden listar los estatus"
                            pRespuesta.Resultado = Nothing
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta
        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "COrdenProd.CargarEstatusColocados")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar los estatus [SP1]" + ex.Message

            Return pRespuesta
        End Try
    End Function


    Function InformacionEmpaque(ByVal prmEmpaque As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("@prmEmpaque") = prmEmpaque
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryEmpaqueMP")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function InformacionPallet(ByVal prmPallet As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("@prmCodigoPallet") = prmPallet
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spWSQryPalletMP")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function EmpaquesPorPallet(ByVal prmPallet As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("@prmPallet") = prmPallet
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryEmpaquesPorPallet")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ConsultaTotalDeEmpaques(ByVal prmPallet As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("@prmPallet") = prmPallet
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryRpt")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Friend Function CargarTotalExistencias(prmNUmParte As String, prmLote As String, prmAlmacen As String, prmPosicion As String, ByVal prmCLiente As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryExistenciaTotalAlmacen", prmNUmParte, prmLote, prmAlmacen, prmPosicion, prmCLiente, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar "
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar "
            Return pRespuesta
        End Try
    End Function


    Friend Function ConsultaPallet(prmAlmacen As String, ByVal prmPosicion As String, prmNumParte As String, prmLote As String, prmEstatus As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryPalletExistencias", prmAlmacen, prmPosicion, prmNumParte, prmLote, prmEstatus, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar pallets"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar pallets"
            Return pRespuesta
        End Try
    End Function

    Friend Function ListaExistenciasTotales(prmNumParte As String, prmAlmacen As String, prmLote As String, prmEstacion As String, prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            If prmNumParte = "TODOS" Then
                prmNumParte = "@"
            End If
            dato.Parametro("@prmNumParte") = prmNumParte
            dato.Parametro("@prmAlmacen") = prmAlmacen
            dato.Parametro("@prmLote") = prmLote
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            ds = dato.Tabla("spRptQryListaExistencia")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Friend Function ResultadoConsultaPallet(ByVal prmTipo As Integer, ByVal prmBusqueda As String, ByVal prmEstatus As String, ByVal prmFechaInicio As String, ByVal prmFechaFin As String, prmCHkFechas As Integer, prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaPalletsRecibidos", prmTipo, prmBusqueda, prmEstatus, prmFechaInicio, prmFechaFin, prmCHkFechas, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar defectos"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar defectos "
            Return pRespuesta
        End Try
    End Function

    Friend Function ConsultaReporte(prmTipoReporte As String, prmAlmacen As String, prmCliente As String, prmNumParte As String, prmPosicion As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryExistenciaReportes", prmTipoReporte, prmAlmacen, prmCliente, prmNumParte, prmPosicion, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar Existencias"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar Existencias "
            Return pRespuesta
        End Try
    End Function

    Friend Function ListaEstatusPallet(ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaEstatusPallet", -1)

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
                            pRespuesta.Texto = "Error al tratar de listar estatus"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar Estatus "
            Return pRespuesta
        End Try
    End Function
End Class
