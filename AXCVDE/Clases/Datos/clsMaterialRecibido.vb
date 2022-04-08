Imports DevExpress.XtraEditors

Public Class ClsMaterialRecibido
    Function ConsultaMaterialRecibido(ByVal prmOrdenCompra As String,
                                      ByVal prmNumParte As String,
                                      ByVal prmRevision As String,
                                      ByVal prmProveedor As String,
                                      ByVal prmFechaInicio As String,
                                      ByVal prmFechaFin As String,
                                      ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmOrdenCompra") = prmOrdenCompra
            dato.Parametro("prmNumParte") = prmNumParte
            dato.Parametro("prmRevision") = prmRevision
            dato.Parametro("prmProveedor") = prmProveedor
            dato.Parametro("prmFechaInicio") = prmFechaInicio
            dato.Parametro("prmFechaFin") = prmFechaFin
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryMaterialRecibido")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function



    Function ConsultaReciboCompra(ByVal prmOrdenCompra As String, ByVal prmStatus As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmOrdenCompra") = prmOrdenCompra
            dato.Parametro("prmStatus") = prmStatus
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryReciboCompraPorFecha")
        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "AXC", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return ds
    End Function



    Function ConsutaReciboCompraDet(ByVal prmOrdenCompra As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmOrdenCompra") = prmOrdenCompra
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryReciboCompraDetPorFecha")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function
    Function ConsultaPalletPorStatus(ByVal prmOrdenCompra As String, ByVal prmStatus As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmOrdenCompra") = prmOrdenCompra
            dato.Parametro("prmStatus") = prmStatus
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaPalletMPMaterialRec")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

#Region "METODOS ARMADO TARIMAS"
    Friend Function ConsultaPalletsArmados(ByVal prmOrdenProd As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmOrdenProd") = prmOrdenProd
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryPalletsArmados")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function


    Friend Function ConsultaEmpaquesPorPallet(ByVal prmPallet As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmPallet") = prmPallet
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryEmpaquesPalletsArmados")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function


#Region "METODOS TRASPASO"
    Friend Function ConsultaDocumentoTraspaso(ByVal prmDocTras As String, ByVal prmStatus As String, ByVal prmFechaInicio As String, ByVal prmFechaFin As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmDocTras") = prmDocTras
            dato.Parametro("prmStatus") = prmStatus
            dato.Parametro("prmFechaInicio") = prmFechaInicio
            dato.Parametro("prmFechaFin") = prmFechaFin
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryDocumentosTraspaso")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Friend Function ConsultaDetalleTras(ByVal prmDocTras As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmDocTras") = prmDocTras
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryDocumentosTraspasoDetalle")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Friend Function ConsultaPalletsTraspaso1(ByVal prmDocTras As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmDocTras") = prmDocTras
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaPalletTraspaso")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function


    Friend Function ConsultaPalletsTraspaso(prmBusqueda As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaPalletTraspaso", prmBusqueda, prmEstacion, prmUsuario)

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




    Friend Function CancerlarTraspaso(ByVal prmTraspaso As String, ByVal prmCodigoPallet As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdCancelaTraspasoPallet", prmTraspaso, prmCodigoPallet, prmEstacion, prmUsuario)

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
            cRespuesta.Texto = ex.Message
        End Try
        Return cRespuesta
    End Function
#End Region
#End Region
End Class
