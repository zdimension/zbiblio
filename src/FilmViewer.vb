Imports System.Xml
Public Class FilmViewer

    ''' <summary>
    ''' Ouvrir un film ZBiblio
    ''' </summary>
    ''' <param name="FilmSrc">Empmlacement du film ZBiblio</param>
    ''' <param name="Capture">Afficher la capture du film ou non. Ne mettez pas ce paramètre à Oui si il n'y a pas de capture indiquée dans le fichier film.</param>
    ''' <remarks>Ouvrir un film ZBiblio</remarks>
    Public Sub Open(ByVal FilmSrc As String)
        Dim doc As New XmlDocument()
        doc.Load(FilmSrc)
        Dim filmnode As XmlNodeList
        filmnode = doc.GetElementsByTagName("film")
        Dim film As Film
        Dim filmname As String
        Dim filmdate As String
        Dim filmcreator As String
        Dim filmcapture As Image
        For Each unfilm As XmlNode In filmnode
            filmname = unfilm.Attributes("name").Value
        Next
        For Each unedate As XmlNode In filmnode
            filmdate = unedate.Attributes("date").Value
        Next
        For Each uncreateur As XmlNode In filmnode
            filmcreator = uncreateur.Attributes("producteur").Value
        Next

        Dim unecapture As XmlNode
        For Each unecapture In filmnode
            Try
                filmcapture = Image.FromFile(unecapture.Attributes("capture").Value)
            Catch ex As IO.FileNotFoundException
            End Try
        Next
        If unecapture.Attributes("capture").Value = "null" Then
            photo.Image = Nothing
        Else
            film = New Film(filmname, filmdate, filmcreator, filmcapture)
            nom.Text = film.Nom
            creator.Text = film.Realisateur
            sortdate.Text = film.DateSortie
            photo.Image = filmcapture
        End If


        film = New Film(filmname, filmdate, filmcreator)
        nom.Text = film.Nom
        creator.Text = film.Realisateur
        sortdate.Text = film.DateSortie

        Me.Text = filmname



    End Sub
End Class