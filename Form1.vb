Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ListRegistry()
    End Sub

    Private Sub ListRegistry()
        Dim key As Microsoft.Win32.RegistryKey = Registry.LocalMachine.OpenSubKey("")
        ' If key Is Nothing Then Exit Sub

        'Button1.Text = "AGORA" ' klave
        ListBox1.Items.Clear()

        For Each s In key.GetSubKeyNames()
            ListBox1.Items.Add(s.ToString)
        Next

    End Sub


    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        Dim t As String = ListBox1.Text
        Dim p As Integer
        Static Dim vKey As String
        Static Dim vOldKey As String

        Label1.Text = vOldKey
        If vKey = "" Then
            vKey = t ' se supone que es la primera vez que secliquea
        ElseIf t = ".." Then
            'Si vkey Contiene Barra '\'
            p = InStrRev(vKey, "\") - 1
            If p > 0 Then
                'obtener todo excepto la ultima carpeta
                Dim k As String = Microsoft.VisualBasic.Strings.Left(vKey, p)
                vKey = k
            Else
                vKey = ""
            End If
        Else
            'ya iniciaado
            '?'vOldKey = vKey
            vKey = vKey & "\" & t
        End If

        ' lo siguiente da error ed seguridad cependiendo de la clave si es SECURITY por ejemplo
        Try

            Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey(vKey)
            If key Is Nothing Then 'la clave esta malformada
                MessageBox.Show("La clave " & vKey & " no existe")
                vKey = ""
                vOldKey = ""
                Exit Sub
            End If

            ListBox1.Items.Clear()
            If InStr(vKey, "\") >= 0 Then
                ListBox1.Items.Add("..") 'acaesta la clave "..\claveanterior"
            End If
            For Each subkey In key.GetSubKeyNames()
                ListBox1.Items.Add(subkey.ToString)
            Next
            'oldK = klave
        Catch ex As Exception
            MessageBox.Show("La clave es fllida " & vKey & vbCrLf & ex.Message)
            vOldKey = ""
            vKey = ""

        End Try


    End Sub








    Private Sub ListRegistryKeys(ByVal RegistryHive As String, ByVal RegistryPath As String)
        Select Case RegistryHive
            Case "HKEY_LOCAL_MACHINE"
                Dim key As Microsoft.Win32.RegistryKey = Registry.LocalMachine.OpenSubKey(RegistryPath)
                For Each subkey In key.GetSubKeyNames
                    ListBox1.Items.Add(subkey.ToString)
                Next
            Case "HKEY_CURRENT_USER"
                Dim key As Microsoft.Win32.RegistryKey = Registry.CurrentUser.OpenSubKey(RegistryPath)
                For Each subkey In key.GetSubKeyNames
                    ListBox1.Items.Add(subkey.ToString)
                Next
            Case "HKEY_CLASSES_ROOT"
                Dim key As Microsoft.Win32.RegistryKey = Registry.ClassesRoot.OpenSubKey(RegistryPath)
                For Each subkey In key.GetSubKeyNames
                    ListBox1.Items.Add(subkey.ToString)
                Next
            Case "HKEY_CURRENT_CONFIG"
                Dim key As Microsoft.Win32.RegistryKey = Registry.CurrentConfig.OpenSubKey(RegistryPath)
                For Each subkey In key.GetSubKeyNames
                    ListBox1.Items.Add(subkey.ToString)
                Next
            Case "HKEY_USERS"
                Dim key As Microsoft.Win32.RegistryKey = Registry.Users.OpenSubKey(RegistryPath)
                For Each subkey In key.GetSubKeyNames
                    ListBox1.Items.Add(subkey.ToString)
                Next
        End Select
    End Sub




End Class
