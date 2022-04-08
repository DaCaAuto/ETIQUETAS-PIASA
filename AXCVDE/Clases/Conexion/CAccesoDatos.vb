Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Configuration
Imports System.Text
Imports System.Security
Imports DevExpress.XtraEditors

Public Class CAccesoDatos

#Region "ATRIBUTOS"
    Private p_conexion As SqlConnection
    Private p_strServidor As String
    Private p_strBaseDatos As String
    Private p_strUsuario As String
    Private p_strPassword As String
    Private p_strConexion As String
    Private p_strDefaultConexion As String
    Private p_sps As String
#End Region

#Region "PROPIEDADES"

    Public Property Conexion() As SqlConnection
        Get
            Return p_conexion
        End Get
        Set(ByVal value As SqlConnection)
            p_conexion = value
        End Set
    End Property

    Public Property StrServidor() As String
        Get
            Return p_strServidor
        End Get
        Set(ByVal value As String)
            p_strServidor = value
        End Set
    End Property

    Public Property StrBaseDatos() As String
        Get
            Return p_strBaseDatos
        End Get
        Set(ByVal value As String)
            p_strBaseDatos = value
        End Set
    End Property

    Public Property StrUsuario() As String
        Get
            Return p_strUsuario
        End Get
        Set(ByVal value As String)
            p_strUsuario = value
        End Set
    End Property

    Public Property StrPassword() As String
        Private Get
            Return p_strPassword
        End Get
        Set(ByVal value As String)
            p_strPassword = value
        End Set
    End Property

    Public Property StrConexion() As String
        Get
            If p_strConexion Is Nothing Then
                Return StrDefaultConexion
            End If
            Return p_strConexion
        End Get
        Set(ByVal value As String)
            If value <> "" Then
                p_strConexion = value
                StrServidor = ""
                StrBaseDatos = ""
                StrUsuario = ""
                StrPassword = ""
            Else
                p_strConexion = Nothing
                ParametrosStringConexion()
            End If
        End Set
    End Property

    Private ReadOnly Property StrDefaultConexion() As String
        Get
            Return String.Format("data source={0};persist security info=False;initial catalog={1};USER={2};PWD={3};", StrServidor, StrBaseDatos, StrUsuario, StrPassword)
        End Get
    End Property

    Public Property Sps() As String
        Get
            Return p_sps
        End Get
        Set(ByVal value As String)
            p_sps = value

            'CMetodos.EscribirBitacora(CMetodos.Bitacora.SPS, value)
        End Set
    End Property

#End Region

#Region "METODOS"

    Public Sub New()
        Try
            Conexion = New SqlConnection()
            ParametrosStringConexion()
        Catch ex As Exception
            'CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex.Message, "AccesoDatos.New")

            XtraMessageBox.Show("Imposible crear el Acceso a Datos | " & Err.Description, "Error de Acceso a Datos")
        End Try
    End Sub

    Private Sub ParametrosStringConexion()
        Try
            If StrServidor = "" Then
                StrServidor = CMetodos.ConfigLeer("Servidor")
            End If

            If StrBaseDatos = "" Then
                StrBaseDatos = CMetodos.ConfigLeer("BaseDatos")
            End If

            If StrUsuario = "" Then
                StrUsuario = CMetodos.ConfigLeer("Usuario")
            End If

            If StrPassword = "" Then
                StrPassword = CMetodos.ConfigLeer("Password", "sqlserver2008")
            End If
        Catch ex As Exception
            'CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex.Message, "AccesoDatos.ParametrosStringConexion")
        End Try
    End Sub

    Public Function Conectar() As Boolean
        Try
            If Conexion.State = ConnectionState.Open Then
                Conexion.Close()
            End If
            Conexion.ConnectionString = StrConexion '"data source=DESKTOP-0V81H8M;persist security info=False;initial catalog=AXCVDE;USER=sa;PWD=Prueba1;"
            Conexion.Open()
            Return True
        Catch ex As Exception
            'CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex.Message, "AccesoDatos.Conectar")

            Return False
        End Try
    End Function

    Public Sub Desconectar()
        If Conexion.State <> ConnectionState.Closed Then
            Conexion.Close()
        End If
    End Sub

    Public Function StatusConexion() As ConnectionState
        Return Conexion.State
    End Function

    Public Function SPToDataSet(ByVal prmStoreProcedure As String, ByVal ParamArray parameterValues As Object()) As CResultado
        Dim pResultado As New CResultado
        Dim ds As New DataSet()
        Sps = prmStoreProcedure

        Try
            If prmStoreProcedure <> "" Then
                Dim sqlcmd As New SqlCommand(prmStoreProcedure)
                Dim sqlda As New SqlDataAdapter(sqlcmd)

                If Conectar() Then
                    sqlcmd.CommandType = CommandType.StoredProcedure
                    sqlcmd.Connection = Conexion

                    ' Obtener Informacion de Parametros y Asignar Valores
                    SqlCommandBuilder.DeriveParameters(sqlcmd)
                    If Not (sqlcmd.Parameters Is Nothing) And sqlcmd.Parameters.Count > 0 Then
                        Dim index As Integer = 0

                        For Each parametro As SqlParameter In sqlcmd.Parameters
                            If parametro.Direction = ParameterDirection.Input Or parametro.Direction = ParameterDirection.InputOutput Then
                                If parameterValues.Length >= index + 1 Then
                                    parametro.Value = IIf(parameterValues(index) Is Nothing, DBNull.Value, parameterValues(index))
                                Else
                                    parametro.Value = DBNull.Value
                                End If

                                index = index + 1
                            End If
                        Next
                    End If

                    sqlda.Fill(ds)

                    Desconectar()

                    pResultado.Estado = True
                    pResultado.Texto = ""
                    pResultado.Resultado = ds
                Else
                    pResultado.Estado = False
                    pResultado.Texto = "Error de Conexion al Origen de Datos"
                    pResultado.Resultado = Nothing
                End If
            End If

            Return pResultado
        Catch ex As Exception
            'CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, "[" & prmStoreProcedure & "] " & ex.Message, "AccesoDatos.SPToDataSet")

            Desconectar()

            pResultado.Estado = False
            pResultado.Texto = "Error en la ejecución de Procedimiento, " & ex.Message
            pResultado.Resultado = Nothing

            Return pResultado
        End Try
    End Function

    Public Function SPToDataSet(ByVal prmSqlcmd As SqlCommand) As CResultado
        Dim pResultado As New CResultado
        Dim ds As New DataSet()
        Dim sqlda As New SqlDataAdapter(prmSqlcmd)

        Try
            If Conectar() Then
                prmSqlcmd.CommandType = CommandType.StoredProcedure
                prmSqlcmd.Connection = Conexion
                sqlda.Fill(ds)
                Desconectar()

                pResultado.Estado = True
                pResultado.Texto = ""
                pResultado.Resultado = ds
            Else
                pResultado.Estado = False
                pResultado.Texto = "Error de Conexion al Origen de Datos"
                pResultado.Resultado = ds
            End If

            Return pResultado
        Catch ex As Exception
            'CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex.Message, "AccesoDatos.SPToDataSet_Cmd")

            Desconectar()

            pResultado.Estado = False
            pResultado.Texto = "Error en la ejecución de Procedimiento, " & ex.Message
            pResultado.Resultado = New DataSet

            Return pResultado
        End Try
    End Function

    Public Function SPToDataTable(ByVal prmStoreProcedure As String, ByVal ParamArray parameterValues As Object()) As CResultado
        Dim pResultado As New CResultado
        Dim dt As New DataTable()
        Sps = prmStoreProcedure

        Try
            If prmStoreProcedure <> "" Then
                Dim sqlcmd As New SqlCommand(prmStoreProcedure)
                Dim sqlda As New SqlDataAdapter(sqlcmd)

                If Conectar() Then
                    sqlcmd.CommandType = CommandType.StoredProcedure
                    sqlcmd.Connection = Conexion

                    ' Obtener Informacion de Parametros y Asignar Valores
                    SqlCommandBuilder.DeriveParameters(sqlcmd)
                    If Not (sqlcmd.Parameters Is Nothing) And sqlcmd.Parameters.Count > 0 Then
                        Dim index As Integer = 0

                        For Each parametro As SqlParameter In sqlcmd.Parameters
                            If parametro.Direction = ParameterDirection.Input Or parametro.Direction = ParameterDirection.InputOutput Then
                                If parameterValues.Length >= index + 1 Then
                                    parametro.Value = IIf(parameterValues(index) Is Nothing, DBNull.Value, parameterValues(index))
                                Else
                                    parametro.Value = DBNull.Value
                                End If

                                index = index + 1
                            End If
                        Next
                    End If

                    sqlda.Fill(dt)

                    Desconectar()

                    pResultado.Estado = True
                    pResultado.Texto = ""
                    pResultado.Resultado = dt
                Else
                    pResultado.Estado = False
                    pResultado.Texto = "Error de Conexion al Origen de Datos"
                    pResultado.Resultado = Nothing
                End If
            End If

            Return pResultado
        Catch ex As Exception
            'CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, "[" & prmStoreProcedure & "] " & ex.Message, "AccesoDatos.SPToDataSet")

            Desconectar()

            pResultado.Estado = False
            pResultado.Texto = "Error en la ejecución de Procedimiento, " & ex.Message
            pResultado.Resultado = Nothing

            Return pResultado
        End Try
    End Function

    Public Function SPToDataTable(ByVal prmSqlcmd As SqlCommand) As CResultado
        Dim pResultado As New CResultado
        Dim dt As New DataTable()
        Dim sqlda As New SqlDataAdapter(prmSqlcmd)

        Try
            If Conectar() Then
                prmSqlcmd.CommandType = CommandType.StoredProcedure
                prmSqlcmd.Connection = Conexion
                sqlda.Fill(dt)
                Desconectar()

                pResultado.Estado = True
                pResultado.Texto = ""
                pResultado.Resultado = dt
            Else
                pResultado.Estado = False
                pResultado.Texto = "Error de Conexion al Origen de Datos"
                pResultado.Resultado = dt
            End If

            Return pResultado
        Catch ex As Exception
            'CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, ex.Message, "AccesoDatos.SPToDataTable_Cmd")

            Desconectar()

            pResultado.Estado = False
            pResultado.Texto = "Error en la ejecución de Procedimiento, " & ex.Message
            pResultado.Resultado = New DataSet

            Return pResultado
        End Try
    End Function

    Public Function SPToDato(ByVal prmStoreProcedure As String, ByVal ParamArray parameterValues As Object()) As CResultado
        Dim pResultado As New CResultado
        Dim pDato As String = "-1"
        Sps = prmStoreProcedure

        Try
            If prmStoreProcedure <> "" Then
                Dim sqlcmd As New SqlCommand(prmStoreProcedure)

                If Conectar() Then
                    sqlcmd.CommandType = CommandType.StoredProcedure
                    sqlcmd.Connection = Conexion

                    ' Obtener Informacion de Parametros y Asignar Valores
                    SqlCommandBuilder.DeriveParameters(sqlcmd)
                    If Not (sqlcmd.Parameters Is Nothing) And sqlcmd.Parameters.Count > 0 Then
                        Dim index As Integer = 0
                        For Each parametro As SqlParameter In sqlcmd.Parameters
                            If parametro.Direction = ParameterDirection.Input Or parametro.Direction = ParameterDirection.InputOutput Then
                                If parameterValues.Length >= index + 1 Then
                                    parametro.Value = IIf(parameterValues(index) Is Nothing, DBNull.Value, parameterValues(index))
                                Else
                                    parametro.Value = DBNull.Value
                                End If

                                index = index + 1
                            End If
                        Next
                    End If

                    pDato = sqlcmd.ExecuteScalar()
                    Desconectar()

                    pResultado.Estado = True
                    pResultado.Texto = ""
                    pResultado.Resultado = pDato
                Else
                    pResultado.Estado = False
                    pResultado.Texto = "Error de Conexion al Origen de Datos"
                    pResultado.Resultado = -2
                End If
            End If

            Return pResultado
        Catch ex As Exception
            'CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, "[" & prmStoreProcedure & "] " & ex.Message, "AccesoDatos.SPToDato")

            Desconectar()

            pResultado.Estado = False
            pResultado.Texto = "Error en la ejecución de Procedimiento, " & ex.Message
            pResultado.Resultado = -1

            Return pResultado
        End Try
    End Function

    Public Function SPToDataRow(ByVal prmStoreProcedure As String, ByVal ParamArray parameterValues As Object()) As CResultado
        Dim pResultado As New CResultado
        Dim dr As DataRow = Nothing
        Sps = prmStoreProcedure

        Try
            If prmStoreProcedure <> "" Then
                Dim sqlcmd As New SqlCommand(prmStoreProcedure)
                Dim sqlda As New SqlDataAdapter(sqlcmd)

                If Conectar() Then
                    sqlcmd.CommandType = CommandType.StoredProcedure
                    sqlcmd.Connection = Conexion

                    ' Obtener Informacion de Parametros y Asignar Valores
                    SqlCommandBuilder.DeriveParameters(sqlcmd)
                    If Not (sqlcmd.Parameters Is Nothing) And sqlcmd.Parameters.Count > 0 Then
                        Dim index As Integer = 0
                        For Each parametro As SqlParameter In sqlcmd.Parameters
                            If parametro.Direction = ParameterDirection.Input Or parametro.Direction = ParameterDirection.InputOutput Then
                                If parameterValues.Length >= index + 1 Then
                                    parametro.Value = IIf(parameterValues(index) Is Nothing, DBNull.Value, parameterValues(index))
                                Else
                                    parametro.Value = DBNull.Value
                                End If

                                index = index + 1
                            End If
                        Next
                    End If

                    Dim dt As New DataTable
                    sqlda.Fill(dt)

                    If Not dt Is Nothing Then
                        If dt.Rows.Count > 0 Then
                            dr = dt.Rows(0)
                        End If
                    End If

                    Desconectar()

                    pResultado.Estado = True
                    pResultado.Texto = ""
                    pResultado.Resultado = dr
                Else
                    pResultado.Estado = False
                    pResultado.Texto = "Error de Conexion al Origen de Datos"
                    pResultado.Resultado = dr
                End If
            End If

            Return pResultado
        Catch ex As Exception
            'CMetodos.EscribirBitacora(CMetodos.Bitacora.INCIDENCIA, "[" & prmStoreProcedure & "] " & ex.Message, "AccesoDatos.SPToDataRow")

            Desconectar()

            pResultado.Estado = False
            pResultado.Texto = "Error en la ejecución de Procedimiento, " & ex.Message
            pResultado.Resultado = Nothing

            Return pResultado
        End Try
    End Function

#End Region

End Class
