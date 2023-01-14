-- phpMyAdmin SQL Dump
-- version 4.5.2
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Apr 24, 2017 at 07:17 AM
-- Server version: 10.1.13-MariaDB
-- PHP Version: 5.5.37

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `webdev`
--

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `autoid` int(11) NOT NULL,
  `name` varchar(150) NOT NULL,
  `address` varchar(255) NOT NULL,
  `gender` varchar(8) NOT NULL,
  `date` varchar(20) NOT NULL,
  `phone` varchar(15) NOT NULL,
  `nic` varchar(15) NOT NULL,
  `email` varchar(255) NOT NULL,
  `country` varchar(50) NOT NULL,
  `reading` int(11) NOT NULL,
  `movie` int(11) NOT NULL,
  `other` int(11) NOT NULL,
  `password` varchar(255) NOT NULL,
  `username` varchar(255) NOT NULL,
  `photo` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`autoid`, `name`, `address`, `gender`, `date`, `phone`, `nic`, `email`, `country`, `reading`, `movie`, `other`, `password`, `username`, `photo`) VALUES
(6, 'newa', 'aaa', 'male', '2017-04-12', '0000000000', '000000000v', 'a@a.lk', 'srilanka', 1, 0, 0, 'nn', 'nn', ''),
(21, 'test2', 'addresssss', 'male', '2017-04-27', '0980800000', '098090000v', 's@s.lk', 'srilanka', 0, 1, 0, 'a', 'a', '8510-1387-8.jpg'),
(23, 'bbb', 'hkh', 'female', '2016-11-10', '9877979879', '879879798v', 'g@jkjk.lk', 'usa', 0, 0, 1, 'aaa', 'aaa', ''),
(24, 'fdsjkl', 'kjsdlf', 'male', '2017-03-02', '9879879798', '879798798v', 'khk@jk.lk', 'india', 0, 1, 0, 'aaaa', 'aaaa', ''),
(25, 'test1', 'tttt', 'male', '2017-04-04', '0000000000', '000000000v', 'a@a.lk', 'usa', 1, 0, 1, 'a', 'aaaaa', ''),
(33, 'dfdsf', 'dfa', 'male', '2017-04-04', '6546546546', '564654654v', 'kjsdha@dsf.lk', 'srilanka', 0, 1, 0, 's', 'ss', '83548-'),
(34, 'asdfa', 'dsf', 'male', '2017-04-13', '5646465465', '546546546v', 'sadfsad@dfsdf.lk', 'srilanka', 0, 0, 0, 'a', 'ww', '22421-IMG_20151231_092831.jpg'),
(35, 'sdfa', 'sdfa', 'male', '2017-04-04', '6546546546', '564654656v', 'dfsa@lk.lk', 'srilanka', 0, 0, 0, 'dd', 'f', '26028-IMG_20160105_090531-01.jpeg'),
(36, 'sdfa', 'sdfa', 'male', '2017-04-04', '6546546546', '564654656v', 'dfsa@lk.lk', 'srilanka', 0, 0, 0, 'a', 'f', '79560-IMG_20160105_090531.jpg'),
(37, 'new user', 'a', 'male', '2017-04-05', '6464654654', '654654654v', 'dfs@dfs.lk', 'srilanka', 1, 0, 0, 'a', 'aaaaaaaa', '49520-');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`autoid`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `autoid` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=38;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
