Public Class Manipular_ListView

    Sub Adicionar_Dados_list(descrição As String, listtemp As ListView)
        Dim lvi As New ListViewItem(Format(Now, "yyyy/MM/dd hh:mm:ss"))
        lvi.SubItems.Add(descrição)
        listtemp.Items.Add(lvi)
    End Sub

End Class
