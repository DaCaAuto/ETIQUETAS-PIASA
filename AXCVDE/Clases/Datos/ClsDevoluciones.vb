Public Class ClsDevoluciones
    'Propiedades

    'Asignacino de propiedades

    'Metodos
    Public Function ConsultaOrdenDevolucion(ByVal prmOrdenDevolucion As String) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet
        Try
            If String.IsNullOrEmpty(prmOrdenDevolucion) Then

            Else
                dato.Parametro("prmOrdenDevolucion") = prmOrdenDevolucion
            End If
            ds = dato.Tabla("spQryListaOrdeneesDevDet")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function
    Public Function ConsultaOrdenDevolucion(ByVal prmOrdenDev As String, ByVal prmEstatus As String) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet
        Try
            If String.IsNullOrEmpty(prmOrdenDev) Then

            Else
                dato.Parametro("prmOrdenDev") = prmOrdenDev
                dato.Parametro("prmStatus") = prmEstatus
            End If
            ds = dato.Tabla("spQryListaOrdenesDevDet")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    'Consulta orden de compra con fechas
    'Consulta orden de compra con fechas
    Public Function ConsultaOrdenDevolucion(ByVal prmTipo As String, ByVal prmBusqueda As String, ByVal prmFechaInicio As String, ByVal prmFechaFin As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmTipo") = prmTipo
            dato.Parametro("prmBusqueda") = prmBusqueda
            dato.Parametro("prmFechaInicio") = prmFechaInicio
            dato.Parametro("prmFechaFin") = prmFechaFin
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario

            ds = dato.Tabla("spQryOrdenesDevPorFecha")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    'Liberar orden de recepcion

    Public Function LiberaOrdenDevolucion(ByVal prmOrdenDevolucion As String,
                                  ByVal prmReferencia As String,
                                  ByVal prmFechaRecibe As Date,
                                  ByVal prmDetalle As String,
                                  ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim CRespuesta As New CResultado
        Dim Dato As New clsDato

        Try
            ''Parametros
            Dato.Parametro("prmOrdenDevolucion") = prmOrdenDevolucion
            Dato.Parametro("prmReferencia") = prmReferencia
            Dato.Parametro("prmFechaRecibe") = prmFechaRecibe.ToString("yyyyMMdd")

            Dato.Parametro("prmDetalle") = prmDetalle.Replace("&lt;", "<").Replace("&gt;", ">")   'XML
            Dato.Parametro("prmEstacion") = prmEstacion
            Dato.Parametro("prmUsuario") = prmUsuario
            Dato.SP(True) = "spInsLiberaOrdenDevolucion"

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
            dato.Parametro("prmDevoluciones") = prmOrdenCompra
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaReciboDev")
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

            Dato.SP(True) = "spDelDocOrdenRecepcionDev"

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
            Dato.SP(True) = "spDelPalletOrdenRecepcionDev"

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
            dato.Parametro("prmIdReciboDevolucion") = prmIdReciboCompra
            dato.Parametro("@prmEstatus") = prmEstatus
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaPalletsRecibidosDev")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try

        Return ds
    End Function

    Public Function ConsultaOrdenDevolucionGeneral(ByVal prmOrdenDev As String) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet
        Try
            If String.IsNullOrEmpty(prmOrdenDev) Then

            Else
                dato.Parametro("prmOrdenDev") = prmOrdenDev
            End If
            ds = dato.Tabla("spQryListaOrdenesDevDet")
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

    Function ActualizaOrdenDevolucion(ByVal prmOrdenCompra As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim wpEstado As String = ""
        Dim wpTextoERR As String = ""
        Dim resp$ = ""
        Try
            Dim dato As New clsDato
            dato.Parametro("prmOrdenCompra") = prmOrdenCompra
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = "spInsOrdenDevolucion"

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

    Public Function ConsultaPalletsXEntrarAlmacen(
                                       ByVal prmOrdenProd As String,
                                       ByVal prmCodigoPallet As String,
                                       ByVal prmFechaInicio As DateTime,
                                       ByVal prmFechaFinal As DateTime,
                                       ByVal prmEstacion As String,
                                       ByVal prmUsuario As String
                                       ) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet
        Try
            dato.Parametro("@prmOrdenProduccion") = prmOrdenProd
            dato.Parametro("prmCodigoPallet") = prmCodigoPallet
            dato.Parametro("prmFechaInicio") = prmFechaInicio
            dato.Parametro("prmFechaFinal") = prmFechaFinal
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaPalletsPendienteEntrarAlmacen")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try

        Return ds
    End Function

    Friend Function RptRecibosDev(ByVal prmOrdenDevolucion As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spRptRecibosDev", prmOrdenDevolucion, prmEstacion, prmUsuario)

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

    Public Function ListaEstatusPalletXEntrar() As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            ds = dato.Tabla("spQryListaEstatusPalletXEntrar")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function


    Function ConsultaReciboDevolucion(ByVal prmOrdenDevolucion As String, ByVal prmStatus As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmOrdenDevolucion") = prmOrdenDevolucion
            dato.Parametro("prmStatus") = prmStatus
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryReciboDevolucionPorFecha")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function


    Function ConsutaReciboDevDet(ByVal prmOrdenVDevolucion As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmOrdenDevolucion") = prmOrdenVDevolucion
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryReciboDevDetPorFecha")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function
    Function ConsultaPalletPorStatus(ByVal prmOrdenDevolucion As String, prmStatus As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmOrdenDevolucion") = prmOrdenDevolucion
            dato.Parametro("prmStatus") = prmStatus
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaPalletMaterialRec")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

End Class
