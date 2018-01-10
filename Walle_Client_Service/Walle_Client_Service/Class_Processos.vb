Public Class Class_Processos

    Private _iID As String
    Public Property ID() As String
        Get
            Return _iID
        End Get
        Set(ByVal value As String)
            _iID = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property


    Private _Processo As String
    Public Property Processo() As String
        Get
            Return _Processo
        End Get
        Set(ByVal value As String)
            _Processo = value
        End Set
    End Property

    Private _Local As String
    Public Property Local() As String
        Get
            Return _Local
        End Get
        Set(ByVal value As String)
            _Local = value
        End Set
    End Property

    Private _HoraIni As date
    Public Property HoraIni() As Date
        Get
            Return _HoraIni
        End Get
        Set(ByVal value As Date)
            _HoraIni = value
        End Set
    End Property

    Private _HoraFim As Date
    Public Property HoraFim() As Date
        Get
            Return _HoraFim
        End Get
        Set(ByVal value As Date)
            _HoraFim = value
        End Set
    End Property

    Private _Tempo As String
    Public Property Tempo() As String
        Get
            Return _Tempo
        End Get
        Set(ByVal value As String)
            _Tempo = value
        End Set
    End Property

    Private _Chave As String
    Public Property Chave() As String
        Get
            Return _Chave
        End Get
        Set(ByVal value As String)
            _Chave = value
        End Set
    End Property

    Private _UsuarioDeRede As String
    Public Property UsuarioDeRede() As String
        Get
            Return _UsuarioDeRede
        End Get
        Set(ByVal value As String)
            _UsuarioDeRede = value
        End Set
    End Property

    Private _URL As String
    Public Property URL() As String
        Get
            Return _URL
        End Get
        Set(ByVal value As String)
            _URL = value
        End Set
    End Property

    Private _Endereco As String
    Public Property Endereco() As String
        Get
            Return _Endereco
        End Get
        Set(ByVal value As String)
            _Endereco = value
        End Set
    End Property

    Private _CodCliente As String
    Public Property CodCliente() As String
        Get
            Return _CodCliente
        End Get
        Set(ByVal value As String)
            _CodCliente = value
        End Set
    End Property

    Private _AppCategoria As String
    Public Property AppCategoria() As String
        Get
            Return _AppCategoria
        End Get
        Set(ByVal value As String)
            _AppCategoria = value
        End Set
    End Property

    Private _PageCategoria As String
    Public Property PageCategoria() As String
        Get
            Return _PageCategoria
        End Get
        Set(ByVal value As String)
            _PageCategoria = value
        End Set
    End Property

    Private _Versao As String
    Public Property Versao() As String
        Get
            Return _Versao
        End Get
        Set(ByVal value As String)
            _Versao = value
        End Set
    End Property

    Private _Rastrear As String
    Public Property Rastrear() As String
        Get
            Return _Rastrear
        End Get
        Set(ByVal value As String)
            _Rastrear = value
        End Set
    End Property

End Class

