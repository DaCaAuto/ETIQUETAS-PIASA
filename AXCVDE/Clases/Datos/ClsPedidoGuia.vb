Public Class ClsPedidoGuia

    Private _pPedido As String
    Public Property pPedido() As String
        Get
            Return _pPedido
        End Get
        Set(ByVal value As String)
            _pPedido = value
        End Set
    End Property
End Class
