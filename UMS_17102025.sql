CREATE DATABASE  IF NOT EXISTS `ums` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `ums`;
-- MySQL dump 10.13  Distrib 8.0.43, for Win64 (x86_64)
--
-- Host: localhost    Database: ums
-- ------------------------------------------------------
-- Server version	8.0.30

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `role_master`
--

DROP TABLE IF EXISTS `role_master`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role_master` (
  `RoleId` int NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(25) NOT NULL,
  `Description` varchar(100) DEFAULT NULL,
  `Active` bit(1) NOT NULL DEFAULT b'1',
  `CreatedBy` varchar(100) NOT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedBy` varchar(100) DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT NULL,
  PRIMARY KEY (`RoleId`),
  UNIQUE KEY `RoleId_UNIQUE` (`RoleId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role_master`
--

LOCK TABLES `role_master` WRITE;
/*!40000 ALTER TABLE `role_master` DISABLE KEYS */;
INSERT INTO `role_master` VALUES (1,'Admin',NULL,_binary '\0','Admn','2025-10-17 16:24:12','Admin','2025-10-17 16:33:52'),(3,'Customer',NULL,_binary '','Admn','2025-10-17 16:42:46','Admin','2025-10-17 16:46:27'),(4,'Admin',NULL,_binary '','Admn','2025-10-17 16:47:11',NULL,NULL),(5,'12',NULL,_binary '\0','Admn','2025-10-17 17:00:29','Admin','2025-10-17 17:05:19');
/*!40000 ALTER TABLE `role_master` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userdetail`
--

DROP TABLE IF EXISTS `userdetail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userdetail` (
  `id` int NOT NULL AUTO_INCREMENT,
  `userid` char(36) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `active` bit(1) NOT NULL DEFAULT b'1',
  `CreatedBy` varchar(100) NOT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedBy` varchar(100) DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userdetail`
--

LOCK TABLES `userdetail` WRITE;
/*!40000 ALTER TABLE `userdetail` DISABLE KEYS */;
INSERT INTO `userdetail` VALUES (2,'ca9bde8d-4328-4236-9596-71c4f0fb47ab','50uUi0MHPxFMeKz4/C1nYpqPUIZhkkab',_binary '','Admin','2025-10-16 12:29:07',NULL,NULL),(3,'d6fad302-41e2-44d7-b369-0d298e6376d6','VlmdeES3/Ich9aaOs/7/cSH9Gk1OKzZa',_binary '','Admin','2025-10-16 20:13:03',NULL,NULL),(4,'ea422ab2-b094-4600-a1d0-40067e972fea','VlmdeES3/Ich9aaOs/7/cSH9Gk1OKzZa',_binary '\0','Admin','2025-10-16 20:15:57','Admin','2025-10-17 12:08:59'),(5,'e30fe702-c44f-4b4c-8b21-a44e286a7790','NlrTaGFermslCEJ4/YLaDolOQf02i+qy',_binary '','Admin','2025-10-16 22:23:48',NULL,NULL),(6,'741a80e6-6297-423c-8edf-7c175ec7d323','iJUxAoeq2iVMBUlO3nY5IaxFaIYJT58e',_binary '','Admin','2025-10-17 09:34:49',NULL,NULL),(7,'c9dcba8d-02c6-4988-b9f1-edda68abd2c2','sXb4wo0a0d/zEiXPDaS+QsyJByGyG/ff',_binary '\0','Admin','2025-10-17 13:53:16','Admin','2025-10-17 14:18:59'),(8,'4d9bc1b6-1445-46e5-a26a-58577cb0f807','mQCrqGCTK6GDhnqBmVS4ehyqTkN3g2M9',_binary '','Admin','2025-10-17 14:25:55',NULL,NULL);
/*!40000 ALTER TABLE `userdetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `userrole`
--

DROP TABLE IF EXISTS `userrole`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `userrole` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Userid` char(36) DEFAULT NULL,
  `Role` varchar(25) DEFAULT NULL,
  `Active` bit(1) DEFAULT b'1',
  `CreatedBy` varchar(100) NOT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedBy` varchar(100) DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `userrole`
--

LOCK TABLES `userrole` WRITE;
/*!40000 ALTER TABLE `userrole` DISABLE KEYS */;
INSERT INTO `userrole` VALUES (1,'ca9bde8d-4328-4236-9596-71c4f0fb47ab','Admin',_binary '','Admin','2025-10-16 12:29:08','Admin','2025-10-17 13:21:19'),(2,'d6fad302-41e2-44d7-b369-0d298e6376d6','Customer',_binary '','Admin','2025-10-16 20:13:03',NULL,NULL),(3,'ea422ab2-b094-4600-a1d0-40067e972fea','Customer',_binary '\0','Admin','2025-10-16 20:15:57','Admin','2025-10-17 12:08:59'),(4,'e30fe702-c44f-4b4c-8b21-a44e286a7790','Customer',_binary '','Admin','2025-10-16 22:23:48',NULL,NULL),(5,'741a80e6-6297-423c-8edf-7c175ec7d323','Customer',_binary '','Admin','2025-10-17 09:34:49',NULL,NULL),(6,'c9dcba8d-02c6-4988-b9f1-edda68abd2c2','Client',_binary '\0','Admin','2025-10-17 13:53:16','Admin','2025-10-17 14:18:59'),(7,'4d9bc1b6-1445-46e5-a26a-58577cb0f807','Student',_binary '','Admin','2025-10-17 14:25:55',NULL,NULL);
/*!40000 ALTER TABLE `userrole` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` char(36) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `Mobile` varchar(25) DEFAULT NULL,
  `Gender` char(1) DEFAULT NULL,
  `Active` bit(1) NOT NULL DEFAULT b'1',
  `CreatedBy` varchar(100) NOT NULL,
  `CreatedAt` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedBy` varchar(100) DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT NULL,
  `IsVerified` bit(1) DEFAULT NULL,
  `DoB` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email_UNIQUE` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES ('4d9bc1b6-1445-46e5-a26a-58577cb0f807','chhaya.kapadia@kapadia.com','Chhaya','Anil','Kapadia','6351560155','F',_binary '','Admin','2025-10-17 14:25:55',NULL,NULL,_binary '','2013-03-31 08:54:35'),('741a80e6-6297-423c-8edf-7c175ec7d323','ashish.kapadia@kapadia.com','Ashish','Babubhai','Kapadia','9999999999','M',_binary '','Admin','2025-10-17 09:34:49',NULL,NULL,_binary '','2025-10-17 04:02:18'),('c9dcba8d-02c6-4988-b9f1-edda68abd2c2','sharma@sharma.com','SharmaFirstName','SharmaMiddleName','SharmaLastName','0000000000','F',_binary '\0','Admin','2025-10-17 13:53:16','Admin','2025-10-17 14:18:59',_binary '','2025-10-17 08:22:08'),('ca9bde8d-4328-4236-9596-71c4f0fb47ab','anil.kapadia@abc.com','Anil','Babubhai','Kapadia','9909737690','M',_binary '','Admin','2025-10-16 12:29:05','Admin','2025-10-17 13:21:19',_binary '','1984-07-22 06:50:54'),('d6fad302-41e2-44d7-b369-0d298e6376d6','first@first.com','FirstName','MiddleName','LastName','1234567890','M',_binary '','Admin','2025-10-16 20:13:03',NULL,NULL,_binary '','2025-10-16 14:13:51'),('e30fe702-c44f-4b4c-8b21-a44e286a7790','anjana@a.com','Anjana','Anil','Kapadia','6351560155','M',_binary '','Admin','2025-10-16 22:23:48',NULL,NULL,_binary '','2025-10-16 16:51:46'),('ea422ab2-b094-4600-a1d0-40067e972fea','first@f.com','FirstName','MiddleName','LastName','1234567890','M',_binary '\0','Admin','2025-10-16 20:15:57','Admin','2025-10-17 12:08:59',_binary '','2025-10-16 14:13:51');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-10-17 18:02:05
