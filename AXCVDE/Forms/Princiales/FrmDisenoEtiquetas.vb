Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports DevExpress.XtraEditors
Imports SAP.Middleware.Connector

Public Class DisenoEtiqueta
#Region "Eventos"

    ' Evento para el botón de abrir un nuevo archivo
    Private Sub btnSeleccionarArch_Click(sender As Object, e As EventArgs) Handles btnSeleccionarArch.Click
        Dim input As String
        abrirArchDialog.Title = "Por favor seleccione un archivo"
        abrirArchDialog.Filter = "Text File|*.prn"
        If abrirArchDialog.ShowDialog = DialogResult.OK Then
            input = File.ReadAllText(abrirArchDialog.FileName)
            ' Remplazamos textos largos agregando FIELD BLOCK
            input = input.Replace("^FD<INGREDIENTES", "^FB900,10,2,J^FD<INGREDIENTES")
            input = input.Replace("^FD<ALMACENAJE1/>", "^FB400,3^FD<ALMACENAJE1/>")
            txtCodigo.Text = input
            ' Cargamos la imagen en el componente imageEdit
            cargarImagen(input)
        End If
    End Sub

    Private Sub btnMostrar_Click(sender As Object, e As EventArgs) Handles btnMostrar.Click
        Dim input As String = txtCodigo.Text

        ' Validaciones
        If validar() = False Then
            Return
        End If

        'Acceso a datos
        Dim pResultado As CSAP = New CSAP
        Dim pEtiqueta As New CResultado
        Dim Cls As New CEtiqueta
        Dim dt As New DataTable

        'Variables para llamar el sp
        Dim material = txtMaterial.Text.PadLeft(18, "0")
        Dim lote = txtLote.Text

        ' Patrón para hacer el reemplazo de los datos
        Dim patron As String = "<(?<TEXT>[^<]*)/>"


        'Verificamos que traiga datos
        Try
            pEtiqueta = Cls.ConsultaInfoEtiqueta(material, lote)
            If (pEtiqueta.Estado <> True) Then
                XtraMessageBox.Show(pEtiqueta.Texto, "AXC")
                Return
            End If

            dt = pEtiqueta.Tabla
        Catch ex As Exception
            XtraMessageBox.Show(pEtiqueta.Texto, "AXC")
            Return
        End Try

        '' Conexión a SAP
        Dim funcion As IRfcFunction = pResultado.ConsultarSap(material)
        'MessageBox.Show(funcion.ToString)

        Dim est_RETURN As IRfcStructure = funcion.GetStructure("ZLBL_MARA") 'Obtención de los campos de ingredientes 
        Dim ing1_RETURN As IRfcTable = funcion.GetTable("INGREDIENTE1")
        Dim ing2_RETURN As IRfcTable = funcion.GetTable("INGREDIENTE2")
        Dim ing3_RETURN As IRfcTable = funcion.GetTable("INGREDIENTE3")

        Dim ing1_EN_RETURN As IRfcTable = funcion.GetTable("INGREDIENTE1_ING")
        Dim ing2_EN_RETURN As IRfcTable = funcion.GetTable("INGREDIENTE2_ING")
        Dim ing3_EN_RETURN As IRfcTable = funcion.GetTable("INGREDIENTE3_ING")

        Dim ingrediente1 As String = ""
        Dim ingrediente2 As String = ""
        Dim ingrediente3 As String = ""

        For Each row As IRfcStructure In ing1_RETURN
            ingrediente1 = ingrediente1 + row.GetString(1)
        Next

        For Each row As IRfcStructure In ing2_RETURN
            ingrediente2 = ingrediente2 + row.GetString(1)
        Next

        For Each row As IRfcStructure In ing3_RETURN
            ingrediente3 = ingrediente3 + row.GetString(1)
        Next

        ' Reemplazos iniciales
        input = input.Replace("<INGREDIENTES1/>", ingrediente1)
        input = input.Replace("<INGREDIENTES2/>", ingrediente2)
        input = input.Replace("<INGREDIENTES3/>", ingrediente3)

        input = input.Replace("<FECHAELABORACION/>", Now.ToString("dd/MM/yyyy"))
        input = input.Replace("<FECHACADUCIDAD/>", DateAdd(DateInterval.Day, dt.Rows(0).Item("DiasCaducidad"), Now).ToString("dd/MM/yyyy"))
        input = input.Replace("<PESO/>", dt.Rows(0).Item("Peso").ToString + " " + dt.Rows(0).Item("UMPeso").ToString)
        input = input.Replace("<CODIGO_SAP/>", txtMaterial.Text)
        input = input.Replace("<i/>", "1")

        'Reemplazo de datos en SAP
        For Each match As Match In Regex.Matches(input, patron)
            Try
                input = input.Replace("<" + match.Groups("TEXT").Value + "/>", est_RETURN(match.Groups("TEXT").Value).GetValue.ToString)
            Catch ex As Exception
                'MessageBox.Show("No se pudo realizar el intercambio de los datos con SAP: " + ex.ToString)
            End Try
        Next

        'Reemplazo de datos en AXC
        For Each row As DataRow In dt.Rows
            For Each match As Match In Regex.Matches(input, patron)
                Try

                    input = input.Replace("<" + match.Groups("TEXT").Value + "/>", row.Item(match.Groups("TEXT").Value).ToString)
                Catch ex As Exception

                End Try
            Next
        Next

        'Generación del código de barras
        Dim codigoBarras = generarCodigoBarras(est_RETURN("CODIGO_CLIENTE").GetValue.ToString,
                                               dt.Rows(0).Item("LOTE").ToString,
                                               est_RETURN("PESO2_CLIENTE").GetValue.ToString,
                                               dt.Rows(0).Item("FechaRegistro"),
                                               "1")

        input = input.Replace("<CODIGOBARRAS/>", codigoBarras)

        cargarImagen(input)
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        ' Validaciones
        If validar() = False Then
            Return
        End If

        If txtNombre.Text = "" Then
            XtraMessageBox.Show("Ingrese un nombre para la etiqueta", "AXC")
            Return
        End If

        Dim pResultado As New CResultado
        Dim cEtiqueta As New CEtiqueta

        Try
            pResultado = cEtiqueta.AgregarEtiqueta(txtCodigo.Text, txtNombre.Text, My.Settings.Estacion, DatosTemporales.Usuario)

            If Not pResultado.Estado Then
                XtraMessageBox.Show(pResultado.Texto, "AXC")
                Return
            End If

            XtraMessageBox.Show("Etiqueta registrada", "AXC")
            txtCodigo.Text = ""
            txtMaterial.Text = ""
            txtLote.Text = ""
            txtNombre.Text = ""

        Catch ex As Exception
            XtraMessageBox.Show(ex.Message, "AXC", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#End Region

#Region "Métodos"
    Private Sub cargarImagen(ByVal input As String)
        Try
            Dim client As New WebClient()
            Dim url As String = "http://api.labelary.com/v1/printers/12dpmm/labels/5x4/0/" + input
            Using client
                imgEtiqueta.Image = Image.FromStream(client.OpenRead(url))
            End Using
        Catch ex As Exception
            MsgBox("Error en el formato de la etiqueta")
        End Try
    End Sub

    Private Function validar() As Boolean
        Dim inicioEtq, finEtq

        inicioEtq = InStr(1, txtCodigo.Text, "^XA", 0)
        finEtq = InStr(1, txtCodigo.Text, "^XZ", 0)

        If inicioEtq = 0 Or finEtq = 0 Then
            XtraMessageBox.Show("No se reconoció el formato de etiqueta [ZPL])", "AXC")
            Return False
        End If

        If txtCodigo.Text = "" Then
            XtraMessageBox.Show("Seleccione una etiqueta", "AXC")
            Return False
        End If

        If (txtLote.Text = "" Or txtMaterial.Text = "") Then
            XtraMessageBox.Show("Los campos de PRODUCTO y LOTE no pueden estar vacios", "AXC")
            Return False
        End If

        Return True
    End Function

    Private Function generarCodigoBarras(ByVal prmProducto As String, ByVal prmLote As String, ByVal prmPiezas As String, ByVal prmFecha As Date, ByVal prmConsecutivo As String) As String
        Dim codigo As String
        Dim piezas As String
        Dim fecha As String
        Dim consecutivo As String
        consecutivo = prmConsecutivo.PadLeft(6, "0")


        piezas = prmPiezas.Replace(".", "")
        fecha = prmFecha.ToString("ddMMyy")

        codigo = prmProducto + prmLote + piezas + fecha + consecutivo

        Return codigo
    End Function



#End Region
End Class