/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


DROP TABLE IF EXISTS `projet_auteur`;
CREATE TABLE `projet_auteur` (
    `Id` integer AUTO_INCREMENT PRIMARY KEY,
    `Nom` varchar(50) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*!40000 ALTER TABLE `projet_auteur` DISABLE KEYS */;
INSERT INTO `projet_auteur` (`Id`,`Nom`) VALUES 
 (1,'David Goudreault'),
 (2,'Véronique Ovaldé'),
 (3,'Patrick Senécal'),
 (4,'Victoria Mas'),
 (5,'Francine Ruel');
/*!40000 ALTER TABLE `projet_auteur` ENABLE KEYS */;

DROP TABLE IF EXISTS `projet_categorie`;
CREATE TABLE `projet_categorie` (
    `Id` integer AUTO_INCREMENT PRIMARY KEY,
    `Nom` varchar(50) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*!40000 ALTER TABLE `projet_categorie` DISABLE KEYS */;
INSERT INTO `projet_categorie` (`Id`,`Nom`) VALUES 
 (1,'Bande dessinée'),
 (2,'Encyclopédie'),
 (3,'Journaux'),
 (4,'Roman'),
 (5,'Science fiction');
/*!40000 ALTER TABLE `projet_categorie` ENABLE KEYS */;

DROP TABLE IF EXISTS `projet_livre`;
CREATE TABLE `projet_livre` (
  `Id` integer AUTO_INCREMENT PRIMARY KEY,
  `ISBN` varchar(15) NOT NULL,
  `Titre` varchar(70) NOT NULL,
  `NbPages` integer NOT NULL,
  `AuteurId` integer NOT NULL,
  `CategorieId` integer NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*!40000 ALTER TABLE `projet_livre` DISABLE KEYS */;
INSERT INTO `projet_livre` (`ISBN`,`Titre`,`NbPages`,`AuteurId`,`CategorieId`) VALUES 
 ('B07X8RVQ73','Ta mort à moi',273,1,4),
 ('B07KPB2CKG','Personne n\'a peur des gens qui sourient',257,2,4),
 ('B07ZFZX787','Ceux de là-bas',450,3,4),
 ('B07WGPDVN8','Le Bal des folles',186,4,4),
 ('B07WJRW7KY','Anna et l\'enfant-vieillard',142,5,4);
/*!40000 ALTER TABLE `projet_livre` ENABLE KEYS */;

DROP TABLE IF EXISTS `projet_membre`;
CREATE TABLE `projet_membre` (
  `Id` integer AUTO_INCREMENT PRIMARY KEY,
  `Nom` varchar(30) NOT NULL,
  `Courriel` varchar(30) NOT NULL,
  `Telephone` varchar(20) NOT NULL,
  `DateCreation` datetime NOT NULL,
  `ApiKey` varchar(40) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `projet_emprunt`;
CREATE TABLE `projet_emprunt` (
  `Id` integer AUTO_INCREMENT PRIMARY KEY,
  `DateEmprunt` datetime NOT NULL,
  `DateRetour` datetime NOT NULL,
  `LivreId` integer NOT NULL,
  `MembreId` integer NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
