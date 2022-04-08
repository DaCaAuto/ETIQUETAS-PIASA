Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient


Public Class clsDato

    Private pCnn As SqlConnection
    Private pSP As SqlCommand
    Private PDR As SqlDataReader
    Private pDS As DataSet
    Private pTbl As DataSet
    Private pCadCon As String


    Public Event DatosRecibidos(ByVal prmSP As String, ByVal prmDataReader As SqlDataReader, ByVal prmTabla As DataTable)

    Public Sub New()
        Dim StrServidor As String
        Dim StrBaseDatos As String
        Dim StrUsuario As String
        Dim StrPassword As String
        StrServidor = CMetodos.ConfigLeer("Servidor")
        StrBaseDatos = CMetodos.ConfigLeer("BaseDatos")
        StrUsuario = CMetodos.ConfigLeer("Usuario")
        StrPassword = CMetodos.ConfigLeer("Password", "sqlserver2008")

        pCadCon = String.Format("data source={0};persist security info=False;initial catalog={1};USER={2};PWD={3};", StrServidor, StrBaseDatos, StrUsuario, StrPassword)
        pCnn = New SqlConnection
        pCnn.ConnectionString = pCadCon

    End Sub
    Public Sub New(ByVal prmServidor As String, ByVal prmUsr As String, ByVal prmPass As String, ByVal prmBD As String)
        pCadCon = String.Format("data source={0};persist security info=False;initial catalog={1};USER={2};PWD={3};", prmServidor, prmBD, prmUsr, prmPass)

        pCnn = New SqlConnection
        pCnn.ConnectionString = pCadCon

    End Sub

    Public ReadOnly Property Tabla(ByVal prmSP As String) As DataSet
        Get
            Dim wpDA As New SqlDataAdapter

            If IsNothing(pSP) Then
                pSP = New SqlCommand
            End If

            pSP.CommandType = CommandType.StoredProcedure
            pSP.CommandText = prmSP
            pSP.Connection = pCnn

            If pCnn.State <> ConnectionState.Open Then
                pCnn.Open()
            End If

            wpDA.SelectCommand = pSP
            Dim wp As Integer
            pTbl = New DataSet

            wp = wpDA.Fill(pTbl)

            Tabla = pTbl

            If pCnn.State <> ConnectionState.Closed Then
                pCnn.Close()
            End If

        End Get

    End Property


    Public Property SPTablas() As String
        Get
            SPTablas = pSP.CommandText
        End Get
        Set(ByVal value As String)


            If pCnn.State <> ConnectionState.Open Then
                pCnn.Open()
            End If

            If IsNothing(pSP) Then
                pSP = New SqlCommand
            End If

            pDS = New DataSet
            pSP.CommandType = CommandType.StoredProcedure
            pSP.Connection = pCnn
            pSP.CommandText = value


            Dim wpAdapter As New SqlDataAdapter(pSP)

            wpAdapter.Fill(pDS)

            pSP.Parameters.Clear()

            Try
                pCnn.Close()
            Catch ex As Exception
                Debug.Print(ex.Message)
            End Try



        End Set
    End Property

    Public Function Tablas(prmTabla As Integer) As DataTableReader

        Dim wpDTR As DataTableReader

        wpDTR = pDS.Tables(prmTabla).CreateDataReader

        Tablas = wpDTR

    End Function

    Public Function Tablas(prmTabla As Short) As DataTable

        Dim wpDTR As DataTable

        wpDTR = pDS.Tables(prmTabla)

        Tablas = wpDTR


    End Function


    Public ReadOnly Property DataReader() As SqlDataReader
        Get
            DataReader = PDR
        End Get
    End Property

    Public Property Parametro(ByVal prmParametro As String) As String
        Get
            Parametro = ""

            If IsNothing(pSP) Then
                pSP = New SqlCommand
            End If

            If pSP.Parameters.Contains(prmParametro) Then
                Parametro = pSP.Parameters(prmParametro).Value
            End If

        End Get

        Set(ByVal value As String)

            If IsNothing(pSP) Then
                pSP = New SqlCommand
            End If

            pSP.Connection = pCnn

            If Not pSP.Parameters.Contains(prmParametro) Then
                pSP.Parameters.AddWithValue(prmParametro, value)
            Else
                pSP.Parameters(prmParametro).Value = value
            End If
        End Set

    End Property

    Public Property SP(ByVal prmValor As Boolean) As String
        Get
            SP = pSP.CommandText
        End Get
        Set(ByVal value As String)

            If pCnn.State <> ConnectionState.Open Then
                pCnn.Open()
            End If

            If IsNothing(pSP) Then
                pSP = New SqlCommand
            End If

            pSP.CommandType = CommandType.StoredProcedure
            pSP.Connection = pCnn
            pSP.CommandText = value

            If prmValor Then
                PDR = pSP.ExecuteReader()
            Else
                pSP.ExecuteNonQuery()
            End If

            pSP.Parameters.Clear()

        End Set
    End Property

    Public Sub Cerrar()
        Try
            pSP.Connection.Close()
            pSP.Dispose()
        Catch ex As Exception

        End Try

        Try
            PDR.Close()

        Catch ex As Exception

        End Try

        Try
            pDS.Dispose()
        Catch ex As Exception

        End Try
        Try
            pCnn.Close()
            pCnn.Dispose()
        Catch ex As Exception

        End Try

    End Sub

    Public Function PruebaConexion() As Boolean

        PruebaConexion = False

        Try
            pCnn.Open()
            pCnn.Close()
            PruebaConexion = True

        Catch ex As Exception

        End Try

    End Function

    Protected Overrides Sub Finalize()

        Try

            If pCnn.State <> ConnectionState.Closed Then
                pCnn.Close()
            End If

            pCnn.Dispose()
            PDR = Nothing
            pSP.Dispose()
            pTbl.Dispose()

            MyBase.Finalize()
        Catch ex As Exception

            Try

                pCnn.Dispose()
                PDR = Nothing
                pSP.Dispose()
                pTbl.Dispose()

            Catch ex1 As Exception

            End Try

        End Try
    End Sub
End Class
