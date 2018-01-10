Imports MySql.Data.MySqlClient
Imports System.IO

Public Class Frm_Inicio

    Dim M As New Manipular_ListView

    Dim Funcao As New Funcao_Sistema
    Public Bloq As Boolean = False
    Public ListaDeArquivosNomes As New ArrayList

    Dim Ultimo_Envio As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        t_automatico.Enabled = True

        'Dim Cont As Integer = 0

        'Funcao.ExcluirArquivosProcessadosAntigos()

        'For Each processo As Process In Process.GetProcesses()

        '    If processo.ProcessName = "Walle_Client_Service" Then

        '        Cont = Cont + 1

        '    End If

        'Next processo

        'If Cont > 1 Then

        '    Application.Exit()

        'End If

        'Iniciar()

        't_inicio.Enabled = True

        'Me.ShowInTaskbar = False
        'Me.WindowState = FormWindowState.Minimized


    End Sub


    Private Sub Iniciar()

        Funcao.Salvar_Log()

        If (Ultimo_Envio = "") Then

            'Funcao.Enviar_Confirmação()
            Ultimo_Envio = Format(Now, "yyyy/MM/dd HH:mm:ss")

        ElseIf (Format(Now, "yyyy/MM/dd HH:mm:ss") > Format(CDate(Ultimo_Envio).AddHours(1), "yyyy/MM/dd HH:mm:ss")) Then

            'Funcao.Enviar_Confirmação()
            Ultimo_Envio = Format(Now, "yyyy/MM/dd HH:mm:ss")

        End If

        Dim ListaArquivos As New ArrayList
        Dim fluxoTexto As IO.StreamReader
        Dim linhaTexto As String
        Dim Chave As String = ""

        For i As Integer = 1000 To 1010
            'i=1002
            ListaDeArquivosNomes.Clear()
            ListaArquivos.Clear()

            M.Adicionar_Dados_list("Iniciando: " & i, My.Forms.Frm_Inicio.lv_processo)

            Txt_Cliente.Text = i
            Progress_Bar.Value = 0
            Lab_Pro.Text = "Iniciando..."
            Application.DoEvents()

            If Directory.Exists("C:\Fourkey\Admin\FilesUpload\" & i) = False Then

                Directory.CreateDirectory("C:\Fourkey\Admin\FilesUpload\" & i)

            End If

            ListaArquivos = Funcao.LerPasta(i)

            If ListaArquivos.Count > 0 Then

                If IO.File.Exists("C:\Fourkey\Admin\Config\" & i & "\Key.txt") Then

                    fluxoTexto = New IO.StreamReader("C:\Fourkey\Admin\Config\" & i & "\Key.txt")
                    linhaTexto = fluxoTexto.ReadLine
                    Chave = linhaTexto
                    fluxoTexto.Close()

                    Funcao.UploadArquivos(ListaArquivos, Chave, i)

                End If

            End If

        Next

        Funcao.Registrar_Execução()


    End Sub

    Private Sub t_inicio_Tick(sender As Object, e As EventArgs) Handles t_inicio.Tick

        If Bloq = False Then

            Bloq = True
            Iniciar()
            Bloq = False

        End If

    End Sub

    Private Sub NotifyIcon1_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon1.DoubleClick

        Me.Show()
        Me.WindowState = FormWindowState.Normal
        Me.ShowInTaskbar = True

    End Sub

    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize

        If Me.WindowState = FormWindowState.Minimized Then

            Me.Hide()

        End If

    End Sub

    Private Sub tb_password_KeyDown(sender As Object, e As KeyEventArgs)

        'If e.KeyCode = Keys.Enter Then

        '    If tb_password.Text = "fourkey" Then

        '        For Each processo As Process In Process.GetProcesses()

        '            If processo.ProcessName = "Walle_Client" Then

        '                processo.Kill()

        '            End If

        '        Next processo

        '        Application.Exit()

        '    Else

        '        tb_password.Clear()

        '    End If

        'End If

    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick

    End Sub

    Private Sub tb_password_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim Banco As New Conexao_MySql

        Banco.GetConexao()

        t_automatico.Enabled = False

        M.Adicionar_Dados_list("Iniciando execução do programa", My.Forms.Frm_Inicio.lv_processo)

        Iniciar()
        t_inicio.Enabled = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        M.Adicionar_Dados_list("Cancelar execução do programa", My.Forms.Frm_Inicio.lv_processo)

        t_inicio.Enabled = False

    End Sub

    Private Sub t_automatico_Tick(sender As Object, e As EventArgs) Handles t_automatico.Tick

        t_automatico.Enabled = False

        M.Adicionar_Dados_list("Iniciando execução do programa", My.Forms.Frm_Inicio.lv_processo)

        Iniciar()
        t_inicio.Enabled = True

    End Sub
End Class
