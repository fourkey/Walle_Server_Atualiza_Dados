Imports System.IO
Imports System.Data.OleDb
'Imports Microsoft.Office.Interop
Imports System.Text
Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography
Imports Microsoft.Win32
Imports System.Security.AccessControl
Imports System.Security.Permissions
Imports System.Net
Imports System.Net.Mail
Imports System.GC


Public Class Funcao_Sistema

    Dim Banco As New Conexao_MySql
    Dim M As New Manipular_ListView

    'Criptografia
    Dim textoCifrado As Byte()
    Dim sal() As Byte = {&H0, &H1, &H2, &H3, &H4, &H5, &H6, &H5, &H4, &H3, &H2, &H1, &H0}

    Public Function LerPasta(ByVal Cod As String) As ArrayList

        Dim ListaDeArquivos As New ArrayList
        Dim NomeParte As String = ""
        Dim Cont As Integer = 0
        Dim Pasta As String

        Pasta = "C:\Fourkey\Admin\Files\" & Cod

        For Each nome In Directory.GetFiles(Pasta)

            ListaDeArquivos.Add(nome)
            Cont = nome.Length - 1

            While nome.Chars(Cont) <> "\"

                NomeParte = nome.Chars(Cont).ToString & NomeParte

                Cont = Cont - 1

            End While

            Frm_Inicio.ListaDeArquivosNomes.Add(NomeParte)

            NomeParte = ""

        Next

        ListaDeArquivos.Clear()

        For Each nome In Directory.GetFiles(Pasta)

            ListaDeArquivos.Add(nome)

        Next

        Return ListaDeArquivos

    End Function

    Public Sub UploadArquivos(ByVal ListaArquivos As ArrayList, ByVal ChaveCript As String, ByVal CodCli As Integer)

        Dim fluxoTexto As IO.StreamReader
        Dim linhaTexto As String
        Dim Dados As New Class_Processos
        Dim ListaProcesso As New List(Of Class_Processos)
        Dim Cont As Integer = 1
        Dim Palavra As String = ""
        Dim Contando As Double
        Dim Valor As Double

        If ListaArquivos.Count = 0 Then

            Exit Sub

        End If

        M.Adicionar_Dados_list("Iniciando upload de arquivos: " & CodCli, My.Forms.Frm_Inicio.lv_processo)
        Frm_Inicio.Lab_Pro.Text = "Lendo Arquivos..."

        Contando = 100 / ListaArquivos.Count

        For i As Integer = 0 To ListaArquivos.Count - 1

            M.Adicionar_Dados_list("Arquivo " & i & " de " & ListaArquivos.Count, My.Forms.Frm_Inicio.lv_processo)

            Try

                If IO.File.Exists(ListaArquivos.Item(i)) Then

                    fluxoTexto = New IO.StreamReader(ListaArquivos.Item(i).ToString)
                    linhaTexto = Trim(fluxoTexto.ReadLine)

                    While linhaTexto <> Nothing

                        Dim algoritmo = New RijndaelManaged()
                        Dim chave As New Rfc2898DeriveBytes(ChaveCript, sal)

                        textoCifrado = Convert.FromBase64String(linhaTexto)

                        algoritmo.Key = chave.GetBytes(16)
                        algoritmo.IV = chave.GetBytes(16)

                        Using StreamFonte = New MemoryStream(textoCifrado)


                            Try


                                Using StreamDestino As New MemoryStream()

                                    Using crypto As New CryptoStream(StreamFonte, algoritmo.CreateDecryptor(), CryptoStreamMode.Read)

                                        moveBytes(crypto, StreamDestino)

                                        Dim bytesDescriptografados() As Byte = StreamDestino.ToArray()
                                        Dim mensagemDescriptografada = New UnicodeEncoding().GetString(bytesDescriptografados)

                                        linhaTexto = mensagemDescriptografada

                                    End Using

                                End Using

                            Catch ex As Exception

                            End Try

                        End Using

                        For j As Integer = 0 To linhaTexto.Length - 1

                            If linhaTexto.Chars(j) <> ";" Then

                                Palavra = Palavra & linhaTexto.Chars(j)

                            Else

                                If Cont = 1 Then

                                    Dados.UsuarioDeRede = Palavra

                                ElseIf Cont = 2 Then

                                    Dados.Processo = Palavra

                                ElseIf Cont = 3 Then

                                    Dados.Nome = Palavra

                                ElseIf Cont = 4 Then

                                    Dados.Local = Palavra

                                ElseIf Cont = 5 Then

                                    If Palavra <> "" Then

                                        Dados.HoraIni = Format(CDate(Palavra), "dd/MM/yyyy HH:mm:ss")

                                    End If

                                ElseIf Cont = 6 Then

                                    If Palavra <> "" Then

                                        Dados.HoraFim = Format(CDate(Palavra), "dd/MM/yyyy HH:mm:ss")

                                    End If

                                ElseIf Cont = 7 Then

                                    Dados.Tempo = Palavra

                                ElseIf Cont = 8 Then

                                    Dados.URL = Palavra

                                ElseIf Cont = 9 Then

                                    Dados.Endereco = Palavra

                                ElseIf Cont = 10 Then

                                    Dados.CodCliente = Palavra

                                ElseIf Cont = 11 Then

                                    Dados.Chave = Palavra

                                ElseIf Cont = 12 Then

                                    Dados.Versao = Palavra

                                ElseIf Cont = 13 Then

                                    Dados.Rastrear = Palavra

                                End If

                                Palavra = ""
                                Cont = Cont + 1

                            End If

                        Next

                        ListaProcesso.Add(Dados)

                        Dados = New Class_Processos
                        Palavra = ""
                        Cont = 1
                        linhaTexto = fluxoTexto.ReadLine

                    End While

                    fluxoTexto.Close()

                    Frm_Inicio.Progress_Bar.Value = Valor
                    Valor = Valor + Contando
                    Application.DoEvents()

                End If

            Catch ex As Exception

                M.Adicionar_Dados_list(ex.Message, My.Forms.Frm_Inicio.lv_processo)

            End Try

        Next

        M.Adicionar_Dados_list("Enviando Dados", My.Forms.Frm_Inicio.lv_processo)

        Frm_Inicio.Progress_Bar.Value = 0
        Frm_Inicio.Lab_Pro.Text = "Enviando Dados..."
        EnviarDados(ListaProcesso, ListaArquivos, CodCli)

    End Sub

    Private Sub moveBytes(ByVal fonte As Stream, ByVal destino As Stream)
        Dim bytes(2048) As Byte
        Dim contador = fonte.Read(bytes, 0, bytes.Length - 1)
        While (0 <> contador)
            destino.Write(bytes, 0, contador)
            contador = fonte.Read(bytes, 0, bytes.Length - 1)
        End While
    End Sub

    Public Sub EnviarDados(ByVal Lista As List(Of Class_Processos), ByVal ListaDeArquivos As ArrayList, ByVal CodCli As Integer)

        Dim Comando As New MySqlCommand
        Dim Sql As New StringBuilder
        Dim Conexao As New MySqlConnection
        Dim ListaDeletar As New ArrayList
        Dim Cont As Integer = 0
        Dim StringEnvio As String = ""
        Dim Inc As String
        Dim Out As String
        Dim Cont4 As Integer
        Dim Contando As Double
        Dim Valor As Double

        M.Adicionar_Dados_list("Enviando dados", My.Forms.Frm_Inicio.lv_processo)

        Sql.Append("INSERT INTO Db_Walle_v4.tb_coleta (Cod_Cliente, Des_Usuario_Rede, Des_Processo, Des_Nome, Des_URL, Des_Local, " _
        & "Date_Hora_Inicio, Date_Hora_Fim, Int_Tempo, Des_Identificador_PC, Des_Chave_Processo, Des_Versao, Des_Rastrear) VALUES ")

        If Lista.Count = 0 Then

            Exit Sub

        End If

        'Try

        Try

            Conexao = Banco.GetConexao()

            If Conexao.ConnectionString = "" Then

                Exit Sub

            End If

        Catch ex As Exception

            M.Adicionar_Dados_list(ex.Message, My.Forms.Frm_Inicio.lv_processo)

            Exit Sub

        End Try

        Comando.Connection = Conexao

        Contando = 100 / Lista.Count

        For i As Integer = 0 To Lista.Count - 1

            Cont4 = i

            If IsNothing(Lista.Item(i).Processo) = False Then

                Lista.Item(i).Processo = Lista.Item(i).Processo.Replace("'", "")
                Lista.Item(i).Nome = Lista.Item(i).Nome.Replace("'", "")
                Lista.Item(i).Local = Lista.Item(i).Local.Replace("'", "")
                Lista.Item(i).URL = Lista.Item(i).URL.Replace("'", "")

                Lista.Item(i).Processo = Lista.Item(i).Processo.Replace("’", "")
                Lista.Item(i).Nome = Lista.Item(i).Nome.Replace("’", "")
                Lista.Item(i).Local = Lista.Item(i).Local.Replace("’", "")
                Lista.Item(i).URL = Lista.Item(i).URL.Replace("’", "")

                Lista.Item(i).URL = Mid(Lista.Item(i).URL, 1, 500)
                Lista.Item(i).Processo = Mid(Lista.Item(i).Processo, 1, 300)
                Lista.Item(i).Nome = Mid(Lista.Item(i).Nome, 1, 500)
                Lista.Item(i).Local = Mid(Lista.Item(i).Local, 1, 700)
                Lista.Item(i).Chave = Replace(Lista.Item(i).Chave, "'", "")

                Dim Tempo As String
                If "".Equals(Lista.Item(i).Tempo) Then
                    Tempo = 0 : Else
                    Tempo = Lista.Item(i).Tempo
                End If

                StringEnvio = StringEnvio & "(" & CodCli & ", '" & Lista.Item(i).UsuarioDeRede & "','" _
                                            & Lista.Item(i).Processo & "','" _
                                            & Lista.Item(i).Nome & "','" _
                                            & Lista.Item(i).URL & "','" _
                                            & Lista.Item(i).Local & "','" _
                                            & Lista.Item(i).HoraIni.ToString("yyyy-MM-dd HH:mm:ss") & "','" _
                                            & Lista.Item(i).HoraFim.ToString("yyyy-MM-dd HH:mm:ss") & "'," _
                                            & Tempo.ToString() & ", '" & Lista.Item(i).Endereco & "', '" & Lista.Item(i).Chave & "', '" _
                                            & Lista.Item(i).Versao & "', '" & Lista.Item(i).Rastrear & "'), "

                Cont = Cont + 1

                    ListaDeletar.Add(Lista.Item(i))

                If Cont > 1000 Then

                    Try

                        If "".ToString().Equals(StringEnvio) = False Then

                            StringEnvio = Sql.ToString & Mid(StringEnvio, 1, StringEnvio.Length - 2)
                            Comando.CommandText = StringEnvio



                            Comando.ExecuteNonQuery()


                        End If

                        StringEnvio = ""
                        Cont = 0

                    Catch ex As Exception

                        M.Adicionar_Dados_list("Erro ao executar a query: " & StringEnvio, My.Forms.Frm_Inicio.lv_processo)
                        M.Adicionar_Dados_list("Erro: " & ex.Message, My.Forms.Frm_Inicio.lv_processo)
                        StringEnvio = ""

                    End Try

                End If

            End If

                Frm_Inicio.Progress_Bar.Value = Valor
            Valor = Valor + Contando
            Application.DoEvents()

        Next

        Try

            If Cont <> 0 Then

                StringEnvio = Mid(StringEnvio, 1, StringEnvio.Length - 2)
                StringEnvio = Sql.ToString & StringEnvio

                Comando.CommandText = StringEnvio

                Comando.ExecuteNonQuery()

                Cont = 0

            End If

        Catch ex As Exception

            M.Adicionar_Dados_list("Erro: " & ex.Message, My.Forms.Frm_Inicio.lv_processo)

        End Try

        Conexao.Close()

        M.Adicionar_Dados_list("Enviando para FilesUpload", My.Forms.Frm_Inicio.lv_processo)

        For i As Integer = 0 To ListaDeArquivos.Count - 1

            Inc = ListaDeArquivos.Item(i)
            Out = ListaDeArquivos.Item(i)
            Out = Out.Replace("C:\Fourkey\Admin\Files", "C:\Fourkey\Admin\FilesUpload")

            Try

                File.Copy(Inc, Out, True)
                System.Threading.Thread.Sleep(1000)
                File.Delete(Inc)

            Catch ex As Exception

                M.Adicionar_Dados_list("Erro ao enviar para FilesUpload: " & Out, My.Forms.Frm_Inicio.lv_processo)

            End Try

        Next

        ListaDeletar.Clear()
        ListaDeArquivos.Clear()
        Frm_Inicio.ListaDeArquivosNomes.Clear()

        'Catch ex As Exception
        'M.Adicionar_Dados_list(ex.Message, My.Forms.Frm_Inicio.lv_processo)
        '    MsgBox(ex.Message)
        '    Exit Sub

        'End Try

        GC.Collect()

    End Sub

    Public Sub ExcluirArquivosProcessadosAntigos()

        Dim Hoje As Date = Format(Now, "yyyy/MM/dd")
        Dim Caminho As String = "C:\Walle\Processados"
        Dim NomePart As String = ""
        Dim NomePartFinal As String = ""
        Dim Cont As Integer = 1

        Hoje = Hoje.AddDays(-5)

        For Each nome In Directory.GetFiles(Caminho)

            NomePart = Mid(nome, nome.Length - 22, nome.Length)
            NomePart = NomePart.Replace(".csv", "")
            Cont = 1
            NomePartFinal = ""

            For i As Integer = 0 To NomePart.Length - 1

                If NomePart.Chars(i) = "-" Then

                    If Cont = 1 Or Cont = 2 Then

                        NomePartFinal = NomePartFinal & "/"

                    ElseIf Cont = 3 Then

                        Exit For

                    End If

                    Cont = Cont + 1

                Else

                    NomePartFinal = NomePartFinal & NomePart.Chars(i)

                End If

            Next

            If CDate(NomePartFinal) <= Hoje Then

                File.Delete(nome)

            End If

        Next

    End Sub

    Public Sub UploadFile(ByVal _FileName As String, ByVal _UploadPath As String, ByVal _FTPUser As String, ByVal _FTPPass As String)
        Dim _FileInfo As New System.IO.FileInfo(_FileName)

        ' Create FtpWebRequest object from the Uri provided
        Dim _FtpWebRequest As System.Net.FtpWebRequest = CType(System.Net.FtpWebRequest.Create(New Uri(_UploadPath)), System.Net.FtpWebRequest)

        ' Provide the WebPermission Credintials
        _FtpWebRequest.Credentials = New System.Net.NetworkCredential(_FTPUser, _FTPPass)

        ' By default KeepAlive is true, where the control connection is not closed
        ' after a command is executed.
        _FtpWebRequest.KeepAlive = False

        ' set timeout for 20 seconds
        _FtpWebRequest.Timeout = 20000

        ' Specify the command to be executed.
        _FtpWebRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile

        ' Specify the data transfer type.
        _FtpWebRequest.UseBinary = True

        ' Notify the server about the size of the uploaded file
        _FtpWebRequest.ContentLength = _FileInfo.Length

        ' The buffer size is set to 2kb
        Dim buffLength As Integer = 2048
        Dim buff(buffLength - 1) As Byte

        ' Opens a file stream (System.IO.FileStream) to read the file to be uploaded
        Dim _FileStream As System.IO.FileStream = _FileInfo.OpenRead()

        ' Stream to which the file to be upload is written
        Dim _Stream As System.IO.Stream = _FtpWebRequest.GetRequestStream()

        ' Read from the file stream 2kb at a time
        Dim contentLen As Integer = _FileStream.Read(buff, 0, buffLength)

        ' Till Stream content ends
        Do While contentLen <> 0
            ' Write Content from the file stream to the FTP Upload Stream
            _Stream.Write(buff, 0, contentLen)
            contentLen = _FileStream.Read(buff, 0, buffLength)
        Loop

        ' Close the file stream and the Request Stream
        _Stream.Close()
        _Stream.Dispose()
        _FileStream.Close()
        _FileStream.Dispose()
    End Sub

    Public Sub CapturarDados(ByVal arquivoFTP As String, ByVal Usuario As String, ByVal Senha As String)

        Dim iContador As Integer
        Dim fwr As FtpWebRequest = FtpWebRequest.Create(arquivoFTP & "/Walle/Client/1002/File")
        Dim sr As New StreamReader(fwr.GetResponse().GetResponseStream())
        Dim str As String = sr.ReadLine()
        Dim linhaTexto As String
        Dim Cont As Boolean = False
        Dim CaminhoCop As String = System.Reflection.Assembly.GetExecutingAssembly().Location
        Dim _FileInfo As New System.IO.FileInfo("C:\Walle\Service")

        fwr.Credentials = New NetworkCredential(Usuario, Senha)
        fwr.Method = WebRequestMethods.Ftp.ListDirectory
        sr = New StreamReader(fwr.GetResponse().GetResponseStream())
        str = sr.ReadLine()

        'Procurando lincenca
        iContador = 0
        While Not str Is Nothing

            linhaTexto = str
            str = sr.ReadLine()

            fwr = WebRequest.Create(arquivoFTP & "/Walle/Client/1002/File/" & linhaTexto)
            fwr.Credentials = New NetworkCredential(Usuario, Senha)
            fwr.Method = WebRequestMethods.Ftp.UploadFile
            Dim ftpResp As FtpWebResponse = fwr.GetResponse

        End While

        sr.Close()
        sr = Nothing
        fwr = Nothing

    End Sub

    Public Sub Enviar_Confirmação()

        Dim Assunto, Corpo, Nome As String
        Dim Usuario As String = "fourkey@hotmail.com"
        Dim Senha As String = "$Tanqueviski69"
        Dim SMTP As String = "smtp.live.com"

        Dim Destinatário As String = "luizh_s@hotmail.com"
        Dim Lista As New List(Of String)
        'Lista.Add("luizh_s@hotmail.com")
        'Lista.Add("luizmbc@gmail.com")
        'Lista.Add("gustavo.up@gmail.com")
        'Lista.Add("tirepafe@gmail.com")

        Assunto = "STATUS EXECUÇÃO WALLE CLIENT SERVICE"
        Corpo = "Este e-mail foi enviado automaticamente pelo Walle, não responda. </br></br>" & Chr(13) & Chr(13) &
                "Aplicação: Walle Client Service </br>" & Chr(13) &
                "Servidor: " & Environment.MachineName & Chr(13) & "</br> " &
                "Status: EXECUTANDO </br></br></br>" & Chr(13) & Chr(13) & Chr(13) &
                "Walle - Um produto Fourkey - © 2017"

        Nome = "Status Report Walle"

        Try
            Enviar_Email(Assunto, Corpo, Destinatário, SMTP, Lista, Usuario, Senha, Nome)
        Catch ex As Exception

        End Try



    End Sub

    Public Function Enviar_Email(Assunto As String,
                                     CorpoEmail As String,
                                     Destinatário As String,
                                     ClientSMTP As String,
                                     Lista_CC As List(Of String),
                                     Usuário As String,
                                     Senha As String,
                                     Nome As String,
                                     Optional Anexo As String = "")

        Dim cliente = New SmtpClient()
        cliente.Host = ClientSMTP 'servidor
        cliente.EnableSsl = True
        cliente.UseDefaultCredentials = False

        cliente.Credentials = New System.Net.NetworkCredential(Usuário, Senha) ' //usario e senha

        Dim message = New MailMessage()
        message.Sender = New MailAddress(Usuário, Nome) '; //remetente
        message.From = New MailAddress(Usuário, Nome) '; //remetente
        message.To.Add(Destinatário) '; //destinatario
        If Anexo <> "" Then
            message.Attachments.Add(New Attachment(Anexo))
        End If
        If Lista_CC.Count <> 0 Then
            For i As Integer = 0 To Lista_CC.Count - 1
                message.CC.Add(Lista_CC.Item(i).ToString)
            Next
        End If
        message.Subject = Assunto '; //assunto
        message.IsBodyHtml = True '; //aceita html
        message.Body = CorpoEmail '; //corpo do email

        cliente.Send(message) '; //enviar

        Return True
    End Function

    Public Sub Registrar_Execução()

        'Nome do arquivo que será criado
        Dim NomeArquivo As String = "C:\Fourkey\Admin\FilesUpload\Upload.4fk"

        If File.Exists("C:\Fourkey\Admin\FilesUpload\Upload.4fk") Then
            File.Delete("C:\Fourkey\Admin\FilesUpload\Upload.4fk")
        End If

        Try

            'Cria o arquivo no formato CSV
            Dim Strm As New StreamWriter(NomeArquivo, True, encoding:=UnicodeEncoding.Unicode)

            'Titulo das colunas
            Strm.WriteLine(Format(Now, "yyyy/MM/dd HH:mm:ss"))

            Strm.Close()

        Catch ex As Exception

            '

        End Try

    End Sub

    Public Sub Salvar_Log()

        'Nome do arquivo que será criado
        Dim NomeArquivo As String = "C:\Fourkey\Admin\" & Format(Now, "YYYY_MM_DD") & "_Log_Walle_Client_Service.txt"


        Try
            Dim fluxoTexto As IO.StreamWriter
            'Dim linhaTexto As String

            If IO.File.Exists(NomeArquivo) Then
                fluxoTexto = New IO.StreamWriter(NomeArquivo, True, encoding:=UnicodeEncoding.Unicode)

                For i As Integer = 0 To Frm_Inicio.lv_processo.Items.Count - 1
                    fluxoTexto.WriteLine(Frm_Inicio.lv_processo.Items(i).SubItems(0).Text & " - " & Frm_Inicio.lv_processo.Items(i).SubItems(1).Text)
                Next

                fluxoTexto.Close()

            Else

                fluxoTexto = New StreamWriter(NomeArquivo, True, encoding:=UnicodeEncoding.Unicode)

                For i As Integer = 0 To Frm_Inicio.lv_processo.Items.Count - 1
                    fluxoTexto.WriteLine(Frm_Inicio.lv_processo.Items(i).SubItems(0).Text & " - " & Frm_Inicio.lv_processo.Items(i).SubItems(1).Text)
                Next

                fluxoTexto.Close()

            End If


            Frm_Inicio.lv_processo.Items.Clear()

        Catch ex As Exception

            '

        End Try

    End Sub



    'Public Sub BuscarCategorias()

    '    Dim Comando As New MySqlCommand
    '    Dim Sql As New StringBuilder
    '    Dim Conexao As New MySqlConnection
    '    Dim Reader As MySqlDataReader

    '    Comando.CommandText("Select * from discounts", Conexao)
    '    Reader = Comando.ExecuteReader

    'End Sub

End Class

