Public Class clsEmbarque

    Public Function ConsultaOrdenEmbarque(ByVal prmOrdenEmbarque As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaOrdenesEmbarqueDet", prmOrdenEmbarque, "@", "@", "@", "@", "@")

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
                            pRespuesta.Texto = "Error al tratar de listar órdenes de embarque"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar órdenes de embarque "
            Return pRespuesta
        End Try
    End Function

    Friend Function ConsultaEmbarque(prmTipo As Integer, prmBusqueda As String, prmFechaInicio As String, prmFechaFin As String, prmEstacion As Object, prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryOrdenEmbarques", prmTipo, prmBusqueda, "@", prmFechaInicio, prmFechaFin, prmEstacion, prmUsuario)

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

    Friend Function ConsultaDocumentos(prmFechaInicio As String, prmFechaFin As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryDocEmbarques", prmFechaInicio, prmFechaFin, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar embarques"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar embarques "
            Return pRespuesta
        End Try
    End Function

    Public Function ConsultaOrdenesEmbarque(ByVal prmOrdenEmbarque As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaOrdenesEmbarque", prmOrdenEmbarque, "@", "@", "@", "@")

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
                            pRespuesta.Texto = "Error al tratar de listar órdenes de embarque"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar órdenes de embarque "
            Return pRespuesta
        End Try
    End Function

    Public Function ConsultaOrdenEmbarque(ByVal prmOrdenEmbarque As String, ByVal prmEstatus As String) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet

        Try
            If String.IsNullOrEmpty(prmOrdenEmbarque) Then


            Else
                dato.Parametro("prmOrdenEmbarque") = prmOrdenEmbarque
                dato.Parametro("prmStatus") = prmEstatus
            End If
            ds = dato.Tabla("spQryListaOrdenesEmbarque")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    'Consulta orden de Embarque con fechas
    Public Function ConsultaOrdenEmbarque(ByVal prmOrdenEmbarque As String, ByVal prmFechaInicio As String, ByVal prmFechaFin As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            If String.IsNullOrEmpty(prmOrdenEmbarque) Then
                dato.Parametro("prmFechaInicio") = prmFechaInicio
                dato.Parametro("prmFechaFin") = prmFechaFin

            Else
                dato.Parametro("prmFechaInicio") = prmFechaInicio
                dato.Parametro("prmFechaFin") = prmFechaFin
                dato.Parametro("prmOrdenEmbarque") = prmOrdenEmbarque
            End If

            ds = dato.Tabla("spQryListaOrdenesProd")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function


    Public Function ConsultaOrdenEmbarqueDet(ByVal prmOrdenEmbarque As String) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet

        Try
            If String.IsNullOrEmpty(prmOrdenEmbarque) Then


            Else
                dato.Parametro("prmOrdenEmbarque") = prmOrdenEmbarque
            End If
            ds = dato.Tabla("spQryListaOrdenesEmbarqueDet")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function
    Public Function ConsultaOrdenEmbarqueDet(ByVal prmOrdenEmbarque As String, ByVal prmEstatus As String) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet

        Try
            If String.IsNullOrEmpty(prmOrdenEmbarque) Then


            Else
                dato.Parametro("prmOrdenEmbarque") = prmOrdenEmbarque
                dato.Parametro("prmStatus") = prmEstatus
            End If
            ds = dato.Tabla("spQryListaOrdenesProdDet")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    'Consulta orden de Embarque con fechas
    Public Function ConsultaOrdenEmbarqueDet(ByVal prmOrdenEmbarque As String, ByVal prmFechaInicio As String, ByVal prmFechaFin As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            If String.IsNullOrEmpty(prmOrdenEmbarque) Then
                dato.Parametro("prmFechaInicio") = prmFechaInicio
                dato.Parametro("prmFechaFin") = prmFechaFin

            Else
                dato.Parametro("prmFechaInicio") = prmFechaInicio
                dato.Parametro("prmFechaFin") = prmFechaFin
                dato.Parametro("prmOrdenEmbarque") = prmOrdenEmbarque
            End If

            ds = dato.Tabla("spQryListaOrdenesProdDet")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    Public Function LiberaOrdenEmbarque(ByVal prmValidacion As Integer, ByVal prmOrdenEmbarque As String,
                                  ByVal prmDetalle As String, ByVal prmGuia As String,
                                  ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim CRespuesta As New CResultado
        Dim Dato As New clsDato
        Try
            ''Parametros
            Dato.Parametro("prmValidacion") = prmValidacion
            Dato.Parametro("prmOrdenEmbarque") = prmOrdenEmbarque
            Dato.Parametro("prmDetalle") = prmDetalle
            Dato.Parametro("prmGuia") = prmGuia
            Dato.Parametro("prmEstacion") = prmEstacion
            Dato.Parametro("prmUsuario") = prmUsuario
            Dato.SP(True) = "spInsLiberaOrdenEmbarque"

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
    Public Function ListaOrdenEmbarque() As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            ds = dato.Tabla("spQryListaOrenesEmbarqueCombo")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    Friend Function GuardarGuia(prmOrdenEmbarque As String, prmGuia As String, ByVal prmPaqueteria As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim sResultado As CResultado

            sResultado = db.SPToDataRow("spUpdGuiaOrdenEmbarque", prmOrdenEmbarque, prmGuia, prmPaqueteria, prmEstacion, prmUsuario)

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

    Friend Function GuardarGuiaCustomer(prmOrdenEmbarque As String, prmGuia As String, ByVal prmPaqueteria As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim sResultado As CResultado

            sResultado = db.SPToDataRow("spUpdGuiaCustomerOrdenEmbarque", prmOrdenEmbarque, prmGuia, prmPaqueteria, prmEstacion, prmUsuario)

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

    Public Function ListaEstaciones() As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            ds = dato.Tabla("spQryListaEstaciones")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function
    Public Function ListaRevisiones(ByVal prmNumParteMP As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmNumParte") = prmNumParteMP
            ds = dato.Tabla("spQryListaRevisiones")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    Function ListaOrdenEmbarqueMonitor(ByVal prmStatus As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmStatus") = prmStatus
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaOrdenProd")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    Function ListaPalletsSinCerrar(ByVal prmOrdenEmbarque As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaOrdenEmbarquePalletSinCerrar", prmOrdenEmbarque, prmEstacion, prmUsuario)

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

    Function ListaPalletsPteValida(ByVal prmOrdenEmbarque As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaOrdenEmbarquePalletPteValida", prmOrdenEmbarque, prmEstacion, prmUsuario)

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

    Function ListaPalletsPteEntrega(ByVal prmOrdenEmbarque As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaOrdenEmbarquePalletPteEntrega", prmOrdenEmbarque, prmEstacion, prmUsuario)

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

    Function ListarPartidas(ByVal prmOrdenEmbarque As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaDetalleOrdenEmbarque", prmOrdenEmbarque, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar Partidas"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar Partidas "
            Return pRespuesta
        End Try
    End Function

    Function ListaPalletsPteTransito(ByVal prmOrdenEmbarque As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaOrdenEmbarquePalletTransito", prmOrdenEmbarque, prmEstacion, prmUsuario)

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

    Friend Function CancelaEmbarque(ByVal prmPedido As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado

        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdCancelaEmbarque", prmPedido, prmEstacion, prmUsuario)

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


    Function ListaOrdenesTrabajo(ByVal prmOrdenTrabajo As String, ByVal prmFechaDesde As String, ByVal prmFechaHasta As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmOrdenTrabajo") = prmOrdenTrabajo
            dato.Parametro("prmFechaDesde") = prmFechaDesde
            dato.Parametro("prmFechaHasta") = prmFechaHasta
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaOrdenTrabajo")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    Function ListaOrdenesTrabajoDet(ByVal prmOrdenTrabajo As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmOrdenTrabajo") = prmOrdenTrabajo
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaOrdenTrabajoDet")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    Function ListaOrdenesTrabajoPallets(ByVal prmOrdenTrabajo As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmOrdenTrabajo") = prmOrdenTrabajo
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaOrdenTrabajoPallets")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try
        Return ds
    End Function

    Function ListaOrdenEmbarqueMonitorTodas(ByVal prmOrdenEmbarque As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaOrdenEmbarqueTodas", prmOrdenEmbarque, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar embarques"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar embarques"
            Return pRespuesta
        End Try
    End Function

    Public Function ConsultaPalletPorEmbarque(
                                       ByVal prmEmbarque As String,
                                       ByVal prmFechaInicio As DateTime,
                                       ByVal prmFechaFinal As DateTime,
                                       ByVal prmEstacion As String,
                                       ByVal prmUsuario As String
                                       ) As DataSet
        Dim dato As New clsDato()
        Dim ds As New DataSet
        Try
            dato.Parametro("prmEmbarque") = prmEmbarque
            dato.Parametro("prmFechaInicio") = prmFechaInicio
            dato.Parametro("prmFechaFinal") = prmFechaFinal
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaPalletsEmbarque")
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, " AXC")
        End Try

        Return ds
    End Function

    Friend Function ListaSurtidos(ByVal prmOrdenCompra As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaDocSurtidos", prmOrdenCompra, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar documentos de surtidos"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar documentos de Surtidos"
            Return pRespuesta
        End Try
    End Function

    Friend Function ListaPartidasSurtido(prmRecepcion As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaPartidasSurtido", prmRecepcion, prmEstacion, prmUsuario)

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

    Friend Function ListaPalletsPorSurtido(prmRecepcion As Integer, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaPalletsSurtido", prmRecepcion, prmEstacion, prmUsuario)

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

    Public Function ConsultaOEDet(ByVal prmOrdenEmbarque As String) As CResultado
        Dim pResupuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryConsultaOEDet", prmOrdenEmbarque)

            If pResultado.Estado Then
                ds = CType(pResultado.Resultado, DataSet)

                For Each dr As DataRow In ds.Tables(0).Rows
                    Select Case dr("Estado").ToString
                        Case "OK"
                            dt = ds.Tables(1).Copy
                            pResupuesta.Estado = True
                            pResupuesta.Texto = ""
                            pResupuesta.Tabla = dt
                        Case "ER"
                            pResupuesta.Estado = False
                            pResupuesta.Texto = dr("Texto").ToString
                        Case Else
                            pResupuesta.Estado = False
                            pResupuesta.Texto = "Error al tratar de listar el detalle"
                    End Select
                Next
            Else
                pResupuesta.Estado = False
                pResupuesta.Texto = pResultado.Texto
            End If

            Return pResupuesta


        Catch ex As Exception
            pResupuesta.Estado = False
            pResupuesta.Texto = "Error al tratar de listar el detalle"
            Return pResupuesta
        End Try

    End Function

    Friend Function BuscarEmbarquesPorFecha(ByVal prmFechaInicio As String, ByVal prmFechaFin As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryOrdenEmbarquesPorFecha", prmFechaInicio, prmFechaFin, prmEstacion, prmUsuario)

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

    Friend Function RptOrdenEmbarque(ByVal prmOrdenCompra As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spRptQryOrdenEmbarque", prmOrdenCompra, prmEstacion, prmUsuario)

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
End Class
