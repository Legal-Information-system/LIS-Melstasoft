-- phpMyAdmin SQL Dump
-- version 4.9.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Nov 04, 2022 at 02:22 PM
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
-- Database: `login_sample_db`
--

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `email` varchar(100) NOT NULL,
  `phone` varchar(50) NOT NULL,
  `gender` varchar(10) NOT NULL,
  `user_id` bigint(20) NOT NULL,
  `user_name` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `date` timestamp NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  KEY `date` (`date`),
  KEY `user_name` (`user_name`),
  KEY `id` (`id`),
  KEY `id_2` (`id`),
  KEY `email` (`email`),
  KEY `phone` (`phone`)
) ENGINE=MyISAM AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `email`, `phone`, `gender`, `user_id`, `user_name`, `password`, `date`) VALUES
(1, 'xyz@gail.com', '0705311007', 'F', 100, 'Nirodha ', 'abc', '2022-11-04 14:22:14'),
(2, 'aa@gmail.com', '0705311111', 'F', 200, 'Nethmini', '123', '2022-11-04 14:22:14'),
(3, 'aba@gmail.com', '0705333778', 'M', 9700, 'Ravi', '987', '2022-11-04 14:22:14'),
(4, 'ajjja@gmail.com', '0705661007', 'F', 554, 'Amali', '554', '2022-11-04 14:22:14'),
(5, 'aaaacc@gmail.com', '0705991007', 'M', 22943, 'Ravindu', '123', '2022-11-04 14:22:14'),
(6, 'ahhha@gmail.com', '0705311333', 'F', 32634962, 'Deppthini', 'qwe', '2022-11-04 14:22:14'),
(7, 'yyff@gmail.com', '0765311007', 'F', 77622, 'niro', 'pqr', '2022-11-04 14:22:14'),
(8, 'bdjbv@gmail.com', '0705355507', 'F', 65863812836, 'Anoma', 'hhh', '2022-11-04 14:22:14'),
(9, 'iii@gmail.com', '0705315555', 'F', 9516782, 'Amali', 'yyy', '2022-11-04 14:22:14'),
(10, 'ggf@gmail.com', '0719999999', 'M', 9271052742669576, 'saman', '666', '2022-11-04 14:22:14');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
