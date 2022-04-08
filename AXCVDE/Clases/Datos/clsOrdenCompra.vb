
Public Class ClsOrdenCompra
    'Propiedades

    'Asignacino de propiedades

    'Metodos
    Public Function ConsultaOrdenCompra(ByVal prmOrdenCompra As String) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet
        Try
            If String.IsNullOrEmpty(prmOrdenCompra) Then

            Else
                dato.Parametro("prmOrdenCompra") = prmOrdenCompra
            End If
            ds = dato.Tabla("spQryListaOrdenesCompraDet")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function
    Public Function ConsultaOrdenCompra(ByVal prmOrdenCompra As String, ByVal prmEstatus As String) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet
        Try
            If String.IsNullOrEmpty(prmOrdenCompra) Then

            Else
                dato.Parametro("prmOrdenCompra") = prmOrdenCompra
                dato.Parametro("prmStatus") = prmEstatus
            End If
            ds = dato.Tabla("spQryListaOrdenesCompraDet")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    Friend Function ListaRecepcionesOC(ByVal prmOrdenCompra As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaDocRecepcion", prmOrdenCompra, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar documentos de rececpción"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar documentos de rececpción"
            Return pRespuesta
        End Try
    End Function

    Friend Function ListaPartidasRecepcion(prmRecepcion As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaPartidasRecepcion", prmRecepcion, prmEstacion, prmUsuario)

            If pResultado.Estado Then
                ds = pResultado.Resultado

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy
                            pRespuesta.Estado = True
                            pRespuesta.Texto = ""
                            pRespuesta.Resultado = pResultado.Resultado
                        Case "ER"
                            pRespuesta.Estado = False
                            pRespuesta.Texto = dr("Texto").ToString
                        Case Else
                            pRespuesta.Estado = False
                            pRespuesta.Texto = "Error al tratar de listar partidas "
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar partidas "
            Return pRespuesta
        End Try
    End Function

    Friend Function ListaPalletsPorRecepcion(prmRecepcion As Integer, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaPalletsRecibidosMP", prmRecepcion, -1, prmEstacion, prmUsuario)

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
            pRespuesta.Texto = "Error al tratar de listar pallets "
            Return pRespuesta
        End Try
    End Function

    'Consulta orden de compra con fechas
    Public Function ConsultaOrdenCompra(ByVal prmTipo As String, ByVal prmBusqueda As String, ByVal prmFechaInicio As String, ByVal prmFechaFin As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmTipo") = prmTipo
            dato.Parametro("prmBusqueda") = prmBusqueda
            dato.Parametro("prmFechaInicio") = prmFechaInicio
            dato.Parametro("prmFechaFin") = prmFechaFin
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario

            ds = dato.Tabla("spQryOrdenesCompraPorFecha")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    Friend Function CierreParcial(prmOrdenCompra As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim sResultado As CResultado

            sResultado = db.SPToDataRow("spUpdCierreParcialOC", prmOrdenCompra, prmEstacion, prmUsuario)

            If sResultado.Estado Then
                dr = CType(sResultado.Resultado, DataRow)


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
                cRespuesta.Texto = sResultado.Texto
                cRespuesta.Resultado = Nothing
            End If
        Catch ex As Exception
            cRespuesta.Estado = False
            cRespuesta.Texto = ex.Message
        End Try
        Return cRespuesta
    End Function

    'Liberar orden de recepcion

    Public Function LiberaOrdenCompra(ByVal prmOrdenCompra As String,
                                  ByVal prmFactura As String,
                                  ByVal prmReferencia As String,
                                  ByVal prmFechaRecibe As Date,
                                  ByVal prmClavePedimento As String,
                                  ByVal prmNumeroAgenteAduanal As String,
                                  ByVal prmNumeroPedimento As String,
                                  ByVal prmFechaPedimento As Date,
                                  ByVal prmDetalle As String,
                                  ByVal prmCostosCarga As String,
                                      ByVal prmCabecera As String,
                                      ByVal prmNota As String,
                                      ByVal prmCarta As String,
                                  ByVal prmEstacion As String, ByVal prmUsuario As String, prmValida As Integer, prmSAP As Integer) As CResultado
        Dim CRespuesta As New CResultado
        Dim Dato As New clsDato

        Try
            ''Parametros
            Dato.Parametro("prmOrdenCompra") = prmOrdenCompra
            Dato.Parametro("prmFactura") = prmFactura
            Dato.Parametro("prmReferencia") = prmReferencia
            Dato.Parametro("prmFechaRecibe") = prmFechaRecibe.ToString("yyyyMMdd")

            Dato.Parametro("prmClavePedimento") = prmClavePedimento
            Dato.Parametro("prmNumeroAgenteAduanal") = prmNumeroAgenteAduanal
            Dato.Parametro("prmNumeroPedimento") = prmNumeroPedimento
            Dato.Parametro("prmFechaPedimento") = prmFechaPedimento.ToString("yyyyMMdd")

            Dato.Parametro("prmDetalle") = prmDetalle.Replace("&lt;", "<").Replace("&gt;", ">")   'XML
            Dato.Parametro("prmCostosCarga") = prmCostosCarga.Replace("&lt;", "<").Replace("&gt;", ">")   'XML
            Dato.Parametro("prmCabecera") = prmCabecera
            Dato.Parametro("prmNota") = prmNota
            Dato.Parametro("prmCarta") = prmCarta
            Dato.Parametro("prmEstacion") = prmEstacion
            Dato.Parametro("prmUsuario") = prmUsuario
            Dato.Parametro("prmValida") = prmValida
            Dato.Parametro("prmSAP") = prmSAP
            Dato.SP(True) = "spInsLiberaOrdenCompraMP1"

            Dim wpEstado As String = ""
            Dim wpTextoERR As String = ""
            While Dato.DataReader.Read

                wpEstado = Dato.DataReader.Item("Estado")
                wpTextoERR = Dato.DataReader.Item("Texto")

            End While

            If wpEstado = "ER" Then

                CRespuesta.Estado = False
                CRespuesta.Texto = wpTextoERR
                CRespuesta.Resultado = Nothing
            Else

                CRespuesta.Estado = True
                CRespuesta.Texto = wpTextoERR
                CRespuesta.Resultado = ""
            End If

        Catch ex As Exception
            CRespuesta.Estado = False
            CRespuesta.Texto = ex.Message
            CRespuesta.Resultado = Nothing
        End Try


        Return CRespuesta
    End Function

    Friend Function AplicaOrdenCompra(prmOrdenCompra As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim sResultado As CResultado

            sResultado = db.SPToDataRow("spUpdOrdenCompraDisponible", prmOrdenCompra, prmEstacion, prmUsuario)

            If sResultado.Estado Then
                dr = CType(sResultado.Resultado, DataRow)


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
                cRespuesta.Texto = sResultado.Texto
                cRespuesta.Resultado = Nothing
            End If
        Catch ex As Exception
            cRespuesta.Estado = False
            cRespuesta.Texto = ex.Message
        End Try
        Return cRespuesta
    End Function

    Public Function ConsultaPallet(ByVal prmCodigoPallet As String) As CResultado
        Dim CRespuesta As New CResultado
        Dim Dato As New clsDato
        Try


            Dim ds As New DataSet
            Dim dr As DataRow

            Dato.Parametro("prmCodigoPallet") = prmCodigoPallet
            ds = Dato.Tabla("spQryListaPalletsSurtidos")

            dr = ds.Tables(0).Rows(0)
            If ds.Tables(0) Is Nothing Then

                CRespuesta.Estado = False
                CRespuesta.Texto = dr("Texto").ToString()
                CRespuesta.Resultado = Nothing
            Else

                If dr("Estado") = "ER" Then
                    CRespuesta.Estado = False
                    CRespuesta.Texto = dr("Texto").ToString()
                    CRespuesta.Resultado = Nothing
                Else
                    CRespuesta.Estado = True
                    CRespuesta.Texto = dr("Texto").ToString()
                    CRespuesta.Resultado = ds.Tables(1).Copy
                End If
            End If

        Catch ex As Exception
            CRespuesta.Estado = False
            CRespuesta.Texto = ex.Message
            CRespuesta.Resultado = Nothing
        End Try


        Return CRespuesta
    End Function
    Public Function ListaDocRecepcion(ByVal prmOrdenCompra As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As DataSet
        Try
            dato.Parametro("prmOrdenCompra") = prmOrdenCompra
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaReciboMP")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function


    Public Function EliminaDocRecepcion(ByVal prmOrdenRecepcion As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim Dato As New clsDato
        Dim wpEstado As String = ""
        Dim wpTextoERR As String = ""
        Try
            Dato.Parametro("prmOrdenRecepcion") = prmOrdenRecepcion
            Dato.Parametro("prmEstacion") = prmEstacion
            Dato.Parametro("prmUsuario") = prmUsuario

            Dato.SP(True) = "spDelDocOrdenRecepcion"

            While Dato.DataReader.Read

                wpEstado = Dato.DataReader.Item("Estado")
                wpTextoERR = Dato.DataReader.Item("Texto")

            End While
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return wpTextoERR
    End Function

    Public Function EliminaPalletDocRecepcion(ByVal prmOrdenRecepcion As String, ByVal prmPallet As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim wpEstado As String = ""
        Dim wpTextoERR As String = ""
        Dim Dato As New clsDato
        Try


            Dato.Parametro("prmOrdenRecepcion") = prmOrdenRecepcion
            Dato.Parametro("prmPallet") = prmPallet
            Dato.Parametro("prmEstacion") = prmEstacion
            Dato.Parametro("prmUsuario") = prmUsuario
            Dato.SP(True) = "spDelPalletOrdenRecepcion"

            While Dato.DataReader.Read

                wpEstado = Dato.DataReader.Item("Estado")
                wpTextoERR = Dato.DataReader.Item("Texto")

            End While
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return wpTextoERR
    End Function

    Function ListaPalletsRecepcion(ByVal prmIdReciboCompra As String, ByVal prmEstatus As String,
                                   ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmIdReciboCompra") = prmIdReciboCompra
            dato.Parametro("@prmEstatus") = prmEstatus
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaPalletsRecibidosMP")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try

        Return ds
    End Function

    Public Function ConsultaOrdenCompraGeneral(ByVal prmOrdenCompra As String) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet
        Try
            If String.IsNullOrEmpty(prmOrdenCompra) Then

            Else
                dato.Parametro("prmOrdenCompra") = prmOrdenCompra
            End If
            ds = dato.Tabla("spQryListaOrdenesCompraDet")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    Public Function InsertaXML(ByVal prmXML As String) As String
        Dim dato As New clsDato

        Dim Estado As String = ""
        Dim Texto As String = ""
        Try
            ''Parametros
            dato.Parametro("xml") = prmXML
            dato.Parametro("prmEstacion") = My.Settings("Estacion")
            dato.Parametro("prmUsuario") = DatosTemporales.Usuario
            dato.SP(True) = "spBaseXML"

            While dato.DataReader.Read
                Estado = dato.DataReader.Item("Estado")
                Texto = dato.DataReader.Item("Texto")
            End While

            If Estado = "ER" Then
                Return Estado
            End If
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try

        Return Texto
    End Function

    Public Function ConsultaPalletRecibidos(
                                           ByVal prmTipo As String,
                                           ByVal prmBusqueda As String,
                                           ByVal prmEstatus As String,
                                           ByVal prmFechaInicio As String,
                                           ByVal prmFechaFinal As String,
                                           ByVal prmEstacion As String,
                                           ByVal prmUsuario As String
                                           ) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet
        Try
            dato.Parametro("prmTipo") = prmTipo
            dato.Parametro("prmBusqueda") = prmBusqueda
            dato.Parametro("prmEstatus") = prmEstatus
            dato.Parametro("prmFechaInicio") = prmFechaInicio
            dato.Parametro("prmFechaFinal") = prmFechaFinal
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaPalletsRecibidos")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try

        Return ds
    End Function



    Public Function ListaEstatusPallet() As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            ds = dato.Tabla("spQryListaEstatusPallet")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    Public Function ConsultaInterfazRecibo(
                                           ByVal prmOrdenCompra As String,
                                           ByVal prmFechaInicio As Date,
                                           ByVal prmFechaFinal As Date,
                                           ByVal prmEstatus As String,
                                           ByVal prmEstacion As String,
                                           ByVal prmUsuario As String
                                           ) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet
        Try
            dato.Parametro("prmOrdenCompra") = prmOrdenCompra
            dato.Parametro("prmFechaInicio") = prmFechaInicio.ToString("yyyyMMdd")
            dato.Parametro("prmFechaFinal") = prmFechaFinal.ToString("yyyyMMdd")
            dato.Parametro("prmEstatus") = prmEstatus
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla(CMetodos.ConfigLeer("BaseDatosBI") + ".dbo.spQryInterfazRecibo")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try

        Return ds
    End Function
    Public Function ListaCostosCarga() As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            ds = dato.Tabla("spQryListaCostosDescarga")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function
    Public Function MonitoreoInterfazRecibo(
                                           ByVal prmOrdenCompra As String,
                                           ByVal prmFechaInicio As Date,
                                           ByVal prmFechaFinal As Date,
                                           ByVal prmEstatus As String,
                                           ByVal prmEstacion As String,
                                           ByVal prmUsuario As String
                                           ) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet
        Try
            dato.Parametro("prmOrdenCompra") = prmOrdenCompra
            dato.Parametro("prmFechaInicio") = prmFechaInicio.ToString("yyyyMMdd")
            dato.Parametro("prmFechaFinal") = prmFechaFinal.ToString("yyyyMMdd")
            dato.Parametro("prmEstatus") = prmEstatus
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla(CMetodos.ConfigLeer("BaseDatosBI") + ".dbo.spQryMonitorInterfazRecibo")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try

        Return ds
    End Function
    Public Function MonitoreoInterfazTransfer(
                                           ByVal prmTransferencia As String,
                                           ByVal prmNumParte As String,
                                           ByVal prmFechaInicio As Date,
                                           ByVal prmFechaFinal As Date,
                                           ByVal prmEstatus As String,
                                           ByVal prmEstacion As String,
                                           ByVal prmUsuario As String
                                           ) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet
        Try
            dato.Parametro("prmTransferencia") = prmTransferencia
            dato.Parametro("prmNumParte") = prmNumParte
            dato.Parametro("prmFechaInicio") = prmFechaInicio.ToString("yyyyMMdd")
            dato.Parametro("prmFechaFinal") = prmFechaFinal.ToString("yyyyMMdd")
            dato.Parametro("prmEstatus") = prmEstatus
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla(CMetodos.ConfigLeer("BaseDatosBI") + ".dbo.spQryMonitorInterfazTransfer")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try

        Return ds
    End Function

    Function ActualizaOrdenCompra(ByVal prmOrdenCompra As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim wpEstado As String = ""
        Dim wpTextoERR As String = ""
        Dim resp$ = ""
        Try
            Dim dato As New clsDato
            dato.Parametro("prmOrdenCompra") = prmOrdenCompra
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = "spInsOrdenCompraMP"

            While dato.DataReader.Read

                wpEstado = dato.DataReader.Item("Estado")
                wpTextoERR = dato.DataReader.Item("Texto")

            End While

            If wpEstado = "OK" Then
                resp = wpEstado
            Else
                resp = wpTextoERR
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return resp
    End Function

    Function ListaEstatusPallets(ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaEstatusPallet", -1)

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
                            pRespuesta.Texto = "Error al tratar de Listar Estatus"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Listar Estatus"

            Return pRespuesta
        End Try
    End Function

    Function CargaPallets(ByVal prmTipo As Integer, ByVal prmOrdenCompra As String, ByVal prmEstatus As String, ByVal prmFechaInicial As String, ByVal prmFechaFinal As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaPalletsRec", prmTipo, prmOrdenCompra, prmEstatus, prmFechaInicial, prmFechaFinal, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de Listar Pallets"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Listar Pallets"

            Return pRespuesta
        End Try
    End Function


    Function ObtenerLoteInterno() As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spUpdLoteInterno")

            If pResultado.Estado Then
                ds = pResultado.Resultado

                For Each dr As DataRow In ds.Tables(0).Rows

                    Select Case dr("Estado").ToString
                        Case "OK"
                            'dt = ds.Tables(1).Copy

                            pRespuesta.Estado = True
                            pRespuesta.Texto = dr("Texto").ToString
                            'Respuesta.Tabla = dt


                        Case "ER"
                            pRespuesta.Estado = False
                            pRespuesta.Texto = dr("Texto").ToString

                        Case Else
                            pRespuesta.Estado = False
                            pRespuesta.Texto = "Error al tratar de Listar Estatus"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de Listar Estatus"

            Return pRespuesta
        End Try
    End Function

    Friend Function BuscaRecibosPorFecha(ByVal prmFechaInicio As String, ByVal prmFechaFin As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryConsultaRecibosPorFecha", prmFechaInicio, prmFechaFin, prmEstacion, prmUsuario)

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

    Friend Function RptRecibosOC(ByVal prmOrdenCompra As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spRptRecibosOC", prmOrdenCompra, prmEstacion, prmUsuario)

            If pResultado.Estado Then
                ds = pResultado.Resultado

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

    Public Function ConsultaOrdenCompraDet(ByVal prmOrdenCompra As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryOrdenesCompraDet", prmOrdenCompra)

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
                            pRespuesta.Texto = "Error al tratar de listar el detalle"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar el detalle"
            Return pRespuesta
        End Try

    End Function

End Class

