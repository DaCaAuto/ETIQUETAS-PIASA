<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmImprimirEtiqueta
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmImprimirEtiqueta))
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.imgEtiqueta = New DevExpress.XtraEditors.PictureEdit()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.comboEtiquetas = New DevExpress.XtraEditors.GridLookUpEdit()
        Me.GridLookUpEdit1View = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.txtLote = New DevExpress.XtraEditors.TextEdit()
        Me.txtMaterial = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtNum = New DevExpress.XtraEditors.TextEdit()
        CType(Me.imgEtiqueta.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.comboEtiquetas.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtMaterial.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNum.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Appearance.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SimpleButton2.Appearance.Options.UseFont = True
        Me.SimpleButton2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.SimpleButton2.ImageOptions.Image = CType(resources.GetObject("SimpleButton2.ImageOptions.Image"), System.Drawing.Image)
        Me.SimpleButton2.Location = New System.Drawing.Point(855, 593)
        Me.SimpleButton2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(94, 32)
        Me.SimpleButton2.TabIndex = 494
        Me.SimpleButton2.Text = "Imprimir"
        '
        'LabelControl4
        '
        Me.LabelControl4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(53, 599)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(232, 19)
        Me.LabelControl4.TabIndex = 492
        Me.LabelControl4.Text = "Número de etiquetas a imprimir:"
        '
        'imgEtiqueta
        '
        Me.imgEtiqueta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.imgEtiqueta.Location = New System.Drawing.Point(32, 273)
        Me.imgEtiqueta.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.imgEtiqueta.Name = "imgEtiqueta"
        Me.imgEtiqueta.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.imgEtiqueta.Properties.Appearance.Options.UseBackColor = True
        Me.imgEtiqueta.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.imgEtiqueta.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
        Me.imgEtiqueta.Size = New System.Drawing.Size(916, 295)
        Me.imgEtiqueta.TabIndex = 491
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.comboEtiquetas)
        Me.GroupControl1.Controls.Add(Me.SimpleButton1)
        Me.GroupControl1.Controls.Add(Me.txtLote)
        Me.GroupControl1.Controls.Add(Me.txtMaterial)
        Me.GroupControl1.Controls.Add(Me.LabelControl3)
        Me.GroupControl1.Controls.Add(Me.LabelControl2)
        Me.GroupControl1.Controls.Add(Me.LabelControl1)
        Me.GroupControl1.Location = New System.Drawing.Point(32, 36)
        Me.GroupControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(916, 217)
        Me.GroupControl1.TabIndex = 490
        Me.GroupControl1.Text = "Parámetros"
        '
        'comboEtiquetas
        '
        Me.comboEtiquetas.Cursor = System.Windows.Forms.Cursors.Default
        Me.comboEtiquetas.Location = New System.Drawing.Point(293, 51)
        Me.comboEtiquetas.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboEtiquetas.Name = "comboEtiquetas"
        Me.comboEtiquetas.Properties.Appearance.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboEtiquetas.Properties.Appearance.Options.UseFont = True
        Me.comboEtiquetas.Properties.AppearanceDisabled.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboEtiquetas.Properties.AppearanceDisabled.Options.UseFont = True
        Me.comboEtiquetas.Properties.AppearanceDropDown.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboEtiquetas.Properties.AppearanceDropDown.Options.UseFont = True
        Me.comboEtiquetas.Properties.AppearanceFocused.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.comboEtiquetas.Properties.AppearanceFocused.Options.UseFont = True
        Me.comboEtiquetas.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.comboEtiquetas.Properties.NullText = ""
        Me.comboEtiquetas.Properties.PopupSizeable = False
        Me.comboEtiquetas.Properties.PopupView = Me.GridLookUpEdit1View
        Me.comboEtiquetas.Size = New System.Drawing.Size(304, 30)
        Me.comboEtiquetas.TabIndex = 62
        '
        'GridLookUpEdit1View
        '
        Me.GridLookUpEdit1View.DetailHeight = 373
        Me.GridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.GridLookUpEdit1View.Name = "GridLookUpEdit1View"
        Me.GridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GridLookUpEdit1View.OptionsView.ShowGroupPanel = False
        '
        'SimpleButton1
        '
        Me.SimpleButton1.ImageOptions.Image = CType(resources.GetObject("SimpleButton1.ImageOptions.Image"), System.Drawing.Image)
        Me.SimpleButton1.Location = New System.Drawing.Point(641, 147)
        Me.SimpleButton1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light
        Me.SimpleButton1.Size = New System.Drawing.Size(39, 43)
        Me.SimpleButton1.TabIndex = 18
        '
        'txtLote
        '
        Me.txtLote.Location = New System.Drawing.Point(293, 154)
        Me.txtLote.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtLote.Name = "txtLote"
        Me.txtLote.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLote.Properties.Appearance.Options.UseFont = True
        Me.txtLote.Size = New System.Drawing.Size(304, 26)
        Me.txtLote.TabIndex = 16
        '
        'txtMaterial
        '
        Me.txtMaterial.Location = New System.Drawing.Point(293, 106)
        Me.txtMaterial.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtMaterial.Name = "txtMaterial"
        Me.txtMaterial.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaterial.Properties.Appearance.Options.UseFont = True
        Me.txtMaterial.Size = New System.Drawing.Size(304, 26)
        Me.txtMaterial.TabIndex = 15
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(231, 158)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(36, 19)
        Me.LabelControl3.TabIndex = 14
        Me.LabelControl3.Text = "Lote:"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(159, 113)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(118, 19)
        Me.LabelControl2.TabIndex = 13
        Me.LabelControl2.Text = "Código material:"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(153, 62)
        Me.LabelControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(123, 19)
        Me.LabelControl1.TabIndex = 12
        Me.LabelControl1.Text = "Dieseño etiqueta:"
        '
        'txtNum
        '
        Me.txtNum.Location = New System.Drawing.Point(287, 596)
        Me.txtNum.Name = "txtNum"
        Me.txtNum.Properties.Appearance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNum.Properties.Appearance.Options.UseFont = True
        Me.txtNum.Properties.BeepOnError = False
        Me.txtNum.Size = New System.Drawing.Size(109, 26)
        Me.txtNum.TabIndex = 495
        '
        'FrmImprimirEtiqueta
        '
        Me.Appearance.BackColor = System.Drawing.Color.FromArgb(CType(CType(57, Byte), Integer), CType(CType(66, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(985, 662)
        Me.Controls.Add(Me.txtNum)
        Me.Controls.Add(Me.SimpleButton2)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.imgEtiqueta)
        Me.Controls.Add(Me.GroupControl1)
        Me.Name = "FrmImprimirEtiqueta"
        Me.Text = "FrmImprimirEtiqueta"
        CType(Me.imgEtiqueta.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupControl1.PerformLayout()
        CType(Me.comboEtiquetas.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridLookUpEdit1View, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLote.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtMaterial.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNum.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents imgEtiqueta As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents txtLote As DevExpress.XtraEditors.TextEdit
    Friend WithEvents txtMaterial As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents comboEtiquetas As DevExpress.XtraEditors.GridLookUpEdit
    Friend WithEvents GridLookUpEdit1View As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents txtNum As DevExpress.XtraEditors.TextEdit
End Class
