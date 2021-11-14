Imports System.Data.Odbc

Public Class Formgantipassword
    Dim conn As OdbcConnection
    Dim da As OdbcDataAdapter
    Dim ds As DataSet
    Dim CMD As New OdbcCommand
    Dim RD As OdbcDataReader
    Dim MyDb As String
    Sub koneksi()
        MyDb = "Driver={Mysql ODBC 5.2 Unicode Driver};Database=dbpenjualan;server=localhost;uid=root"
        conn = New OdbcConnection(MyDb)
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub
    Sub kondisiawal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox1.Enabled = True
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox1.Focus()

    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Formgantipassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiawal()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            CMD = New OdbcCommand("select * from tbl_admin where kodeadmin='" & FormUtama.STLabel2.Text & "' and pass='" & TextBox1.Text & "'", conn)
            RD = CMD.ExecuteReader
            RD.Read()
            If RD.HasRows Then
                TextBox2.Enabled = True
                TextBox3.Enabled = True
                TextBox2.Focus()
            Else
                
                MsgBox("Password Lama Salah")
                TextBox1.Text = ""
            End If
        End If
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("Password Baru Harus Diisi")
        Else
            If TextBox2.Text <> TextBox3.Text Then
                MsgBox(" Password Baru Dan Konfirmasi Pasword Harus Sama")
            Else
                Call koneksi()
                Dim EditPass As String = "update tbl_admin set pass='" & TextBox3.Text & "'where kodeadmin='" & FormUtama.STLabel2.Text & "'"
                CMD = New OdbcCommand(EditPass, conn)
                CMD.ExecuteNonQuery()
                MsgBox("Edit Password Berhasil")
                Me.Close()
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox1.UseSystemPasswordChar = False

        Else
            TextBox1.UseSystemPasswordChar = True


        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            TextBox2.UseSystemPasswordChar = False
            TextBox3.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True
            TextBox3.UseSystemPasswordChar = True


        End If
    End Sub
End Class