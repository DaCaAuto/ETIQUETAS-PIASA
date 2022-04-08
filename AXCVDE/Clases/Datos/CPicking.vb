Public Class CPicking
    Public Function ObtenerPosicionesPK(ByVal prmEstacion As String) As CResultado
        Dim pRespuesta As New CResultado
        Dim dt As New DataTable

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet


            pResultado = db.SPToDataSet("spQryPosicionesPK", prmEstacion)

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
                            pRespuesta.Texto = "Error al Obtener Posiciones PK [SP0]"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
                pRespuesta.Resultado = Nothing
            End If
            Return pRespuesta
        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "ObtenerPosicionesPK")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al Obtener Posiciones PK [SP1] " + ex.Message

            Return pRespuesta
        End Try
    End Function

    Public Function NumPartePorPosicionPK(prmPosicion As String, ByVal prmEstacion As String) As CResultado
        Dim pRespuesta As New CResultado
        Dim dt As New DataTable

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet


            pResultado = db.SPToDataSet("spQryNumPartePorPosicionPK", prmPosicion, prmEstacion)

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
                            pRespuesta.Texto = "Error al Obtener Posiciones PK [SP0]"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
                pRespuesta.Resultado = Nothing
            End If
            Return pRespuesta
        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "ObtenerPosicionesPK")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al Obtener Posiciones PK [SP1] " + ex.Message

            Return pRespuesta
        End Try
    End Function

    Public Function AgregarNumParte(ByVal prmUbicacion As String,
                                               ByVal prmNumParte As String,
                                               ByVal prmMinimo As Double,
                                               ByVal prmMaximo As Double,
                                               ByVal prmEstacion As String,
                                               ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spInsCapacidadUbicacionPK", prmUbicacion, prmNumParte, prmMinimo, prmMaximo, prmEstacion, prmUsuario)

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
            cRespuesta.Texto = "Error al AgregarNumParte" + ex.Message
            cRespuesta.Resultado = Nothing
        End Try

        Return cRespuesta
    End Function

    Public Function ObtenerOrdenesReabastecimiento(prmProducto As String, prmEstacion As String, prmuUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Dim dt As New DataTable

        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet


            pResultado = db.SPToDataSet("spQryOrdenReabasteceADMIN", prmProducto, prmEstacion, prmuUsuario)

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
                            pRespuesta.Texto = "Error al Obtener ordenes de reabastecimiento PK [SP0]"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
                pRespuesta.Resultado = Nothing
            End If
            Return pRespuesta
        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "ObtenerOrdenesReabastecimiento")

            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al Obtener ordenes de reabastecimiento PK [SP1] " + ex.Message

            Return pRespuesta
        End Try
    End Function

    Public Function DeshabilitaOrden(prmOrden As String, ByVal prmEstacion As String,
                                              ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdDeshabilitaOrdenReabastece", prmOrden, prmEstacion, prmUsuario)

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
            cRespuesta.Texto = "Error al AgregarNumParte" + ex.Message
            cRespuesta.Resultado = Nothing
        End Try

        Return cRespuesta
    End Function

    Public Function Eliminar(prmOrden As String, ByVal prmEstacion As String,
                                              ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado

        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spDelOrdenReabastece", prmOrden, prmEstacion, prmUsuario)

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
            cRespuesta.Texto = "Error al eliminar orden" + ex.Message
            cRespuesta.Resultado = Nothing
        End Try

        Return cRespuesta
    End Function
End Class
