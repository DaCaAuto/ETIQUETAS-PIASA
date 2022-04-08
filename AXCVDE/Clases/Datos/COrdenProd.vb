Public Class COrdenProd
    Public Function BuscarOrdenProd(ByVal prmOrdenProd As String) As CResultado
        Dim pRespuesta As New CResultado
        Dim dt As DataTable
        Dim ds As DataSet
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado

            pResultado = db.SPToDataSet("spQryConsultaOP", prmOrdenProd, My.Settings.Estacion)

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
                            pRespuesta.Texto = "No se pudo procesar la orden"
                            pRespuesta.Resultado = Nothing
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta
        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "COrdenProduccion.BuscarOrdenProduccion")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de buscar orden de Produccion [SP1]" + ex.Message

            Return pRespuesta
        End Try
    End Function

    Public Function ListaNumParte() As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaNumParte", "@")

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
                            pRespuesta.Texto = "Error al listar los números de parte [SP0]"
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
            pRespuesta.Texto = ex.Message

            Return pRespuesta
        End Try
    End Function

    Public Function ConsultaNumParte(ByVal prmNumParte As String) As CResultado
        Dim pRespuesta As New CResultado
        Dim dt As DataTable
        Dim ds As DataSet
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado

            pResultado = db.SPToDataSet("spQryListaNumParte", prmNumParte)

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

    Public Function AgregarPartida(ByVal prmOrdenProd As String, ByVal prmNumParte As String, _
                                  ByVal Cantidad As Double, prmLinea As String, ByVal prmReferencia As String, ByVal prmEstacion As String, _
                                  ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spInsNuevaOrdenProduccion", prmOrdenProd, prmNumParte, Cantidad, prmLinea, prmReferencia, prmEstacion, prmUsuario)

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

    Public Function ModificarPartida(ByVal prmOrdenProd As String, ByVal prmPartida As String, _
                                 ByVal Cantidad As Double, ByVal prmEstacion As String, _
                                 ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdModificaPartidadOP", prmOrdenProd, prmPartida, Cantidad, prmEstacion, prmUsuario)

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

    Public Function EliminarPartida(ByVal prmOrdenProd As String, ByVal prmPartida As String, _
                                ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spDelEliminaPartidadOP", prmOrdenProd, prmPartida, prmEstacion, prmUsuario)

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

    Function ConsultaNuevoOP() As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spQryNuevaOP")

            If CResultado.Estado Then
                dr = CResultado.Resultado

                If dr("Estado") = "ER" Then
                    cRespuesta.Estado = False
                    cRespuesta.Texto = dr("Texto").ToString
                    cRespuesta.Resultado = Nothing
                Else
                    cRespuesta.Estado = True
                    cRespuesta.Texto = dr("Texto").ToString
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

    Public Function ListaLinea() As CResultado
        Dim pRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaLineas", "@")

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
                            pRespuesta.Texto = "Error al listar las líneas [SP0]"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "COrdenProd.ListaLineas")

            pRespuesta.Estado = False
            pRespuesta.Texto = ex.Message

            Return pRespuesta
        End Try
    End Function

End Class
