Imports System.Data.Odbc
Public Class FormBarang
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
    Sub siapisi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        ComboBox1.Enabled = True
        Call isikategori()
    End Sub
    Sub isikategori()
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("--PILIH--")
        ComboBox1.Items.Add("Rokok")
        ComboBox1.Items.Add("Kosmetik")
        ComboBox1.Items.Add("Sembako")
        ComboBox1.Items.Add("Obat")
        ComboBox1.Items.Add("Alat Tulis")
        ComboBox1.Items.Add("Makanan")
        ComboBox1.Items.Add("Minuman")
        ComboBox1.Items.Add("Peralatan Dapur")
        ComboBox1.Items.Add("Peralatan RT")
        ComboBox1.Items.Add("Kelistrikan")
        ComboBox1.SelectedIndex = 0
    End Sub
    Sub isicari()
        ComboBox2.Items.Clear()
        ComboBox2.Items.Add("Kode Barang")
        ComboBox2.Items.Add("Nama Barang")
        ComboBox2.Items.Add("Kategori")
        ComboBox2.SelectedIndex = 0
    End Sub
    Sub nomorotomatis()
        Call koneksi()
        CMD = New OdbcCommand("select * from tbl_barang where kodebarang in(select max(kodebarang)from tbl_barang)", conn)
        Dim UrutanKode As String
        Dim hitung As Long
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            UrutanKode = "BRG" + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(RD.GetString(0), 3) + 1
            UrutanKode = "BRG" + Microsoft.VisualBasic.Right("000" & hitung, 3)

        End If
        TextBox1.Text = UrutanKode
    End Sub
    Sub kondisiawal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        ComboBox1.Items.Clear()
        ComboBox1.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        ComboBox1.Enabled = False
        TextBox1.MaxLength = 8
        Button1.Text = "Cari"
        Button2.Text = "Tambah"
        Button3.Text = "Edit"
        Button4.Text = "Hapus"
        Button5.Text = "Tutup"
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        TextBox1.Focus()
        Call isicari()
        Call koneksi()
        da = New OdbcDataAdapter("select * from tbl_barang", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "tbl_barang")
        DataGridView1.DataSource = (ds.Tables("tbl_barang"))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Button2.Text = "Tambah" Then
            Button2.Text = "Simpan"
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Text = "Batal"
            Call siapisi()
            Call nomorotomatis()
            TextBox1.Enabled = False
            TextBox2.Focus()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Pastikan Semua Field Terisi")
            Else
                Call koneksi()
                Dim inputdata As String = "insert into tbl_barang values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')"
                CMD = New OdbcCommand(inputdata, conn)
                CMD.ExecuteNonQuery()
                MsgBox("Input Data Berhasil")
                Call kondisiawal()
            End If

        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            CMD = New OdbcCommand("select * from tbl_barang where kodebarang='" & TextBox1.Text & "'", conn)
            RD = CMD.ExecuteReader
            RD.Read()
            If Not RD.HasRows Then
                MsgBox("Data Tidak Ada")
            Else
                TextBox1.Text = RD.Item("Kodebarang")
                TextBox2.Text = RD.Item("namabarang")
                TextBox3.Text = RD.Item("harga")
                TextBox4.Text = RD.Item("stok")
                ComboBox1.Text = RD.Item("kategori")
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "Edit" Then
            Button3.Text = "Simpan"
            Button2.Enabled = False
            Button4.Enabled = False
            Button5.Text = "Batal"
            Call siapisi()
            TextBox1.Focus()
        Else
            If TextBox1.Text = "" Or TextBox1.Text = "" Or ComboBox1.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Pastikan Semua Field Terisi")
            Else
                Call koneksi()
                Dim editdata As String = "update tbl_barang set namabarang ='" & TextBox2.Text & "',kategori='" & ComboBox1.Text & "',harga='" & TextBox3.Text & "',stok='" & TextBox4.Text & "'where kodebarang='" & TextBox1.Text & "'"
                CMD = New OdbcCommand(editdata, conn)
                CMD.ExecuteNonQuery()
                MsgBox("Edit Data Berhasil")
                Call kondisiawal()
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Button5.Text = "Tutup" Then
            Me.Close()
        Else
            Call kondisiawal()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Button4.Text = "Hapus" Then
            Button4.Text = "Delete"
            Button2.Enabled = False
            Button3.Enabled = False
            Button5.Text = "Batal"
            Call siapisi()
            TextBox1.Focus()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Pastikan Data yang akan di Hapus Terisi")
            Else
                Call koneksi()
                Dim hapusdata As String = "delete from tbl_barang where kodebarang='" & TextBox1.Text & "'"
                CMD = New OdbcCommand(hapusdata, conn)
                CMD.ExecuteNonQuery()
                MsgBox("Delete Data Berhasil")
                Call kondisiawal()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sql As String = ""
        Select Case ComboBox2.SelectedIndex
            Case 0
                sql = "select * from tbl_barang where kodebarang like '%" & TextBox5.Text & "%'"
            Case 1
                sql = "select * from tbl_barang where namabarang like '%" & TextBox5.Text & "%'"
            Case 2
                sql = "select * from tbl_barang where kategori like '%" & TextBox5.Text & "%'"
        End Select
        Dim da As New OdbcDataAdapter(sql, conn)
        Dim ds As New DataSet
        da.Fill(ds, sql)
        DataGridView1.DataSource = (ds.Tables(sql))
        If DataGridView1.RowCount > 0 Then
            MsgBox("Data yang di cari ada=" & DataGridView1.RowCount, vbInformation, "Ini data yang di cari")
            TextBox5.Text = ""
        Else
            MsgBox("Data tidak di temukan", vbInformation, "Cari Data Yang Lain")
            Call kondisiawal()

        End If
    End Sub

    Private Sub FormBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiawal()

    End Sub

    
    
End Class