Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class Form1
    Dim cn As MySqlConnection

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Button1.Text = "Se connecter" Then

            cn = New MySqlConnection("SERVER=" & serveurtxt.Text & ";PORT=" & Porttxt.Text & ";DATABASE=" & Bdtxt.Text & ";UID=" & nomUtilisateurtxt.Text & ";PWD=" & Mdptxt.Text)
            Try
                If cn.State = ConnectionState.Closed Then
                    cn.Open()
                    Button1.Text = "Se deconnecter"
                End If
            Catch ex As Exception

            End Try
        Else
            cn.Close()
            Button1.Text = "Se connecter"
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If ConnectionState.Open Then

            If AjoutNomtxt.Text.Trim() = "" Then
                MessageBox.Show("Entrez un nom")
            ElseIf AjoutPrenomtxt.Text.Trim() = "" Then
                MessageBox.Show("Entrez un prenom")
            ElseIf AjoutClassetxt.Text.Trim() = "" Then
                MessageBox.Show("Entrez la classe")
            ElseIf AjoutEmailtxt.Text.Trim() = "" Then
                MessageBox.Show("Entrez un Email")
            Else

                Dim cmd As New MySqlCommand("INSERT INTO " & Tabletxt.Text & "(Nom,Prenom,Classe,Email) VALUES(@Nom,@Prenom,@Classe,@Email)", cn)
                cmd.Parameters.AddWithValue("@Nom", AjoutNomtxt.Text)
                cmd.Parameters.AddWithValue("@Prenom", AjoutPrenomtxt.Text)
                cmd.Parameters.AddWithValue("@Classe", AjoutClassetxt.Text)
                cmd.Parameters.AddWithValue("@Email", AjoutEmailtxt.Text)
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                AjoutNomtxt.Clear()
                AjoutPrenomtxt.Clear()
                AjoutClassetxt.Clear()
                AjoutEmailtxt.Clear()
                MessageBox.Show("Ajout termine")

            End If
        Else
            MessageBox.Show("La connection n'est pas ouverte !", "FAILED")
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If cn.State = ConnectionState.Open Then
            ListView1.Items.Clear()

            Dim cmd As New MySqlCommand("SELECT * FROM " & Tabletxt.Text, cn)
            Using L As MySqlDataReader = cmd.ExecuteReader()

                While L.Read()
                    Dim ID As String = L("ID")
                    Dim Nom As String = L("Nom")
                    Dim Prenom As String = L("Prenom")
                    Dim Classe As String = L("Classe")
                    Dim Email As String = L("Email")

                    ListView1.Items.Add(New ListViewItem(New String() {ID, Nom, Prenom, Classe, Email}))
                End While

            End Using
        End If
    End Sub

    'Reste a faire (Suppression des donnees et modification)
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ConnectionState.Open Then

            If supptxt.Text.Trim() = "" Then
                MessageBox.Show("Entrez un Nom")

            Else

                Dim cmd2 As New MySqlCommand("DELETE FROM " & Tabletxt.Text & "WHERE Nom=@Nom", cn)
                cmd2.Parameters.AddWithValue("@Nom", supptxt.Text)
                cmd2.ExecuteNonQuery()
                cmd2.Parameters.Clear()
                supptxt.Clear()
                MessageBox.Show("Suppression termine")

            End If
        Else
            MessageBox.Show("La connection n'est pas ouverte !", "FAILED")
        End If
    End Sub
End Class
