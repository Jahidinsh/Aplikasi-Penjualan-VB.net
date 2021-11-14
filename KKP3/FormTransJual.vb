Imports System.Data.Odbc
Public Class FormTransJual
    Dim TglMysql As String
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
    Sub isicari()
        ComboBox2.Items.Clear()
        ComboBox2.Items.Add("Kode Barang")
        ComboBox2.Items.Add("Nama Barang")
        ComboBox2.Items.Add("Kategori")
        ComboBox2.SelectedIndex = 0
    End Sub
    Sub munculdata()
        Call koneksi()
        da = New OdbcDataAdapter("select kodebarang,namabarang from tbl_barang", conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "tbl_barang")
        DataGridView2.DataSource = (ds.Tables("tbl_barang"))
    End Sub
    Sub kondisiawal()
        LBLNama.Text = ""
        LBLTelepon.Text = ""
        LBLAlamat.Text = ""
        LBLTanggal.Text = Today
        LBLAdmin.Text = Formutama.STLabel4.Text
        LBLKembali.Text = ""
        TextBox2.Text = ""
        TextBox1.Text = ""
        ComboBox1.Text = ""
        LBLNamaBaarang.Text = ""
        LBLHargaBarang.Text = ""
        TextBox3.Enabled = False
        LBLItem.Text = ""
        Call isicari()


    End Sub
    Sub nomorotomatis()
        Call koneksi()
        CMD = New OdbcCommand("select * from tbl_jual where nojual in(select max(nojual)from tbl_jual)", conn)
        Dim UrutanKode As String
        Dim hitung As Long
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            UrutanKode = "J" + Format(Now, "yymmdd") + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(RD.GetString(0), 9) + 1
            UrutanKode = "J" + Format(Now, "yymmdd") + Microsoft.VisualBasic.Right("000" & hitung, 3)

        End If
        LBLNoJual.Text = UrutanKode
    End Sub
    Sub munculkodepelanggan()
        Call koneksi()
        ComboBox1.Items.Clear()
        CMD = New OdbcCommand("Select * from tbl_Pelanggan", conn)
        RD = CMD.ExecuteReader
        Do While RD.Read
            ComboBox1.Items.Add(RD.Item(0))
        Loop
    End Sub
    Sub buatkolom()
        DataGridView1.Columns.Clear()
        DataGridView1.Columns.Add("Kode", "Kode")
        DataGridView1.Columns.Add("Namabarang", "Nama Barang")
        DataGridView1.Columns.Add("Harga", "Harga")
        DataGridView1.Columns.Add("Jumlah", "Jumlah")
        DataGridView1.Columns.Add("Subtotal", "Subtotal")
    End Sub
    Private Sub FormTransJual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call kondisiawal()
        Call munculdata()
        Call munculkodepelanggan()
        Call nomorotomatis()
        Call buatkolom()
    End Sub
    Sub rumussubtotal()
        Dim hitung As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            hitung = hitung + DataGridView1.Rows(i).Cells(4).Value
            Label17.Text = hitung
        Next
    End Sub
    

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call koneksi()
        CMD = New OdbcCommand("Select * from tbl_Pelanggan where kodepelanggan ='" & ComboBox1.Text & "'", conn)
        RD = CMD.ExecuteReader
        RD.Read()
        If RD.HasRows Then
            LBLNama.Text = RD!nama
            LBLAlamat.Text = RD!alamat
            LBLTelepon.Text = RD!telepon
        End If
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            CMD = New OdbcCommand("select * from tbl_barang where kodebarang='" & TextBox2.Text & "'", conn)
            RD = CMD.ExecuteReader
            RD.Read()
            If Not RD.HasRows Then
                MsgBox("Data Tidak Ada")
                Call kondisiawal()
            Else
                TextBox2.Text = RD.Item("kodebarang")
                LBLNamaBaarang.Text = RD.Item("namabarang")
                LBLHargaBarang.Text = RD.Item("harga")
                LBLJumlahBarang.Text = RD.Item("stok")
                TextBox3.Enabled = True
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If LBLNamaBaarang.Text = "" Or TextBox3.Text = "" Then
            MsgBox(" Silahka Masukan Kode Barang dan tekan Enter")
        Else
            If Val(LBLJumlahBarang.Text) < Val(TextBox3.Text) Then
                MsgBox("Stok Kurang")
            Else
                If Val(LBLJumlahBarang.Text) < Val(TextBox3.Text) Then
                    MsgBox("Stok Kurang")
                Else
                    DataGridView1.Rows.Add(New String() {TextBox2.Text, LBLNamaBaarang.Text, LBLHargaBarang.Text, TextBox3.Text, Val(LBLHargaBarang.Text) * Val(TextBox3.Text)})
                    Call rumussubtotal()
                    TextBox2.Text = ""
                    LBLNamaBaarang.Text = ""
                    LBLHargaBarang.Text = ""
                    TextBox3.Text = ""
                    TextBox3.Enabled = False
                    TextBox2.Focus()
                    Call rumuscariitem()
                End If
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If Val(TextBox1.Text) < Val(Label17.Text) Then
                MsgBox("Pembayaran Kurang")
            ElseIf Val(TextBox1.Text) = Val(Label17.Text) Then
                LBLKembali.Text = 0
            ElseIf Val(TextBox1.Text) > Val(Label17.Text) Then
                LBLKembali.Text = Val(TextBox1.Text) - Val(Label17.Text)
                Button1.Focus()
            End If
        End If
    End Sub
    Sub rumuscariitem()
        Dim hitungitem As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            hitungitem = hitungitem + DataGridView1.Rows(i).Cells(3).Value
            LBLItem.Text = hitungitem

        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If LBLKembali.Text = "" Or LBLNama.Text = "" Or Label17.Text = "" Then
            MsgBox("Transaksi Tidak ada Silahkan Transaksi terlebih dahulu")
        Else
                Dim simpanjual As String = "insert into tbl_jual values('" & LBLNoJual.Text & "','" & LBLTanggal.Text & "','" & LBLJam.Text & "','" & LBLItem.Text & "','" & Label17.Text & "','" & TextBox1.Text & "','" & LBLKembali.Text & "','" & ComboBox1.Text & "','" & FormUtama.STLabel2.Text & "')"
                CMD = New OdbcCommand(simpanjual, conn)
                CMD.ExecuteNonQuery()
                For baris As Integer = 0 To DataGridView1.Rows.Count - 2
                    Dim simpandetail As String = "insert into tbl_detailjual values('" & LBLNoJual.Text & "','" & DataGridView1.Rows(baris).Cells(0).Value & "','" & DataGridView1.Rows(baris).Cells(1).Value & "','" & DataGridView1.Rows(baris).Cells(2).Value & "','" & DataGridView1.Rows(baris).Cells(3).Value & "','" & DataGridView1.Rows(baris).Cells(4).Value & "')"
                    CMD = New OdbcCommand(simpandetail, conn)
                    CMD.ExecuteNonQuery()
                    CMD = New OdbcCommand("select * from tbl_barang where kodebarang='" & DataGridView1.Rows(baris).Cells(0).Value & "'", conn)
                    RD = CMD.ExecuteReader
                    RD.Read()
                    Dim kurangistok As String = "update tbl_barang set stok='" & RD.Item("stok") - DataGridView1.Rows(baris).Cells(3).Value & "'where kodebarang='" & DataGridView1.Rows(baris).Cells(0).Value & "'"
                    CMD = New OdbcCommand(kurangistok, conn)
                    CMD.ExecuteNonQuery()
                Next
                If MessageBox.Show("Apakah ingin cetak nota...?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    AxCrystalReport1.SelectionFormula = "totext({tbl_Jual.NoJual})='" & LBLNoJual.Text & "'"
                    AxCrystalReport1.ReportFileName = "notajual.rpt"
                    AxCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized
                    AxCrystalReport1.RetrieveDataFiles()
                    AxCrystalReport1.Action = 1
                    Call kondisiawal()
                Else
                    Call kondisiawal()
                    MsgBox(" Transaksi Berhasil")
            End If
        End If


    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LBLJam.Text = TimeOfDay

    End Sub

   

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim sql As String = ""
        Select Case ComboBox2.SelectedIndex
            Case 0
                sql = "select kodebarang,namabarang from tbl_barang where kodebarang like '%" & TextBox5.Text & "%'"
            Case 1
                sql = "select kodebarang,namabarang from tbl_barang where namabarang like '%" & TextBox5.Text & "%'"
            Case 2
                sql = "select kodebarang,namabarang from tbl_barang where kategori like '%" & TextBox5.Text & "%'"
        End Select
        Dim da As New OdbcDataAdapter(sql, conn)
        Dim ds As New DataSet
        da.Fill(ds, sql)
        DataGridView2.DataSource = (ds.Tables(sql))
        If DataGridView2.RowCount > 0 Then
            MsgBox("Data yang di cari ada=" & DataGridView1.RowCount, vbInformation, "Ini data yang di cari")
            TextBox5.Text = ""
        Else
            MsgBox("Data tidak di temukan", vbInformation, "Cari Data Yang Lain")
            Call kondisiawal()

        End If
    End Sub
End Class