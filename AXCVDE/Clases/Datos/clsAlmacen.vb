Imports AXC
Imports AXCSchettino
Imports Microsoft.VisualBasic

Public Class ClsAlmacen
    'Propiedades
    Private pIdAlmacen As Integer
    Private pIdPlanta As Integer
    Private pAlmacen As String
    Private pERPAlmacen As String
    Private pTag1 As String
    Private pTag2 As String
    Private pFechaCrea As String

    'Asignacion de propiedades
    Public Property IdAlmacen() As Integer
        Get
            Return pIdAlmacen
        End Get
        Set(ByVal value As Integer)
            Me.pIdAlmacen = value
        End Set
    End Property

    Public Property IdPlanta() As Integer
        Get
            Return pIdPlanta
        End Get
        Set(ByVal value As Integer)
            Me.pIdPlanta = value
        End Set
    End Property

    Public Property Almacen() As String
        Get
            Return pAlmacen
        End Get
        Set(ByVal value As String)
            Me.pAlmacen = value
        End Set
    End Property

    Public Property ERPAlmacen() As String
        Get
            Return pERPAlmacen
        End Get
        Set(ByVal value As String)
            Me.pERPAlmacen = value
        End Set
    End Property

    Public Property Tag1() As String
        Get
            Return pTag1
        End Get
        Set(ByVal value As String)
            Me.pTag1 = value
        End Set
    End Property

    Public Property Tag2() As String
        Get
            Return pTag2
        End Get
        Set(ByVal value As String)
            Me.pTag2 = value
        End Set
    End Property

    Public Property FechaCrea() As String
        Get
            Return pFechaCrea
        End Get
        Set(ByVal value As String)
            Me.pFechaCrea = value
        End Set
    End Property

    'Metodos

    Public Function CreaAlmacen(ByVal prmPlanta As String,
                                ByVal prmAlmacen As String,
                                ByVal prmERPAlmacen As String,
                                ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Dim dato As New clsDato
        CreaAlmacen = ""
        Dim Estado As String = ""
        Dim Texto As String = ""

        dato.Parametro("@prmPlanta") = prmPlanta
        dato.Parametro("@prmAlmacen") = prmAlmacen
        dato.Parametro("@prmERPAlmacen") = prmERPAlmacen
        dato.Parametro("@prmEstacion") = prmEstacion
        dato.Parametro("@prmUsuario") = prmUsuario

        dato.SP(True) = "spInsCreaAlmacen"

        While dato.DataReader.Read
            Estado = dato.DataReader.Item("Estado")
            Texto = dato.DataReader.Item("Texto")
        End While


        If Estado = "ER" Then
            Return Estado
        End If

    End Function

    Public Function ListaAlmacenes() As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            ds = dato.Tabla("spQryListadoAlmacen")

        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Friend Function ListaAlmacenesPorEstacion(ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaAlmacenPorEstacion", prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar almacenes "
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar almacenes "
            Return pRespuesta
        End Try
    End Function

    Friend Function ListaTipoUbicacion(prmTipoUbicacion As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaTiposUbicacion", prmTipoUbicacion, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar Tipos de ubicación"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar tipos de ubicación "
            Return pRespuesta
        End Try
    End Function

    Public Function ListaAlmacenes(ByVal prmPlanta As String) As DataSet
        Try
            Dim dato As New clsDato
            Dim ds As New DataSet
            dato.Parametro("prmPlanta") = prmPlanta
            ds = dato.Tabla("spQryListadoAlmacen")
            Return ds
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function

    Public Function ListaLineasProd() As DataSet
        Try
            Dim dato As New clsDato
            Dim ds As New DataSet
            ds = dato.Tabla("spQryListadoLinea")
            Return ds
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function

    Public Function ListaPlantas() As DataSet
        Try
            Dim dato As New clsDato
            Dim ds As New DataSet
            ds = dato.Tabla("spQryListadoPlanta")
            Return ds
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function



    Function ListaTiposInventarios(ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaTiposInventarios")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaEjerciciosInventario(ByVal prmIdEjercicio As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmIdEjercicio") = prmIdEjercicio
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaEjerciciosInventario")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function CreaEjercicioInventario(ByVal prmTipoEjercicio$, ByVal prmComentarios$, ByVal prmAlmacen As String, ByVal prmEstacion$, ByVal prmUsuario$) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spInsCreaEjercicioInventario", prmTipoEjercicio, prmComentarios, prmAlmacen, prmEstacion, prmUsuario)

            If CResultado.Estado Then
                dr = CType(CResultado.Resultado, DataRow)


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

    Function CreaEjercicioInventarioArticulo(ByVal prmInventario%, ByVal prmNumParte$, ByVal prmEstacion$, ByVal prmUsuario$) As String
        Dim resp$ = ""
        Dim Estado$ = ""
        Dim Texto$ = ""
        Dim dato As New clsDato
        Try
            dato.Parametro("prmInventario") = prmInventario
            dato.Parametro("prmNumParte") = prmNumParte
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = "spInsEjercicioNumParte"

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

    Friend Function ListaPlantas(prmPlanta As String, prmEstacion As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListadoPlantas", prmPlanta, prmEstacion)

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
                            pRespuesta.Texto = "Error al tratar de listar plantas"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar plantas "
            Return pRespuesta
        End Try
    End Function

    Friend Function ActualizarPrevioInventario(ByVal prmInventario As Long, prmEstacion As String, prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdInvCierrePrevio", prmInventario, prmEstacion, prmUsuario)

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

    Function CreaEjercicioInventarioRack(ByVal prmIdInventario$, ByVal prmRack$, ByVal prmEstacion$, ByVal prmUsuario$) As String
        Dim resp$ = ""
        Dim Estado$ = ""
        Dim Texto$ = ""
        Dim dato As New clsDato
        Try
            dato.Parametro("prmInventario") = prmIdInventario
            dato.Parametro("prmRack") = prmRack
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = "spInsCreaEjercicioPorRack"

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

    Function CreaEjercicioInventarioPosicion(ByVal prmIdInventario$, ByVal prmPosicion$, ByVal prmEstacion$, ByVal prmUsuario$) As String
        Dim resp$ = ""
        Dim Estado$ = ""
        Dim Texto$ = ""
        Dim dato As New clsDato
        Try
            dato.Parametro("prmInventario") = prmIdInventario
            dato.Parametro("prmPosicion") = prmPosicion
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = "spInsCreaEjercicioPorPosicion"

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

    'Function CreaEjercicioInventarioPosicion(ByVal prmTipoEjercicio%, ByVal prmEstacion$, ByVal prmUsuario$) As CResultado
    '    Dim cRespuesta As New CResultado
    '    Try
    '        Dim db As New CAccesoDatos
    '        Dim dr As DataRow
    '        Dim sResultado As CResultado

    '        sResultado = db.SPToDataRow("spInsCreaEjercicioTotal", prmTipoEjercicio, prmEstacion, prmUsuario)

    '        If sResultado.Estado Then
    '            dr = CType(sResultado.Resultado, DataRow)


    '            If dr("Estado") = "ER" Then
    '                cRespuesta.Estado = False
    '                cRespuesta.Texto = dr("Texto").ToString
    '                cRespuesta.Resultado = Nothing
    '            Else
    '                cRespuesta.Estado = True
    '                cRespuesta.Texto = dr("Texto").ToString
    '                cRespuesta.Resultado = Nothing
    '            End If
    '        Else
    '            cRespuesta.Estado = False
    '            cRespuesta.Texto = sResultado.Texto
    '            cRespuesta.Resultado = Nothing
    '        End If
    '    Catch ex As Exception
    '        cRespuesta.Estado = False
    '        cRespuesta.Texto = ex.Message
    '    End Try
    '    Return cRespuesta
    'End Function

    Function CreaEjercicioInventarioTotal(ByVal prmIdInventario$, ByVal prmEstacion$, ByVal prmUsuario$) As String
        Dim resp$ = ""
        Dim Estado$ = ""
        Dim Texto$ = ""
        Dim dato As New clsDato
        Try
            dato.Parametro("prmInventario") = CInt(prmIdInventario)
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = "spInsCreaEjercicioTotal"

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

    'Function CreaEjercicioInventarioTotal(ByVal prmTipoEjercicio%, ByVal prmEstacion$, ByVal prmUsuario$) As CResultado
    '    Dim cRespuesta As New CResultado
    '    Try
    '        Dim db As New CAccesoDatos
    '        Dim dr As DataRow
    '        Dim sResultado As CResultado

    '        sResultado = db.SPToDataRow("spInsCreaEjercicioTotal", prmTipoEjercicio, prmEstacion, prmUsuario)

    '        If sResultado.Estado Then
    '            dr = CType(sResultado.Resultado, DataRow)


    '            If dr("Estado") = "ER" Then
    '                cRespuesta.Estado = False
    '                cRespuesta.Texto = dr("Texto").ToString
    '                cRespuesta.Resultado = Nothing
    '            Else
    '                cRespuesta.Estado = True
    '                cRespuesta.Texto = dr("Texto").ToString
    '                cRespuesta.Resultado = Nothing
    '            End If
    '        Else
    '            cRespuesta.Estado = False
    '            cRespuesta.Texto = sResultado.Texto
    '            cRespuesta.Resultado = Nothing
    '        End If
    '    Catch ex As Exception
    '        cRespuesta.Estado = False
    '        cRespuesta.Texto = ex.Message
    '    End Try
    '    Return cRespuesta
    'End Function


    Function ListaPalletsInventario(ByVal prmIdEjercicio%, ByVal prmPosicion As String, prmNumParte As String, ByVal prmEstacion$, ByVal prmUsuario$) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmIdEjercicio") = prmIdEjercicio
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaPalletsInventarios")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function EstatusInventario(ByVal prmIdEjercicio%, ByVal prmEstacion$, ByVal prmUsuario$) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmIdEjercicio") = prmIdEjercicio
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryEstatusInventario")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaPalletsInventarioNoLeidos(ByVal prmIdEjercicio%, ByVal prmPosicion As String, prmNumParte As String, ByVal prmEstacion$, ByVal prmUsuario$) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmIdEjercicio") = prmIdEjercicio
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaPalletsInventariosNoLeidos")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaPalletsInventarioEnRegistro(ByVal prmIdEjercicio%, ByVal prmPosicion As String, prmNumParte As String, ByVal prmEstacion$, ByVal prmUsuario$) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmIdEjercicio") = prmIdEjercicio
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaPalletsInventariosEnRegistro")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ObtieneUltimoIdInventarios(ByVal prmTipoEjercicio$, ByVal prmAlmacen As String, ByVal prmEstacion$, ByVal prmUsuario$) As String
        Dim resp$ = ""
        Dim Estado$ = ""
        Dim Texto$ = ""
        Dim dato As New clsDato
        Try
            dato.Parametro("prmTipoEjercicio") = prmTipoEjercicio
            dato.Parametro("prmAlmacen") = prmAlmacen
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = "spQryObtieneUltimoId"

            While dato.DataReader.Read
                Estado = dato.DataReader.Item("Estado")
                Texto = dato.DataReader.Item("Texto")
            End While

            Return Texto

        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return resp
    End Function

    Function CierraInventario(ByVal prmInventario%, ByVal prmEstacion$, ByVal prmUsuario$) As String
        Dim resp$ = ""
        Dim Estado$ = ""
        Dim Texto$ = ""
        Dim dato As New clsDato
        Try
            dato.Parametro("prmInventario") = prmInventario
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = "spUpdCierraInventario"

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

    Function CancelaInventario(ByVal prmInventario%, ByVal prmEstacion$, ByVal prmUsuario$) As String
        Dim resp$ = ""
        Dim Estado$ = ""
        Dim Texto$ = ""
        Dim dato As New clsDato
        Try
            dato.Parametro("prmInventario") = prmInventario
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = "spUpdCancelaInventario"

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

    Function ListaResultadosInventario(ByVal prmInventario%, ByVal prmNumParte As String, ByVal prmLote As String, ByVal prmEstacion$, ByVal prmUsuario$, ByVal prmAgrupa As Int64) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmInventario") = prmInventario
            'dato.Parametro("prmNumParte") = prmNumParte
            'dato.Parametro("prmLote") = prmLote
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.Parametro("prmAgrupa") = prmAgrupa
            ds = dato.Tabla("spQryResultadoInventarios")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function



    Function ListaIdsInventarios(ByVal prmTipoEjercicio$, ByVal prmAlmacen As String, ByVal prmEstacion$, ByVal prmUsuario$) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmTipoEjercicio") = prmTipoEjercicio
            dato.Parametro("prmAlmacen") = prmAlmacen
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaIdInventarios")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaTiposAjustesInventarios(ByVal prmEstacion$, ByVal prmUsuario$) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaTipoAjusteInventario")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function ListaConceptosAjustesInventario(ByVal prmTipoAjuste$, ByVal prmEstacion$, ByVal prmUsuario$) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmTipoAjuste") = prmTipoAjuste
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaConceptosAjustesInventario")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function CreaConceptoAjusteInventario(ByVal prmNombre$, ByVal prmTipoAjuste$, ByVal prmEstacion$, ByVal prmUsuario$) As String
        Dim resp$ = ""
        Dim Estado$ = ""
        Dim Texto$ = ""
        Dim dato As New clsDato
        Try
            dato.Parametro("prmNombre") = prmNombre
            dato.Parametro("prmTipoAjuste") = prmTipoAjuste
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.SP(True) = "spInsCreaConceptoAjusteInventario"

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

    Function ListaAlmacen(ByVal prmPlanta As String, ByVal prmAlmacen As String, ByVal prmEstacion As String) As CResultado
        Dim pRespuesta As New CResultado
        Dim dt As DataTable
        Dim ds As DataSet
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado

            pResultado = db.SPToDataSet("spQryListadoAlmacen", prmPlanta, prmAlmacen, prmEstacion)

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
                            pRespuesta.Texto = "Error"
                            pRespuesta.Resultado = Nothing
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta
        Catch ex As Exception
            CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex, "clsAlmacen.ListadoAlmacen")
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de buscar Almacenes [SP1]" + ex.Message

            Return pRespuesta
        End Try
    End Function

    Function ListaResultadoInvPorFechas(ByVal prmFechaInicio As String, ByVal prmFechaFinal As String, ByVal prmEstacion$, ByVal prmUsuario$, ByVal prmAgrupa As Integer) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmFechaInicial") = prmFechaInicio
            dato.Parametro("prmFechaFinal") = prmFechaFinal
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            dato.Parametro("prmAgrupa") = prmAgrupa
            ds = dato.Tabla("spRptQryInventarioPorFechas")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Friend Function ActualizarInformacionInventario(prmInventario As Object, prmEstacion As String, prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdInvCierrePrevio", prmInventario, prmEstacion, prmUsuario)

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

    Friend Function ListaEstatusInventario(prmBusqueda As String, prmEstacion As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListadoEstatusInv", prmBusqueda, prmEstacion)

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
                            pRespuesta.Texto = "Error al tratar de listar plantas"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar plantas "
            Return pRespuesta
        End Try
    End Function

    Function ListaTiposInventariosCResult(ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaTiposInventariosSinTotal", prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar plantas"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar plantas "
            Return pRespuesta
        End Try
    End Function

    Friend Function ListaConsultaEjerciciosInventario(prmBusqueda$, prmTipoInv$, prmPlanta$, prmEstatus$, prmFechaIni$, prmFechaFin$, prmEstacion$, prmUsuario$) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryInventariosPorFecha", prmBusqueda, prmTipoInv, prmPlanta, prmEstatus, prmFechaIni, prmFechaFin, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar inventarios por fecha"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar inventarios por fecha "
            Return pRespuesta
        End Try
    End Function

    Friend Function ListaConsultaEjerciciosInventarioDetalles(prmBusqueda As Integer, prmEstacion$, prmUsuario$) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryInventariosPorFechaDetalle", prmBusqueda, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar inventarios por fecha"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar inventarios por fecha "
            Return pRespuesta
        End Try
    End Function

End Class
