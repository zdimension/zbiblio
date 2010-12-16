Public Class Film

    Private _Nom As String
    Public Property Nom() As String
        Get
            Return _Nom
        End Get
        Set(ByVal value As String)
            _Nom = value
        End Set
    End Property

    Private _Capture As Image
    Public Property Capture() As Image
        Get
            Return _Capture
        End Get
        Set(ByVal value As Image)
            _Capture = value
        End Set
    End Property

    Private _DateSortie As Date
    Public Property DateSortie() As Date
        Get
            Return _DateSortie
        End Get
        Set(ByVal value As Date)
            _DateSortie = value
        End Set
    End Property

    Private _Realisateur As String
    Public Property Realisateur() As String
        Get
            Return _Realisateur
        End Get
        Set(ByVal value As String)
            _Realisateur = value
        End Set
    End Property


    Public Sub New()

    End Sub

    Public Sub New(ByVal Nom As String, ByVal DateSortie As Date, ByVal Realisateur As String)
        _Nom = Nom
        _DateSortie = DateSortie
        _Realisateur = Realisateur

    End Sub
    Public Sub New(ByVal Nom As String, ByVal DateSortie As Date, ByVal Realisateur As String, ByVal Capture As Image)
        _Nom = Nom
        _DateSortie = DateSortie
        _Realisateur = Realisateur
        _Capture = Capture
    End Sub
    Public Sub Update(ByVal Nom As String, ByVal DateSortie As Date, ByVal Realisateur As String)
        _Nom = Nom
        _DateSortie = DateSortie
        _Realisateur = Realisateur
        
    End Sub

    'Je surcharge le Tostring
    Public Overrides Function ToString() As String
        Return _Nom
    End Function

End Class
