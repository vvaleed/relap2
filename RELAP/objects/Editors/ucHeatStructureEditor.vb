﻿Public Class ucHeatStructureEditor
    Private _HeatStructureMeshData As HeatStructureMeshData
    Public Property HeatStructureMeshData() As HeatStructureMeshData
        Get
            Return _HeatStructureMeshData
        End Get
        Set(ByVal value As HeatStructureMeshData)
            _HeatStructureMeshData = value
        End Set
    End Property
    Private _us As RELAP.SistemasDeUnidades.Unidades
    Public Property SystemOfUnits() As RELAP.SistemasDeUnidades.Unidades
        Get
            Return _us
        End Get
        Set(ByVal value As RELAP.SistemasDeUnidades.Unidades)
            _us = value
        End Set
    End Property
    Private _nf As String

    Public Property NumberFormat() As String
        Get
            Return _nf
        End Get
        Set(ByVal value As String)
            _nf = value
        End Set
    End Property

   
    
    Private Sub ucHeatStructureEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim gobj As Microsoft.Msdn.Samples.GraphicObjects.HeatStructureGraphic = My.Application.ActiveSimulation.FormSurface.FlowsheetDesignSurface.SelectedObject
        Dim myCOTK As RELAP.SimulationObjects.UnitOps.HeatStructure = My.Application.ActiveSimulation.Collections.CLCS_HeatStructureCollection(gobj.Name)

        chkboxmeshgeometry.Checked = True
        CmbBoxSelectFormat.SelectedIndex = 0
        dgvformat2.Hide()
        dgvWithDecay.Hide()
        dgvTemp2.Hide()
        ComboBoxTemp.SelectedIndex = 0

        If myCOTK.HeatStructureMeshData.MeshDataFormat1.Count <> 0 Then

        End If
    End Sub

    Private Sub chkboxmeshgeometry_CheckStateChanged(sender As Object, e As EventArgs) Handles chkboxmeshgeometry.CheckStateChanged
        Try
            If chkboxmeshgeometry.Checked = False Then
                HeatStructureMeshData.EnterMeshGeometry = "1"
                CmbBoxSelectFormat.Hide()

                dgvformat1.Hide()
                dgvformat1.Rows.Clear()

                dgvformat2.Hide()
                dgvformat2.Rows.Clear()

                dgvNoDecay.Hide()
                dgvNoDecay.Rows.Clear()

                dgvWithDecay.Hide()
                dgvWithDecay.Rows.Clear()

                dgvComposition.Hide()
                dgvComposition.Rows.Clear()

                TextBox1.Hide()
                TextBox2.Hide()
                TextBox4.Hide()
                txtboxDecayHeat.Hide()


            ElseIf chkboxmeshgeometry.Checked = True Then
                HeatStructureMeshData.EnterMeshGeometry = "0"
                CmbBoxSelectFormat.Show()
                dgvformat1.Show()
                dgvformat2.Show()
                dgvNoDecay.Show()
                dgvWithDecay.Show()
                dgvComposition.Show()
                TextBox1.Show()
                TextBox2.Show()
                TextBox4.Show()
                txtboxDecayHeat.Show()


            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CmbBoxSelectFormat_SelectedValueChanged(sender As Object, e As EventArgs) Handles CmbBoxSelectFormat.SelectedValueChanged
        If CmbBoxSelectFormat.SelectedIndex = 0 Then
            HeatStructureMeshData.SelectFormat = "1"
            dgvformat1.Show()
            dgvformat2.Rows.Clear()
            dgvformat2.Hide()

        ElseIf CmbBoxSelectFormat.SelectedIndex = 1 Then
            HeatStructureMeshData.SelectFormat = "2"
            dgvformat1.Rows.Clear()
            dgvformat1.Hide()
            dgvformat2.Show()
        End If
    End Sub
    Private Sub ComboBoxTemp_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBoxTemp.SelectedValueChanged
        If ComboBoxTemp.SelectedIndex = 0 Then
            HeatStructureMeshData.SelectTemp = "0"
            dgvTemp2.Rows.Clear()
            dgvTemp2.Hide()
            dgvTemp1.Show()
        ElseIf ComboBoxTemp.SelectedIndex = 1 Then
            HeatStructureMeshData.SelectTemp = "-1"
            dgvTemp1.Rows.Clear()
            dgvTemp1.Hide()
            dgvTemp2.Show()
        Else
            HeatStructureMeshData.SelectTemp = "1"
            dgvTemp1.Rows.Clear()
            dgvTemp2.Rows.Clear()
        End If
    End Sub

    Private Sub txtboxDecayHeat_TextChanged(sender As Object, e As EventArgs) Handles txtboxDecayHeat.TextChanged
        HeatStructureMeshData.DecayHeat = txtboxDecayHeat.Text.ToString

        If txtboxDecayHeat.Text = "" Then
            dgvNoDecay.Show()
            dgvWithDecay.Hide()
            dgvWithDecay.Rows.Clear()
        ElseIf txtboxDecayHeat.Text = "0" Then
            dgvNoDecay.Show()
            dgvWithDecay.Hide()
            dgvWithDecay.Rows.Clear()
        Else
            dgvNoDecay.Hide()
            dgvNoDecay.Rows.Clear()
            dgvWithDecay.Show()

        End If
    End Sub



    Private Sub cmdsave_Click(sender As Object, e As EventArgs) Handles cmdsave.Click
        Dim row As New DataGridViewRow
        Dim cv As New RELAP.SistemasDeUnidades.Conversor
        Dim v1, v2 As Object

        If Not Me.HeatStructureMeshData Is Nothing Then
            Me.HeatStructureMeshData.MeshDataFormat1.Clear()
        End If
        For i = 0 To dgvformat1.Rows.Count - 2
            row = dgvformat1.Rows(i)
            v1 = row.Cells(0).Value
            v2 = row.Cells(1).Value
            Me.HeatStructureMeshData.MeshDataFormat1.Add(row.Index + 1, New HSMeshDataFormat1(v1, v2))
        Next

        If Not Me.HeatStructureMeshData Is Nothing Then
            Me.HeatStructureMeshData.MeshDataFormat1.Clear()
        End If
        For i = 0 To dgvformat1.Rows.Count - 2
            row = dgvformat1.Rows(i)
            v1 = row.Cells(0).Value
            v2 = row.Cells(1).Value
            Me.HeatStructureMeshData.MeshDataFormat1.Add(row.Index + 1, New HSMeshDataFormat1(v1, v2))
        Next

        If Not Me.HeatStructureMeshData Is Nothing Then
            Me.HeatStructureMeshData.MeshDataFormat1.Clear()
        End If
        For i = 0 To dgvformat2.Rows.Count - 2
            row = dgvformat2.Rows(i)
            v1 = row.Cells(0).Value
            v2 = row.Cells(1).Value
            Me.HeatStructureMeshData.MeshDataFormat2.Add(row.Index + 1, New HSMeshDataFormat2(v1, v2))
        Next

        If Not Me.HeatStructureMeshData Is Nothing Then
            Me.HeatStructureMeshData.MeshDataNoDecay.Clear()
        End If
        For i = 0 To dgvNoDecay.Rows.Count - 2
            row = dgvNoDecay.Rows(i)
            v1 = row.Cells(0).Value
            v2 = row.Cells(1).Value
            Me.HeatStructureMeshData.MeshDataNoDecay.Add(row.Index + 1, New HSMeshDataNoDecay(v1, v2))
        Next

        If Not Me.HeatStructureMeshData Is Nothing Then
            Me.HeatStructureMeshData.MeshDataWithDecay.Clear()
        End If
        For i = 0 To dgvWithDecay.Rows.Count - 2
            row = dgvWithDecay.Rows(i)
            v1 = row.Cells(0).Value
            v2 = row.Cells(1).Value
            Me.HeatStructureMeshData.MeshDataWithDecay.Add(row.Index + 1, New HSMeshDataWithDecay(v1, v2))
        Next

        If Not Me.HeatStructureMeshData Is Nothing Then
            Me.HeatStructureMeshData.MeshDataComposition.Clear()
        End If
        For i = 0 To dgvComposition.Rows.Count - 2
            row = dgvComposition.Rows(i)
            v1 = row.Cells(0).Value
            v2 = row.Cells(1).Value
            Me.HeatStructureMeshData.MeshDataComposition.Add(row.Index + 1, New HSMeshDataComposition(v1, v2))
        Next

        If Not Me.HeatStructureMeshData Is Nothing Then
            Me.HeatStructureMeshData.Temp1.Clear()
        End If
        For i = 0 To dgvTemp1.Rows.Count - 2
            row = dgvTemp2.Rows(i)
            v1 = row.Cells(0).Value
            v2 = row.Cells(1).Value
            Me.HeatStructureMeshData.Temp1.Add(row.Index + 1, New HSTemp1(v1, v2))
        Next

        If Not Me.HeatStructureMeshData Is Nothing Then
            Me.HeatStructureMeshData.Temp2.Clear()
        End If
        For i = 0 To dgvTemp2.Rows.Count - 2
            row = dgvTemp2.Rows(i)
            v1 = row.Cells(0).Value
            v2 = row.Cells(1).Value
            Me.HeatStructureMeshData.Temp2.Add(row.Index + 1, New HSTemp2(v1, v2))
        Next
        row.Dispose()

    End Sub



End Class

