﻿
Public Class ucPipeEditor
    Protected m_profile As PipeProfile

    Public Property Profile() As PipeProfile
        Get
            Return m_profile
        End Get
        Set(ByVal value As PipeProfile)
            m_profile = value
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

    Private Sub ucPipeEditor_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim gobj As Microsoft.Msdn.Samples.GraphicObjects.PipeGraphic = My.Application.ActiveSimulation.FormSurface.FlowsheetDesignSurface.SelectedObject
        Dim myCOTK As RELAP.SimulationObjects.UnitOps.pipe = My.Application.ActiveSimulation.Collections.CLCS_PipeCollection(gobj.Name)

        For i = 1 To myCOTK.NumberOfVoulmes
            dgv.Rows.Add(i.ToString)
        Next
        For i = 1 To myCOTK.NumberOfVoulmes - 1
            dgv2.Rows.Add(i.ToString)
        Next
        For Each row As DataGridViewRow In dgv.Rows
            For i = 1 To row.Cells.Count - 1
                row.Cells(i).Value = 0
            Next
        Next
        For Each row As DataGridViewRow In dgv2.Rows
            For i = 1 To row.Cells.Count - 1
                row.Cells(i).Value = 0
            Next
        Next

    End Sub



    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles dgv.CellEnter, dgv2.CellEnter
        Dim row As New DataGridViewRow
        Dim cv As New RELAP.SistemasDeUnidades.Conversor
        Dim v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16 As Object

        If Not Me.Profile Is Nothing Then
            Me.Profile.Sections.Clear()
            Me.Profile.Junctions.Clear()
        End If


        For Each row In Me.dgv.Rows

            v1 = row.Cells(0).Value
            v2 = row.Cells(1).Value
            v3 = row.Cells(2).Value
            v4 = row.Cells(3).Value
            v5 = row.Cells(4).Value
            v6 = row.Cells(5).Value
            v7 = row.Cells(6).Value
            v8 = row.Cells(7).Value
            v9 = row.Cells(8).Value
            v10 = row.Cells(9).Value
            v11 = row.Cells(10).Value
            v12 = row.Cells(11).Value
            v13 = row.Cells(12).Value
            v14 = row.Cells(13).Value
            v15 = row.Cells(14).Value
            v16 = row.Cells(15).Value

            Me.Profile.Sections.Add(row.Index + 1, New PipeSection(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16))

        Next


        For Each row In Me.dgv2.Rows

            v1 = row.Cells(0).Value
            v2 = row.Cells(1).Value
            v3 = row.Cells(2).Value
            v4 = row.Cells(3).Value
            v5 = row.Cells(4).Value
            v6 = row.Cells(5).Value
            v7 = row.Cells(6).Value
            v8 = row.Cells(7).Value
            v9 = row.Cells(8).Value
            v10 = row.Cells(9).Value
            v11 = row.Cells(10).Value

            Me.Profile.Junctions.Add(row.Index + 1, New PipeJunctions(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11))

        Next


        row.Dispose()
    End Sub
    Dim selectedcells As New List(Of Object)
    Private Sub cmdCopy_Click(sender As Object, e As EventArgs) Handles cmdCopy.Click
        If dgv.SelectedRows.Count = 1 Then
            selectedcells.Clear()
            For Each cell As DataGridViewCell In dgv.SelectedRows(0).Cells
                selectedcells.Add(cell.Value)
            Next

        End If
    End Sub

    Private Sub cmdPaste_Click(sender As Object, e As EventArgs) Handles cmdPaste.Click
        For Each row As DataGridViewRow In dgv.SelectedRows
            Dim i = 0


            For i = 1 To row.Cells.Count - 1
                row.Cells(i).Value = selectedcells(i)
            Next


        Next

    End Sub

    Private Sub dgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellClick
        cmdCopy.Enabled = False
        cmdPaste.Enabled = False
    End Sub

    Private Sub dgv_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv.RowHeaderMouseClick
        If dgv.SelectedRows.Count = 1 Then
            cmdCopy.Enabled = True
        Else
            cmdCopy.Enabled = False
        End If
        If dgv.SelectedRows.Count > 0 Then
            cmdPaste.Enabled = True
        Else
            cmdPaste.Enabled = True
        End If
    End Sub

    Dim selectedcells2 As New List(Of Object)
    Private Sub cmdCopy2_Click(sender As Object, e As EventArgs) Handles cmdCopy2.Click
        If dgv.SelectedRows.Count = 1 Then
            selectedcells.Clear()
            For Each cell As DataGridViewCell In dgv2.SelectedRows(0).Cells
                selectedcells2.Add(cell.Value)
            Next

        End If
    End Sub

    Private Sub cmdPaste2_Click(sender As Object, e As EventArgs) Handles cmdPaste2.Click
        For Each row As DataGridViewRow In dgv.SelectedRows
            Dim i = 0


            For i = 1 To row.Cells.Count - 1
                row.Cells(i).Value = selectedcells2(i)
            Next


        Next

    End Sub

    Private Sub dgv2_RowHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv2.RowHeaderMouseClick
        If dgv2.SelectedRows.Count = 1 Then
            cmdCopy2.Enabled = True
        Else
            cmdCopy2.Enabled = False
        End If
        If dgv2.SelectedRows.Count > 0 Then
            cmdPaste2.Enabled = True
        Else
            cmdPaste2.Enabled = True
        End If
    End Sub
    Private Sub dgv2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv2.CellClick
        cmdCopy2.Enabled = False
        cmdPaste2.Enabled = False
    End Sub
End Class

