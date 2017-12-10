Partial Class Form1
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	#Region "Windows Form Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.pccDiskDriveControl = New System.Drawing.PieChart.PieChartControl()
		Me.label1 = New System.Windows.Forms.Label()
		Me.lblFilename = New System.Windows.Forms.Label()
		Me.btnStartStop = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		' 
		' pccDiskDriveControl
		' 
		Me.pccDiskDriveControl.Location = New System.Drawing.Point(12, 12)
		Me.pccDiskDriveControl.Name = "pccDiskDriveControl"
		Me.pccDiskDriveControl.Size = New System.Drawing.Size(662, 391)
		Me.pccDiskDriveControl.TabIndex = 0
		Me.pccDiskDriveControl.ToolTips = Nothing
		' 
		' label1
		' 
		Me.label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(20, 409)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(23, 13)
		Me.label1.TabIndex = 1
		Me.label1.Text = "File"
		' 
		' lblFilename
		' 
		Me.lblFilename.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblFilename.Location = New System.Drawing.Point(58, 409)
		Me.lblFilename.Name = "lblFilename"
		Me.lblFilename.Size = New System.Drawing.Size(616, 23)
		Me.lblFilename.TabIndex = 2
		Me.lblFilename.Text = "..."
		' 
		' btnStartStop
		' 
		Me.btnStartStop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnStartStop.Location = New System.Drawing.Point(599, 454)
		Me.btnStartStop.Name = "btnStartStop"
		Me.btnStartStop.Size = New System.Drawing.Size(75, 23)
		Me.btnStartStop.TabIndex = 5
		Me.btnStartStop.Text = "Start"
		Me.btnStartStop.UseVisualStyleBackColor = True
		AddHandler Me.btnStartStop.Click, New System.EventHandler(AddressOf Me.btnStartStop_Click)
		' 
		' Form1
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(686, 489)
		Me.ControlBox = False
		Me.Controls.Add(Me.btnStartStop)
		Me.Controls.Add(Me.lblFilename)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.pccDiskDriveControl)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Name = "Form1"
		Me.Text = "Hard Drive Scan Progress"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private pccDiskDriveControl As System.Drawing.PieChart.PieChartControl
	Private label1 As System.Windows.Forms.Label
	Private lblFilename As System.Windows.Forms.Label
	Private btnStartStop As System.Windows.Forms.Button
End Class

