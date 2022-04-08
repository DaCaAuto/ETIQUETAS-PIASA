Imports AXCSchettino
Imports Microsoft.VisualBasic
Imports System.Data

Public Class clsTransfer

#Region "PROPIEDADES DE RESPUESTA"

    Private pEstado As Boolean
    Public Property Estado() As Integer
        Get
            Return pEstado
        End Get
        Set(ByVal value As Integer)
            pEstado = value
        End Set
    End Property

    Private pTexto As Boolean
    Public Property Texto() As Integer
        Get
            Return pTexto
        End Get
        Set(ByVal value As Integer)
            pTexto = value
        End Set
    End Property

#End Region

#Region "METODOS"

    Public Function ListarInterorgs(prmEstatus As String, ByVal prmEstacion As String) As CResultado

        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim dt As DataTable

            pResultado = db.SPToDataTable("spQryInterorgsSalida", -1, prmEstatus, prmEstacion)

            If pResultado.Estado Then
                dt = pResultado.Resultado

                pRespuesta.Estado = True
                pRespuesta.Texto = ""
                dt.TableName = "Resultado"
                pRespuesta.Tabla = dt.Copy

            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "clsTransfer.Interorgs")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Listar transferencias " + ex.Message

            Return pRespuesta
        End Try
    End Function

    Public Function ListarEstatusInterorg() As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim dt As DataTable


            pResultado = db.SPToDataTable("spQryCatEstatusTraspaso")

            If pResultado.Estado Then
                dt = pResultado.Resultado

                pRespuesta.Estado = True
                pRespuesta.Texto = ""
                dt.TableName = "Resultado"
                pRespuesta.Tabla = dt.Copy

            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = ex.Message
            Return pRespuesta
        End Try
    End Function

    Public Function ListarInterorgsLiberados(ByVal prmEstacion As String) As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim dt As DataTable

            pResultado = db.SPToDataTable("spQryInterorgSalidaLiberado", prmEstacion)

            If pResultado.Estado Then
                dt = pResultado.Resultado

                pRespuesta.Estado = True
                pRespuesta.Texto = ""
                dt.TableName = "Resultado"
                pRespuesta.Tabla = dt.Copy

            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "clsTransfer.InterorgsLiberados")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Listar transferencias liberadas [SP1] " + ex.Message

            Return pRespuesta
        End Try
    End Function

    Public Function ListarInterorgsCerrados(ByVal prmEstacion As String) As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim dt As DataTable

            pResultado = db.SPToDataTable("spQryInterorgCerrado", prmEstacion)

            If pResultado.Estado Then
                dt = pResultado.Resultado

                pRespuesta.Estado = True
                pRespuesta.Texto = ""
                dt.TableName = "Resultado"
                pRespuesta.Tabla = dt.Copy

            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "clsTransfer.InterorgsCerrados")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Listar transferencias cerradas [SP1] " + ex.Message

            Return pRespuesta
        End Try
    End Function

    Public Function ListarInterorgsCancelados(ByVal prmEstacion As String) As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim dt As DataTable

            pResultado = db.SPToDataTable("spQryInterorgCancelado", prmEstacion)

            If pResultado.Estado Then
                dt = pResultado.Resultado

                pRespuesta.Estado = True
                pRespuesta.Texto = ""
                dt.TableName = "Resultado"
                pRespuesta.Tabla = dt.Copy

            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "clsTransfer.InterorgsCancelado")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Listar transferencias canceladas [SP1] " + ex.Message

            Return pRespuesta
        End Try
    End Function

    Public Function CrearInterorgSalida(ByVal prmTransfer As String,
                                        ByVal prmAlmacenOrigen As String,
                                                ByVal prmAlmacenDestino As String,
                                                ByVal prmCantidad As Integer,
                                                ByVal prmEstacion As String,
                                                ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spInsRegTraspasoMP", prmTransfer, prmAlmacenOrigen, prmAlmacenDestino, prmCantidad, prmEstacion, prmUsuario)

            If CResultado.Estado Then
                dr = CResultado.Resultado

                If dr("Estado") = "ER" Then
                    cRespuesta.Estado = False
                    cRespuesta.Texto = dr("Texto").ToString
                    cRespuesta.Resultado = Nothing
                Else
                    cRespuesta.Estado = True
                    cRespuesta.Texto = dr("Texto").ToString 'Debe regresarme el ID del Interorg para agregar detalles
                    cRespuesta.Resultado = Nothing
                End If
            Else
                cRespuesta.Estado = False
                cRespuesta.Texto = CResultado.Texto
                cRespuesta.Resultado = Nothing
            End If
        Catch ex As Exception
            cRespuesta.Estado = False
            cRespuesta.Texto = "Error al generar transferencia de salida " + ex.Message
            cRespuesta.Resultado = Nothing
        End Try

        Return cRespuesta
    End Function

    Public Function AgregarLineaInterorgSalida(ByVal prmInterorg As String,
                                               ByVal prmArticulos As String,
                                               ByVal prmArticulo As String,
                                               ByVal prmTonoCalibre As String,
                                               ByVal prmRevision As String,
                                               ByVal prmCantidad As Integer,
                                               ByVal prmEstacion As String,
                                               ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spInsRegTraspasoDetMP", prmInterorg, prmArticulos, prmArticulo, prmTonoCalibre, prmRevision, prmCantidad, prmEstacion, prmUsuario)

            If CResultado.Estado Then
                dr = CResultado.Resultado

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
            cRespuesta.Texto = "Error al generar transferencia de salida " + ex.Message
            cRespuesta.Resultado = Nothing
        End Try

        Return cRespuesta
    End Function

    Friend Function CierreParcialdocumento(prmDocumento As String, prmEstacion As String, prmUsuario As String, prmVerifica As Integer) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdDocumentoTraspasoCierreParcial", prmDocumento, prmEstacion, prmUsuario, prmVerifica)

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

    Friend Function CancelaPallet(prmDocumento As String, prmCodigoPallet As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdCancelaTraspasoPallet", prmDocumento, prmCodigoPallet, prmEstacion, prmUsuario)

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

    Public Function CerrarInterorgSalida(ByVal prmInterorg As String,
                                              ByVal prmEstacion As String,
                                              ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdCerrarParcialTraspaso", prmInterorg, prmEstacion, prmUsuario)

            If CResultado.Estado Then
                dr = CResultado.Resultado

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
            cRespuesta.Texto = "Error al cerrar transferencia de salida " + ex.Message
            cRespuesta.Resultado = Nothing
        End Try

        Return cRespuesta
    End Function

    Public Function CancelarInterorgSalida(ByVal prmInterorg As String,
                                              ByVal prmMotivo As String,
                                              ByVal prmEstacion As String,
                                              ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdCancelaInterorgSalida", prmInterorg, prmMotivo, prmEstacion, prmUsuario)

            If CResultado.Estado Then
                dr = CResultado.Resultado

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
            cRespuesta.Texto = "Error al cancelar transferencia de salida " + ex.Message
            cRespuesta.Resultado = Nothing
        End Try

        Return cRespuesta
    End Function

    Public Function RegistrarPiezasSalida(ByVal prmInterorg As String,
                                             ByVal prmPallet As String,
                                             ByVal prmCantidad As Integer,
                                             ByVal prmEstacion As String,
                                             ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spInsPiezasInterorgSalida", prmInterorg, prmPallet, prmCantidad, prmEstacion, prmUsuario)

            If CResultado.Estado Then
                dr = CResultado.Resultado

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
            cRespuesta.Texto = "Error al registrar piezas de transferencia de salida " + ex.Message
            cRespuesta.Resultado = Nothing
        End Try

        Return cRespuesta
    End Function

    Public Function RechazarPiezasSalida(ByVal prmInterorg As String,
                                             ByVal prmPallet As String,
                                             ByVal prmCantidad As Integer,
                                             ByVal prmEstacion As String,
                                             ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spDelRegresaPiezasInterorgSalida", prmInterorg, prmPallet, prmCantidad, prmEstacion, prmUsuario)

            If CResultado.Estado Then
                dr = CResultado.Resultado

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
            cRespuesta.Texto = "Error al registrar piezas Transferencia de salida " + ex.Message
            cRespuesta.Resultado = Nothing
        End Try

        Return cRespuesta
    End Function

    Public Function BuscarInterorg(prmInterorg As String) As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim dt As DataTable

            pResultado = db.SPToDataTable("spQryInterorgsSalida", prmInterorg, -1, My.Settings.Estacion)

            If pResultado.Estado Then
                dt = pResultado.Resultado

                pRespuesta.Estado = True
                pRespuesta.Texto = ""
                dt.TableName = "Resultado"
                pRespuesta.Tabla = dt.Copy

            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "clsTransfer.InterorgsLiberados")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Listar transferencias Liberadas [SP1] " + ex.Message

            Return pRespuesta
        End Try
    End Function

    Public Function ListarPartidasInterorg(ByVal prmInterorg As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryInterorgSalidaDet", prmInterorg)

            If pResultado.Estado Then
                ds = pResultado.Resultado

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy

                            cRespuesta.Estado = True
                            cRespuesta.Texto = ""
                            cRespuesta.Tabla = dt


                        Case "ER"
                            cRespuesta.Estado = False
                            cRespuesta.Texto = dr("Texto").ToString

                        Case Else
                            cRespuesta.Estado = False
                            cRespuesta.Texto = "Error al tratar de Listar Partidas  [SP0]"
                    End Select
                Next
            Else
                cRespuesta.Estado = False
                cRespuesta.Texto = pResultado.Texto
            End If

            Return cRespuesta
        Catch ex As Exception
            cRespuesta.Estado = False
            cRespuesta.Texto = ex.Message
        End Try
        Return cRespuesta
    End Function

    Public Function ListarPalletsInterorg(ByVal prmInterorg As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryTraspasoPallets", prmInterorg)

            If pResultado.Estado Then
                ds = pResultado.Resultado

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy

                            cRespuesta.Estado = True
                            cRespuesta.Texto = ""
                            cRespuesta.Tabla = dt


                        Case "ER"
                            cRespuesta.Estado = False
                            cRespuesta.Texto = dr("Texto").ToString

                        Case Else
                            cRespuesta.Estado = False
                            cRespuesta.Texto = "Error al tratar de Listar Partidas[SP0]"
                    End Select
                Next
            Else
                cRespuesta.Estado = False
                cRespuesta.Texto = pResultado.Texto
            End If

            Return cRespuesta
        Catch ex As Exception
            cRespuesta.Estado = False
            cRespuesta.Texto = ex.Message
        End Try
        Return cRespuesta
    End Function

    Public Function ListaEstatusTraspaso() As CResultado
        Dim pRespuesta As New CResultado
        Try

            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim dt As New DataTable

            pResultado = db.SPToDataTable("spQryListaEstatusTraspaso", -1, "@", "@")

            If pResultado.Estado Then
                dt = pResultado.Resultado
                pRespuesta.Estado = True
                pRespuesta.Texto = ""
                dt.TableName = "Resultado"
                pRespuesta.Tabla = dt.Copy
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If
            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = ex.Message
            Return pRespuesta
        End Try
    End Function

    Public Function ListaTrans() As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim dt As DataTable

            pResultado = db.SPToDataTable("spQryTraspaso", "@", "-1")

            If pResultado.Estado Then
                dt = pResultado.Resultado

                pRespuesta.Estado = True
                pRespuesta.Texto = ""
                dt.TableName = "Resultado"
                pRespuesta.Tabla = dt.Copy

            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Listar transferencias " + ex.Message

            Return pRespuesta
        End Try
    End Function

    Public Function ListaTransPT() As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim dt As DataTable

            pResultado = db.SPToDataTable(CMetodos.ConfigLeer("BaseDatosPT") + ".dbo.spQryTraspaso", "@", "-1")

            If pResultado.Estado Then
                dt = pResultado.Resultado

                pRespuesta.Estado = True
                pRespuesta.Texto = ""
                dt.TableName = "Resultado"
                pRespuesta.Tabla = dt.Copy

            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Listar Ordenes de Produccion " + ex.Message

            Return pRespuesta
        End Try
    End Function

    Public Function BuscarTransfer(prmTransfer As String) As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim dt As DataTable

            pResultado = db.SPToDataTable("spQryBuscaTransfer", prmTransfer)

            If pResultado.Estado Then
                dt = pResultado.Resultado

                pRespuesta.Estado = True
                pRespuesta.Texto = ""
                dt.TableName = "Resultado"
                pRespuesta.Tabla = dt.Copy

            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "clsTransfer.BuscarTransfer")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de buscar el transfer [SP1] " + ex.Message

            Return pRespuesta
        End Try
    End Function


    Public Function DetalleTraspaso(ByVal prmEmbarque As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryDetalleTraspaso", prmEmbarque)

            If pResultado.Estado Then
                ds = pResultado.Resultado

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy

                            cRespuesta.Estado = True
                            cRespuesta.Texto = ""
                            cRespuesta.Tabla = dt


                        Case "ER"
                            cRespuesta.Estado = False
                            cRespuesta.Texto = dr("Texto").ToString

                        Case Else
                            cRespuesta.Estado = False
                            cRespuesta.Texto = "Error al tratar de mostrar el detalle [SP0]"
                    End Select
                Next
            Else
                cRespuesta.Estado = False
                cRespuesta.Texto = pResultado.Texto
            End If

            Return cRespuesta
        Catch ex As Exception
            cRespuesta.Estado = False
            cRespuesta.Texto = ex.Message
        End Try
        Return cRespuesta
    End Function

    Public Function DetalleTraspasoPT(ByVal prmEmbarque As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet(CMetodos.ConfigLeer("BaseDatosPT") + ".dbo.spQryDetalleTraspaso", prmEmbarque)

            If pResultado.Estado Then
                ds = pResultado.Resultado

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy

                            cRespuesta.Estado = True
                            cRespuesta.Texto = ""
                            cRespuesta.Tabla = dt


                        Case "ER"
                            cRespuesta.Estado = False
                            cRespuesta.Texto = dr("Texto").ToString

                        Case Else
                            cRespuesta.Estado = False
                            cRespuesta.Texto = "Error al tratar de mostrar el seguimiento de surtido [SP0]"
                    End Select
                Next
            Else
                cRespuesta.Estado = False
                cRespuesta.Texto = pResultado.Texto
            End If

            Return cRespuesta
        Catch ex As Exception
            cRespuesta.Estado = False
            cRespuesta.Texto = ex.Message
        End Try
        Return cRespuesta
    End Function

    Public Function CrearInterorgSalidaPT(ByVal prmTransfer As String,
                                            ByVal prmReferencia As String,
                                            ByVal prmEstacion As String,
                                            ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spInsRegTraspasoPT", prmTransfer, prmReferencia, prmEstacion, prmUsuario)

            If CResultado.Estado Then
                dr = CResultado.Resultado

                If dr("Estado") = "ER" Then
                    cRespuesta.Estado = False
                    cRespuesta.Texto = dr("Texto").ToString
                    cRespuesta.Resultado = Nothing
                Else
                    cRespuesta.Estado = True
                    cRespuesta.Texto = dr("Texto").ToString 'Debe regresarme el ID del Interorg para agregar detalles
                    cRespuesta.Resultado = Nothing
                End If
            Else
                cRespuesta.Estado = False
                cRespuesta.Texto = CResultado.Texto
                cRespuesta.Resultado = Nothing
            End If
        Catch ex As Exception
            cRespuesta.Estado = False
            cRespuesta.Texto = "Error al generar Interorg de salida PT" + ex.Message
            cRespuesta.Resultado = Nothing
        End Try

        Return cRespuesta
    End Function

    Public Function AgregarLineaInterorgSalidaPT(ByVal prmInterorg As String,
                                                 ByVal prmAlmacenOrigen As String,
                                            ByVal prmAlmacenDestino As String,
                                               ByVal prmArticulos As String,
                                               ByVal prmArticulo As String,
                                               ByVal prmTonoCalibre As String,
                                               ByVal prmRevision As String,
                                               ByVal prmCantidad As Decimal,
                                               ByVal prmEstacion As String,
                                               ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spInsRegTraspasoDetPT", prmInterorg, prmAlmacenOrigen, prmAlmacenDestino, prmArticulos, prmArticulo, prmTonoCalibre, prmRevision, prmCantidad, prmEstacion, prmUsuario)

            If CResultado.Estado Then
                dr = CResultado.Resultado

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
            cRespuesta.Texto = "Error al generar Interorg de salida linea PT" + ex.Message
            cRespuesta.Resultado = Nothing
        End Try

        Return cRespuesta
    End Function

    Public Function ConsultaTransfersMP() As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryConsultaTransferMP")

            If pResultado.Estado Then
                ds = pResultado.Resultado

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy

                            cRespuesta.Estado = True
                            cRespuesta.Texto = ""
                            cRespuesta.Tabla = dt


                        Case "ER"
                            cRespuesta.Estado = False
                            cRespuesta.Texto = dr("Texto").ToString

                        Case Else
                            cRespuesta.Estado = False
                            cRespuesta.Texto = "Error al tratar de mostrar elista de embarques [SP0]"
                    End Select
                Next
            Else
                cRespuesta.Estado = False
                cRespuesta.Texto = pResultado.Texto
            End If

            Return cRespuesta
        Catch ex As Exception
            cRespuesta.Estado = False
            cRespuesta.Texto = ex.Message
        End Try
        Return cRespuesta
    End Function

    Public Function BuscarTransferMP(prmTransfer As String) As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim dt As DataTable

            pResultado = db.SPToDataTable("spQryBuscaTransferMP", prmTransfer)

            If pResultado.Estado Then
                dt = pResultado.Resultado

                pRespuesta.Estado = True
                pRespuesta.Texto = ""
                dt.TableName = "Resultado"
                pRespuesta.Tabla = dt.Copy

            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "CEmbarque.BuscarEmbarque")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de buscar el Embarque " + ex.Message

            Return pRespuesta
        End Try
    End Function

    Public Function BuscarTransferDet(ByVal prmTransfer As String) As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryBuscaTransferDetMP", prmTransfer)

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
                            pRespuesta.Texto = "Error al tratar de Listar Partidas Liberadas [SP0]"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "CInterorg.ListaInterorgsLiberados")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Listar Interorgs Liberados [SP1]"

            Return pRespuesta
        End Try
    End Function

    Public Function BuscarTransferDetPallet(ByVal prmTransfer As String) As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryBuscaTransferDetPalletMP", prmTransfer)

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
                            pRespuesta.Texto = "Error al tratar de Listar Partidas Liberadas [SP0]"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "CInterorg.ListaInterorgsLiberados")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Listar Interorgs Liberados [SP1]"

            Return pRespuesta
        End Try
    End Function


    Public Function BuscarTransferPT(prmTransfer As String) As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim dt As DataTable

            pResultado = db.SPToDataTable("spQryBuscaTransferPT", prmTransfer)

            If pResultado.Estado Then
                dt = pResultado.Resultado

                pRespuesta.Estado = True
                pRespuesta.Texto = ""
                dt.TableName = "Resultado"
                pRespuesta.Tabla = dt.Copy

            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "CEmbarque.BuscarTransfer")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de buscar el Embarque " + ex.Message

            Return pRespuesta
        End Try
    End Function

    Public Function ConsultaTransfersPT() As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryConsultaTransferPT")

            If pResultado.Estado Then
                ds = pResultado.Resultado

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy

                            cRespuesta.Estado = True
                            cRespuesta.Texto = ""
                            cRespuesta.Tabla = dt


                        Case "ER"
                            cRespuesta.Estado = False
                            cRespuesta.Texto = dr("Texto").ToString

                        Case Else
                            cRespuesta.Estado = False
                            cRespuesta.Texto = "Error al tratar de mostrar elista de embarques [SP0]"
                    End Select
                Next
            Else
                cRespuesta.Estado = False
                cRespuesta.Texto = pResultado.Texto
            End If

            Return cRespuesta
        Catch ex As Exception
            cRespuesta.Estado = False
            cRespuesta.Texto = ex.Message
        End Try
        Return cRespuesta
    End Function

    Public Function BuscarTransferDetPT(ByVal prmTransfer As String) As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryBuscaTransferDetPT", prmTransfer)

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
                            pRespuesta.Texto = "Error al tratar de Listar Partidas Liberadas [SP0]"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "CInterorg.ListaInterorgsLiberados")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Listar Interorgs Liberados [SP1]"

            Return pRespuesta
        End Try
    End Function

    Public Function BuscarTransferDetPalletPT(ByVal prmTransfer As String) As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet(CMetodos.ConfigLeer("BaseDatosPT") + ".dbo.spQryBuscaTransferDetPalletPT", prmTransfer)

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
                            pRespuesta.Texto = "Error al tratar de Listar Transfers [SP0]"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "CInterorg.ListaInterorgsLiberados")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Listar Transfers [SP1]"

            Return pRespuesta
        End Try
    End Function

    Public Function ListaEstatusTransfer() As CResultado
        Dim pRespuesta As New CResultado
        Try

            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim dt As New DataTable

            pResultado = db.SPToDataTable("spQryListaEstatusTransfer", -1, "@", "@")

            If pResultado.Estado Then
                dt = pResultado.Resultado
                pRespuesta.Estado = True
                pRespuesta.Texto = ""
                dt.TableName = "Resultado"
                pRespuesta.Tabla = dt.Copy
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If
            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = ex.Message
            Return pRespuesta
        End Try
    End Function

#End Region

End Class

