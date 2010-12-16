Imports System.Windows.Forms
Imports System.IO
Imports System.Net
Imports System.Text
Public Class Update
    Private Function ReadNetworkStream(ByVal stream As Stream)
        Dim buffer(32768) As Byte
        Using ms As New MemoryStream
            While True
                Dim read As Integer = stream.Read(buffer, 0, buffer.Length)
                If (read <= 0) Then
                    Return ms.ToArray()
                End If
                ms.Write(buffer, 0, read)
            End While
            Return ms.ToArray()
        End Using
    End Function
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        ' Dim rd_setup As New StreamReader("http://zippedfire.free.fr/ZBiblio_setup.exe")
        ' Dim wr_setup As New StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "\ZBiblio_setup.exe")
        ' wr_setup.Write(rd_setup.ReadToEnd)
        Dim request As HttpWebRequest = WebRequest.Create("http://zippedfire.free.fr/ZBiblio_setup.exe")
        Dim response As HttpWebResponse = request.GetResponse()
        Dim webstream As Stream = response.GetResponseStream()
        Dim bytes() As Byte = ReadNetworkStream(webstream)
        Using fs As FileStream = New FileStream(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) & "\ZBiblio_setup.exe", FileMode.CreateNew,
        FileAccess.Write)
            fs.Write(bytes, 0, bytes.Length)
            fs.Close()
        End Using
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Public Sub LaunchInstaller()
        Process.Start("http://zippedfire.free.fr/zbiblio/setup.exe")
        Application.Exit()
    End Sub
    Private Sub Update_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'MsgBox(Application.StartupPath & "\Temporary\ZBiblio_version_number_latest.txt")
        Dim request As HttpWebRequest = WebRequest.Create("http://zippedfire.free.fr/ZBiblio_version_number_latest.txt")
        Dim response As HttpWebResponse = request.GetResponse()
        Dim webstream As Stream = response.GetResponseStream()
        Dim bytes() As Byte = ReadNetworkStream(webstream)

        Dim wr As New StreamWriter(Application.StartupPath & "\Temporary\ZBiblio_version_number_latest.txt")
        wr.Write(bytes.ToString)
        Dim rd_to_ver As New StreamReader(Application.StartupPath & "\Temporary\ZBiblio_version_number_latest.txt")
        Dim latest As String = rd_to_ver.ReadToEnd
        Label4.Text = latest
        Label3.Text = AboutBox1.LabelVersion.Text
        If Label4.Text.Equals(Label3.Text) Then
            OK_Button.Enabled = False
            MsgBox("Aucune mise à jour disponible.", vbInformation, "Mises à jour")
        Else
            OK_Button.Enabled = True
            AddHandler OK_Button.Click, AddressOf LaunchInstaller
        End If
        'Dim rd As New IO.StreamReader("http://zippedfire.free.fr/zbiblio/version.txt")
        'Dim version As String = My.Application.Info.Version.ToString
        'If rd.ReadToEnd <> version Then



        'OK_Button.Enabled = True
        'Else
        'Label3.Text = Version
        'Label4.Text = rd.ReadToEnd
        'OK_Button.Enabled = False

        'End If
    End Sub

   

End Class
