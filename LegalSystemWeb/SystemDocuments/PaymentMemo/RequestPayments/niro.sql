-- phpMyAdmin SQL Dump
-- version 4.9.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Nov 02, 2022 at 01:17 PM
-- Server version: 10.4.10-MariaDB
-- PHP Version: 7.3.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `niro`
--

-- --------------------------------------------------------

--
-- Table structure for table `student`
--

DROP TABLE IF EXISTS `student`;
CREATE TABLE IF NOT EXISTS `student` (
  `Name` varchar(15) NOT NULL,
  `Email` varchar(30) NOT NULL,
  `Password` varchar(10) NOT NULL,
  `Date of birth` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `students`
--

DROP TABLE IF EXISTS `students`;
CREATE TABLE IF NOT EXISTS `students` (
  `Name` text NOT NULL,
  `Email` text NOT NULL,
  `Index no` int(10) NOT NULL,
  `NIC` varchar(10) NOT NULL,
  `Date of birth` date NOT NULL,
  `Country` varchar(20) NOT NULL,
  `Gender` varchar(6) NOT NULL,
  `Photo` varchar(200) NOT NULL,
  `Fluent subject areas` varchar(50) NOT NULL,
  `Hobbies` varchar(50) NOT NULL,
  `Comment` text NOT NULL,
  `username` varchar(25) NOT NULL,
  `password` varchar(12) NOT NULL,
  PRIMARY KEY (`Index no`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `students`
--

INSERT INTO `students` (`Name`, `Email`, `Index no`, `NIC`, `Date of birth`, `Country`, `Gender`, `Photo`, `Fluent subject areas`, `Hobbies`, `Comment`, `username`, `password`) VALUES
('Nirodha', 'abc@gmail.com', 15127, '996761525v', '1999-06-24', 'Sri Lanka', 'Female', 'me1.png', 'Chemistry', 'Reading', 'Bsc Undergraduate', 'niro', 'Deepthi@99');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
