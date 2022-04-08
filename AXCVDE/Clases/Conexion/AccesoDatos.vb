Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.XtraEditors

Public Class AccesoDatos

#Region "Atributos"
    Private p_conexion As SqlConnection
    Private p_strServidor As String
    Private p_Rack As String
    Private p_strBaseDatos As String
    Private p_strUsuario As String
    Private p_strPassword As String
    Private p_strConexion As String
    Private p_strDefaultConexion As String
    Private p_strQuery As String
    Private p_sps As String
    Private p_dsResult As DataSet
#End Region

#Region "Propiedades"
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

    Public Property Rack() As String
        Get
            Return p_Rack
        End Get
        Set(ByVal value As String)
            p_Rack = value
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

    Public Property StrQuery() As String
        Get
            Return p_strQuery
        End Get
        Set(ByVal value As String)
            p_strQuery = value

            Metodos.EscribirBitacora(Metodos.Bitacora.QUERY, value)
        End Set
    End Property

    Public Property Sps() As String
        Get
            Return p_sps
        End Get
        Set(ByVal value As String)
            p_sps = value

            Metodos.EscribirBitacora(Metodos.Bitacora.SPS, value)
        End Set
    End Property

    Public Property DataSetResult() As DataSet
        Get
            Return p_dsResult
        End Get
        Set(ByVal value As DataSet)
            p_dsResult = value
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Sub New()
        Try
            Conexion = New SqlConnection()
            ParametrosStringConexion()
        Catch ex As Exception
            Metodos.EscribirBitacora(Metodos.Bitacora.INCIDENCIA, ex.Message, "clsAccesoDatos.New")

            XtraMessageBox.Show("Imposible crear el Acceso a Datos | " & Err.Description, "Error de Acceso a Datos")
        End Try
    End Sub

    Private Sub ParametrosStringConexion()
        Try
            If StrServidor = "" Then
                StrServidor = Metodos.ConfigLeer("Servidor")
            End If

            If StrBaseDatos = "" Then
                StrBaseDatos = Metodos.ConfigLeer("BaseDatos")
            End If

            If StrUsuario = "" Then
                StrUsuario = Metodos.ConfigLeer("Usuario")
            End If

            If StrPassword = "" Then
                StrPassword = Metodos.ConfigLeer("Password", "sqlserver2008")
            End If
        Catch ex As Exception
            Metodos.EscribirBitacora(Metodos.Bitacora.INCIDENCIA, ex.Message, "clsAccesoDatos.ParametrosStringConexion")
        End Try
    End Sub

    Public Function Conectar() As Boolean
        Try
            If Conexion.State = ConnectionState.Open Then
                Conexion.Close()
            End If

            Conexion.ConnectionString = StrConexion '"data source=AUTOMATICATECH\SQL2008R2;persist security info=False;initial catalog=AXC;USER=sa;PWD=mcolunga;" ' 
            Conexion.Open()
            Return True
        Catch ex As Exception
            Metodos.EscribirBitacora(Metodos.Bitacora.INCIDENCIA, ex.Message, "clsAccesoDatos.Conectar")

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

    ' Regresa DataSet Nulo si no hay conexion a la Base de Datos
    ' Regresa DataSet Vacio si hubo alguno error en el Query
    Public Function QueryToDataSet(ByVal prmQuery As String) As DataSet
        Dim ds As New DataSet()
        StrQuery = prmQuery

        Try
            If prmQuery <> "" Then
                Dim sqlcmd As New SqlCommand(StrQuery)
                Dim sqlda As New SqlDataAdapter(sqlcmd)

                If Conectar() Then
                    sqlcmd.CommandType = CommandType.Text
                    sqlcmd.Connection = Conexion
                    sqlda.Fill(ds)
                    Desconectar()
                Else
                    ds = Nothing
                End If
            End If

            DataSetResult = ds
            Return ds
        Catch ex As Exception
            Metodos.EscribirBitacora(Metodos.Bitacora.INCIDENCIA, ex.Message & "|" & prmQuery, "clsAccesoDatos.QueryToDataSet")

            Return New DataSet
        End Try
    End Function

    ' Regresa un ER si no hay conexion a la Base de Datos
    ' Regresa un Vacio si hubo alguno error en el Query
    Public Function QueryToDato(ByVal prmQuery As String) As String
        Dim dato As String = ""
        StrQuery = prmQuery

        Try
            If prmQuery <> "" Then
                Dim sqlcmd As New SqlCommand(StrQuery)

                If Conectar() Then
                    sqlcmd.Connection = Conexion
                    dato = sqlcmd.ExecuteScalar().ToString()
                    Desconectar()
                Else
                    Return "ER"
                End If
            End If

            Return dato
        Catch ex As Exception
            Metodos.EscribirBitacora(Metodos.Bitacora.INCIDENCIA, ex.Message & "|" & prmQuery, "clsAccesoDatos.QueryToDato")

            Return ""
        End Try
    End Function

    ' Regresa -1 Si Hubo algun Error en el Query
    ' Regresa -2 Si fallo la conexion a la Base de Datos
    Public Function QueryEjecuta(ByVal prmQuery As String) As Integer
        Dim filasAfectadas As Integer = -1
        StrQuery = prmQuery

        Try
            If prmQuery <> "" Then
                Dim sqlcmd As New SqlCommand(StrQuery)

                If Conectar() Then
                    sqlcmd.Connection = Conexion
                    filasAfectadas = sqlcmd.ExecuteNonQuery()
                    Desconectar()
                Else
                    Return -2
                End If
            End If

            Return filasAfectadas
        Catch ex As Exception
            Metodos.EscribirBitacora(Metodos.Bitacora.INCIDENCIA, ex.Message & "|" & prmQuery, "clsAccesoDatos.QueryEjecuta")

            Return -1
        End Try
    End Function

    ' Regresa DataSet Nulo si no hay conexion a la Base de Datos
    ' Regresa DataSet Vacio si hubo alguno error en el SP
    Public Function SPToDataSet(ByVal storeProcedure As String, ByVal ParamArray parameterValues As Object()) As DataSet
        Dim ds As New DataSet()
        Sps = storeProcedure

        Try
            If storeProcedure <> "" Then
                Dim sqlcmd As New SqlCommand(storeProcedure)
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
                                'parametro.Value = IIf(parameterValues.Length >= (index + 1), parameterValues(index), DBNull.Value)

                                index = index + 1
                            End If
                        Next
                    End If

                    sqlda.Fill(ds)
                    Desconectar()
                Else
                    ds = Nothing
                End If
            End If

            DataSetResult = ds
            Return ds
        Catch ex As Exception
            Metodos.EscribirBitacora(Metodos.Bitacora.INCIDENCIA, ex.Message & "|" & storeProcedure, "clsAccesoDatos.SPToDataSet")

            Return New DataSet
        End Try
    End Function

    ' Regresa DataSet Nulo si no hay conexion a la Base de Datos
    ' Regresa DataSet Vacio si hubo alguno error en el SP
    Public Function SPToDataSet(ByVal sqlcmd As SqlCommand) As DataSet
        Dim ds As New DataSet()
        Dim sqlda As New SqlDataAdapter(sqlcmd)

        Try
            If Conectar() Then
                sqlcmd.CommandType = CommandType.StoredProcedure
                sqlcmd.Connection = Conexion
                sqlda.Fill(ds)
                Desconectar()
            Else
                ds = Nothing
            End If

            DataSetResult = ds
            Return ds
        Catch ex As Exception
            Metodos.EscribirBitacora(Metodos.Bitacora.INCIDENCIA, ex.Message, "clsAccesoDatos.SPToDataSet_Cmd")

            Return New DataSet
        End Try
    End Function

    ' Regresa -1 Si Hubo algun Error en el SP
    ' Regresa -2 Si fallo la conexion a la Base de Datos
    Public Function SPInsert(ByVal storeProcedure As String, ByVal ParamArray parameterValues As Object()) As Integer
        Dim id As Integer = -1
        Sps = storeProcedure

        Try
            If storeProcedure <> "" Then
                Dim sqlcmd As New SqlCommand(storeProcedure)

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

                    id = sqlcmd.ExecuteScalar()
                    Desconectar()
                Else
                    Return -2
                End If
            End If

            Return id
        Catch ex As Exception
            Metodos.EscribirBitacora(Metodos.Bitacora.INCIDENCIA, ex.Message & "|" & storeProcedure, "clsAccesoDatos.SPInsert")

            Return -1
        End Try
    End Function

    ' Regresa -1 Si Hubo algun Error en el SP
    ' Regresa -2 Si fallo la conexion a la Base de Datos
    Public Function SPInsert(ByVal sqlcmd As SqlCommand) As Integer
        Dim id As Integer = -1

        Try
            If Conectar() Then
                sqlcmd.CommandType = CommandType.StoredProcedure
                sqlcmd.Connection = Conexion
                id = sqlcmd.ExecuteScalar()
                Desconectar()
            Else
                Return -2
            End If

            Return id
        Catch ex As Exception
            Metodos.EscribirBitacora(Metodos.Bitacora.INCIDENCIA, ex.Message, "clsAccesoDatos.SPInsert_Com")

            Return -1
        End Try
    End Function

    ' Regresa -1 Si Hubo algun Error en el SP
    ' Regresa -2 Si fallo la conexion a la Base de Datos
    Public Function SPEjecuta(ByVal storeProcedure As String, ByVal ParamArray parameterValues As Object()) As Integer
        Dim filasAfectadas As Integer = -1
        Sps = storeProcedure

        Try
            If storeProcedure <> "" Then
                Dim sqlcmd As New SqlCommand(storeProcedure)

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

                    filasAfectadas = sqlcmd.ExecuteNonQuery()
                    Desconectar()
                Else
                    Return -2
                End If
            End If

            Return filasAfectadas
        Catch ex As Exception
            Metodos.EscribirBitacora(Metodos.Bitacora.INCIDENCIA, ex.Message & "|" & storeProcedure, "clsAccesoDatos.SPEjecuta")

            Return -1
        End Try
    End Function

    ' Regresa -1 Si Hubo algun Error en el SP
    ' Regresa -2 Si fallo la conexion a la Base de Datos
    Public Function SPEjecuta(ByVal sqlcmd As SqlCommand) As Integer
        Dim filasAfectadas As Integer = -1

        Try
            If Conectar() Then
                sqlcmd.CommandType = CommandType.StoredProcedure
                sqlcmd.Connection = Conexion
                filasAfectadas = sqlcmd.ExecuteNonQuery()
                Desconectar()
            Else
                Return -2
            End If

            Return filasAfectadas
        Catch ex As Exception
            Metodos.EscribirBitacora(Metodos.Bitacora.INCIDENCIA, ex.Message, "clsAccesoDatos.SPEjecuta")

            Return -1
        End Try
    End Function

    ' Regresa -1 Si Hubo algun Error en el SP
    ' Regresa -2 Si fallo la conexion a la Base de Datos
    Public Function SPToDato(ByVal storeProcedure As String, ByVal ParamArray parameterValues As Object()) As String
        Dim pDato As String = "-1"
        Sps = storeProcedure

        Try
            If storeProcedure <> "" Then
                Dim sqlcmd As New SqlCommand(storeProcedure)

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
                Else
                    Return "-2"
                End If
            End If

            Return pDato
        Catch ex As Exception
            Metodos.EscribirBitacora(Metodos.Bitacora.INCIDENCIA, ex.Message & "|" & storeProcedure, "clsAccesoDatos.SPInsert")

            Return "-1"
        End Try
    End Function

    ' Regresa Coleccion Nula si no hay conexion a la Base de Datos
    ' Regresa Coleccion Vacio si hubo alguno error en el SP
    Public Function SPToOutputs(ByVal storeProcedure As String, ByVal ParamArray parameterValues As Object()) As Collection
        Dim outPutsColleccion As New Collection
        Sps = storeProcedure

        Try
            If storeProcedure <> "" Then
                Dim sqlcmd As New SqlCommand(storeProcedure)
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
                                    parametro.Value = parameterValues(index)
                                Else
                                    parametro.Value = DBNull.Value
                                End If

                                index = index + 1
                            End If
                        Next
                    End If

                    sqlcmd.ExecuteReader()

                    If Not (sqlcmd.Parameters Is Nothing) And sqlcmd.Parameters.Count > 0 Then
                        For Each parametro As SqlParameter In sqlcmd.Parameters
                            If parametro.Direction = ParameterDirection.InputOutput Then
                                outPutsColleccion.Add(parametro.Value, parametro.ParameterName)
                            End If
                        Next
                    End If

                    Desconectar()
                Else
                    outPutsColleccion = Nothing
                End If
            End If

            Return outPutsColleccion
        Catch ex As Exception
            Metodos.EscribirBitacora(Metodos.Bitacora.INCIDENCIA, ex.Message & "|" & storeProcedure, "clsAccesoDatos.SPToOutputs")

            Return New Collection
        End Try
    End Function
#End Region
End Class
