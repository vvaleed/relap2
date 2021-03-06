﻿Imports Microsoft.MSDN.Samples.GraphicObjects

Public Class ucThermoDynamicStates

    ' Private ThermoDynamicStates As List(Of ThermoDynamicState)
    'Public _ThermoDynamicStates As List(Of ThermoDynamicState)




    Private _ThermoDynamicStates As ThermoDynamicStates
    Public Property ThermoDynamicStates() As ThermoDynamicStates
        Get
            Return _ThermoDynamicStates
        End Get
        Set(ByVal value As ThermoDynamicStates)
            _ThermoDynamicStates = value
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

    Private Sub ucThermoDynamicStates_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If ThermoDynamicStates.cmbocheck = 1 Then
            cmbothermostates.SelectedIndex = ThermoDynamicStates.storeindex
        Else
            cmbothermostates.SelectedIndex = 0
        End If

        '  Dim str As String = ""

        If ThermoDynamicStates.State.Count > 0 Then
            Dim j As Integer = 0
            Dim i As Integer
            DataGridView1.Rows.Add(ThermoDynamicStates.State.Count)
            If My.Application.ActiveSimulation.ComponentType <> "pipe" And My.Application.ActiveSimulation.ComponentType <> "Annulus" Then

                For Each state In ThermoDynamicStates.State
                    Dim str() As String = state.Value.StatesString.Split(" ")

                    i = 1
                    Dim row As DataGridViewRow = DataGridView1.Rows(j)
                    For Each cell As DataGridViewCell In row.Cells
                        cell.Value = str(i)
                        i = i + 1
                        cmbothermostates.SelectedIndex = state.Value.StateType
                        'str = str & " " & CDbl(cell.Value).ToString("F")
                    Next
                    j = j + 1
                Next

            Else
                For Each state In ThermoDynamicStates.State
                    Dim str() As String = state.Value.StatesString.Split(" ")
                    i = 1
                    Dim row As DataGridViewRow = DataGridView1.Rows(j)
                    For Each cell As DataGridViewCell In row.Cells
                        cell.Value = str(i)
                        i = i + 1
                        cmbothermostates.SelectedIndex = state.Value.StateType
                        'str = str & " " & CDbl(cell.Value).ToString("F")
                    Next
                    j = j + 1
                Next

            End If
        End If
    End Sub

    Private Sub cmbothermostates_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbothermostates.SelectedValueChanged
        '        Pressure, Liquid Specific Internal Energy, Vapor Specific Internal Energy, Void Fraction
        'Temperature, Static Quality
        'Pressure, Static Quality
        'Pressure, Temperature
        'Pressure, Temperature, Static Quality
        'Temperature, Static Quality, Non condensable Quality
        'Pressure, Liquid Specific Internal Energy, Vapor Specific Internal Energy, Void Fraction, Non condensable Quality
        'Pressure, Liquid Temperature, Vapor Temperature, Void Fraction, Non condensable Quality
        DataGridView1.Columns.Clear()
        ThermoDynamicStates.cmbocheck = 1
        ThermoDynamicStates.storeindex = cmbothermostates.SelectedIndex
        If (My.Application.ActiveSimulation.ComponentType <> "pipe" And My.Application.ActiveSimulation.ComponentType <> "SingleVolume" And My.Application.ActiveSimulation.ComponentType <> "Branch" And My.Application.ActiveSimulation.ComponentType <> "Pump" And My.Application.ActiveSimulation.ComponentType <> "Annulus" And My.Application.ActiveSimulation.ComponentType <> "Separator") Then
            DataGridView1.Columns.Add("Time", "Time")
        End If

            If cmbothermostates.SelectedIndex = 0 Then

                DataGridView1.Columns.Add("Pressure", "Pressure")
                DataGridView1.Columns.Add("LiquidSpecificInternalEnergy", "Liquid Specific Internal Energy")
                DataGridView1.Columns.Add("VaporSpecificInternalEnergy", "Vapor Specific Internal Energy")
                DataGridView1.Columns.Add("VoidFraction", "Void Fraction")
            End If
            If cmbothermostates.SelectedIndex = 1 Then

                DataGridView1.Columns.Add("Temperature", "Temperature")
                DataGridView1.Columns.Add("StaticQuality", "Static Quality")

            End If
            If cmbothermostates.SelectedIndex = 2 Then

                DataGridView1.Columns.Add("Pressure", "Pressure")
                DataGridView1.Columns.Add("StaticQuality", "Static Quality")

            End If
            If cmbothermostates.SelectedIndex = 3 Then

                DataGridView1.Columns.Add("Pressure", "Pressure")
                DataGridView1.Columns.Add("Temperature", "Temperature")

            End If
            If cmbothermostates.SelectedIndex = 4 Then

                DataGridView1.Columns.Add("Pressure", "Pressure")
                DataGridView1.Columns.Add("Temperature", "Temperature")
                DataGridView1.Columns.Add("StaticQuality", "Static Quality")
            End If
            If cmbothermostates.SelectedIndex = 5 Then

                DataGridView1.Columns.Add("Pressure", "Pressure")
                DataGridView1.Columns.Add("Temperature", "Temperature")
                DataGridView1.Columns.Add("NoncondensableQuality", "Non condensable Quality")
            End If
            If cmbothermostates.SelectedIndex = 6 Then

                DataGridView1.Columns.Add("Pressure", "Pressure")
                DataGridView1.Columns.Add("LiquidSpecificInternalEnergy", "Liquid Specific Internal Energy")
                DataGridView1.Columns.Add("VaporSpecificInternalEnergy", "Vapor Specific Internal Energy")
                DataGridView1.Columns.Add("VoidFraction", "Void Fraction")
                DataGridView1.Columns.Add("NoncondensableQuality", "Non condensable Quality")
        End If

        If (My.Application.ActiveSimulation.ComponentType <> "SingleVolume" And My.Application.ActiveSimulation.ComponentType <> "Branch" And My.Application.ActiveSimulation.ComponentType <> "Pump" And My.Application.ActiveSimulation.ComponentType <> "Separator") Then
            If cmbothermostates.SelectedIndex = 7 Then

                DataGridView1.Columns.Add("LiquidTemperature", "Liquid Temperature")
                DataGridView1.Columns.Add("VaporTemperature", "Vapor Temperature")
                DataGridView1.Columns.Add("VoidFraction", "Void Fraction")
                DataGridView1.Columns.Add("NoncondensableQuality", "Non condensable Quality")
            End If
        End If
        If My.Application.ActiveSimulation.ComponentType = "pipe" Or My.Application.ActiveSimulation.ComponentType = "Annulus" Then
            DataGridView1.Columns.Add("Volume", "Volume Number")
        End If

    End Sub

    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdSave.Click
        Dim str As String = ""
        Dim row As DataGridViewRow
        If Not Me.ThermoDynamicStates Is Nothing Then
            Me.ThermoDynamicStates.State.Clear()
        End If
        If My.Application.ActiveSimulation.ComponentType <> "pipe" And My.Application.ActiveSimulation.ComponentType <> "Annulus" Then
            For i = 0 To DataGridView1.Rows.Count - 2
                str = ""
                row = DataGridView1.Rows(i)

                For Each cell As DataGridViewCell In row.Cells
                    str = str & " " & CDbl(cell.Value).ToString("F")
                Next
                ThermoDynamicStates.State.Add(row.Index + 1, New ThermoDynamicState(str, cmbothermostates.SelectedIndex))
            Next
        Else
            For i = 0 To DataGridView1.Rows.Count - 2
                str = ""
                row = DataGridView1.Rows(i)
                Dim cell As DataGridViewCell
                For j = 0 To row.Cells.Count - 2
                    cell = row.Cells(j)
                    str = str & " " & CDbl(cell.Value).ToString("F").ToString
                Next
                For j = row.Cells.Count - 1 To 4
                    str = str & " 0.0"
                Next
                str = str & " " & row.Cells(row.Cells.Count - 1).Value
                ThermoDynamicStates.State.Add(row.Index + 1, New ThermoDynamicState(str, cmbothermostates.SelectedIndex))
            Next
        End If
    End Sub

    Private Function FlowsheetDesignSurface() As Object
        Throw New NotImplementedException
    End Function

End Class
