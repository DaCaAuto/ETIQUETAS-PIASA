Imports System.Data

<Serializable()> _
Public Class CResultado

    Private pEstado As Boolean
    Public Property Estado() As Boolean
        Get
            Return pEstado
        End Get
        Set(ByVal value As Boolean)
            pEstado = value
        End Set
    End Property

    Private pTexto As String
    Public Property Texto() As String
        Get
            Return pTexto
        End Get
        Set(ByVal value As String)
            pTexto = value
        End Set
    End Property

    Private pResultado As Object
    Public Property Resultado() As Object
        Get
            Return pResultado
        End Get
        Set(ByVal value As Object)
            pResultado = value
        End Set
    End Property

    Private pTabla As DataTable
    Public Property Tabla() As DataTable
        Get
            Return pTabla
        End Get
        Set(ByVal value As DataTable)
            pTabla = value
        End Set
    End Property

    Private pTablas As DataSet
    Public Property Tablas() As DataSet
        Get
            Return pTablas
        End Get
        Set(ByVal value As DataSet)
            pTablas = value
        End Set
    End Property

    Sub New()
    End Sub
End Class
