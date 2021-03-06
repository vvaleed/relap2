﻿Imports Microsoft.Msdn.Samples.GraphicObjects
Imports System.Collections.Generic
Imports System.ComponentModel
Imports PropertyGridEx
Imports WeifenLuo.WinFormsUI.Docking
Imports LukeSw.Windows.Forms
'Imports RELAP.RELAP.Flowsheet.FlowsheetSolver
'Imports RELAP.RELAP.SimulationObjects
Imports System.Drawing.Drawing2D

Public Class frmSurface
    Inherits DockContent

    Private m_connecting As Boolean = False
    Private m_stPoint As New Point

    Private ChildParent As FormFlowsheet
    Public PGEx2 As PropertyGridEx.PropertyGridEx
    Public PGEx1 As PropertyGridEx.PropertyGridEx

    Public m_startobj, m_endobj As GraphicObject
    Public uid As String
    Public m_qt As RELAP.GraphicObjects.QuickTableGraphic

    Public ticks As Integer

    Public Function ReturnForm(ByVal str As String) As IDockContent

        If str = Me.ToString Then
            Return Me
        Else
            Return Nothing
        End If

    End Function

    Public Sub SetupGraphicsSurface()
        'load up the design surface with the default bounds and margins
        Dim defSettings As Printing.PageSettings = _
            designSurfacePrintDocument.DefaultPageSettings
        With defSettings

            Dim bounds As Rectangle = .Bounds
            Dim horizRes As Integer = .PrinterResolution.X

            Dim vertRes As Integer = .PrinterResolution.Y

            Me.FlowsheetDesignSurface.SurfaceBounds = bounds

            'Me.FlowsheetDesignSurface.GridSize = 50
            Me.FlowsheetDesignSurface.SurfaceMargins = _
                New Rectangle(bounds.Left + .Margins.Left, _
                    bounds.Top + .Margins.Top, _
                    bounds.Width - .Margins.Left - .Margins.Right, _
                    bounds.Height - .Margins.Top - .Margins.Bottom)
        End With


    End Sub

    Private Sub designSurfacePrintDocument_PrintPage(ByVal sender As System.Object, _
            ByVal e As System.Drawing.Printing.PrintPageEventArgs) _
            Handles designSurfacePrintDocument.PrintPage
        Dim drawobj As GraphicObjectCollection = Me.FlowsheetDesignSurface.drawingObjects
        Me.FlowsheetDesignSurface.drawingObjects.PrintObjects(e.Graphics, -FlowsheetDesignSurface.HorizontalScroll.Value, -FlowsheetDesignSurface.VerticalScroll.Value)
    End Sub

    Private Sub frmSurface_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        uid = 0
        ChildParent = Me.ParentForm
        PGEx1 = Me.ChildParent.FormProps.PGEx1
        PGEx2 = Me.ChildParent.FormProps.PGEx2

        'If Not ChildParent.Options.SelectedPropertyPackage Is Nothing Then Me.LabelPP.Text = "PP: " & ChildParent.Options.SelectedPropertyPackage.ComponentName

    End Sub

    '
    '
    '
    '
    '   UPDATE SELECTED OBJECT
    '
    '
    '
    '

    Public Sub UpdateSelectedObject()

        If Not Me.FlowsheetDesignSurface.SelectedObject Is Nothing Then
            ChildParent.FormProps.SuspendLayout()
            If Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.GO_Tabela Then
                ChildParent.FormProps.LblNomeObj.Text = RELAP.App.GetLocalString("Tabela")
                ChildParent.FormProps.LblTipoObj.Text = RELAP.App.GetLocalString("TabeladeDados")
             
            ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.GO_MasterTable Then
                ChildParent.FormProps.LblNomeObj.Text = RELAP.App.GetLocalString("MasterTable")
                ChildParent.FormProps.LblTipoObj.Text = RELAP.App.GetLocalString("MasterTable")
              
            ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.GO_Figura Then
                ChildParent.FormProps.LblNomeObj.Text = RELAP.App.GetLocalString("Figura")
                ChildParent.FormProps.LblTipoObj.Text = RELAP.App.GetLocalString("ImagemBitmap")
               
            ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.GO_Texto Then
                ChildParent.FormProps.LblNomeObj.Text = RELAP.App.GetLocalString("Texto")
                ChildParent.FormProps.LblTipoObj.Text = RELAP.App.GetLocalString("CaixadeTexto")
              
            Else
                '   Dim nodes = ChildParent.FormObjList.TreeViewObj.Nodes.Find(Me.FlowsheetDesignSurface.SelectedObject.Tag, True)
                ChildParent.FormProps.LblNomeObj.Text = Me.FlowsheetDesignSurface.SelectedObject.Tag
                ChildParent.FormProps.LblTipoObj.Text = RELAP.App.GetLocalString(ChildParent.Collections.ObjectCollection.Item(Me.FlowsheetDesignSurface.SelectedObject.Name).Descricao)
              
            End If
            ChildParent.PopulatePGEx2(Me.FlowsheetDesignSurface.SelectedObject)
            Try
                If Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.GO_Tabela Then
                    CType(Me.FlowsheetDesignSurface.SelectedObject, RELAP.GraphicObjects.TableGraphic).PopulateGrid(PGEx1)
                ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.GO_MasterTable Then
                    CType(Me.FlowsheetDesignSurface.SelectedObject, RELAP.GraphicObjects.MasterTableGraphic).PopulateGrid(PGEx1, ChildParent)
                Else
                    ChildParent.Collections.ObjectCollection(Me.FlowsheetDesignSurface.SelectedObject.Name).PopulatePropertyGrid(PGEx1, ChildParent.Options.SelectedUnitSystem)
                End If
               
                ChildParent.FormProps.ResumeLayout()
            Catch ex As Exception
                PGEx1.SelectedObject = Nothing
                VDialog.Show(ex.Message & " - " & ex.StackTrace, RELAP.App.GetLocalString("Erro"), MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                PGEx2.Refresh()
                PGEx1.Refresh()
            End Try

        Else

            PGEx2.SelectedObject = Nothing
            PGEx1.SelectedObject = Nothing

        End If

    End Sub

    Private Sub FlowsheetDesignSurface_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FlowsheetDesignSurface.KeyDown

        If e.KeyCode = Keys.Delete Then
            Call Me.ChildParent.DeleteSelectedObject(sender, e)


        End If

        If Not Me.FlowsheetDesignSurface.SelectedObject Is Nothing Then
            For Each go As GraphicObject In Me.FlowsheetDesignSurface.SelectedObjects.Values
                If e.KeyCode = Keys.Up Then
                    If e.Modifiers = Keys.Control Then
                        go.Y = go.Y - 1
                    Else
                        go.Y = go.Y - 5
                    End If
                ElseIf e.KeyCode = Keys.Down Then
                    If e.Modifiers = Keys.Control Then
                        go.Y = go.Y + 1
                    Else
                        go.Y = go.Y + 5
                    End If
                ElseIf e.KeyCode = Keys.Left Then
                    If e.Modifiers = Keys.Control Then
                        go.X = go.X - 1
                    Else
                        go.X = go.X - 5
                    End If
                ElseIf e.KeyCode = Keys.Right Then
                    If e.Modifiers = Keys.Control Then
                        go.X = go.X + 1
                    Else
                        go.X = go.X + 5
                    End If
                End If
            Next
            Me.FlowsheetDesignSurface.Invalidate()
        Else
            If e.KeyCode = Keys.Up Then
                If Me.FlowsheetDesignSurface.VerticalScroll.Value > 4 * Me.FlowsheetDesignSurface.VerticalScroll.SmallChange Then
                    Me.FlowsheetDesignSurface.VerticalScroll.Value -= 4 * Me.FlowsheetDesignSurface.VerticalScroll.SmallChange
                Else
                    Me.FlowsheetDesignSurface.VerticalScroll.Value = 0
                End If
            ElseIf e.KeyCode = Keys.Down Then
                Me.FlowsheetDesignSurface.VerticalScroll.Value += 4 * Me.FlowsheetDesignSurface.VerticalScroll.SmallChange
            ElseIf e.KeyCode = Keys.Left Then
                If Me.FlowsheetDesignSurface.HorizontalScroll.Value > 4 * Me.FlowsheetDesignSurface.HorizontalScroll.SmallChange Then
                    Me.FlowsheetDesignSurface.HorizontalScroll.Value -= 4 * Me.FlowsheetDesignSurface.HorizontalScroll.SmallChange
                Else
                    Me.FlowsheetDesignSurface.HorizontalScroll.Value = 0
                End If
            ElseIf e.KeyCode = Keys.Right Then
                Me.FlowsheetDesignSurface.HorizontalScroll.Value += 4 * Me.FlowsheetDesignSurface.HorizontalScroll.SmallChange
            End If
            Me.FlowsheetDesignSurface.Invalidate()
            Me.FlowsheetDesignSurface.Invalidate()
        End If

    End Sub

    Private Sub FlowsheetDesignSurface_SelectionChanged(ByVal sender As Object, _
            ByVal e As Microsoft.MSDN.Samples.DesignSurface.SelectionChangedEventArgs) Handles FlowsheetDesignSurface.SelectionChanged

        Try
            If Not e.SelectedObject Is Nothing Then
                If Not e.SelectedObject.IsConnector Then
                    If Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.GO_Tabela Then
                        ChildParent.FormProps.LblNomeObj.Text = RELAP.App.GetLocalString("Tabela")
                        ChildParent.FormProps.LblTipoObj.Text = RELAP.App.GetLocalString("TabeladeDados")
                       
                    ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.GO_MasterTable Then
                        ChildParent.FormProps.LblNomeObj.Text = "MasterTable"
                        ChildParent.FormProps.LblTipoObj.Text = RELAP.App.GetLocalString("MasterTable")
                       
                    ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.GO_Figura Then
                        ChildParent.FormProps.LblNomeObj.Text = RELAP.App.GetLocalString("Figura")
                        ChildParent.FormProps.LblTipoObj.Text = RELAP.App.GetLocalString("ImagemBitmap")
                       
                    ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.GO_Texto Then
                        ChildParent.FormProps.LblNomeObj.Text = RELAP.App.GetLocalString("Texto")
                        ChildParent.FormProps.LblTipoObj.Text = RELAP.App.GetLocalString("CaixadeTexto")
                      
                    Else
                        '   Dim nodes = ChildParent.FormObjList.TreeViewObj.Nodes.Find(e.SelectedObject.Tag, True)
                        If Me.FlowsheetDesignSurface.SelectedObjects.Count < 2 Then
                            ChildParent.FormProps.LblNomeObj.Text = e.SelectedObject.Tag
                            ChildParent.FormProps.LblTipoObj.Text = RELAP.App.GetLocalString(ChildParent.Collections.ObjectCollection.Item(e.SelectedObject.Name).Descricao)
                           
                        End If
                        End If
                        If Not Me.FlowsheetDesignSurface.SelectedObject Is Nothing Then
                        If Me.FlowsheetDesignSurface.SelectedObject.IsConnector = False And Me.FlowsheetDesignSurface.SelectedObjects.Count < 2 Then
                            ChildParent.PopulatePGEx2(Me.FlowsheetDesignSurface.SelectedObject)
                            Try
                                If Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.GO_Tabela Then
                                    CType(Me.FlowsheetDesignSurface.SelectedObject, RELAP.GraphicObjects.TableGraphic).PopulateGrid(PGEx1)
                                ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.GO_MasterTable Then
                                    CType(Me.FlowsheetDesignSurface.SelectedObject, RELAP.GraphicObjects.MasterTableGraphic).PopulateGrid(PGEx1, ChildParent)
                                Else
                                    My.Application.ActiveSimulation.ComponentType = ""
                                    If Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.Pipe Then
                                        My.Application.ActiveSimulation.ComponentType = "pipe"
                                    ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.Annulus Then
                                        My.Application.ActiveSimulation.ComponentType = "Annulus"
                                    ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.SingleVolume Then
                                        My.Application.ActiveSimulation.ComponentType = "SingleVolume"
                                    ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.Branch Then
                                        My.Application.ActiveSimulation.ComponentType = "Branch"
                                    ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.Separator Then
                                        My.Application.ActiveSimulation.ComponentType = "Separator"
                                    ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.Pump Then
                                        My.Application.ActiveSimulation.ComponentType = "Pump"


                                    ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.SingleJunction Then
                                        For Each kvp In My.Application.ActiveSimulation.Collections.CLCS_SingleJunctionCollection
                                            If kvp.Value.GraphicObject.Name = Me.FlowsheetDesignSurface.SelectedObject.Name Then
                                                My.Application.ActiveSimulation.FromComponent = kvp.Value.FromComponent
                                                My.Application.ActiveSimulation.ToComponent = kvp.Value.ToComponent
                                            End If
                                        Next

                                    ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.TimeDependentJunction Then
                                        For Each kvp In My.Application.ActiveSimulation.Collections.CLCS_TimeDependentJunctionCollection
                                            If kvp.Value.GraphicObject.Name = Me.FlowsheetDesignSurface.SelectedObject.Name Then
                                                My.Application.ActiveSimulation.FromComponent = kvp.Value.FromComponent
                                                My.Application.ActiveSimulation.ToComponent = kvp.Value.ToComponent
                                            End If
                                        Next


                                    End If
                                    ChildParent.Collections.ObjectCollection(Me.FlowsheetDesignSurface.SelectedObject.Name).PopulatePropertyGrid(PGEx1, ChildParent.Options.SelectedUnitSystem)
                                End If
                                ChildParent.FormProps.ResumeLayout()
                            Catch ex As Exception
                                PGEx1.SelectedObject = Nothing
                            Finally
                                ChildParent.FormSurface.Select()
                            End Try
                        Else
                            Me.FlowsheetDesignSurface.SelectedObject = Nothing
                        End If
                    Else
                        PGEx2.SelectedObject = Nothing
                        PGEx1.SelectedObject = Nothing
                    End If
                    PGEx2.Refresh()
                    PGEx1.Refresh()
                Else
                    ChildParent.FormProps.LblNomeObj.Text = RELAP.App.GetLocalString("Nenhumselecionado")
                    ChildParent.FormProps.LblTipoObj.Text = "-"
                   
                    'ChildParent.FormObjList.TreeViewObj.CollapseAll()
                    'ChildParent.FormObjList.TreeViewObj.SelectedNode = Nothing
                End If
            Else
                PGEx2.SelectedObject = Nothing
                PGEx1.SelectedObject = Nothing
            End If
        Catch
            'ChildParent.FormObjList.TreeViewObj.CollapseAll()
            'ChildParent.FormObjList.TreeViewObj.SelectedNode = Nothing
        End Try
        If Me.FlowsheetDesignSurface.SelectedObject Is Nothing Then
            ChildParent.FormProps.LblNomeObj.Text = RELAP.App.GetLocalString("Nenhumselecionado")
            ChildParent.FormProps.LblTipoObj.Text = "-"
           
        End If
    End Sub

    Private Sub ToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
        If Me.FlowsheetDesignSurface.SelectedObject.Rotation + 90 >= 360 Then
            Me.FlowsheetDesignSurface.SelectedObject.Rotation = Math.Truncate((Me.FlowsheetDesignSurface.SelectedObject.Rotation + 90) / 360)
        Else
            Me.FlowsheetDesignSurface.SelectedObject.Rotation = Me.FlowsheetDesignSurface.SelectedObject.Rotation + 90
        End If
        Me.FlowsheetDesignSurface.Invalidate()
    End Sub

    Private Sub BToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BToolStripMenuItem.Click
        If Me.FlowsheetDesignSurface.SelectedObject.Rotation + 180 >= 360 Then
            Me.FlowsheetDesignSurface.SelectedObject.Rotation = Math.Truncate((Me.FlowsheetDesignSurface.SelectedObject.Rotation + 180) / 360)
        Else
            Me.FlowsheetDesignSurface.SelectedObject.Rotation = Me.FlowsheetDesignSurface.SelectedObject.Rotation + 180
        End If
        Me.FlowsheetDesignSurface.Invalidate()
    End Sub

    Private Sub ToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem7.Click
        If Me.FlowsheetDesignSurface.SelectedObject.Rotation + 270 >= 360 Then
            Me.FlowsheetDesignSurface.SelectedObject.Rotation = Math.Truncate((Me.FlowsheetDesignSurface.SelectedObject.Rotation + 270) / 360)
        Else
            Me.FlowsheetDesignSurface.SelectedObject.Rotation = Me.FlowsheetDesignSurface.SelectedObject.Rotation + 270
        End If
        Me.FlowsheetDesignSurface.Invalidate()
    End Sub

    Private Sub FlowsheetDesignSurface_StatusUpdate(ByVal sender As Object, ByVal e As Microsoft.MSDN.Samples.DesignSurface.StatusUpdateEventArgs) Handles FlowsheetDesignSurface.StatusUpdate
        ChildParent.TSTBZoom.Text = Format(FlowsheetDesignSurface.Zoom, "#%")

    End Sub

    Private Sub FlowsheetDesignSurface_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles FlowsheetDesignSurface.MouseDown

        If e.Button = Windows.Forms.MouseButtons.Left Then

            If Me.FlowsheetDesignSurface.QuickConnect And My.Computer.Keyboard.CtrlKeyDown Then
                Dim mousePT As New Point(ChildParent.gscTogoc(e.X, e.Y))
                Dim mpx = mousePT.X
                Dim mpy = mousePT.Y
                Me.m_stPoint = mousePT
                Dim myCTool As New ConnectToolGraphic(mousePT)
                myCTool.Name = "CTOOL1234567890"
                myCTool.Width = mousePT.X
                myCTool.Height = mousePT.Y
                Me.m_startobj = Me.FlowsheetDesignSurface.drawingObjects.FindObjectAtPoint(mousePT)
                Me.FlowsheetDesignSurface.drawingObjects.Add(myCTool)
                Me.m_connecting = True
            Else
                Me.FlowsheetDesignSurface.SelectRectangle = True
            End If

            Me.FlowsheetDesignSurface.Invalidate()

        End If


    End Sub

    Public Sub FlowsheetDesignSurface_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles FlowsheetDesignSurface.MouseUp

        If Me.m_connecting Then

            Me.m_connecting = False

            Me.FlowsheetDesignSurface.drawingObjects.Remove(Me.FlowsheetDesignSurface.drawingObjects.FindObjectWithName("CTOOL1234567890"))

            Me.m_endobj = Me.FlowsheetDesignSurface.drawingObjects.FindObjectAtPoint(ChildParent.gscTogoc(e.X, e.Y))

            Me.FlowsheetDesignSurface.SelectRectangle = True
            Me.FlowsheetDesignSurface.Invalidate()

            If Not m_startobj Is Nothing And Not m_endobj Is Nothing Then
                If m_startobj.Name <> m_endobj.Name Then Call ChildParent.ConnectObject(Me.m_startobj, Me.m_endobj)

            End If
        End If

        If e.Button = Windows.Forms.MouseButtons.Left Then

            If Not ChildParent.ClickedToolStripMenuItem Is Nothing Then
                If ChildParent.InsertingObjectToPFD Then

                    Dim gObj As GraphicObject = Nothing
                    Dim fillclr As Color = Color.WhiteSmoke
                    Dim lineclr As Color = Color.Red
                    Dim mousePT As Point = ChildParent.gscTogoc(e.X, e.Y)
                    Dim mpx = mousePT.X '- SplitContainer1.SplitterDistance
                    Dim mpy = mousePT.Y '- ToolStripContainer1.TopToolStripPanel.Height
                    Dim tobj As TipoObjeto = TipoObjeto.Nenhum

                    Select Case ChildParent.ClickedToolStripMenuItem.Name

              
                        Case "TSMIMixer"
                            tobj = TipoObjeto.NodeIn
                        Case "TSMISplitter"
                            tobj = TipoObjeto.NodeOut
                        Case "TSMICompressor"
                            tobj = TipoObjeto.Compressor
                        Case "TSMIExpander"
                            tobj = TipoObjeto.Expander
                        Case "TSMIPump"
                            tobj = TipoObjeto.Pump
                        Case "TSMIBranch"
                            tobj = TipoObjeto.Branch
                        Case "TSMISeparator"
                            tobj = TipoObjeto.Separator
                        Case "TSMIPipe"
                            tobj = TipoObjeto.Pipe
                        Case "TSMIAnnulus"
                            tobj = TipoObjeto.Annulus
                        Case "TSMIValve"
                            tobj = TipoObjeto.Valve
                        Case "TSMISeparator"
                            tobj = TipoObjeto.Vessel
                        Case "TSMIHeater"
                            tobj = TipoObjeto.Heater
                        Case "TSMISingleVolume"
                            tobj = TipoObjeto.SingleVolume
                        Case "TSMICooler"
                            tobj = TipoObjeto.SingleJunction
                        Case "TSMICooler"
                            tobj = TipoObjeto.TimeDependentJunction

                        Case "TSMIHeatStructure"
                            tobj = TipoObjeto.HeatStructure
                        Case "TSMITank"
                            tobj = TipoObjeto.Tank
                  
                    End Select

                    AddObjectToSurface(tobj, mpx, mpy)

                    ChildParent.ClickedToolStripMenuItem = Nothing
                    ChildParent.InsertingObjectToPFD = False

                End If

            End If

        

        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then

            If Not Me.FlowsheetDesignSurface.SelectedObject Is Nothing Then

                Me.CMS_Sel.Items("TSMI_Label").Text = Me.FlowsheetDesignSurface.SelectedObject.Tag
                Me.CMS_Sel.Show(MousePosition)

            Else

                Me.CMS_NoSel.Show(MousePosition)

            End If

        End If
        Dim frm As FormFlowsheet = Me.ParentForm
        If Me.FlowsheetDesignSurface.SelectedObjects.Count > 1 Then

            frm.tsmiGroupComponents.Visible = True

        Else
            frm.tsmiGroupComponents.Visible = False
        End If

    End Sub

    Private Sub FlowsheetDesignSurface_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles FlowsheetDesignSurface.MouseMove

        Dim px As Point = ChildParent.gscTogoc(e.Location.X, e.Location.Y)
        Dim px2 As Point = ChildParent.gscTogoc(e.Location.X + 20, e.Location.Y + 20)

        If Me.m_connecting Then

            Dim myCTool As ConnectToolGraphic = Me.FlowsheetDesignSurface.drawingObjects.FindObjectWithName("CTOOL1234567890")

            Dim mousePT As New Point(ChildParent.gscTogoc(e.X, e.Y))

            myCTool.Width = mousePT.X
            myCTool.Height = mousePT.Y

            Me.FlowsheetDesignSurface.Invalidate()

        Else

            Dim gobj As GraphicObject = Me.FlowsheetDesignSurface.drawingObjects.FindObjectAtPoint(px)

            If Not gobj Is Nothing Then

                If Me.m_qt Is Nothing And Not _
                    gobj.TipoObjeto = TipoObjeto.GO_TabelaRapida And Not _
                    gobj.TipoObjeto = TipoObjeto.GO_MasterTable And Not _
                    gobj.TipoObjeto = TipoObjeto.GO_Tabela And Not _
                    gobj.TipoObjeto = TipoObjeto.GO_Figura And Not _
                    gobj.TipoObjeto = TipoObjeto.GO_Texto And Not _
                    gobj.TipoObjeto = TipoObjeto.Nenhum Then




                ElseIf gobj.TipoObjeto = TipoObjeto.GO_TabelaRapida Then

                    If Me.FlowsheetDesignSurface.drawingObjects.Contains(Me.m_qt) Then
                        Me.FlowsheetDesignSurface.drawingObjects.Remove(Me.m_qt)
                    End If
                    Me.m_qt = Nothing
                    Me.ticks = 0
                    Me.FlowsheetDesignSurface.Invalidate()

                End If

            Else

                Try
                    If Me.FlowsheetDesignSurface.drawingObjects.Contains(Me.m_qt) Then
                        Me.FlowsheetDesignSurface.drawingObjects.Remove(Me.m_qt)
                    End If
                    Me.m_qt = Nothing
                    Me.ticks = 0
                    Me.FlowsheetDesignSurface.Invalidate()
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try

            End If

        End If

        If Not Me.m_qt Is Nothing Then Me.m_qt.SetPosition(px2)
        Me.FlowsheetDesignSurface.Invalidate()

    End Sub





    Private Sub CMS_Sel_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles CMS_Sel.Opened

        Dim naoLimparListaDeDesconectar As Boolean = False

        Me.CMS_ItemsToDisconnect.Items.Clear()
        Me.CMS_ItemsToConnect.Items.Clear()

        Me.DesconectarDeToolStripMenuItem.Visible = False
        Me.ConectarAToolStripMenuItem.Visible = False


        If Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto <> TipoObjeto.GO_Figura And _
            Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto <> TipoObjeto.GO_Tabela And _
            Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto <> TipoObjeto.GO_MasterTable And _
            Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto <> TipoObjeto.GO_TabelaRapida And _
            Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto <> TipoObjeto.DistillationColumn And _
            Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto <> TipoObjeto.AbsorptionColumn And _
             Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto <> TipoObjeto.ReboiledAbsorber And _
             Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto <> TipoObjeto.RefluxedAbsorber And _
             Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto <> TipoObjeto.HeatStructure And _
            Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto <> TipoObjeto.GO_Texto Then


            Me.ToolStripSeparator6.Visible = True
            Me.TabelaToolStripMenuItem.Visible = True
            Me.ClonarToolStripMenuItem.Visible = True
            Me.ExcluirToolStripMenuItem.Visible = True
            Me.HorizontalmenteToolStripMenuItem.Visible = True
            Try
                Dim obj As SimulationObjects_BaseClass = ChildParent.Collections.ObjectCollection(Me.FlowsheetDesignSurface.SelectedObject.Name)
                If Me.IsObjectDownstreamConnectable(obj.GraphicObject.Tag) Then
                    Dim arr As ArrayList = Me.ReturnDownstreamConnectibles(obj.GraphicObject.Tag)
                    Me.CMS_ItemsToConnect.Items.Clear()
                    If arr.Count <> 0 Then
                        Dim i As Integer = 0
                        Do
                            Me.CMS_ItemsToConnect.Items.Add(arr(i))
                            i = i + 1
                            Me.ConectarAToolStripMenuItem.Visible = True

                            Me.ConectarAToolStripMenuItem.DropDown = Me.CMS_ItemsToConnect
                        Loop Until i = arr.Count
                    End If
                Else
                    Dim arr As ArrayList = Me.ReturnDownstreamDisconnectables(obj.GraphicObject.Tag)
                    Me.CMS_ItemsToDisconnect.Items.Clear()
                    If arr.Count <> 0 Then
                        naoLimparListaDeDesconectar = True
                        Dim i As Integer = 0
                        Do
                            Me.CMS_ItemsToDisconnect.Items.Add(arr(i))
                            i = i + 1
                        Loop Until i = arr.Count
                        Me.DesconectarDeToolStripMenuItem.Visible = True

                        Me.DesconectarDeToolStripMenuItem.DropDown = Me.CMS_ItemsToDisconnect
                    End If
                End If
                If Me.IsObjectUpstreamConnectable(obj.GraphicObject.Tag) = False Then
                    Dim arr As ArrayList = Me.ReturnUpstreamDisconnectables(obj.GraphicObject.Tag)
                    If naoLimparListaDeDesconectar = False Then Me.CMS_ItemsToDisconnect.Items.Clear()
                    If arr.Count <> 0 Then
                        Dim i As Integer = 0
                        Do
                            Me.CMS_ItemsToDisconnect.Items.Add(arr(i))
                            i = i + 1
                        Loop Until i = arr.Count
                        Me.DesconectarDeToolStripMenuItem.Visible = True

                        Me.DesconectarDeToolStripMenuItem.DropDown = Me.CMS_ItemsToDisconnect
                    End If
                End If
                If obj.GraphicObject.FlippedH Then
                    Me.HorizontalmenteToolStripMenuItem.Checked = True
                Else
                    Me.HorizontalmenteToolStripMenuItem.Checked = False
                End If
                If obj.Tabela Is Nothing Then
                    Me.MostrarToolStripMenuItem.Checked = False
                Else
                    Me.MostrarToolStripMenuItem.Checked = True
                End If

                If Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.MaterialStream Then
                    EditCompTSMI.Visible = True
                Else
                    EditCompTSMI.Visible = False
                End If

            Catch ex As Exception
                CMS_Sel.Hide()
            End Try

        ElseIf Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.AbsorptionColumn Or _
        Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.DistillationColumn Or _
        Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.ReboiledAbsorber Or _
        Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.RefluxedAbsorber Then


            Me.ToolStripSeparator6.Visible = True

            Me.ConectarAToolStripMenuItem.Visible = False
            Me.DesconectarDeToolStripMenuItem.Visible = False
            Me.EditCompTSMI.Visible = False

            Me.TabelaToolStripMenuItem.Visible = True
            Me.ClonarToolStripMenuItem.Visible = True
            Me.ExcluirToolStripMenuItem.Visible = True
            Me.HorizontalmenteToolStripMenuItem.Visible = True
            Dim obj As SimulationObjects_BaseClass = ChildParent.Collections.ObjectCollection(Me.FlowsheetDesignSurface.SelectedObject.Name)

            If obj.GraphicObject.FlippedH Then
                Me.HorizontalmenteToolStripMenuItem.Checked = True
            Else
                Me.HorizontalmenteToolStripMenuItem.Checked = False
            End If
            If obj.Tabela Is Nothing Then
                Me.MostrarToolStripMenuItem.Checked = False
            Else
                Me.MostrarToolStripMenuItem.Checked = True
            End If

        Else

            Me.TSMI_Label.Text = "Tabela"
            Me.TabelaToolStripMenuItem.Visible = False
            Me.ClonarToolStripMenuItem.Visible = False
            Me.HorizontalmenteToolStripMenuItem.Visible = False
            Me.ExcluirToolStripMenuItem.Visible = False

            Me.ToolStripSeparator6.Visible = False
            Me.EditCompTSMI.Visible = False

        End If
        'Me.InverterToolStripMenuItem.Visible = False

    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Me.ChildParent = My.Application.ActiveSimulation
        With ChildParent.FormProps
          
            .LblNomeObj.Text = RELAP.App.GetLocalString("Fluxograma")
            .LblTipoObj.Text = "-"
        End With

        ChildParent.FormProps.FTSProps.SelectedItem = ChildParent.FormProps.TSObj

        With ChildParent.FormProps.PGEx2

            .PropertySort = PropertySort.NoSort
            .ShowCustomProperties = True
            Try
                .Item.Clear()
            Catch ex As Exception
            Finally
                .Item.Clear()
            End Try
            .Item.Add(RELAP.App.GetLocalString("Cordofundo"), FlowsheetDesignSurface, "BackColor", False, "", "", True)
            With .Item(.Item.Count - 1)
                .DefaultType = GetType(System.Drawing.Color)
            End With
            .Item.Add(RELAP.App.GetLocalString("Cordagrade"), FlowsheetDesignSurface, "GridColor", False, "", "", True)
            With .Item(.Item.Count - 1)
                .DefaultType = GetType(System.Drawing.Color)
            End With
            .Item.Add(RELAP.App.GetLocalString("Espessuradagrade"), FlowsheetDesignSurface, "GridLineWidth", False, "", "", True)
            .Item.Add(RELAP.App.GetLocalString("SnapToGrid"), FlowsheetDesignSurface, "SnapToGrid", False, "", "", True)
            .Item.Add(RELAP.App.GetLocalString("GridSize"), FlowsheetDesignSurface, "GridSize", False, "", "", True)
            .Item.Add(RELAP.App.GetLocalString("Largura"), FlowsheetDesignSurface.SurfaceBounds, "Width", False, "", "", True)
            .Item.Add(RELAP.App.GetLocalString("Altura"), FlowsheetDesignSurface.SurfaceBounds, "Height", False, "", "", True)
            .Item.Add(RELAP.App.GetLocalString("Larguradeimpresso"), FlowsheetDesignSurface.SurfaceMargins, "Width", False, "", "", True)
            .Item.Add(RELAP.App.GetLocalString("Alturadeimpresso"), FlowsheetDesignSurface.SurfaceMargins, "Height", False, "", "", True)

            .Refresh()
        End With

    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        PreviewDialog.ShowDialog()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Me.ticks += 1
    End Sub

    Private Sub CMS_ItemsToDisconnect_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles CMS_ItemsToDisconnect.ItemClicked

        Call Me.ChildParent.DisconnectObject(Me.FlowsheetDesignSurface.SelectedObject, FormFlowsheet.SearchSurfaceObjectsByTag(e.ClickedItem.Text, Me.FlowsheetDesignSurface))

    End Sub

    Private Sub CMS_ItemsToConnect_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles CMS_ItemsToConnect.ItemClicked

        Call Me.ChildParent.ConnectObject(Me.FlowsheetDesignSurface.SelectedObject, FormFlowsheet.SearchSurfaceObjectsByTag(e.ClickedItem.Text, Me.FlowsheetDesignSurface))

    End Sub

    Function IsObjectDownstreamConnectable(ByVal objTag As String) As Boolean

        Dim obj As GraphicObject = FormFlowsheet.SearchSurfaceObjectsByTag(objTag, Me.FlowsheetDesignSurface)

        If Not obj Is Nothing Then

            Dim cp As ConnectionPoint
            For Each cp In obj.OutputConnectors
                If cp.IsAttached = False Then Return True
            Next

        End If

        Return False

    End Function

    Function IsObjectUpstreamConnectable(ByVal objTag As String) As Boolean

        Dim obj As GraphicObject = FormFlowsheet.SearchSurfaceObjectsByTag(objTag, Me.FlowsheetDesignSurface)

        If Not obj Is Nothing Then

            Dim cp As ConnectionPoint
            For Each cp In obj.InputConnectors
                If cp.IsAttached = False Then Return True
            Next

        End If

        Return False

    End Function

    Function ReturnDownstreamConnectibles(ByVal objtag As String)

        Dim refobj As GraphicObject = FormFlowsheet.SearchSurfaceObjectsByTag(objtag, Me.FlowsheetDesignSurface)

        Dim obj As SimulationObjects_BaseClass
        Dim cp As ConnectionPoint

        Dim conables As New ArrayList

        For Each obj In Me.ChildParent.Collections.ObjectCollection.Values
            If obj.GraphicObject.Tag <> refobj.Tag Then
                If obj.GraphicObject.TipoObjeto <> TipoObjeto.GO_Texto And _
                    obj.GraphicObject.TipoObjeto <> TipoObjeto.GO_TabelaRapida And _
                    obj.GraphicObject.TipoObjeto <> TipoObjeto.GO_MasterTable And _
                    obj.GraphicObject.TipoObjeto <> TipoObjeto.GO_Tabela And _
                    obj.GraphicObject.TipoObjeto <> TipoObjeto.OT_Ajuste And _
                    obj.GraphicObject.TipoObjeto <> TipoObjeto.OT_Especificacao And _
                    obj.GraphicObject.TipoObjeto <> TipoObjeto.DistillationColumn And _
                    obj.GraphicObject.TipoObjeto <> TipoObjeto.AbsorptionColumn And _
                    obj.GraphicObject.TipoObjeto <> TipoObjeto.RefluxedAbsorber And _
                    obj.GraphicObject.TipoObjeto <> TipoObjeto.ReboiledAbsorber And _
                    obj.GraphicObject.TipoObjeto <> TipoObjeto.Nenhum Then

                    If refobj.TipoObjeto = TipoObjeto.MaterialStream Then
                        For Each cp In obj.GraphicObject.InputConnectors
                            If Not cp.IsAttached And Not conables.Contains(obj.GraphicObject.Tag) And Not _
                            obj.GraphicObject.TipoObjeto = TipoObjeto.MaterialStream And Not _
                            obj.GraphicObject.TipoObjeto = TipoObjeto.EnergyStream And _
                            cp.Type = ConType.ConIn Then conables.Add(obj.GraphicObject.Tag)
                        Next
                    ElseIf refobj.TipoObjeto = TipoObjeto.EnergyStream Then
                        If obj.GraphicObject.TipoObjeto <> TipoObjeto.Heater And _
                        obj.GraphicObject.TipoObjeto <> TipoObjeto.Pump And _
                        obj.GraphicObject.TipoObjeto <> TipoObjeto.Compressor And _
                        obj.GraphicObject.TipoObjeto <> TipoObjeto.MaterialStream Then
                            cp = obj.GraphicObject.EnergyConnector
                            If Not cp.IsAttached And Not conables.Contains(obj.GraphicObject.Tag) Then conables.Add(obj.GraphicObject.Tag)
                        ElseIf obj.GraphicObject.TipoObjeto = TipoObjeto.MaterialStream Then

                        Else
                            For Each cp In obj.GraphicObject.InputConnectors
                                If Not cp.IsAttached And Not conables.Contains(obj.GraphicObject.Tag) And Not _
                                obj.GraphicObject.TipoObjeto = TipoObjeto.MaterialStream And Not _
                                obj.GraphicObject.TipoObjeto = TipoObjeto.EnergyStream And _
                                cp.Type = ConType.ConEn Then conables.Add(obj.GraphicObject.Tag)
                            Next
                        End If
                    Else
                        For Each cp In obj.GraphicObject.InputConnectors
                            If Not cp.IsAttached And Not conables.Contains(obj.GraphicObject.Tag) Then conables.Add(obj.GraphicObject.Tag)
                        Next
                        If obj.GraphicObject.TipoObjeto = TipoObjeto.MaterialStream Then
                            cp = obj.GraphicObject.InputConnectors(0)
                            If Not cp.IsAttached And Not conables.Contains(obj.GraphicObject.Tag) Then conables.Add(obj.GraphicObject.Tag)
                        ElseIf obj.GraphicObject.TipoObjeto = TipoObjeto.EnergyStream Then
                            cp = obj.GraphicObject.InputConnectors(0)
                            If Not cp.IsAttached And Not refobj.EnergyConnector.IsAttached And Not conables.Contains(obj.GraphicObject.Tag) Then conables.Add(obj.GraphicObject.Tag)
                        End If
                    End If
                End If
            End If
        Next

        Return conables

    End Function

    Function ReturnDownstreamDisconnectables(ByVal objTag As String) As ArrayList

        Dim obj As GraphicObject = FormFlowsheet.SearchSurfaceObjectsByTag(objTag, Me.FlowsheetDesignSurface)

        Dim conables As New ArrayList

        If Not obj Is Nothing Then

            Dim cp As ConnectionPoint
            For Each cp In obj.OutputConnectors
                If cp.AttachedConnector.AttachedTo.TipoObjeto <> TipoObjeto.AbsorptionColumn And cp.AttachedConnector.AttachedTo.TipoObjeto <> TipoObjeto.DistillationColumn And _
                cp.AttachedConnector.AttachedTo.TipoObjeto <> TipoObjeto.RefluxedAbsorber And cp.AttachedConnector.AttachedTo.TipoObjeto <> TipoObjeto.ReboiledAbsorber Then
                    If cp.IsAttached = True And Not conables.Contains(cp.AttachedConnector.AttachedTo.Tag) Then conables.Add(cp.AttachedConnector.AttachedTo.Tag)
                End If
            Next
        End If

        Return conables

    End Function

    Function ReturnUpstreamDisconnectables(ByVal objTag As String) As ArrayList

        Dim obj As GraphicObject = FormFlowsheet.SearchSurfaceObjectsByTag(objTag, Me.FlowsheetDesignSurface)

        Dim conables As New ArrayList

        If Not obj Is Nothing Then

            Dim cp As ConnectionPoint
            For Each cp In obj.InputConnectors
                If cp.AttachedConnector.AttachedTo.TipoObjeto <> TipoObjeto.AbsorptionColumn And cp.AttachedConnector.AttachedTo.TipoObjeto <> TipoObjeto.DistillationColumn And _
                cp.AttachedConnector.AttachedTo.TipoObjeto <> TipoObjeto.RefluxedAbsorber And cp.AttachedConnector.AttachedTo.TipoObjeto <> TipoObjeto.ReboiledAbsorber Then
                    If cp.IsAttached = True And Not conables.Contains(cp.AttachedConnector.AttachedFrom.Tag) Then conables.Add(cp.AttachedConnector.AttachedFrom.Tag)
                End If
            Next

        End If

        Return conables

    End Function



    Private Sub ExcluirToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcluirToolStripMenuItem.Click
        Call Me.ChildParent.DeleteSelectedObject(sender, e)
    End Sub

    Private Sub HorizontalmenteToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HorizontalmenteToolStripMenuItem.Click
        If Me.HorizontalmenteToolStripMenuItem.Checked Then
            Me.FlowsheetDesignSurface.SelectedObject.FlippedH = True
        Else
            Me.FlowsheetDesignSurface.SelectedObject.FlippedH = False
        End If
        Me.FlowsheetDesignSurface.Invalidate()
    End Sub
    Public Function AddSubsytemToSurface(ByVal x As Integer, ByVal y As Integer, Optional ByVal tag As String = "") As String
        ChildParent = My.Application.ActiveSimulation

        If ChildParent.Collections.ObjectCounter Is Nothing Then ChildParent.Collections.InitializeCounter()

        Dim gObj As GraphicObject = Nothing
        Dim fillclr As Color = Color.WhiteSmoke
        Dim lineclr As Color = Color.Red
        Dim mpx = x '- SplitContainer1.SplitterDistance
        Dim mpy = y '- ToolStripContainer1.TopToolStripPanel.Height

        Dim myTank As New SubSystemGraphic(mpx, mpy, 50, 50, 0)
        myTank.LineWidth = 2
        myTank.Fill = True
        myTank.FillColor = fillclr
        myTank.LineColor = lineclr
        myTank.Tag = "SS" & Format(ChildParent.Collections.ObjectCounter("Subsystem"), "00#")
        ChildParent.Collections.UpdateCounter("Subsystem")
        If tag <> "" Then myTank.Tag = tag
        gObj = myTank
        gObj.Name = "SS-" & Guid.NewGuid.ToString
        ChildParent.Collections.SubSystemCollection.Add(gObj.Name, myTank)
       
        Dim myCOTK As RELAP.SimulationObjects.UnitOps.Subsystem = New RELAP.SimulationObjects.UnitOps.Subsystem(myTank.Name, "Subsystem")
        myCOTK.GraphicObject = myTank
        ChildParent.Collections.ObjectCollection.Add(myTank.Name, myCOTK)
        ChildParent.Collections.CLCS_SubSystemCollection.Add(myTank.Name, myCOTK)
        If Not gObj Is Nothing Then
            Me.FlowsheetDesignSurface.drawingObjects.Add(gObj)
            Me.FlowsheetDesignSurface.Invalidate()
            Application.DoEvents()
            Dim arrays(ChildParent.Collections.ObjectCollection.Count - 1) As String
            '   Dim aNode, aNode2 As TreeNode
            Dim i As Integer = 0
           
        End If

        Me.FlowsheetDesignSurface.Cursor = Cursors.Arrow


        Return gObj.Name
    End Function
    Public Function AddObjectToSurface(ByVal type As TipoObjeto, ByVal x As Integer, ByVal y As Integer, Optional ByVal tag As String = "") As String

        ChildParent = My.Application.ActiveSimulation

        If ChildParent.Collections.ObjectCounter Is Nothing Then ChildParent.Collections.InitializeCounter()

        Dim gObj As GraphicObject = Nothing
        Dim fillclr As Color = Color.WhiteSmoke
        Dim lineclr As Color = Color.Red
        Dim mpx = x '- SplitContainer1.SplitterDistance
        Dim mpy = y '- ToolStripContainer1.TopToolStripPanel.Height

        Select Case type
            Case TipoObjeto.Tank
                Dim myTank As New TankGraphic(mpx, mpy, 50, 50, 0)
                myTank.LineWidth = 2
                myTank.Fill = True
                myTank.FillColor = fillclr
                myTank.LineColor = lineclr
                myTank.Tag = "TDV" & Format(ChildParent.Collections.ObjectCounter("TANK"), "00#")
                ChildParent.Collections.UpdateCounter("TANK")
                If tag <> "" Then myTank.Tag = tag
                gObj = myTank
                gObj.Name = "TQ-" & Guid.NewGuid.ToString
                ChildParent.Collections.TankCollection.Add(gObj.Name, myTank)
               
                Dim myCOTK As RELAP.SimulationObjects.UnitOps.Tank = New RELAP.SimulationObjects.UnitOps.Tank(myTank.Name, "Tanque")
                myCOTK.GraphicObject = myTank
                ChildParent.Collections.ObjectCollection.Add(myTank.Name, myCOTK)
                ChildParent.Collections.CLCS_TankCollection.Add(myTank.Name, myCOTK)
            Case TipoObjeto.SingleVolume
                Dim myVolume As New SingleVolumeGraphic(mpx, mpy, 50, 40, 0)
                myVolume.LineWidth = 2
                myVolume.Fill = True
                myVolume.FillColor = fillclr
                myVolume.LineColor = lineclr
                myVolume.Tag = "SV" & Format(ChildParent.Collections.ObjectCounter("SingleVolume"), "00#")
                ChildParent.Collections.UpdateCounter("SingleVolume")
                If tag <> "" Then myVolume.Tag = tag
                gObj = myVolume
                gObj.Name = "SNGLVOL-" & Guid.NewGuid.ToString
                ChildParent.Collections.SingleVolumeCollection.Add(gObj.Name, myVolume)
               
                Dim myCOTK As RELAP.SimulationObjects.UnitOps.SingleVolume = New RELAP.SimulationObjects.UnitOps.SingleVolume(myVolume.Name, "SingleVolume")
                myCOTK.GraphicObject = myVolume
                ChildParent.Collections.ObjectCollection.Add(myVolume.Name, myCOTK)
                ChildParent.Collections.CLCS_SingleVolumeCollection.Add(myVolume.Name, myCOTK)
            Case TipoObjeto.Accumulator
                Dim myVolume As New SubSystemGraphic(mpx, mpy, 50, 40, 0)
                myVolume.LineWidth = 2
                myVolume.Fill = True
                myVolume.FillColor = fillclr
                myVolume.LineColor = lineclr
                myVolume.Tag = "ACC" & Format(ChildParent.Collections.ObjectCounter("Accumulator"), "00#")
                ChildParent.Collections.UpdateCounter("Accumulator")
                If tag <> "" Then myVolume.Tag = tag
                gObj = myVolume
                gObj.Name = "ACCUM-" & Guid.NewGuid.ToString
                ChildParent.Collections.AccumulatorCollection.Add(gObj.Name, myVolume)

                Dim myCOTK As RELAP.SimulationObjects.UnitOps.Accumulator = New RELAP.SimulationObjects.UnitOps.Accumulator(myVolume.Name, "Accumulator")
                myCOTK.GraphicObject = myVolume
                ChildParent.Collections.ObjectCollection.Add(myVolume.Name, myCOTK)
                ChildParent.Collections.CLCS_AccumulatorCollection.Add(myVolume.Name, myCOTK)
            Case TipoObjeto.Turbine
                Dim myVolume As New TurbineGraphic(mpx, mpy, 50, 40, 0)
                myVolume.LineWidth = 2
                myVolume.Fill = True
                myVolume.FillColor = fillclr
                myVolume.LineColor = lineclr
                myVolume.Tag = "TRB" & Format(ChildParent.Collections.ObjectCounter("Turbine"), "00#")
                ChildParent.Collections.UpdateCounter("Turbine")
                If tag <> "" Then myVolume.Tag = tag
                gObj = myVolume
                gObj.Name = "TURBINE-" & Guid.NewGuid.ToString
                ChildParent.Collections.TurbineCollection.Add(gObj.Name, myVolume)

                Dim myCOTK As RELAP.SimulationObjects.UnitOps.Turbine = New RELAP.SimulationObjects.UnitOps.Turbine(myVolume.Name, "Turbine")
                myCOTK.GraphicObject = myVolume
                ChildParent.Collections.ObjectCollection.Add(myVolume.Name, myCOTK)
                ChildParent.Collections.CLCS_TurbineCollection.Add(myVolume.Name, myCOTK)
            Case TipoObjeto.Pump
                Dim myPump As New PumpGraphic(mpx, mpy, 50, 50, 0)
                myPump.LineWidth = 2
                myPump.Fill = True
                myPump.FillColor = fillclr
                myPump.LineColor = lineclr
                myPump.Tag = "PUMP" & Format(ChildParent.Collections.ObjectCounter("Pump"), "00#")
                ChildParent.Collections.UpdateCounter("Pump")
                If tag <> "" Then myPump.Tag = tag
                gObj = myPump
                gObj.Name = "PUMP-" & Guid.NewGuid.ToString
                ChildParent.Collections.PumpCollection.Add(gObj.Name, myPump)
                
                Dim myCOTK As RELAP.SimulationObjects.UnitOps.Pump = New RELAP.SimulationObjects.UnitOps.Pump(myPump.Name, "Bomba")
                myCOTK.GraphicObject = myPump
                ChildParent.Collections.ObjectCollection.Add(myPump.Name, myCOTK)
                ChildParent.Collections.CLCS_PumpCollection.Add(myPump.Name, myCOTK)
            Case TipoObjeto.SingleJunction
                Dim myCooler As New SingleJunctionGraphic(mpx, mpy, 15, 15, 0)
                myCooler.LineWidth = 2
                myCooler.Fill = True
                myCooler.FillColor = fillclr
                myCooler.LineColor = lineclr
                myCooler.Tag = "SJ" & Format(ChildParent.Collections.ObjectCounter("SingleJunction"), "00#")
                ChildParent.Collections.UpdateCounter("SingleJunction")
                If tag <> "" Then myCooler.Tag = tag
                gObj = myCooler
                gObj.Name = "SNGLJUN-" & Guid.NewGuid.ToString
                ChildParent.Collections.SingleJunctionCollection.Add(gObj.Name, myCooler)
               
                Dim myCOTK As RELAP.SimulationObjects.UnitOps.SingleJunction = New RELAP.SimulationObjects.UnitOps.SingleJunction(myCooler.Name, "SingleJunction")
                myCOTK.GraphicObject = myCooler
                ChildParent.Collections.ObjectCollection.Add(myCooler.Name, myCOTK)
                ChildParent.Collections.CLCS_SingleJunctionCollection.Add(myCooler.Name, myCOTK)

            Case TipoObjeto.TimeDependentJunction
                Dim myTMDPJUN As New TimeDependentJunctionGraphic(mpx, mpy, 15, 20, 0)
                myTMDPJUN.LineWidth = 2
                myTMDPJUN.Fill = True
                myTMDPJUN.FillColor = fillclr
                myTMDPJUN.LineColor = lineclr
                myTMDPJUN.Tag = "TDJ" & Format(ChildParent.Collections.ObjectCounter("TimeDependentJunction"), "00#")
                ChildParent.Collections.UpdateCounter("TimeDependentJunction")
                If tag <> "" Then myTMDPJUN.Tag = tag
                gObj = myTMDPJUN
                gObj.Name = "TMDPJUN-" & Guid.NewGuid.ToString
                ChildParent.Collections.TimeDependentJunctionCollection.Add(gObj.Name, myTMDPJUN)
                
                Dim myCOTK As RELAP.SimulationObjects.UnitOps.TimeDependentJunction = New RELAP.SimulationObjects.UnitOps.TimeDependentJunction(myTMDPJUN.Name, "TimeDependentJunction")
                myCOTK.GraphicObject = myTMDPJUN
                ChildParent.Collections.ObjectCollection.Add(myTMDPJUN.Name, myCOTK)
                ChildParent.Collections.CLCS_TimeDependentJunctionCollection.Add(myTMDPJUN.Name, myCOTK)

            Case TipoObjeto.HeatStructure
                Dim myHeatStructure As New HeatStructureGraphic(mpx, mpy, 50, 8, 0)
                myHeatStructure.LineWidth = 2
                myHeatStructure.Fill = True
                myHeatStructure.FillColor = fillclr
                myHeatStructure.LineColor = lineclr
                myHeatStructure.Tag = "HS" & Format(ChildParent.Collections.ObjectCounter("HeatStructure"), "00#")
                ChildParent.Collections.UpdateCounter("HeatStructure")
                If tag <> "" Then myHeatStructure.Tag = tag
                gObj = myHeatStructure
                gObj.Name = "HEATST-" & Guid.NewGuid.ToString
                ChildParent.Collections.HeatStructureCollection.Add(gObj.Name, myHeatStructure)
               
                Dim myCOTK As RELAP.SimulationObjects.UnitOps.HeatStructure = New RELAP.SimulationObjects.UnitOps.HeatStructure(myHeatStructure.Name, "HeatStructure")
                myCOTK.GraphicObject = myHeatStructure
                ChildParent.Collections.ObjectCollection.Add(myHeatStructure.Name, myCOTK)
                ChildParent.Collections.CLCS_HeatStructureCollection.Add(myHeatStructure.Name, myCOTK)
            Case TipoObjeto.branch
                Dim mybranch As New BranchGraphic(mpx, mpy, 50, 50, 0)
                mybranch.LineWidth = 2
                mybranch.Fill = True

                mybranch.FillColor = fillclr
                mybranch.LineColor = lineclr
                mybranch.Tag = "BR" & Format(ChildParent.Collections.ObjectCounter("Branch"), "00#")
                ChildParent.Collections.UpdateCounter("Branch")
                If tag <> "" Then mybranch.Tag = tag
                gObj = mybranch
                gObj.Name = "BRANCH" & Guid.NewGuid.ToString
                ChildParent.Collections.BranchCollection.Add(gObj.Name, mybranch)
                
                Dim myCOTK As RELAP.SimulationObjects.UnitOps.Branch = New RELAP.SimulationObjects.UnitOps.Branch(mybranch.Name, "Branch")
                myCOTK.GraphicObject = mybranch
                ChildParent.Collections.ObjectCollection.Add(mybranch.Name, myCOTK)
                ChildParent.Collections.CLCS_BranchCollection.Add(mybranch.Name, myCOTK)
            Case TipoObjeto.Separator
                Dim mySeperator As New SeparatorGraphic(mpx, mpy, 50, 50, 0)
                mySeperator.LineWidth = 2
                mySeperator.Fill = True

                mySeperator.FillColor = fillclr
                mySeperator.LineColor = lineclr
                mySeperator.Tag = "SP" & Format(ChildParent.Collections.ObjectCounter("Separator"), "00#")
                ChildParent.Collections.UpdateCounter("Separator")
                If tag <> "" Then mySeperator.Tag = tag
                gObj = mySeperator
                gObj.Name = "SEPARATOR" & Guid.NewGuid.ToString
                ChildParent.Collections.SeparatorCollection.Add(gObj.Name, mySeperator)

                Dim myCOTK As RELAP.SimulationObjects.UnitOps.Separator = New RELAP.SimulationObjects.UnitOps.Separator(mySeperator.Name, "Separator")
                myCOTK.GraphicObject = mySeperator
                ChildParent.Collections.ObjectCollection.Add(mySeperator.Name, myCOTK)
                ChildParent.Collections.CLCS_SeparatorCollection.Add(mySeperator.Name, myCOTK)
            Case TipoObjeto.Pipe
                Dim myPipe As New PipeGraphic(mpx, mpy, 50, 10, 0)
                myPipe.LineWidth = 2
                myPipe.Fill = True
                myPipe.FillColor = fillclr
                myPipe.LineColor = lineclr
                myPipe.Tag = "PIPE" & Format(ChildParent.Collections.ObjectCounter("Pipe"), "00#")
                ChildParent.Collections.UpdateCounter("Pipe")
                If tag <> "" Then myPipe.Tag = tag
                gObj = myPipe
                gObj.Name = "PIPE-" & Guid.NewGuid.ToString
                ChildParent.Collections.PipeCollection.Add(gObj.Name, myPipe)
                Dim myCOTK As RELAP.SimulationObjects.UnitOps.pipe = New RELAP.SimulationObjects.UnitOps.pipe(myPipe.Name, "Pipe")
                myCOTK.GraphicObject = myPipe
                ChildParent.Collections.ObjectCollection.Add(myPipe.Name, myCOTK)
                ChildParent.Collections.CLCS_PipeCollection.Add(myPipe.Name, myCOTK)
            Case TipoObjeto.Annulus
                Dim myAnn As New AnnulusGraphic(mpx, mpy, 50, 10, 0)
                myAnn.LineWidth = 2
                myAnn.Fill = True
                myAnn.FillColor = fillclr
                myAnn.LineColor = lineclr
                myAnn.Tag = "ANN" & Format(ChildParent.Collections.ObjectCounter("Annulus"), "00#")
                ChildParent.Collections.UpdateCounter("Annulus")
                If tag <> "" Then myAnn.Tag = tag
                gObj = myAnn
                gObj.Name = "ANN-" & Guid.NewGuid.ToString
                ChildParent.Collections.AnnulusCollection.Add(gObj.Name, myAnn)
                Dim myCOTK As RELAP.SimulationObjects.UnitOps.Annulus = New RELAP.SimulationObjects.UnitOps.Annulus(myAnn.Name, "Annulus")
                myCOTK.GraphicObject = myAnn
                ChildParent.Collections.ObjectCollection.Add(myAnn.Name, myCOTK)
                ChildParent.Collections.CLCS_AnnulusCollection.Add(myAnn.Name, myCOTK)
            Case TipoObjeto.Valve
                Dim myValve As New ValveGraphic(mpx, mpy, 50, 50, 0)
                myValve.LineWidth = 2
                myValve.Fill = True
                myValve.FillColor = fillclr
                myValve.LineColor = lineclr
                myValve.Tag = "VALVE" & Format(ChildParent.Collections.ObjectCounter("Valve"), "00#")
                ChildParent.Collections.UpdateCounter("Valve")
                If tag <> "" Then myValve.Tag = tag
                gObj = myValve
                gObj.Name = "VALVE-" & Guid.NewGuid.ToString
                ChildParent.Collections.ValveCollection.Add(gObj.Name, myValve)
           
                Dim myCOTK As RELAP.SimulationObjects.UnitOps.Valve = New RELAP.SimulationObjects.UnitOps.Valve(myValve.Name, "Valve")
                myCOTK.GraphicObject = myValve
                ChildParent.Collections.ObjectCollection.Add(myValve.Name, myCOTK)
                ChildParent.Collections.CLCS_ValveCollection.Add(myValve.Name, myCOTK)
            Case TipoObjeto.FuelRod
                If My.Application.ActiveSimulation.FormGeneralCoreInput.txtAxialNodes.Value > 0 Then

              
                Dim myTank As New FuelRodGraphic(mpx, mpy, 10, 50, 0)
                myTank.LineWidth = 2
                myTank.Fill = True
                myTank.FillColor = fillclr
                myTank.LineColor = lineclr
                myTank.Tag = "FUELR" & Format(ChildParent.Collections.ObjectCounter("FuelRod"), "00#")
                ChildParent.Collections.UpdateCounter("FuelRod")
                If tag <> "" Then myTank.Tag = tag
                gObj = myTank
                gObj.Name = "FR-" & Guid.NewGuid.ToString
                ChildParent.Collections.FuelRodCollection.Add(gObj.Name, myTank)

                Dim myCOTK As RELAP.SimulationObjects.UnitOps.FuelRod = New RELAP.SimulationObjects.UnitOps.FuelRod(myTank.Name, "FuelRod")
                myCOTK.GraphicObject = myTank
                ChildParent.Collections.ObjectCollection.Add(myTank.Name, myCOTK)
                    ChildParent.Collections.CLCS_FuelRodCollection.Add(myTank.Name, myCOTK)
                Else
                    MsgBox("Please Enter the Number of Axial Nodes before Adding Fuel Rod")
                    Return ""
                End If
            Case TipoObjeto.PWRControlRod
                Dim myTank As New PWRControlRodGraphic(mpx, mpy, 10, 50, 0)
                    myTank.LineWidth = 2
                    myTank.Fill = True
                    myTank.FillColor = fillclr
                    myTank.LineColor = lineclr
                myTank.Tag = "PCR" & Format(ChildParent.Collections.ObjectCounter("PCR"), "00#")
                ChildParent.Collections.UpdateCounter("PCR")
                    If tag <> "" Then myTank.Tag = tag
                    gObj = myTank
                gObj.Name = "PCR-" & Guid.NewGuid.ToString
                ChildParent.Collections.PWRControlRodCollection.Add(gObj.Name, myTank)

                Dim myCOTK As RELAP.SimulationObjects.UnitOps.PWRControlRod = New RELAP.SimulationObjects.UnitOps.PWRControlRod(myTank.Name, "PWRControlRod")
                    myCOTK.GraphicObject = myTank
                    ChildParent.Collections.ObjectCollection.Add(myTank.Name, myCOTK)
                ChildParent.Collections.CLCS_PWRControlRodCollection.Add(myTank.Name, myCOTK)
                
            Case TipoObjeto.Simulator
                Dim myTank As New SimulatorGraphic(mpx, mpy, 50, 50, 0)
                myTank.LineWidth = 2
                myTank.Fill = True
                myTank.FillColor = fillclr
                myTank.LineColor = lineclr
                myTank.Tag = "Sim" & Format(ChildParent.Collections.ObjectCounter("Simulator"), "00#")
                ChildParent.Collections.UpdateCounter("Simulator")
                If tag <> "" Then myTank.Tag = tag
                gObj = myTank
                gObj.Name = "SIM-" & Guid.NewGuid.ToString
                ChildParent.Collections.SimulatorCollection.Add(gObj.Name, myTank)

                Dim myCOTK As RELAP.SimulationObjects.UnitOps.Simulator = New RELAP.SimulationObjects.UnitOps.Simulator(myTank.Name, "Simulator")
                myCOTK.GraphicObject = myTank
                ChildParent.Collections.ObjectCollection.Add(myTank.Name, myCOTK)
                ChildParent.Collections.CLCS_SimulatorCollection.Add(myTank.Name, myCOTK)

        End Select
        'ChildParent.FormObjList.TreeViewObj.Refresh()

        If Not gObj Is Nothing Then
            Me.FlowsheetDesignSurface.drawingObjects.Add(gObj)
            Me.FlowsheetDesignSurface.Invalidate()
            Application.DoEvents()
            Dim arrays(ChildParent.Collections.ObjectCollection.Count - 1) As String
            '   Dim aNode, aNode2 As TreeNode
            Dim i As Integer = 0
           
        End If

        Me.FlowsheetDesignSurface.Cursor = Cursors.Arrow


        Return gObj.Name
    End Function

    Private Sub FlowsheetDesignSurface_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FlowsheetDesignSurface.DragEnter
        Dim i As Integer
        For i = 0 To e.Data.GetFormats().Length - 1
            If e.Data.GetFormats()(i).Equals _
               ("System.Windows.Forms.DataGridViewRow") Then
                'The data from the drag source is moved to the target.
                e.Effect = DragDropEffects.Copy
            End If
        Next
    End Sub

    Private Sub FlowsheetDesignSurface_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles FlowsheetDesignSurface.DragDrop
        If e.Effect = DragDropEffects.Copy Then
            Dim obj As DataGridViewRow = e.Data.GetData("System.Windows.Forms.DataGridViewRow")
            Dim tobj As TipoObjeto = TipoObjeto.Nenhum
            Dim p As Point = Me.FlowsheetDesignSurface.PointToClient(New Point(e.X, e.Y))
            Dim mousePT As Point = ChildParent.gscTogoc(p.X, p.Y)
            Dim mpx = mousePT.X
            Dim mpy = mousePT.Y
            Dim text As String = ChildParent.FormObjListView.DataGridView1.Rows(obj.Index).Cells(0).Value.ToString.TrimEnd(" ")
            Select Case text
                Case "PWRControlRod"
                    tobj = TipoObjeto.PWRControlRod
                Case "FuelRod"
                    tobj = TipoObjeto.FuelRod
                Case "Simulator"
                    tobj = TipoObjeto.Simulator
                Case "Ajuste"
                    tobj = TipoObjeto.OT_Ajuste
                Case "Especificao"
                    tobj = TipoObjeto.OT_Especificacao
                Case "Reciclo"
                    tobj = TipoObjeto.OT_Reciclo
                Case "EnergyRecycle"
                    tobj = TipoObjeto.OT_EnergyRecycle
                Case "Misturador"
                    tobj = TipoObjeto.NodeIn
                Case "Divisor"
                    tobj = TipoObjeto.NodeOut
                Case "Bomba"
                    tobj = TipoObjeto.Pump
                Case "Tanque"
                    tobj = TipoObjeto.Tank
                Case "Accumulator"
                    tobj = TipoObjeto.Accumulator
                Case "Turbinas"
                    tobj = TipoObjeto.Turbine
                Case "VasoSeparadorGL"
                    tobj = TipoObjeto.Vessel
                Case "CorrentedeMatria"
                    tobj = TipoObjeto.MaterialStream
                Case "Correntedeenergia"
                    tobj = TipoObjeto.EnergyStream
                Case "CompressorAdiabtico"
                    tobj = TipoObjeto.Compressor
                Case "TurbinaAdiabtica"
                    tobj = TipoObjeto.Expander
                Case "SingleVolume"
                    tobj = TipoObjeto.SingleVolume
                Case "SingleJunction"
                    tobj = TipoObjeto.SingleJunction
                Case "TimeDependentJunction"
                    tobj = TipoObjeto.TimeDependentJunction
                Case "Aquecedor"
                    tobj = TipoObjeto.Heater
                Case "Branch"
                    tobj = TipoObjeto.Branch
                Case "Separator"
                    tobj = TipoObjeto.Separator
                Case "Tubulao"
                    tobj = TipoObjeto.Pipe
                Case "Annulus"
                    tobj = TipoObjeto.Annulus
                Case "Vlvula"
                    tobj = TipoObjeto.Valve
                Case "ReatorConversao"
                    tobj = TipoObjeto.RCT_Conversion
                Case "ReatorEquilibrio"
                    tobj = TipoObjeto.RCT_Equilibrium
                Case "ReatorGibbs"
                    tobj = TipoObjeto.RCT_Gibbs
                Case "ReatorCSTR"
                    tobj = TipoObjeto.RCT_CSTR
                Case "ReatorPFR"
                    tobj = TipoObjeto.RCT_PFR
                Case "HeatStructure"
                    tobj = TipoObjeto.HeatStructure
                Case "ShortcutColumn"
                    tobj = TipoObjeto.ShortcutColumn
                Case "DistillationColumn"
                    tobj = TipoObjeto.DistillationColumn
                Case "AbsorptionColumn"
                    tobj = TipoObjeto.AbsorptionColumn
                Case "ReboiledAbsorber"
                    tobj = TipoObjeto.ReboiledAbsorber
                Case "RefluxedAbsorber"
                    tobj = TipoObjeto.RefluxedAbsorber
                Case "ComponentSeparator"
                    tobj = TipoObjeto.ComponentSeparator
                Case "OrificePlate"
                    tobj = TipoObjeto.OrificePlate
                Case "CustomUnitOp"
                    tobj = TipoObjeto.CustomUO
                Case "CapeOpenUnitOperation"
                    tobj = TipoObjeto.CapeOpenUO
            End Select

            AddObjectToSurface(tobj, mpx, mpy)
        End If
    End Sub

    Public calcstart As Date

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer2.Tick

        Dim ts As TimeSpan = Date.Now - calcstart

        '  Me.LabelTime.Text = Format(ts.Hours, "0#") & ":" & Format(ts.Minutes, "0#") & ":" & Format(ts.Seconds, "0#") & "." & Format(ts.Milliseconds, "####")

    End Sub



    Private Sub FlowsheetDesignSurface_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles FlowsheetDesignSurface.MouseEnter

    End Sub

    'Private Sub EditCompTSMI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditCompTSMI.Click

    '    If Me.FlowsheetDesignSurface.SelectedObject.TipoObjeto = TipoObjeto.MaterialStream Then

    '        Dim mystr As RELAP.SimulationObjects.Streams.MaterialStream = ChildParent.Collections.CLCS_MaterialStreamCollection(ChildParent.FormSurface.FlowsheetDesignSurface.SelectedObject.Name)

    '        If Not mystr.GraphicObject.InputConnectors(0).IsAttached Then

    '            Dim selectionControl As New CompositionEditorForm
    '            selectionControl.Text = mystr.GraphicObject.Tag & RELAP.App.GetLocalString("EditComp")
    '            selectionControl.Componentes = mystr.Fases(0).Componentes

    '            selectionControl.ShowDialog(Me)

    '            mystr.Fases(0).Componentes = selectionControl.Componentes

    '            selectionControl.Dispose()
    '            selectionControl = Nothing

    '            Application.DoEvents()
    '            CalculateMaterialStream(ChildParent, mystr)
    '            Call ChildParent.FormSurface.UpdateSelectedObject()
    '            Call ChildParent.FormSurface.FlowsheetDesignSurface.Invalidate()
    '            Application.DoEvents()
    '            ProcessCalculationQueue(ChildParent)

    '        Else

    '            VDialog.Show("The composition of this Material Stream is not editable.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '        End If

    '    End If

    'End Sub

    Private Sub ToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem8.Click
        DrawToBitmapScaled(2)
    End Sub

    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        DrawToBitmapScaled(1)
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        DrawToBitmapScaled(0.5)
    End Sub

    Private Sub ToolStripMenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem10.Click
        DrawToBitmapScaled(3)
    End Sub

    Sub DrawToBitmapScaled(ByVal scale As Double)

        Dim rect As Rectangle = New Rectangle(0, 0, scale * (Me.FlowsheetDesignSurface.Width - 14), scale * (Me.FlowsheetDesignSurface.Height - 14))
        Dim img As Image = New Bitmap(rect.Width, rect.Height)
        Dim g As Graphics = Graphics.FromImage(img)

        Try
            g.SmoothingMode = SmoothingMode.AntiAlias
            'get the dpi settings of the graphics context,
            'for example; 96dpi on screen, 600dpi for the printer
            'used to adjust grid and margin sizing.
            Me.FlowsheetDesignSurface.m_HorizRes = g.DpiX
            Me.FlowsheetDesignSurface.m_VertRes = g.DpiY

            Me.FlowsheetDesignSurface.DrawGrid(g)

            'handle the possibility that the viewport is scrolled,
            'adjust my origin coordintates to compensate
            Dim pt As Point = Me.FlowsheetDesignSurface.AutoScrollPosition
            g.TranslateTransform(pt.X * scale, pt.Y * scale)

            'draw the actual objects onto the page, on top of the grid

            For Each gr As GraphicObject In Me.FlowsheetDesignSurface.SelectedObjects.Values
                Me.FlowsheetDesignSurface.drawingObjects.DrawSelectedObject(g, gr, scale * Me.FlowsheetDesignSurface.Zoom)
            Next

            With Me.FlowsheetDesignSurface.drawingObjects
                'pass the graphics resolution onto the objects
                'so that images and other objects can be sized
                'correct taking the dpi into consideration.
                .HorizontalResolution = g.DpiX
                .VerticalResolution = g.DpiY
                'doesn't really draw the selected object, but instead the
                'selection indicator, a dotted outline around the selected object
                .DrawObjects(g, scale * Me.FlowsheetDesignSurface.Zoom)
                If Not Me.FlowsheetDesignSurface.SelectedObject Is Nothing Then
                    If Not Me.FlowsheetDesignSurface.SelectedObjects.ContainsKey(Me.FlowsheetDesignSurface.SelectedObject.Name) Then
                        .DrawSelectedObject(g, Me.FlowsheetDesignSurface.SelectedObject, scale * Me.FlowsheetDesignSurface.Zoom)
                    End If
                End If
            End With

            Clipboard.SetImage(img)

            '  Me.ChildParent.WriteToLog("Image created and sent to clipboard sucessfully.", Color.Blue, RELAP.FormClasses.TipoAviso.Informacao)

        Catch ex As Exception

            '  Me.ChildParent.WriteToLog("Error capturing flowsheet snapshot: " & ex.ToString, Color.Red, RELAP.FormClasses.TipoAviso.Erro)

        Finally

            img.Dispose()
            g.Dispose()

        End Try


    End Sub




End Class
