<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DisenoEtiqueta
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DisenoEtiqueta))
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.btnMostrar = New DevExpress.XtraEditors.SimpleButton()
        Me.SeparatorControl1 = New DevExpress.XtraEditors.SeparatorControl()
        Me.btnGuardar = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.abrirArchDialog = New DevExpress.XtraEditors.XtraOpenFileDialog(Me.components)
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.txtNombre = New DevExpress.XtraEditors.TextEdit()
        Me.txtLote = New DevExpress.XtraEditors.TextEdit()
        Me.txtMaterial = New DevExpress.XtraEditors.TextEdit()
        Me.imgEtiqueta = New DevExpress.XtraEditors.PictureEdit()
        Me.btnSeleccionarArch = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.SeparatorControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaterial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imgEtiqueta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl2
        '
        Me.LabelControl2.ImageOptions.Image = CType(resources.GetObject("LabelControl2.ImageOptions.Image"), System.Drawing.Image)
        Me.LabelControl2.Location = New System.Drawing.Point(39, 581)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(32, 32)
        Me.LabelControl2.TabIndex = 499
        Me.LabelControl2.ToolTip = "Ingrese código de material y el lote para ver demostración"
        Me.LabelControl2.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information
        Me.LabelControl2.ToolTipTitle = "Información"
        '
        'btnMostrar
        '
        Me.btnMostrar.Appearance.BorderColor = System.Drawing.Color.Transparent
        Me.btnMostrar.Appearance.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMostrar.Appearance.Options.UseBorderColor = True
        Me.btnMostrar.Appearance.Options.UseFont = True
        Me.btnMostrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnMostrar.ImageOptions.Image = CType(resources.GetObject("btnMostrar.ImageOptions.Image"), System.Drawing.Image)
        Me.btnMostrar.Location = New System.Drawing.Point(336, 586)
        Me.btnMostrar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMostrar.Name = "btnMostrar"
        Me.btnMostrar.Size = New System.Drawing.Size(80, 32)
        Me.btnMostrar.TabIndex = 498
        Me.btnMostrar.Text = "Mostrar"
        '
        'SeparatorControl1
        '
        Me.SeparatorControl1.LineOrientation = System.Windows.Forms.Orientation.Vertical
        Me.SeparatorControl1.Location = New System.Drawing.Point(472, 4)
        Me.SeparatorControl1.Name = "SeparatorControl1"
        Me.SeparatorControl1.Padding = New System.Windows.Forms.Padding(8, 9, 8, 9)
        Me.SeparatorControl1.Size = New System.Drawing.Size(35, 686)
        Me.SeparatorControl1.TabIndex = 500
        '
        'btnGuardar
        '
        Me.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGuardar.ImageOptions.Image = CType(resources.GetObject("btnGuardar.ImageOptions.Image"), System.Drawing.Image)
        Me.btnGuardar.Location = New System.Drawing.Point(885, 579)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(80, 29)
        Me.btnGuardar.TabIndex = 503
        Me.btnGuardar.Text = "Guardar"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Calibri", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(537, 581)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(105, 21)
        Me.LabelControl1.TabIndex = 504
        Me.LabelControl1.Text = "Guardar como:"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Calibri", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(513, 28)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(102, 21)
        Me.LabelControl3.TabIndex = 505
        Me.LabelControl3.Text = "Demostración:"
        '
        'abrirArchDialog
        '
        Me.abrirArchDialog.FileName = "XtraOpenFileDialog1"
        '
        'txtCodigo
        '
        Me.txtCodigo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCodigo.Location = New System.Drawing.Point(19, 69)
        Me.txtCodigo.Multiline = True
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCodigo.Size = New System.Drawing.Size(448, 621)
        Me.txtCodigo.TabIndex = 506
        '
        'txtNombre
        '
        Me.txtNombre.Location = New System.Drawing.Point(646, 578)
        Me.txtNombre.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombre.Properties.Appearance.Options.UseFont = True
        Me.txtNombre.Size = New System.Drawing.Size(220, 30)
        Me.txtNombre.TabIndex = 502
        '
        'txtLote
        '
        Me.txtLote.Location = New System.Drawing.Point(88, 621)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLote.Properties.Appearance.Options.UseFont = True
        Me.txtLote.Properties.NullValuePrompt = "Lote"
        Me.txtLote.Size = New System.Drawing.Size(231, 30)
        Me.txtLote.TabIndex = 501
        '
        'txtMaterial
        '
        Me.txtMaterial.Location = New System.Drawing.Point(88, 583)
        Me.txtMaterial.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMaterial.Name = "txtMaterial"
        Me.txtMaterial.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaterial.Properties.Appearance.Options.UseFont = True
        Me.txtMaterial.Properties.NullValuePrompt = "Código del material"
        Me.txtMaterial.Size = New System.Drawing.Size(231, 30)
        Me.txtMaterial.TabIndex = 497
        '
        'imgEtiqueta
        '
        Me.imgEtiqueta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.imgEtiqueta.Location = New System.Drawing.Point(513, 57)
        Me.imgEtiqueta.Margin = New System.Windows.Forms.Padding(4)
        Me.imgEtiqueta.Name = "imgEtiqueta"
        Me.imgEtiqueta.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.imgEtiqueta.Properties.Appearance.Options.UseBackColor = True
        Me.imgEtiqueta.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.imgEtiqueta.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.imgEtiqueta.Size = New System.Drawing.Size(448, 505)
        Me.imgEtiqueta.TabIndex = 496
        '
        'btnSeleccionarArch
        '
        Me.btnSeleccionarArch.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSeleccionarArch.Appearance.Options.UseFont = True
        Me.btnSeleccionarArch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSeleccionarArch.ImageOptions.Image = CType(resources.GetObject("btnSeleccionarArch.ImageOptions.Image"), System.Drawing.Image)
        Me.btnSeleccionarArch.Location = New System.Drawing.Point(18, 13)
        Me.btnSeleccionarArch.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSeleccionarArch.Name = "btnSeleccionarArch"
        Me.btnSeleccionarArch.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.[False]
        Me.btnSeleccionarArch.Size = New System.Drawing.Size(206, 36)
        Me.btnSeleccionarArch.TabIndex = 494
        Me.btnSeleccionarArch.Text = "Selecionar archivo .prn"
        '
        'DisenoEtiqueta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(985, 702)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.txtNombre)
        Me.Controls.Add(Me.txtLote)
        Me.Controls.Add(Me.SeparatorControl1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.btnMostrar)
        Me.Controls.Add(Me.txtMaterial)
        Me.Controls.Add(Me.imgEtiqueta)
        Me.Controls.Add(Me.btnSeleccionarArch)
        Me.Controls.Add(Me.txtCodigo)
        Me.IconOptions.Image = Global.AXCPIASA.My.Resources.Resources.Logo_X_150x150_01
        Me.Name = "DisenoEtiqueta"
        Me.Text = "Crear nueva etiqueta"
        CType(Me.SeparatorControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNombre.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaterial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imgEtiqueta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Private WithEvents btnMostrar As DevExpress.XtraEditors.SimpleButton
    Private WithEvents txtMaterial As DevExpress.XtraEditors.TextEdit
    Private WithEvents imgEtiqueta As DevExpress.XtraEditors.PictureEdit
    Private WithEvents btnSeleccionarArch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SeparatorControl1 As DevExpress.XtraEditors.SeparatorControl
    Private WithEvents txtLote As DevExpress.XtraEditors.TextEdit
    Private WithEvents txtNombre As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btnGuardar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents abrirArchDialog As DevExpress.XtraEditors.XtraOpenFileDialog
    Friend WithEvents txtCodigo As TextBox
End Class
