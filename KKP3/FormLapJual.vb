Public Class FormLapJual
    Sub isitahun()
        ComboBox2.Items.Clear()
        ComboBox2.Text = Date.Now.Year
        For i As Integer = 0 To 5
            ComboBox2.Items.Add(Date.Now.Year + i)
        Next
       
    End Sub
    Sub isibulan()
        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("01 - JANUARI")
        ComboBox1.Items.Add("02 - FEBRUARI")
        ComboBox1.Items.Add("03 - MARET")
        ComboBox1.Items.Add("04 - APRIL")
        ComboBox1.Items.Add("05 - MEI")
        ComboBox1.Items.Add("06 - JUNI")
        ComboBox1.Items.Add("07 - JULI")
        ComboBox1.Items.Add("08 - AGUSTUS")
        ComboBox1.Items.Add("09 - SEPTEMBER")
        ComboBox1.Items.Add("10 - OKTOBER")
        ComboBox1.Items.Add("11 - NOVEMBER")
        ComboBox1.Items.Add("12 - DESEMBER")
    End Sub
    Private Sub FormLapJual_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call isibulan()
        Call isitahun()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AxCrystalReport1.SelectionFormula = "totext({tbl_Jual.TGLJUAL})='" & DateTimePicker1.Value & "'"
        AxCrystalReport1.ReportFileName = "LaporanHarian.rpt"
        AxCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized
        AxCrystalReport1.RetrieveDataFiles()
        AxCrystalReport1.Action = 1
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ComboBox1.Text = "" Or ComboBox2.Text = "" Then
            MsgBox("Isi Bulan Dan Tahun Dulu")
        End If
        AxCrystalReport1.SelectionFormula = "Month({tbl_Jual.TGLJUAL})=" & Val(ComboBox1.Text) & " and year({tbl_jual.tgljual})=" & Val(ComboBox2.Text)
        AxCrystalReport1.ReportFileName = "LaporanBulanan.rpt"
        AxCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized
        AxCrystalReport1.RetrieveDataFiles()
        AxCrystalReport1.Action = 1
    End Sub

    
End Class