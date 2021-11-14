-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 14 Nov 2021 pada 08.11
-- Versi server: 10.4.21-MariaDB
-- Versi PHP: 7.3.31

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `dbpenjualan`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbl_admin`
--

CREATE TABLE `tbl_admin` (
  `kodeadmin` varchar(15) NOT NULL,
  `username` varchar(75) NOT NULL,
  `pass` varchar(50) NOT NULL,
  `level` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tbl_admin`
--

INSERT INTO `tbl_admin` (`kodeadmin`, `username`, `pass`, `level`) VALUES
('ADM001', 'Jahidin', '123', 'Admin'),
('ADM002', 'Zili', '123', 'User'),
('ADM003', 'Saep', '123', 'Admin'),
('ADM004', 'JAKI', '124', 'User'),
('ADM005', 'Hinyai', '123', 'Admin'),
('ADM006', 'Yudi', '123', 'ADMIN'),
('ADM007', 'Santri', '123', 'USER');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbl_barang`
--

CREATE TABLE `tbl_barang` (
  `kodebarang` varchar(20) NOT NULL,
  `namabarang` varchar(20) NOT NULL,
  `kategori` varchar(25) NOT NULL,
  `harga` int(11) NOT NULL,
  `stok` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tbl_barang`
--

INSERT INTO `tbl_barang` (`kodebarang`, `namabarang`, `kategori`, `harga`, `stok`) VALUES
('BRG001', 'Sampurna Mild', 'Rokok', 26000, 10),
('BRG002', 'Sampurna Kretek', 'Rokok', 15000, 7),
('BRG003', 'Kiwi', 'Peralatan RT', 10000, 11),
('BRG004', 'Umild', 'Rokok', 22000, 44),
('BRG005', 'Surya', 'Rokok', 26000, 23),
('BRG006', 'Minyak Sayur 1/4', 'Sembako', 5000, 5),
('BRG007', 'Betadine', 'Obat', 7000, 10),
('BRG008', 'Larutan Botol Besar', 'Minuman', 7000, 5),
('BRG009', 'Roti Roma', 'Makanan', 8000, 5),
('BRG010', 'Teh Pucuk', 'Minuman', 3000, 10),
('BRG011', 'Aqua Sedang', 'Minuman', 3000, 20),
('BRG012', 'Aqua Besar', 'Minuman', 6000, 10);

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbl_detailjual`
--

CREATE TABLE `tbl_detailjual` (
  `nojual` varchar(10) NOT NULL,
  `kodebarang` varchar(20) NOT NULL,
  `namabarang` varchar(20) NOT NULL,
  `hargajual` int(11) NOT NULL,
  `jumlahjual` int(11) NOT NULL,
  `subtotal` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tbl_detailjual`
--

INSERT INTO `tbl_detailjual` (`nojual`, `kodebarang`, `namabarang`, `hargajual`, `jumlahjual`, `subtotal`) VALUES
('J211509001', 'BRG001', 'Sampurna Mild', 26000, 12, 312000),
('j212110002', 'BRG001', 'Sampurna Mild', 26000, 12, 312000),
('j212212003', 'BRG003', 'Kiwi', 10000, 2, 20000),
('j212212003', 'BRG004', 'Umild', 22000, 2, 44000),
('J215212004', 'BRG001', 'Sampurna Mild', 26000, 4, 104000),
('J211212005', 'BRG001', 'Sampurna Mild', 26000, 1, 26000),
('J215212005', 'BRG004', 'Umild', 22000, 1, 22000),
('J215212005', 'BRG002', 'Sampurna Kretek', 15000, 3, 45000),
('J210412006', 'BRG001', 'Sampurna Mild', 26000, 2, 52000),
('J210612006', 'BRG003', 'Kiwi', 10000, 1, 10000),
('J213712006', 'BRG002', 'Sampurna Kretek', 15000, 3, 45000),
('J213712006', 'BRG004', 'Umild', 22000, 1, 22000),
('J214012006', 'BRG002', 'Sampurna Kretek', 15000, 1, 15000),
('J214312006', 'BRG001', 'Sampurna Mild', 26000, 469, 12194000),
('J215112006', 'BRG004', 'Umild', 22000, 2, 44000);

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbl_detailretur`
--

CREATE TABLE `tbl_detailretur` (
  `noretur` varchar(10) NOT NULL,
  `kodebarang` varchar(6) NOT NULL,
  `namabarang` varchar(100) NOT NULL,
  `hargajual` int(11) NOT NULL,
  `jumlahretur` int(11) NOT NULL,
  `subtotal` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbl_jual`
--

CREATE TABLE `tbl_jual` (
  `nojual` varchar(10) NOT NULL,
  `tgljual` date NOT NULL,
  `jamjual` time NOT NULL,
  `itemjual` int(11) NOT NULL,
  `totaljual` int(11) NOT NULL,
  `dibayar` int(11) NOT NULL,
  `kembali` int(11) NOT NULL,
  `kodepelanggan` varchar(15) NOT NULL,
  `kodeadmin` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tbl_jual`
--

INSERT INTO `tbl_jual` (`nojual`, `tgljual`, `jamjual`, `itemjual`, `totaljual`, `dibayar`, `kembali`, `kodepelanggan`, `kodeadmin`) VALUES
('J210412006', '2021-11-12', '00:00:00', 2, 52000, 60000, 8000, 'PLG001', 'ADM001'),
('J210612006', '2021-11-12', '05:07:18', 1, 10000, 10000, 0, 'PLG002', 'ADM004'),
('J211212005', '2021-11-12', '00:00:00', 1, 26000, 50000, 24000, 'PLG001', 'ADM001'),
('J211509001', '2021-11-09', '10:15:31', 12, 312000, 350000, 38000, 'PLG001', 'ADM001'),
('j212110002', '2021-11-10', '06:21:56', 12, 312000, 400000, 88000, 'PLG001', 'ADM001'),
('j212212003', '2021-11-12', '00:00:00', 4, 64000, 70000, 6000, 'PLG002', 'ADM001'),
('J213712006', '2021-11-12', '05:38:04', 4, 67000, 100000, 33000, 'PLG002', 'ADM001'),
('J214012006', '2021-11-12', '05:40:40', 1, 15000, 20000, 5000, 'PLG001', 'ADM001'),
('J214312006', '2021-11-12', '06:46:02', 469, 12194000, 100000000, 87806000, 'PLG001', 'ADM001'),
('J215112006', '2021-11-12', '06:51:49', 2, 44000, 45000, 1000, 'PLG002', 'ADM001'),
('J215212004', '2021-11-12', '00:00:00', 4, 104000, 120000, 16000, 'PLG001', 'ADM001'),
('J215212005', '2021-11-12', '00:00:00', 4, 67000, 100000, 33000, 'PLG002', 'ADM001');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbl_pelanggan`
--

CREATE TABLE `tbl_pelanggan` (
  `kodepelanggan` varchar(15) NOT NULL,
  `nama` varchar(20) NOT NULL,
  `alamat` varchar(50) NOT NULL,
  `telepon` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data untuk tabel `tbl_pelanggan`
--

INSERT INTO `tbl_pelanggan` (`kodepelanggan`, `nama`, `alamat`, `telepon`) VALUES
('PLG001', 'Sayidah Azizah', 'Pasirranji', '081290404994'),
('PLG002', 'Samsul', 'Sogol', '0856724568765'),
('PLG003', 'Saiton', 'Pasirranji', '0875363862386');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbl_retur`
--

CREATE TABLE `tbl_retur` (
  `noretur` varchar(10) NOT NULL,
  `nojual` varchar(10) NOT NULL,
  `tglretur` date NOT NULL,
  `jamretur` time NOT NULL,
  `itemretur` int(11) NOT NULL,
  `totalretur` int(11) NOT NULL,
  `kodepelanggan` varchar(6) NOT NULL,
  `kodeadmin` varchar(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `tbl_admin`
--
ALTER TABLE `tbl_admin`
  ADD PRIMARY KEY (`kodeadmin`);

--
-- Indeks untuk tabel `tbl_barang`
--
ALTER TABLE `tbl_barang`
  ADD PRIMARY KEY (`kodebarang`);

--
-- Indeks untuk tabel `tbl_detailjual`
--
ALTER TABLE `tbl_detailjual`
  ADD KEY `tbl_detailjual_ibfk_1` (`kodebarang`);

--
-- Indeks untuk tabel `tbl_jual`
--
ALTER TABLE `tbl_jual`
  ADD PRIMARY KEY (`nojual`),
  ADD KEY `tbl_jual_ibfk_1` (`kodeadmin`),
  ADD KEY `tbl_jual_ibfk_2` (`kodepelanggan`);

--
-- Indeks untuk tabel `tbl_pelanggan`
--
ALTER TABLE `tbl_pelanggan`
  ADD PRIMARY KEY (`kodepelanggan`);

--
-- Ketidakleluasaan untuk tabel pelimpahan (Dumped Tables)
--

--
-- Ketidakleluasaan untuk tabel `tbl_detailjual`
--
ALTER TABLE `tbl_detailjual`
  ADD CONSTRAINT `tbl_detailjual_ibfk_1` FOREIGN KEY (`kodebarang`) REFERENCES `tbl_barang` (`kodebarang`) ON UPDATE CASCADE;

--
-- Ketidakleluasaan untuk tabel `tbl_jual`
--
ALTER TABLE `tbl_jual`
  ADD CONSTRAINT `tbl_jual_ibfk_1` FOREIGN KEY (`kodeadmin`) REFERENCES `tbl_admin` (`kodeadmin`) ON UPDATE CASCADE,
  ADD CONSTRAINT `tbl_jual_ibfk_2` FOREIGN KEY (`kodepelanggan`) REFERENCES `tbl_pelanggan` (`kodepelanggan`) ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
