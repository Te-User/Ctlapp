<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Logs = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Logs
        '
        Me.Logs.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Logs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Logs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Logs.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Logs.ForeColor = System.Drawing.Color.Lime
        Me.Logs.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.Logs.Location = New System.Drawing.Point(0, 0)
        Me.Logs.Margin = New System.Windows.Forms.Padding(10)
        Me.Logs.Multiline = True
        Me.Logs.Name = "Logs"
        Me.Logs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Logs.Size = New System.Drawing.Size(271, 201)
        Me.Logs.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(271, 201)
        Me.Controls.Add(Me.Logs)
        Me.ForeColor = System.Drawing.Color.Lime
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " Ctlapp Server"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Logs As System.Windows.Forms.TextBox
End Class
