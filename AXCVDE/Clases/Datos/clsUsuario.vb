Imports System.Net.NetworkInformation

Public Class clsUsuario


    Dim wpdato As New clsDato
    Public Function CreaUsuario(ByVal prmNuevoUsuario As String, ByVal prmNombre As String, ByVal prmPass As String, ByVal prmEstacion As String, ByVal prmUser As String) As String

        Dim wpdato As New clsDato
        Try
            wpdato.Parametro("prmNuevoUsuario") = prmNuevoUsuario
            wpdato.Parametro("prmPass") = prmPass
            wpdato.Parametro("prmNombre") = prmNombre
            wpdato.Parametro("prmEstacion") = prmEstacion
            wpdato.Parametro("prmUsuario") = prmUser

            wpdato.SP(True) = "spInsNuevoUsuario"

            Dim wpEstado As String = ""
            Dim wpTextoERR As String = ""

            While wpdato.DataReader.Read

                wpEstado = wpdato.DataReader.Item("Estado")
                wpTextoERR = wpdato.DataReader.Item("Texto")

            End While

            If wpEstado = "ER" Then
                Return wpTextoERR
            ElseIf wpEstado = "OK" Then
                Return wpEstado
            End If
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try

        Return 0

    End Function

    Public Function HabilitaUsuario(ByVal prmUsr As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String

        Try
            wpdato.Parametro("prmUsr") = prmUsr
            wpdato.Parametro("prmEstacion") = prmEstacion
            wpdato.Parametro("prmUsuario") = prmUsuario

            wpdato.SP(True) = "spUpdHabilitaUsuario"

            Dim wpEstado As String = ""
            Dim wpTextoERR As String = ""

            While wpdato.DataReader.Read

                wpEstado = wpdato.DataReader.Item("Estado")
                wpTextoERR = wpdato.DataReader.Item("Texto")

            End While

            If wpEstado = "ER" Then
                Return wpTextoERR
            ElseIf wpEstado = "OK" Then
                Return wpEstado
            End If
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try

        Return 0



    End Function

    Public Function DeshabilitaUsuario(ByVal prmUsr1 As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String
        Try
            wpdato.Parametro("prmUsr") = prmUsr1
            wpdato.Parametro("prmEstacion") = prmEstacion
            wpdato.Parametro("prmUsuario") = prmUsuario

            wpdato.SP(True) = "spUpdDeshabilitaUsuario"

            Dim wpEstado As String = ""
            Dim wpTextoERR As String = ""

            While wpdato.DataReader.Read

                wpEstado = wpdato.DataReader.Item("Estado")
                wpTextoERR = wpdato.DataReader.Item("Texto")

            End While
            If wpEstado = "ER" Then
                Return wpTextoERR
            ElseIf wpEstado = "OK" Then
                Return wpEstado
            End If
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try

        Return 0

    End Function

    Public Function AsociaUsuarioGrupo(ByVal prmGrupo As String, ByVal prmUsr As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String

        Dim wpdato As New clsDato
        Try
            wpdato.Parametro("prmGrupo") = prmGrupo
            wpdato.Parametro("PrmUsr") = prmUsr
            wpdato.Parametro("prmEstacion") = prmEstacion
            wpdato.Parametro("prmUsuario") = prmUsuario

            wpdato.SP(True) = "spUpdAsociaUsuario"

            Dim wpEstado As String = ""
            Dim wpTextoERR As String = ""

            While wpdato.DataReader.Read

                wpEstado = wpdato.DataReader.Item("Estado")
                wpTextoERR = wpdato.DataReader.Item("Texto")

            End While

            If wpEstado = "ER" Then
                Return wpTextoERR
            ElseIf wpEstado = "OK" Then
                Return wpEstado
            End If
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try

        Return 0

    End Function

    Public Function DesasociaUsuarioGrupo(ByVal prmGrupo As String, ByVal prmUsr As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String

        Dim wpdato As New clsDato
        Try
            wpdato.Parametro("prmGrupo") = prmGrupo
            wpdato.Parametro("prmUsr") = prmUsr
            wpdato.Parametro("prmEstacion") = prmEstacion
            wpdato.Parametro("prmUsuario") = prmUsuario

            wpdato.SP(True) = "spUpdDesasociaUsuario"

            Dim wpEstado As String = ""
            Dim wpTextoERR As String = ""

            While wpdato.DataReader.Read

                wpEstado = wpdato.DataReader.Item("Estado")
                wpTextoERR = wpdato.DataReader.Item("Texto")

            End While

            If wpEstado = "ER" Then
                Return wpTextoERR
            ElseIf wpEstado = "OK" Then
                Return wpEstado
            End If
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try

        Return 0

    End Function

    Public Function Login(ByVal prmUsuario As String, ByVal prmPassword As String, ByVal prmEstacion As String, ByVal prmAutorizacion As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim CResultado As New CResultado
            Dim pDb As New CAccesoDatos
            Dim dr As DataRow

            Dim pMacServer As String = CMetodos.MACfromIP(prmAutorizacion).ToUpper

            CResultado = pDb.SPToDataRow("spUpdLogin", prmUsuario, prmPassword, prmEstacion) ', pMacServer)

            If CResultado.Estado Then
                dr = CResultado.Resultado

                If dr("Estado") = "ER" Then
                    Login = cRespuesta
                    cRespuesta.Texto = dr("Texto").ToString()
                    Exit Function
                Else

                    Dim wpAutoriza As String = ""
                    Dim Str As String = ""
                    Dim wpLic As String = ""

                    cRespuesta.Estado = False

                    Dim wpNics() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces
                    Dim wpNic As NetworkInterface

                    For Each wpNic In wpNics
                        wpLic = ""

                        wpAutoriza = wpNic.GetPhysicalAddress.ToString().Replace("-", "").Replace(":", "")
                        wpLic = prmEstacion.ToString() & wpAutoriza.ToString()
                        wpAutoriza = Metodos.Encriptar(wpLic, "AUTORIZA")

                        If wpAutoriza = dr("Autoriza").ToString Then
                            cRespuesta.Estado = True
                            Exit For
                        End If
                    Next


                    cRespuesta.Texto = "Equipo sin Autorización "
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
            cRespuesta.Resultado = Nothing
        End Try
        Return cRespuesta
    End Function

    Function ImpresioraCredencialPorEstacion(ByVal prmEstacionValidar As String, ByVal prmTipo As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try

            dato.Parametro("prmEstacionValidar") = prmEstacionValidar
            dato.Parametro("prmTipo") = prmTipo
            dato.Parametro("prmEstacion") = prmEstacion
            dato.Parametro("prmUsuario") = prmUsuario
            ds = dato.Tabla("spQryListaImpresoraPorEstacion")

            Return ds
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
    End Function

    Function ObtienePassUsuario(ByVal prmUsuarioValida As String) As DataSet
        Dim dato As New clsDato
        Dim ds As New DataSet
        Try
            dato.Parametro("prmUsuarioValida") = prmUsuarioValida
            ds = dato.Tabla("spQryListaPassUsuarios")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Public Function ActualizaUsuario(ByVal prmUsr As String, ByVal prmPass As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As String

        Dim wpdato As New clsDato
        Try
            wpdato.Parametro("@prmUsr") = prmUsr
            wpdato.Parametro("prmPass") = prmPass
            wpdato.Parametro("prmEstacion") = prmEstacion
            wpdato.Parametro("prmUsuario") = prmUsuario

            wpdato.SP(True) = "spUpdUsuario"

            Dim wpEstado As String = ""
            Dim wpTextoERR As String = ""

            While wpdato.DataReader.Read

                wpEstado = wpdato.DataReader.Item("Estado")
                wpTextoERR = wpdato.DataReader.Item("Texto")

            End While

            If wpEstado = "ER" Then
                Return wpTextoERR
            ElseIf wpEstado = "OK" Then
                Return wpEstado
            End If
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try

        Return 0

    End Function

    Public Function CargaListaUsuarios() As DataSet
        Dim ds As DataSet
        Dim dato As New clsDato
        Try
            ds = dato.Tabla("spQryListaUsuariosAlCrear")
        Catch ex As Exception
            MsgBox("Error: " + ex.Message, MsgBoxStyle.Critical, "AXC")
        End Try
        Return ds
    End Function

    Function getMacAddress() As String()
        Dim nics() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces
        Dim wpMacs As String()
        Dim wpNics As System.Net.NetworkInformation.NetworkInterface

        Dim wpCont As Integer = 0

        For Each wpNics In nics

            wpMacs(wpCont) = wpNics.GetPhysicalAddress.ToString()

            wpCont = wpCont + 1

        Next

        Return wpMacs


    End Function

    Function BloqueaUsuario(ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim Dato As New clsDato
        Dim CRespuesta As New CResultado
        Try
            ''Parametros

            Dato.Parametro("prmEstacion") = prmEstacion
            Dato.Parametro("prmUsuario") = prmUsuario
            Dato.SP(True) = "spUpdBloqueoUsuario"

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

    Friend Function ListaUsuarios(ByVal prmBusqueda As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaUsuariosB", prmBusqueda, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar Usuarios"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar Usuarios "
            Return pRespuesta
        End Try
    End Function


    Friend Function Agregar(ByVal prmUsuarioNuevo As String, ByVal prmNombre As String, prmActivo As Integer, ByVal prmContrasena As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spInsUsuario", prmUsuarioNuevo, prmNombre, prmActivo, prmContrasena, prmEstacion, prmUsuario)

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

    Friend Function Editar(ByVal prmUsuarioNuevo As String, prmActivo As Integer, ByVal prmContrasena As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdUsuario", prmUsuarioNuevo, prmActivo, prmContrasena, prmEstacion, prmUsuario)

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


#Region "METODOS DE CONFIGURACION DE GRUPOS Y PERMISOS"
    Friend Function ResultadoListaGrupos(ByVal prmBusqueda As String, prmEstacion As String, prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListadoGrupo", prmBusqueda, prmEstacion)

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
                            pRespuesta.Texto = "Error al tratar de listar Usuarios"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar Usuarios "
            Return pRespuesta
        End Try
    End Function

    Friend Function ListaUsuariosExcluidos(ByVal prmBusqueda As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaUsuariosExcluidos", prmBusqueda, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar usuarios Excluidos"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar usuarios Excluidos "
            Return pRespuesta
        End Try
    End Function

    Friend Function ListaUsuariosIncluidos(ByVal prmBusqueda As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaUsuarios", prmBusqueda, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar usuarios incluidos"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar usuarios incluidos"
            Return pRespuesta
        End Try
    End Function

    Friend Function ListaTransaccionesExcluida(ByVal prmBusqueda As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaTransaccionExcluida", prmBusqueda, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar Transacciones Excluidas"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar Transacciones excluidas "
            Return pRespuesta
        End Try
    End Function

    Friend Function ListaTransaccionesIncluidas(ByVal prmBusqueda As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim pRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim pResultado As CResultado
            Dim ds As DataSet
            Dim dt As DataTable

            pResultado = db.SPToDataSet("spQryListaTransaccion", prmBusqueda, prmEstacion, prmUsuario)

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
                            pRespuesta.Texto = "Error al tratar de listar usuarios Excluidos"
                    End Select
                Next
            Else
                pRespuesta.Estado = False
                pRespuesta.Texto = pResultado.Texto
            End If

            Return pRespuesta

        Catch ex As Exception
            pRespuesta.Estado = False
            pRespuesta.Texto = "Error al tratar de listar usuarios Excluidos "
            Return pRespuesta
        End Try
    End Function

    Friend Function DesasignarUsuarioPorGrupo(ByVal prmGrupo As String, ByVal prmUsuarioA As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdDesasociaGrupoUsuario", prmGrupo, prmUsuarioA, prmEstacion, prmUsuario)

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

    Friend Function AsignarUsuariosPorGrupo(ByVal prmGrupo As String, ByVal prmUsuarioA As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdAsociaGrupoUsuario", prmGrupo, prmUsuarioA, prmEstacion, prmUsuario)

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

    Friend Function DesasignarTransaccionPorGrupo(ByVal prmGrupo As String, ByVal prmTransaccion As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdDesasociaGrupoTransaccion", prmGrupo, prmTransaccion, prmEstacion, prmUsuario)

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

    Friend Function AsignarTransaccionPorGrupo(ByVal prmGrupo As String, ByVal prmTransaccion As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdAsociaGrupoTransaccion", prmGrupo, prmTransaccion, prmEstacion, prmUsuario)

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

    Friend Function EditarGrupo(ByVal prmId As Int64, ByVal prmGrupo As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spUpdGrupo", prmId, prmGrupo, prmEstacion, prmUsuario)

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


    Friend Function EliminarGrupo(prmId As Long, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spDelGrupo", prmId, prmEstacion, prmUsuario)

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

    Friend Function AgregarGrupo(ByVal prmGrupo As String, ByVal prmEstacion As String, ByVal prmUsuario As String) As CResultado
        Dim cRespuesta As New CResultado
        Try
            Dim db As New CAccesoDatos
            Dim dr As DataRow
            Dim CResultado As CResultado

            CResultado = db.SPToDataRow("spInsGrupo", prmGrupo, prmEstacion, prmUsuario)

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
#End Region
End Class


