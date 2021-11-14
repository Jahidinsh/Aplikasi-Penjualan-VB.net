Imports System.Data.Odbc
Public Class Formtransretur
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
    Sub kondisiawal()
        LBLNamaPLG.Text = ""
        LBLNamaBaarang.Text = ""
        LBLAlamat.Text = ""
        LBLTanggal.Text = Today
        LBLAdmin.Text = FormUtama.STLabel4.Text
        LBLHargaBarang.Text = ""
        TextBox2.Text = ""
        TextBox1.Text = ""
        TextBox3.Text = ""
        LBLKodePLG.Text = ""
        'ComboBox1.Text = ""
        TextBox3.Enabled = False
        'LBLItem.Text = ""

    End Sub
    Sub nomorotomatis()
        Call koneksi()
        CMD = New OdbcCommand("select * from tbl_retur where noretur in(select max(noretur)from tbl_retur)", conn)
        Dim UrutanKode As String
        Dim hitung As Long
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            UrutanKode = "R" + Format(Now, "yymmdd") + "001"
        Else
            hitung = Microsoft.VisualBasic.Right(RD.GetString(0), 9) + 1
            UrutanKode = "R" + Format(Now, "yymmdd") + Microsoft.VisualBasic.Right("000" & hitung, 3)

        End If
        LBLNoretur.Text = UrutanKode
    End Sub
    'Sub munculkodepelanggan()
    '    Call koneksi()
    '    ComboBox1.Items.Clear()
    '    CMD = New OdbcCommand("Select * from tbl_Pelanggan", conn)
    '    RD = CMD.ExecuteReader
    '    Do While RD.Read
    '        ComboBox1.Items.Add(RD.Item(0))
    '    Loop
    'End Sub
    'Sub buatkolom()
    '    DataGridView1.Columns.Clear()
    '    DataGridView1.Columns.Add("Kode", "Kode")
    '    DataGridView1.Columns.Add("Namabarang", "Nama Barang")
    '    DataGridView1.Columns.Add("Harga", "Harga")
    '    DataGridView1.Columns.Add("Jumlah", "Jumlah")
    '    DataGridView1.Columns.Add("Subtotal", "Subtotal")
    'End Sub
    Private Sub Formtransretur_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call nomorotomatis()
        Call kondisiawal()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LBLJam.Text = TimeOfDay
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call munculdata()

        End If
        '    If Val(TextBox1.Text) < Val(Label17.Text) Then
        '        MsgBox("Pembayaran Kurang")
        '    ElseIf Val(TextBox1.Text) = Val(Label17.Text) Then
        '        LBLKembali.Text = 0
        '    ElseIf Val(TextBox1.Text) > Val(Label17.Text) Then
        '        LBLKembali.Text = Val(TextBox1.Text) - Val(Label17.Text)
        '        Button1.Focus()
        '    End If
        'End If
    End Sub
    Sub munculdata()
        Call koneksi()
        da = New OdbcDataAdapter("select kodebarang,namabarang,hargajual,jumlahjual,subtotal from tbl_detailjual where nojual='" & TextBox1.Text & "'", conn)
        ds = New DataSet
        da.Fill(ds, "tbl_detailjual")
        DataGridView2.DataSource = ds.Tables("tbl_detailjual")
        DataGridView2.ReadOnly = True
        Call koneksi()
        CMD = New OdbcCommand("select * from tbl_jual where nojual='" & TextBox1.Text & "'", conn)
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            MsgBox("No jual Tidak Ada")
        Else
            LBLKodePLG.Text = RD.Item("kodepelanggan")
            LBLAdmin.Text = RD.Item("kodeadmin")
        End If
        Call koneksi()
        CMD = New OdbcCommand("select * from tbl_pelanggan where kodepelanggan='" & LBLKodePLG.Text & "'", conn)
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            MsgBox("Data Tidak Ada")
        Else
            LBLNamaPLG.Text = RD.Item("Nama")
            LBLAlamat.Text = RD.Item("alamat")
        End If

    End Sub

    Private Sub DataGridView2_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick

    End Sub
End Class