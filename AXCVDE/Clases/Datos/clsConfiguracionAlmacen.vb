Imports AXCMateriaPrima

Public Class ClsConfiguracionAlmacen

    'Metodos
    Public Function ListaPasillosPK(ByVal prmClase$) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmClase") = prmClase
            ds = dato.Tabla("spQryListaPasillosPK")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function
    Public Function ListaPasillos(ByVal prmClase$, ByVal prmAlmacen As String) As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            dato.Parametro("prmTipo") = prmClase
            dato.Parametro("prmAlmacen") = prmAlmacen
            ds = dato.Tabla("spQryListaRacksAlmacen")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Public Function ListaNumParte() As DataSet
        Dim ds As New DataSet
        Try
            Dim dato As New clsDato
            ds = dato.Tabla("spQryListaNumParte")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Public Function CalculaLayout(ByVal prmPasillo As String, ByVal prmAlmacen As String) As clsDato
        Dim dato As New clsDato
        Try
            dato.Parametro("prmPasillo") = prmPasillo
            dato.Parametro("prmAlmacen") = prmAlmacen
            dato.SP(True) = "spQryCalculaLayoutRack"
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return dato
    End Function

    Public Function ListaLayout(ByVal prmPasillo As String, ByVal prmAlmacen As String) As clsDato
        Dim dato As New clsDato
        Try
            dato.Parametro("prmPasillo") = prmPasillo
            dato.Parametro("prmAlmacen") = prmAlmacen
            dato.SP(True) = "spQryLayoutRack"
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return dato
    End Function

    Public Function ActualizaTipoEstante(ByVal prmCodigoEstante As String, ByVal prmTipo As String, ByVal prmEstacion As String, ByVal prmUsuario As String)
        Dim dato As New clsDato
        Dim dr As DataSet
        Dim x
        Try
            If String.IsNullOrEmpty(prmCodigoEstante) Then
                MsgBox("No se ha seleccionado una ubicación", MsgBoxStyle.Critical, "AXC")
                Return 0
            End If

            dato.Parametro("prmCodigoEstante") = prmCodigoEstante
            dato.Parametro("prmTipo") = prmTipo
            dato.Parametro("prmUsuario") = prmUsuario
            dato.Parametro("prmEstacion") = prmEstacion
            'dr = dato.SP(True) = "spUpdTipoEstante"
            dr = dato.Tabla("spUpdTipoEstante")

            x = dr.Tables.Item(0).Rows(0).Item(1)
            'Dim y
            'y = x

        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return x
    End Function

    Public Function ActualizaNumParteEstante(ByVal prmCodigoEstante As String, ByVal prmNumParte As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmCodigoEstante") = prmCodigoEstante
            dato.Parametro("prmNumParte") = prmNumParte
            dato.Parametro("prmUsuario") = prmUsuario
            dato.Parametro("prmEstacion") = prmEstacion
            ds = dato.Tabla("spUpdNumParteEstante")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Public Function ActualizaCapacidad(ByVal prmCodigoEstante As String, ByVal prmCapacidad As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmCodigoEstante") = prmCodigoEstante
            dato.Parametro("prmCapacidad") = prmCapacidad
            dato.Parametro("prmUsuario") = prmUsuario
            dato.Parametro("prmEstacion") = prmEstacion
            ds = dato.Tabla("spUpdCapacidadEstante")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Public Function BajaEstante(ByVal prmCodigoEstante As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmCodigoEstante") = prmCodigoEstante
            dato.Parametro("prmUsuario") = prmUsuario
            dato.Parametro("prmEstacion") = prmEstacion
            ds = dato.Tabla("spUpdStatusBajaEstante")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Public Function BloqueaEstante(ByVal prmCodigoEstante As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmCodigoEstante") = prmCodigoEstante
            dato.Parametro("prmUsuario") = prmUsuario
            dato.Parametro("prmEstacion") = prmEstacion
            ds = dato.Tabla("spUpdStatusBloqueadoEstante")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Public Function ActivaEstante(ByVal prmCodigoEstante As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmCodigoEstante") = prmCodigoEstante
            dato.Parametro("prmUsuario") = prmUsuario
            dato.Parametro("prmEstacion") = prmEstacion
            ds = dato.Tabla("spUpdStatusActivoEstante")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function CargaFamiliasExcluidas(ByVal prmPosiciones As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmPosiciones") = prmPosiciones
            dato.Parametro("prmUsuario") = prmUsuario
            dato.Parametro("prmEstacion") = prmEstacion
            ds = dato.Tabla("spQryFamiliaEstanteExcluido")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function CargaFamiliasIncluidas(ByVal prmPosiciones As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmPosiciones") = prmPosiciones
            dato.Parametro("prmUsuario") = prmUsuario
            dato.Parametro("prmEstacion") = prmEstacion
            ds = dato.Tabla("spQryFamiliaEstante")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function AsociaFamilia(ByVal prmFamilia$, ByVal prmUbicacion$, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Try

            Dim dato As New clsDato

            Dim Estado As String = ""
            Dim Texto As String = ""

            dato.Parametro("@prmFamilia") = prmFamilia
            dato.Parametro("@prmUbicacion") = prmUbicacion
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            dato.SP(True) = "spUpdAsociaUbicacionFamilia"

            While dato.DataReader.Read
                Estado = dato.DataReader.Item("Estado")
                Texto = dato.DataReader.Item("Texto")
            End While

            If Estado = "ER" Then
                Return Texto
            Else
                Return Estado
            End If

        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function

    Function DesasociaFamilia(ByVal prmFamilia$, ByVal prmUbicacion$, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Try

            Dim dato As New clsDato

            Dim Estado As String = ""
            Dim Texto As String = ""

            dato.Parametro("@prmFamilia") = prmFamilia
            dato.Parametro("@prmUbicacion") = prmUbicacion
            dato.Parametro("@prmEstacion") = prmEstacion
            dato.Parametro("@prmUsuario") = prmUsuario
            dato.SP(True) = "spUpdDesAsociaUbicacionFamilia"

            While dato.DataReader.Read
                Estado = dato.DataReader.Item("Estado")
                Texto = dato.DataReader.Item("Texto")
            End While

            If Estado = "ER" Then
                Return Texto
            Else
                Return Estado
            End If

        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function

    Function AgregaPosicionAlmacen(ByVal prmRack As String, ByVal prmPosNuevas As String, ByVal prmCapacidad As String, ByVal prmAlmacen As String, ByVal prmTipo As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim resp As String = ""
        Dim Estado As String = ""
        Dim Texto As String = ""
        Dim dato As New clsDato
        Try
            dato.Parametro("prmRack") = prmRack
            dato.Parametro("prmPosNuevas") = prmPosNuevas
            dato.Parametro("prmCapacidad") = prmCapacidad
            dato.Parametro("prmAlmacen") = prmAlmacen
            dato.Parametro("prmTipo") = prmTipo
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = "spInsAgregaPosicion"

            While dato.DataReader.Read
                Estado = dato.DataReader.Item("Estado")
                Texto = dato.DataReader.Item("Texto")
            End While

            If Estado = "OK" Then
                resp = Estado
            Else
                resp = Texto
            End If

        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return resp
    End Function

    Function AgregaNivelAlmacen(ByVal prmRack As String, ByVal prmNivelesNuevos As String, ByVal prmCapacidad As String, ByVal prmAlmacen As String, ByVal prmTipo As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim resp As String = ""
        Dim Estado As String = ""
        Dim Texto As String = ""
        Dim dato As New clsDato
        Try
            dato.Parametro("prmRack") = prmRack
            dato.Parametro("@prmNivelesNuevos") = prmNivelesNuevos
            dato.Parametro("prmCapacidad") = prmCapacidad
            dato.Parametro("prmAlmacen") = prmAlmacen
            dato.Parametro("prmTipo") = prmTipo
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = "spInsAgregaNivel"

            While dato.DataReader.Read
                Estado = dato.DataReader.Item("Estado")
                Texto = dato.DataReader.Item("Texto")
            End While

            If Estado = "OK" Then
                resp = Estado
            Else
                resp = Texto
            End If

        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return resp
    End Function

    Function AgregaRackAlmacen(ByVal prmUbicacion As String, ByVal prmAlmacen As String, ByVal prmNiveles As String, ByVal prmPosiciones As String, ByVal prmCapacidadEstante As Integer, ByVal prmTipo As String, ByVal prmTipoUbicacion As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim resp As String = ""
        Dim Estado As String = ""
        Dim Texto As String = ""
        Dim dato As New clsDato
        Try
            dato.Parametro("@prmUbicacion") = prmUbicacion
            dato.Parametro("prmAlmacen") = prmAlmacen
            dato.Parametro("prmNiveles") = prmNiveles
            dato.Parametro("prmPosiciones") = prmPosiciones
            dato.Parametro("prmCapacidadEstante") = prmCapacidadEstante
            dato.Parametro("prmTipoMaterial") = prmTipo
            dato.Parametro("prmTipoUbicacion") = prmTipoUbicacion
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = "spInsCreaUbicacion"

            While dato.DataReader.Read
                Estado = dato.DataReader.Item("Estado")
                Texto = dato.DataReader.Item("Texto")
            End While

            If Estado = "OK" Then
                resp = Estado
            Else
                resp = Texto
            End If

        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return resp
    End Function

    Friend Function ListaUbicacionDisponible(ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim ds As New DataSet
        Dim dato As New clsDato
        Try
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryUbicacionesDisponibles")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaPosiciones(ByVal prmClase$, ByVal prmPasillo$, ByVal prmLado$, ByVal prmCodigoPos$, ByVal prmEstacion$, ByVal prmUsuario$) As DataSet
        Dim ds As New DataSet
        Dim dato As New clsDato
        Try
            dato.Parametro("prmClase") = prmClase
            dato.Parametro("prmPasillo") = prmPasillo
            dato.Parametro("prmLado") = prmLado
            dato.Parametro("prmCodigoPos") = prmCodigoPos
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryPosicionesRack")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaPasillosPisos(ByVal prmEstacion$, ByVal prmUsuario$) As DataSet
        Dim ds As New DataSet
        Dim dato As New clsDato
        Try
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaPasillosPisos")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaPosPiso(ByVal prmPos$, ByVal prmEstacion$, ByVal prmUsuario$) As DataSet
        Dim ds As New DataSet
        Dim dato As New clsDato
        Try
            dato.Parametro("prmPos") = prmPos
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaPosPiso")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Friend Function BajaPosiciones(prmPosiciones As String, prmAlmacen As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdBajaPosiciones", prmPosiciones, prmAlmacen, prmEstacion, prmUsuario)

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

    Friend Function ActivaPosiciones(prmPosiciones As String, prmAlmacen As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdActivaPosiciones", prmPosiciones, prmAlmacen, prmEstacion, prmUsuario)

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

    Friend Function BloqueaPosiciones(prmPosiciones As String, prmAlmacen As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdBloqueaPosiciones", prmPosiciones, prmAlmacen, prmEstacion, prmUsuario)

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
    Friend Function ConsultaPosiciones(prmPosiciones As String, prmAlmacen As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryConsultaPosicionesLayout", prmPosiciones, prmAlmacen, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar posiciones"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar posiciones "
            Return pRespuesta
        End Try
    End Function

    Friend Function ActualizaPosiciones(prmPosiciones As String, ByVal prmAlmacen As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdActualizaPosiciones", prmPosiciones, prmAlmacen, prmEstacion, prmUsuario)

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
