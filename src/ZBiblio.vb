Imports System.Windows.Forms

Public Class ZBiblio
    Dim depart As Long = 90
    Dim fin As Long = 0
    Dim capture As Boolean
    Dim dateactivation As Long = depart
    Shadows activated As Boolean = False
    Dim filenameopen As String
    Public Sub open_sub()
        Dim FileName As String = filenameopen
        If FileName <> String.Empty And IO.File.Exists(FileName) Then
            Dim viewer As New FilmViewer

            Dim rd As New IO.StreamReader(FileName)
            If rd.ReadToEnd.Contains("null") Then
                viewer.MdiParent = Me
                viewer.Dock = DockStyle.Fill
                viewer.Open(FileName)
                viewer.Show()
            Else
                viewer.MdiParent = Me
                viewer.Dock = DockStyle.Fill
                viewer.Open(FileName)
                viewer.Show()

            End If


        End If
    End Sub
    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs) Handles OpenToolStripMenuItem.Click, OpenToolStripButton.Click
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Films ZBiblio (*.film)|*.film"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            filenameopen = OpenFileDialog.FileName
            open_sub()
        End If
    End Sub

   


    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub


    Private Sub ToolBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ToolBarToolStripMenuItem.Click, BarreDoutilsToolStripMenuItem.Click
        Me.ToolStrip.Visible = Me.ToolBarToolStripMenuItem.Checked
    End Sub

   

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Fermez tous les formulaires enfants du parent.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub



    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click, NewToolStripMenuItem.Click
        Dim createur As New FilmCreator
        createur.MdiParent = Me
        createur.Show()

    End Sub



    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsToolStripMenuItem.Click
        Options.ShowDialog(Me)
    End Sub

    Private Sub ZBiblio_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        Dim bidule = e.Data.GetData(GetType(Object))

        If e.Data.GetData(GetType(Object)).Name.Contains(".film") Then
            filenameopen = e.Data.GetData(GetType(Object)).Name
            open_sub()
        End If
    End Sub

    Private Sub ZBiblio_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
       
    End Sub
  
    Private Sub ZBiblio_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MsgBox(My.Application.Info.Version.ToString)

        Dim Bureau As IWshRuntimeLibrary.WshShell

        Dim Raccourci As IWshRuntimeLibrary.WshShortcut

        Dim Nom As String

        Bureau = New IWshRuntimeLibrary.WshShell

        '   Chemin et nom du raccourci

        Nom = My.Computer.FileSystem.SpecialDirectories.Desktop & "\ZBiblio.lnk"

        Raccourci = CType(Bureau.CreateShortcut(Nom), IWshRuntimeLibrary.WshShortcut)

        '   Cible à exécuter

        Raccourci.TargetPath = Application.ExecutablePath

        '   Icône à utiliser

        Raccourci.IconLocation = Application.ExecutablePath

        '   Enregistrement du raccourci

        Raccourci.Save()
        If My.Application.CommandLineArgs.ToString <> "" Then
            Dim i
            For i = 0 To My.Application.CommandLineArgs.Count - 1
                If Mid(My.Application.CommandLineArgs(i).ToString, 1, 2) = "-o" Then
                    filenameopen = Mid(My.Application.CommandLineArgs(i).ToString, 3)
                    open_sub()
                End If
            Next
        End If
    End Sub


    Private Sub LicenceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.ShowDialog(Me)
    End Sub

    Private Sub ChercherDesMisesÀJourToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChercherDesMisesÀJourToolStripMenuItem.Click
        My.Forms.Update.ShowDialog(Me)
    End Sub
    Sub AddAtContextMenu()
        Dim regKey As Microsoft.Win32.RegistryKey
        regKey = My.Computer.Registry.ClassesRoot.CreateSubKey(".film\OpenWithProgIds\ZBiblio.Film")
        My.Computer.Registry.ClassesRoot.SetValue("ZBiblio.Film", "", "Film ZBiblio")
        My.Computer.Registry.ClassesRoot.SetValue("ZBiblio.Film\shell\Open\command", "", Application.ExecutablePath & " -o " & Chr(34) & "%L" & Chr(34))
        My.Computer.Registry.ClassesRoot.SetValue("ZBiblio.Film\DefaultIcon", Application.ExecutablePath & ",0")
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusBarToolStripMenuItem.Click, BarreDesMenusToolStripMenuItem.Click
        Me.MenuStrip.Visible = StatusBarToolStripMenuItem.Checked
    End Sub

    Private Sub MenuStrip_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuStrip.VisibleChanged
        StatusBarToolStripMenuItem.Checked = MenuStrip.Visible
        BarreDesMenusToolStripMenuItem.Checked = MenuStrip.Visible
    End Sub

    Private Sub ToolStrip_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStrip.VisibleChanged
        BarreDoutilsToolStripMenuItem.Checked = ToolStrip.Visible
        ToolBarToolStripMenuItem.Checked = ToolStrip.Visible
    End Sub
End Class
