-- phpMyAdmin SQL Dump
-- version 3.2.4
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Feb 20, 2020 at 07:46 AM
-- Server version: 5.1.41
-- PHP Version: 5.3.1

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `student_activity`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

CREATE TABLE IF NOT EXISTS `admin` (
  `AID` varchar(16) NOT NULL,
  `AName` varchar(48) NOT NULL,
  PRIMARY KEY (`AID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `admin`
--


-- --------------------------------------------------------

--
-- Table structure for table `admin_program`
--

CREATE TABLE IF NOT EXISTS `admin_program` (
  `AID` varchar(16) NOT NULL,
  `PID` varchar(16) NOT NULL,
  PRIMARY KEY (`AID`,`PID`),
  KEY `PID` (`PID`),
  KEY `AID` (`AID`),
  KEY `PID_2` (`PID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `admin_program`
--


-- --------------------------------------------------------

--
-- Table structure for table `admin_report`
--

CREATE TABLE IF NOT EXISTS `admin_report` (
  `AID` varchar(16) NOT NULL,
  `RID` varchar(16) NOT NULL,
  PRIMARY KEY (`AID`,`RID`),
  KEY `AID` (`AID`),
  KEY `RID` (`RID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `admin_report`
--


-- --------------------------------------------------------

--
-- Table structure for table `club`
--

CREATE TABLE IF NOT EXISTS `club` (
  `CID` varchar(16) NOT NULL,
  `CName` varchar(48) NOT NULL,
  `Coordinator` varchar(16) NOT NULL,
  PRIMARY KEY (`CID`),
  KEY `Coordinator` (`Coordinator`),
  KEY `CID` (`CID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `club`
--

INSERT INTO `club` (`CID`, `CName`, `Coordinator`) VALUES
('1', 'CSClub', 'badr alshehri');

-- --------------------------------------------------------

--
-- Table structure for table `program`
--

CREATE TABLE IF NOT EXISTS `program` (
  `PID` varchar(16) NOT NULL,
  `PTitle` varchar(48) NOT NULL,
  `PType` varchar(24) NOT NULL,
  `PStartDate` date NOT NULL,
  `PEndDate` date NOT NULL,
  `PTime` time NOT NULL,
  `CID` varchar(16) NOT NULL,
  `AID` varchar(16) NOT NULL,
  `MaxStdNo` int(11) NOT NULL,
  PRIMARY KEY (`PID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `program`
--


-- --------------------------------------------------------

--
-- Table structure for table `report`
--

CREATE TABLE IF NOT EXISTS `report` (
  `RID` varchar(16) NOT NULL,
  `PID` varchar(16) NOT NULL,
  `Description` varchar(500) NOT NULL,
  `CID` varchar(16) NOT NULL,
  PRIMARY KEY (`RID`),
  KEY `RID` (`RID`),
  KEY `PID` (`PID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `report`
--


-- --------------------------------------------------------

--
-- Table structure for table `student`
--

CREATE TABLE IF NOT EXISTS `student` (
  `SID` varchar(16) NOT NULL,
  `SName` varchar(48) NOT NULL,
  `SEmail` varchar(48) NOT NULL,
  `MobileNo` varchar(10) NOT NULL,
  PRIMARY KEY (`SID`),
  KEY `SID` (`SID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `student`
--

INSERT INTO `student` (`SID`, `SName`, `SEmail`, `MobileNo`) VALUES
('36110580', 'Mohammed j. Sadiq', 'diamoh0@gmail.com', '0547158828');

-- --------------------------------------------------------

--
-- Table structure for table `student_club`
--

CREATE TABLE IF NOT EXISTS `student_club` (
  `SID` varchar(16) NOT NULL,
  `CID` varchar(16) NOT NULL,
  PRIMARY KEY (`SID`,`CID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `student_club`
--


-- --------------------------------------------------------

--
-- Table structure for table `student_program`
--

CREATE TABLE IF NOT EXISTS `student_program` (
  `SID` varchar(16) NOT NULL,
  `PID` varchar(16) NOT NULL,
  PRIMARY KEY (`SID`,`PID`),
  UNIQUE KEY `SID_2` (`SID`),
  KEY `SID` (`SID`,`PID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Dumping data for table `student_program`
--


/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
