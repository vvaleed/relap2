﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucHeatStructureEditor
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.chkboxmeshgeometry = New System.Windows.Forms.CheckBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.dgvformat1 = New System.Windows.Forms.DataGridView()
        Me.Numberofintervals = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RightCoordinate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvformat2 = New System.Windows.Forms.DataGridView()
        Me.MeshInterval = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IntervalNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvNoDecay = New System.Windows.Forms.DataGridView()
        Me.SourceValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MeshIntervalNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.txtboxDecayHeat = New System.Windows.Forms.TextBox()
        Me.dgvWithDecay = New System.Windows.Forms.DataGridView()
        Me.GammaAttenuationCo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MeshIntervalNumber2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvComposition = New System.Windows.Forms.DataGridView()
        Me.CompositionNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MeshIntervalNumber3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.CmbBoxSelectFormat = New System.Windows.Forms.ComboBox()
        Me.cmdsave = New System.Windows.Forms.Button()
        CType(Me.dgvformat1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvformat2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvNoDecay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvWithDecay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvComposition, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkboxmeshgeometry
        '
        Me.chkboxmeshgeometry.AutoSize = True
        Me.chkboxmeshgeometry.Location = New System.Drawing.Point(34, 29)
        Me.chkboxmeshgeometry.Name = "chkboxmeshgeometry"
        Me.chkboxmeshgeometry.Size = New System.Drawing.Size(128, 17)
        Me.chkboxmeshgeometry.TabIndex = 0
        Me.chkboxmeshgeometry.Text = "Enter Mesh Geometry"
        Me.chkboxmeshgeometry.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(34, 59)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(100, 13)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.Text = "Select Format"
        '
        'dgvformat1
        '
        Me.dgvformat1.AccessibleDescription = "                            "
        Me.dgvformat1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvformat1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Numberofintervals, Me.RightCoordinate})
        Me.dgvformat1.Location = New System.Drawing.Point(34, 114)
        Me.dgvformat1.Name = "dgvformat1"
        Me.dgvformat1.Size = New System.Drawing.Size(256, 125)
        Me.dgvformat1.TabIndex = 4
        '
        'Numberofintervals
        '
        Me.Numberofintervals.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Numberofintervals.HeaderText = "Number of Intervals"
        Me.Numberofintervals.Name = "Numberofintervals"
        '
        'RightCoordinate
        '
        Me.RightCoordinate.HeaderText = "Right Coordinate"
        Me.RightCoordinate.Name = "RightCoordinate"
        '
        'dgvformat2
        '
        Me.dgvformat2.AccessibleDescription = "                            "
        Me.dgvformat2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvformat2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MeshInterval, Me.IntervalNumber})
        Me.dgvformat2.Location = New System.Drawing.Point(34, 114)
        Me.dgvformat2.Name = "dgvformat2"
        Me.dgvformat2.Size = New System.Drawing.Size(256, 125)
        Me.dgvformat2.TabIndex = 5
        '
        'MeshInterval
        '
        Me.MeshInterval.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.MeshInterval.HeaderText = "Mesh Interval"
        Me.MeshInterval.Name = "MeshInterval"
        '
        'IntervalNumber
        '
        Me.IntervalNumber.HeaderText = "Interval Number"
        Me.IntervalNumber.Name = "IntervalNumber"
        '
        'dgvNoDecay
        '
        Me.dgvNoDecay.AccessibleDescription = "                            "
        Me.dgvNoDecay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNoDecay.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SourceValue, Me.MeshIntervalNumber})
        Me.dgvNoDecay.Location = New System.Drawing.Point(344, 114)
        Me.dgvNoDecay.Name = "dgvNoDecay"
        Me.dgvNoDecay.Size = New System.Drawing.Size(256, 125)
        Me.dgvNoDecay.TabIndex = 6
        '
        'SourceValue
        '
        Me.SourceValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.SourceValue.HeaderText = "Source Value"
        Me.SourceValue.Name = "SourceValue"
        '
        'MeshIntervalNumber
        '
        Me.MeshIntervalNumber.HeaderText = "Mesh Interval Number"
        Me.MeshIntervalNumber.Name = "MeshIntervalNumber"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(344, 59)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(100, 13)
        Me.TextBox2.TabIndex = 7
        Me.TextBox2.Text = "Decay Heat"
        '
        'txtboxDecayHeat
        '
        Me.txtboxDecayHeat.Location = New System.Drawing.Point(422, 59)
        Me.txtboxDecayHeat.Name = "txtboxDecayHeat"
        Me.txtboxDecayHeat.Size = New System.Drawing.Size(100, 20)
        Me.txtboxDecayHeat.TabIndex = 8
        '
        'dgvWithDecay
        '
        Me.dgvWithDecay.AccessibleDescription = "                            "
        Me.dgvWithDecay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvWithDecay.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.GammaAttenuationCo, Me.MeshIntervalNumber2})
        Me.dgvWithDecay.Location = New System.Drawing.Point(344, 114)
        Me.dgvWithDecay.Name = "dgvWithDecay"
        Me.dgvWithDecay.Size = New System.Drawing.Size(256, 125)
        Me.dgvWithDecay.TabIndex = 9
        '
        'GammaAttenuationCo
        '
        Me.GammaAttenuationCo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.GammaAttenuationCo.HeaderText = "Gamma Attenuation Co."
        Me.GammaAttenuationCo.Name = "GammaAttenuationCo"
        '
        'MeshIntervalNumber2
        '
        Me.MeshIntervalNumber2.HeaderText = "Mesh Interval Number"
        Me.MeshIntervalNumber2.Name = "MeshIntervalNumber2"
        '
        'dgvComposition
        '
        Me.dgvComposition.AccessibleDescription = "                            "
        Me.dgvComposition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvComposition.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CompositionNumber, Me.MeshIntervalNumber3})
        Me.dgvComposition.Location = New System.Drawing.Point(34, 280)
        Me.dgvComposition.Name = "dgvComposition"
        Me.dgvComposition.Size = New System.Drawing.Size(256, 125)
        Me.dgvComposition.TabIndex = 10
        '
        'CompositionNumber
        '
        Me.CompositionNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.CompositionNumber.HeaderText = "Composition Number"
        Me.CompositionNumber.Name = "CompositionNumber"
        '
        'MeshIntervalNumber3
        '
        Me.MeshIntervalNumber3.HeaderText = "Mesh Interval Number"
        Me.MeshIntervalNumber3.Name = "MeshIntervalNumber3"
        '
        'TextBox4
        '
        Me.TextBox4.BackColor = System.Drawing.SystemColors.Control
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox4.Location = New System.Drawing.Point(34, 261)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.ReadOnly = True
        Me.TextBox4.Size = New System.Drawing.Size(100, 13)
        Me.TextBox4.TabIndex = 11
        Me.TextBox4.Text = "Composition Data"
        '
        'CmbBoxSelectFormat
        '
        Me.CmbBoxSelectFormat.FormattingEnabled = True
        Me.CmbBoxSelectFormat.Items.AddRange(New Object() {"Number of Intervals, Right Coordinate", "Mesh Interval, Interval number"})
        Me.CmbBoxSelectFormat.Location = New System.Drawing.Point(34, 82)
        Me.CmbBoxSelectFormat.Name = "CmbBoxSelectFormat"
        Me.CmbBoxSelectFormat.Size = New System.Drawing.Size(256, 21)
        Me.CmbBoxSelectFormat.TabIndex = 12
        '
        'cmdsave
        '
        Me.cmdsave.Location = New System.Drawing.Point(617, 382)
        Me.cmdsave.Name = "cmdsave"
        Me.cmdsave.Size = New System.Drawing.Size(75, 23)
        Me.cmdsave.TabIndex = 13
        Me.cmdsave.Text = "Save"
        Me.cmdsave.UseVisualStyleBackColor = True
        '
        'ucHeatStructureEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.cmdsave)
        Me.Controls.Add(Me.CmbBoxSelectFormat)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.dgvComposition)
        Me.Controls.Add(Me.txtboxDecayHeat)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.dgvNoDecay)
        Me.Controls.Add(Me.dgvformat1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.chkboxmeshgeometry)
        Me.Controls.Add(Me.dgvWithDecay)
        Me.Controls.Add(Me.dgvformat2)
        Me.Name = "ucHeatStructureEditor"
        Me.Size = New System.Drawing.Size(912, 422)
        CType(Me.dgvformat1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvformat2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvNoDecay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvWithDecay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvComposition, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkboxmeshgeometry As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents dgvformat1 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvformat2 As System.Windows.Forms.DataGridView
    Friend WithEvents dgvNoDecay As System.Windows.Forms.DataGridView
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents txtboxDecayHeat As System.Windows.Forms.TextBox
    Friend WithEvents dgvWithDecay As System.Windows.Forms.DataGridView
    Friend WithEvents Numberofintervals As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RightCoordinate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MeshInterval As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IntervalNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvComposition As System.Windows.Forms.DataGridView
    Friend WithEvents SourceValue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MeshIntervalNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GammaAttenuationCo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MeshIntervalNumber2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CompositionNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MeshIntervalNumber3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents CmbBoxSelectFormat As System.Windows.Forms.ComboBox
    Friend WithEvents cmdsave As System.Windows.Forms.Button

End Class
