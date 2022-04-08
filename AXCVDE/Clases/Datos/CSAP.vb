Imports SAP.Middleware.Connector
Public Class CSAP
    Dim SapParametros As New SAP.Middleware.Connector.RfcConfigParameters
    Dim RfcConfigParameters As New SAP.Middleware.Connector.RfcConfigParameters

    Public Function ConsultarSap(prmMaterial As String) As IRfcFunction
        'Configuración de parámetros
        Dim sapParametros As New RfcConfigParameters
        sapParametros(RfcConfigParameters.User) = CMetodos.ConfigLeer("SAPuser")
        sapParametros(RfcConfigParameters.Password) = CMetodos.ConfigLeer("SAPpass")
        sapParametros(RfcConfigParameters.SystemID) = CMetodos.ConfigLeer("SAPsystemId")
        sapParametros(RfcConfigParameters.Client) = CMetodos.ConfigLeer("SAPclient")
        sapParametros(RfcConfigParameters.Name) = CMetodos.ConfigLeer("SAPname")
        sapParametros(RfcConfigParameters.AppServerHost) = CMetodos.ConfigLeer("SAPhost")
        sapParametros(RfcConfigParameters.SystemNumber) = CMetodos.ConfigLeer("SAPsystemNumber")
        sapParametros(RfcConfigParameters.Language) = "ES"

        Try
            'Conexión a SAP
            Dim destino As RfcDestination
            destino = RfcDestinationManager.GetDestination(sapParametros)

            destino.Ping()

            'Función para ejecutar la BAPI
            Dim funcion As IRfcFunction = destino.Repository.CreateFunction("ZAXC_MATERIAL_LABEL")
            'Parámetros de entrada a enviar
            funcion.SetValue("MATNR", prmMaterial)
            funcion.SetValue("LANGU", "S")

            'Ejecutar la BAPI
            Dim ejecuta As New RfcTransaction
            funcion.Invoke(destino)
            ejecuta.AddFunction(funcion)
            ejecuta.Commit(destino)

            Return funcion
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
    End Function

    Private Function sapToDt(sapTabla As IRfcTable)
        Dim dt As New DataTable
        Dim metadata As RfcElementMetadata
        'Dim dr As DataRow
        For iElement As Integer = 0 To iElement < sapTabla.ElementCount
            metadata = sapTabla.GetElementMetadata(iElement)
            dt.Columns.Add(metadata.Name, metadata.DataType.GetType)
            MessageBox.Show(metadata.Name.ToString + " -- " + metadata.DataType.GetType.ToString)
            iElement = iElement + 1
        Next


        'For Each row As IRfcStructure In sapTabla
        'dr = dt.NewRow
        'For iElement As Integer = 0 To iElement < sapTabla.ElementCount

        'Next
        'Next

        Return "OK"
    End Function
End Class
