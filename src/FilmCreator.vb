Public Class FilmCreator

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try

            Dim header As String = "<?xml version=""1.0"" encoding=""utf-8"" ?>"
            Dim filmln As String = "<film name=""" & nom.Text & """ date=""" & DateTimePicker1.Value & """ producteur=""" & createur.Text & """ capture=""" & capture.Text & """ />"
            If Not IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ZBiblio") Then
                MkDir(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments Or 5) & "\" & "ZBiblio" & "\")
                If Not IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) & "\ZBiblio\Films") Then
                    MkDir(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments Or 5) & "\" & "ZBiblio" & "\" & "Films" & "\")
                End If
            End If
            Dim filepath As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments Or 5) & "\" & "ZBiblio" & "\" & "Films" & "\" & nom.Text & ".film"
            Dim writer As New IO.StreamWriter(filepath)
            writer.WriteLine(header)
            writer.WriteLine(filmln)
            writer.Close()
        Catch ex As Exception
            MessageBox.Show("Une erreur s'est produite. Message : " & ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog(Me) = Windows.Forms.DialogResult.OK And OpenFileDialog1.FileName <> String.Empty Or "" Then
            capture.Text = OpenFileDialog1.FileName
            If capture.Text <> "null" And IO.File.Exists(capture.Text) Then
                PictureBox1.Image = Image.FromFile(capture.Text)
            Else
                PictureBox1.Image = Nothing
            End If

        End If
    End Sub

    Private Sub FilmCreator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = Now
    End Sub

    Private Sub capture_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles capture.GotFocus
        Select Case capture.Text
            Case "<Aucune>"
                capture.Text = ""
            Case ""
            Case Else
        End Select
        capture.ForeColor = System.Drawing.SystemColors.WindowText
        capture.Font = New Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub

    Private Sub capture_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles capture.LostFocus
        If capture.Text = "" Then
            capture.Text = "<Aucune>"
            capture.ForeColor = System.Drawing.SystemColors.InactiveCaption
            capture.Font = New Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        End If
    End Sub
End Class