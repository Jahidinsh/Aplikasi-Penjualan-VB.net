Imports System.Data.Odbc
Public Class FormLogin
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
    Sub terbuka()
        Formutama.LoginToolStripMenuItem.Enabled = False
        Formutama.LogOutToolStripMenuItem.Enabled = True
        Formutama.MasterToolStripMenuItem.Enabled = True
        Formutama.TransaksiToolStripMenuItem.Enabled = True
        Formutama.LaporanToolStripMenuItem.Enabled = True
        Formutama.UtilityToolStripMenuItem.Enabled = True
    End Sub
    Sub kondisiawal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox1.Focus()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("data login belum lengkap")

        Else

            Call koneksi()
            CMD = New OdbcCommand("select * from tbl_admin where username='" & TextBox1.Text & "' and pass='" & TextBox2.Text & "'", conn)
            RD = CMD.ExecuteReader
            RD.Read()
            If RD.HasRows Then
                Me.Close()
                Call terbuka()
                FormUtama.STLabel2.Text = RD!kodeadmin
                FormUtama.STLabel4.Text = RD!username
                FormUtama.STLabel6.Text = RD!level
                If FormUtama.STLabel6.Text = "User" Then
                    FormUtama.DataAdminToolStripMenuItem.Enabled = False
                Else
                    FormUtama.DataAdminToolStripMenuItem.Enabled = True
                End If

            Else
                MsgBox("Kode Admin atau Password salah")
            End If

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiawal()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox2.UseSystemPasswordChar = False
        Else
            TextBox2.UseSystemPasswordChar = True


        End If
    End Sub
End Class
