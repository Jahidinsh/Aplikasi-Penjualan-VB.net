Imports System.Data.Odbc
Public Class FormPelanggan
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
    Sub nomorotomatis()
        Call koneksi()
        CMD = New OdbcCommand("select * from tbl_pelanggan where kodepelanggan in(select max(kodepelanggan)from tbl_pelanggan)", conn)
        Dim UrutanKode As String
        Dim hitung As Long
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            UrutanKode = "PLG" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(RD.GetString(0), 3) + 1
            UrutanKode = "PLG" + Microsoft.VisualBasic.Right("000" & hitung, 3)
        End If
        TextBox1.Text = UrutanKode
    End Sub
    Sub kondisiawal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox1.MaxLength = 8
        Button1.Text = "Tambah"
        Button2.Text = "Edit"
        Button3.Text = "Hapus"
        Button4.Text = "Tutup"
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        TextBox1.Focus()
        'Call isilevel()
        Call koneksi()
        da = New OdbcDataAdapter("select * from tbl_pelanggan", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "tbl_pelanggan")
        DataGridView1.DataSource = (ds.Tables("tbl_pelanggan"))
    End Sub
    Sub siapisi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
    End Sub
    Private Sub FormPelanggan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiawal()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            CMD = New OdbcCommand("select * from tbl_pelanggan where kodepelanggan='" & TextBox1.Text & "'", conn)
            RD = CMD.ExecuteReader
            RD.Read()
            If Not RD.HasRows Then
                MsgBox("Data Tidak Ada")
            Else
                TextBox1.Text = RD.Item("Kodepelanggan")
                TextBox2.Text = RD.Item("nama")
                TextBox3.Text = RD.Item("alamat")
                TextBox4.Text = RD.Item("telepon")
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Tambah" Then
            Button1.Text = "Simpan"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call siapisi()
            Call nomorotomatis()
            TextBox1.Enabled = False
            TextBox2.Focus()
            TextBox4.MaxLength = 13
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox3.Text = "" Then
                MsgBox("Pastikan Semua Field Terisi")
            Else
                Call koneksi()
                Dim inputdata As String = "insert into tbl_pelanggan values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')"
                CMD = New OdbcCommand(inputdata, conn)
                CMD.ExecuteNonQuery()
                MsgBox("Input Data Berhasil")
                Call kondisiawal()

            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Edit" Then
            Button1.Enabled = False
            Button2.Text = "Simpan"
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call siapisi()
            TextBox4.MaxLength = 13
            TextBox1.Focus()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox3.Text = "" Then
                MsgBox("Pastikan Semua Field Terisi")
            Else
                Call koneksi()
                Dim editdata As String = "update tbl_pelanggan set nama ='" & TextBox2.Text & "',alamat='" & TextBox3.Text & "',telepon='" & TextBox4.Text & "'where kodepelanggan='" & TextBox1.Text & "'"
                CMD = New OdbcCommand(editdata, conn)
                CMD.ExecuteNonQuery()
                MsgBox("Edit Data Berhasil")
                Call kondisiawal()
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Hapus" Then
            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Text = "Delete"
            Button4.Text = "Batal"
            Call siapisi()
            TextBox1.Focus()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox3.Text = "" Then
                MsgBox("Pastikan Data yang akan di Hapus Terisi")
            Else
                Call koneksi()
                Dim hapusdata As String = "delete from tbl_pelanggan where kodepelanggan='" & TextBox1.Text & "'"
                CMD = New OdbcCommand(hapusdata, conn)
                CMD.ExecuteNonQuery()
                MsgBox("Delete Data Berhasil")
                Call kondisiawal()
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Button4.Text = "Tutup" Then
            Me.Close()
        Else
            Call kondisiawal()
        End If
    End Sub
End Class