Imports System.Windows.Forms
Imports System.Xml
Public Class Options

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        LoadTheme(ComboBox1.SelectedItem)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Sub LoadTheme(ByVal path As String)
        Dim doc As New XmlDocument
        doc.Load(Application.StartupPath & "\" & path)
        Dim statusbar As XmlNodeList
        statusbar = doc.GetElementsByTagName("statusbar")
        Dim statuscolor As Color
        For Each unecolor As XmlNode In statusbar
            statuscolor = Color.FromName(unecolor.Attributes("color").Value)
        Next
        Dim menustrip As XmlNodeList
        menustrip = doc.GetElementsByTagName("menustrip")
        Dim menucolor As Color
        For Each unecolor As XmlNode In menustrip
            menucolor = Color.FromName(unecolor.Attributes("color").Value)
        Next
        Dim toolstrip As XmlNodeList
        toolstrip = doc.GetElementsByTagName("toolstrip")
        Dim toolcolor As Color
        For Each unecolor As XmlNode In toolstrip
            toolcolor = Color.FromName(unecolor.Attributes("color").Value)
        Next
        ZBiblio.MenuStrip.BackColor = menucolor
        ZBiblio.ToolStrip.BackColor = toolcolor
        My.Settings.Theme = path
    End Sub
    Private Sub Options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Select Case ComboBox1.Items.Count
            Case 0
                For Each foundfile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath, FileIO.SearchOption.SearchAllSubDirectories, "*.ztheme")
                    ComboBox1.Items.Add(My.Computer.FileSystem.GetName(foundfile))
                    ComboBox1.SelectedItem = My.Settings.Theme
                Next
            Case Is = 1, Is > 1
                ComboBox1.Items.Clear()
                For Each foundfile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath, FileIO.SearchOption.SearchAllSubDirectories, "*.ztheme")
                    ComboBox1.Items.Add(My.Computer.FileSystem.GetName(foundfile))
                    ComboBox1.SelectedItem = My.Settings.Theme
                Next
        End Select

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK And OpenFileDialog1.FileName.EndsWith(".ztheme") Then
            FileCopy(OpenFileDialog1.FileName, Application.StartupPath)
            ComboBox1.Items.Clear()
            For Each foundfile As String In My.Computer.FileSystem.GetFiles(Application.StartupPath, FileIO.SearchOption.SearchAllSubDirectories, "*.ztheme")
                ComboBox1.Items.Add(My.Computer.FileSystem.GetName(foundfile))
                ComboBox1.SelectedItem = My.Settings.Theme
            Next
        End If

    End Sub
End Class
