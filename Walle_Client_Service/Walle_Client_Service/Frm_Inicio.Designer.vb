<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Inicio
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Inicio))
        Me.t_inicio = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Process1 = New System.Diagnostics.Process()
        Me.Progress_Bar = New System.Windows.Forms.ProgressBar()
        Me.Txt_Cliente = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Lab_Pro = New System.Windows.Forms.Label()
        Me.lv_processo = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.t_automatico = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        't_inicio
        '
        Me.t_inicio.Interval = 30000
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Walle Client Service"
        Me.NotifyIcon1.Visible = True
        '
        'Process1
        '
        Me.Process1.StartInfo.Domain = ""
        Me.Process1.StartInfo.LoadUserProfile = False
        Me.Process1.StartInfo.Password = Nothing
        Me.Process1.StartInfo.StandardErrorEncoding = Nothing
        Me.Process1.StartInfo.StandardOutputEncoding = Nothing
        Me.Process1.StartInfo.UserName = ""
        Me.Process1.SynchronizingObject = Me
        '
        'Progress_Bar
        '
        Me.Progress_Bar.Location = New System.Drawing.Point(9, 77)
        Me.Progress_Bar.Margin = New System.Windows.Forms.Padding(2)
        Me.Progress_Bar.Name = "Progress_Bar"
        Me.Progress_Bar.Size = New System.Drawing.Size(364, 19)
        Me.Progress_Bar.TabIndex = 0
        '
        'Txt_Cliente
        '
        Me.Txt_Cliente.Location = New System.Drawing.Point(9, 30)
        Me.Txt_Cliente.Margin = New System.Windows.Forms.Padding(2)
        Me.Txt_Cliente.Name = "Txt_Cliente"
        Me.Txt_Cliente.ReadOnly = True
        Me.Txt_Cliente.Size = New System.Drawing.Size(365, 20)
        Me.Txt_Cliente.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Location = New System.Drawing.Point(9, 112)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(162, 25)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Parar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.Location = New System.Drawing.Point(211, 112)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(162, 25)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Iniciar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Cliente"
        '
        'Lab_Pro
        '
        Me.Lab_Pro.AutoSize = True
        Me.Lab_Pro.Location = New System.Drawing.Point(9, 61)
        Me.Lab_Pro.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Lab_Pro.Name = "Lab_Pro"
        Me.Lab_Pro.Size = New System.Drawing.Size(74, 13)
        Me.Lab_Pro.TabIndex = 5
        Me.Lab_Pro.Text = "Aguardando..."
        '
        'lv_processo
        '
        Me.lv_processo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lv_processo.Location = New System.Drawing.Point(9, 142)
        Me.lv_processo.Name = "lv_processo"
        Me.lv_processo.Size = New System.Drawing.Size(364, 265)
        Me.lv_processo.Sorting = System.Windows.Forms.SortOrder.Descending
        Me.lv_processo.TabIndex = 7
        Me.lv_processo.UseCompatibleStateImageBehavior = False
        Me.lv_processo.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "DataHora"
        Me.ColumnHeader1.Width = 135
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Descrição"
        Me.ColumnHeader2.Width = 212
        '
        't_automatico
        '
        Me.t_automatico.Interval = 60000
        '
        'Frm_Inicio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(382, 417)
        Me.Controls.Add(Me.lv_processo)
        Me.Controls.Add(Me.Lab_Pro)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Txt_Cliente)
        Me.Controls.Add(Me.Progress_Bar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Frm_Inicio"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Walle Client Service 1.0"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents t_inicio As Timer
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents Process1 As Process
    Friend WithEvents Label1 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Txt_Cliente As TextBox
    Friend WithEvents Progress_Bar As ProgressBar
    Friend WithEvents Lab_Pro As Label
    Friend WithEvents lv_processo As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents t_automatico As Timer
End Class
