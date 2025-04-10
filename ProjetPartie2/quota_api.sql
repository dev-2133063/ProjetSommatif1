/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

DROP TABLE IF EXISTS `projet_quota_api`;
CREATE TABLE `projet_quota_api` (
    `Id` integer AUTO_INCREMENT PRIMARY KEY,
    `MembreId` integer NOT NULL,
    `DateMAJ` datetime NOT NULL,
    `MaxRequetes` INT NOT NULL DEFAULT 20,
    `RequetesUtilisees` INT NOT NULL DEFAULT 0
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `projet_membre_roles`;
CREATE TABLE `projet_membre_roles` (
  `Id` int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  `MembreId` int NOT NULL,
  `Role` varchar(255) NOT NULL) ENGINE=MyISAM DEFAULT CHARSET=latin1;

INSERT INTO `projet_membre` (`Nom`, `Courriel`, `Telephone`, `DateCreation`, `ApiKey`) 
VALUES 
('Alice Dupont', 'alice.dupont@example.com', '123-456-7890', '2025-04-01 08:00:00', 'admin_apikey'),
('Bob Martin', 'bob.martin@example.com', '987-654-3210', '2025-03-28 14:30:00', 'user_apikey'),
('Charlie Durand', 'charlie.durand@example.com', '456-789-1234', '2025-03-15 10:15:00', 'editor_apikey');

INSERT INTO `projet_quota_api` (`MembreId`, `DateMAJ`) 
VALUES 
(1, '2025-04-01 08:00:00'),
(2, '2025-04-01 08:00:00'),
(3, '2025-04-01 08:00:00');

INSERT INTO `projet_membre_roles` (`MembreId`, `Role`) 
VALUES 
(1, 'admin'),
(2, 'user'),
(3, 'editor');
