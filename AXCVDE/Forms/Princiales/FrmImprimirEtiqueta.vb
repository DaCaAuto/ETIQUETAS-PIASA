﻿Imports System.Net
Imports System.Text.RegularExpressions
Imports DevExpress.XtraEditors
Imports SAP.Middleware.Connector

Public Class FrmImprimirEtiqueta

    Dim input As String
    Dim contador As Integer
    Dim codBarra As String


    Private Sub FrmImprimirEtiqueta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListaEtiquetas()
    End Sub

    Private Sub ListaEtiquetas()
        Dim pResultado As New CResultado
        Dim Cls As New CEtiqueta
        Dim dt As New DataTable

        Try
            pResultado = Cls.ConsultaEtiqueta()

            dt = pResultado.Tabla

            comboEtiquetas.Properties.ValueMember = "id"
            comboEtiquetas.Properties.DisplayMember = "nombre"
            comboEtiquetas.Properties.DataSource = dt
        Catch ex As Exception

        End Try
    End Sub


    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click

        ' Validaciones
        If validar() = False Then
            Return
        End If




        'Acceso a datos
        Dim pResultadoSAP As CSAP = New CSAP
        Dim pEtiqueta As New CResultado
        Dim ClsE As New CEtiqueta
        Dim dt2 As New DataTable

        'Variables para llamar el sp
        Dim material = txtMaterial.Text.PadLeft(18, "0")
        Dim lote = txtLote.Text

        ' Patrón para hacer el reemplazo de los datos
        Dim patron As String = "<(?<TEXT>[^<]*)/>"


        'Verificamos que traiga datos
        Try
            pEtiqueta = ClsE.ConsultaInfoEtiqueta(material, lote)
            If (pEtiqueta.Estado <> True) Then
                XtraMessageBox.Show(pEtiqueta.Texto.ToString, "AXC")
                Return
            End If

            dt2 = pEtiqueta.Tabla
        Catch ex As Exception
            XtraMessageBox.Show(pEtiqueta.Texto.ToString, "AXC")
            Return
        End Try

        ' Conexión a SAP
        Dim funcion As IRfcFunction = pResultadoSAP.ConsultarSap(material)

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
        input = input.Replace("<FECHACADUCIDAD/>", DateAdd(DateInterval.Day, dt2.Rows(0).Item("DiasCaducidad"), Now).ToString("dd/MM/yyyy"))
        input = input.Replace("<PESO/>", dt2.Rows(0).Item("Peso").ToString + " " + dt2.Rows(0).Item("UMPeso").ToString)
        input = input.Replace("<CODIGO_SAP/>", txtMaterial.Text)
        'input = input.Replace("<i/>", "000001")

        'Reemplazo de datos en SAP
        For Each match As Match In Regex.Matches(input, patron)
            Try
                input = input.Replace("<" + match.Groups("TEXT").Value + "/>", est_RETURN(match.Groups("TEXT").Value).GetValue.ToString)
            Catch ex As Exception
                'MessageBox.Show("No se pudo realizar el intercambio de los datos con SAP: " + ex.ToString)
            End Try
        Next

        'Reemplazo de datos en AXC
        For Each row As DataRow In dt2.Rows
            For Each match As Match In Regex.Matches(input, patron)
                Try
                    input = input.Replace("<" + match.Groups("TEXT").Value + "/>", row.Item(match.Groups("TEXT").Value).ToString)
                Catch ex As Exception

                End Try
            Next
        Next

        'Generación del código de barras
        codBarra = generarCodigoBarras(est_RETURN("CODIGO_CLIENTE").GetValue.ToString,
                                               dt2.Rows(0).Item("LOTE").ToString,
                                               est_RETURN("PESO2_CLIENTE").GetValue.ToString,
                                               dt2.Rows(0).Item("FechaRegistro"),
                                               "1")


        Dim input2 = input.Replace("<CODIGOBARRAS/>", codBarra + "000001")
        input2 = input2.Replace("<i/>", "1")

        cargarImagen(input2)

    End Sub

#Region "METODOS"

    Private Sub imprimirEtiqueta(ByVal codigo As String, ByVal consecutivo As String)
        ' XtraMessageBox.Show(codigo)
        codigo = codigo.Replace("<CODIGOBARRAS/>", codBarra + consecutivo.PadLeft(6, "0"))
        codigo = codigo.Replace("<i/>", consecutivo)
        ' XtraMessageBox.Show(consecutivo)
        XtraMessageBox.Show(codBarra + consecutivo, "AXC")
        ' XtraMessageBox.Show(codigo)


        ' ******* Manda la impresión *******
        RawPrinter.SendToPrinter("Zebra" & Now, codigo.ToString, My.Settings.Impresora) 'print the string
        ' **********************************
    End Sub

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

    Private Function generarCodigoBarras(ByVal prmProducto As String, ByVal prmLote As String, ByVal prmPiezas As String, ByVal prmFecha As Date, ByVal prmConsecutivo As String) As String
        Dim codigo As String
        Dim piezas As String
        Dim fecha As String
        Dim consecutivo As String
        consecutivo = prmConsecutivo.PadLeft(6, "0")


        piezas = prmPiezas.Replace(".", "")
        fecha = prmFecha.ToString("ddMMyy")

        codigo = prmProducto + prmLote + piezas + fecha '+ consecutivo

        Return codigo
    End Function

    Private Function validar() As Boolean
        If input = "" Then
            XtraMessageBox.Show("Seleccione una etiqueta", "AXC")
            Return False
        End If

        If (txtLote.Text = "" Or txtMaterial.Text = "") Then
            XtraMessageBox.Show("Los campos de PRODUCTO y LOTE no pueden estar vacios", "AXC")
            Return False
        End If

        Return True
    End Function

    Private Sub comboEtiquetas_EditValueChanged(sender As Object, e As EventArgs) Handles comboEtiquetas.EditValueChanged
        Dim pResultado As New CResultado
        Dim Cls As New CEtiqueta
        Dim dt As New DataTable

        Try
            pResultado = Cls.ConsultaCodigoEtiqueta(Integer.Parse(comboEtiquetas.EditValue.ToString))
            dt = pResultado.Tabla
            input = dt.Rows(0).Item(0)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Dim numEtiqueta As Integer
        'Dim consecutivo As String
        If IsNumeric(txtNum.Text) = False Then
            XtraMessageBox.Show("Número de etiquetas a imprimir no válido, inténtelo nuevamente", "AXC")
            limpiarCampos()
            Return
        End If

        Try
            numEtiqueta = Integer.Parse(txtNum.Text)
        Catch ex As Exception
            XtraMessageBox.Show(ex.ToString, "AXC")
            limpiarCampos()
            Return
        End Try


        If numEtiqueta <= 0 Then
            XtraMessageBox.Show("El número de etiquetas a imprimir debe ser mayor a 0", "AXC")
            limpiarCampos()
            Return
        End If



        For i As Integer = 1 To numEtiqueta
            'consecutivo = i
            'consecutivo = consecutivo.PadLeft(6, "0")
            imprimirEtiqueta(input, i.ToString)
        Next

    End Sub

    Public Sub limpiarCampos()
        txtMaterial.Text = ""
        txtLote.Text = ""
        txtNum.Text = ""

    End Sub

#End Region
End Class