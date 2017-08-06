/*
Navicat MySQL Data Transfer

Source Server         : Locale
Source Server Version : 50144
Source Host           : localhost:3306
Source Database       : sagra

Target Server Type    : MYSQL
Target Server Version : 50144
File Encoding         : 65001

Date: 2014-08-30 10:27:29
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `alimento`
-- ----------------------------
DROP TABLE IF EXISTS `alimento`;
CREATE TABLE `alimento` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_categoria` int(11) NOT NULL,
  `descrizione` varchar(255) NOT NULL,
  `descrizioneAlternativa` varchar(255) DEFAULT NULL,
  `prezzo` double NOT NULL,
  `quantit` int(11) DEFAULT '9999999',
  `attivo` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`),
  KEY `idFk` (`id_categoria`),
  CONSTRAINT `idFk` FOREIGN KEY (`id_categoria`) REFERENCES `categoria` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=82 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of alimento
-- ----------------------------
INSERT INTO `alimento` VALUES ('28', '10', 'Coperto', null, '1', '9999999', '1');
INSERT INTO `alimento` VALUES ('29', '1', 'ACQUA NATU 1 LT', null, '1', '9999999', '1');
INSERT INTO `alimento` VALUES ('30', '1', 'ACQUA NATURALE 1/2 LT', null, '0.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('31', '1', 'ACQUA GASSATA 1 LT', null, '1', '9999999', '1');
INSERT INTO `alimento` VALUES ('32', '1', 'ACQUA GASSATA 1/2 LT', null, '0.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('33', '1', 'COCA COLA PICCOLA', null, '2.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('34', '1', 'COCA COLA MEDIA', null, '3.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('35', '1', 'BIRRA PICCOLA', null, '2.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('36', '1', 'BIRRA MEDIA', null, '3.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('37', '1', 'BIRRA BOTTIGLIA 66', null, '3', '9999999', '1');
INSERT INTO `alimento` VALUES ('38', '1', 'FANTA IN LATTINA', null, '1.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('39', '1', 'COCA IN LATTINA', null, '1.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('40', '9', 'VINO DA TAVOLA ROSSO 1/2 LT', null, '3', '9999999', '1');
INSERT INTO `alimento` VALUES ('41', '9', 'VINO DA TAVOLA BIANCO 1/2 LT', null, '3', '9999999', '1');
INSERT INTO `alimento` VALUES ('42', '9', 'VINO DA TAVOLA BIANCO 1 L', null, '4.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('43', '9', 'VINO DA TAVOLA ROSSO 1 LT', null, '4.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('44', '9', 'MALVASIA bt 0.750', null, '6.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('45', '9', 'CHIANTI D.O.C.G bt 0.750', null, '6.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('46', '9', 'VERNACCIA SAN GIMIGNANO bt 0.750 ', null, '6.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('47', '9', 'VERNACCIA SA GIMIGNANO TROPIE\' bt 0.750', null, '9.5', '9999998', '1');
INSERT INTO `alimento` VALUES ('48', '9', 'GROTTONI bt 0.750', null, '9.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('49', '9', 'LENDO I.G.T.', null, '15.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('50', '2', 'CROSTINI MISTI (MELANZANA,ZUCCHINE,FEGATINO,TOSCANO, CIPOLLA)', null, '4.5', '9999995', '1');
INSERT INTO `alimento` VALUES ('51', '2', 'PECORINI TOSCANI CON COMPOSTA', null, '6', '9999992', '1');
INSERT INTO `alimento` VALUES ('52', '2', 'ANTIPASTO CIPOLLA CERTALDO (salame alla cipolla ,formaggio con composta', null, '7', '9999995', '1');
INSERT INTO `alimento` VALUES ('53', '2', 'CROSTONE ALLA CIPOLLA DI CERTALDO', null, '2.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('54', '2', 'PIATTO DI SALUMI TOSCANI (PROSCIUTTO,SALAME CIPOLLA DI CERTALDO,FINOCCHIONA)', null, '6', '9999999', '1');
INSERT INTO `alimento` VALUES ('55', '3', 'CANNELLONI DI CIPOLLA E RADICCHIO', null, '6', '9999997', '1');
INSERT INTO `alimento` VALUES ('56', '3', 'PENNE AL POMODORO', null, '4', '9999999', '1');
INSERT INTO `alimento` VALUES ('57', '3', 'ZUPPA DI CIPOLLA DI CERTALDO', null, '7', '9999991', '1');
INSERT INTO `alimento` VALUES ('58', '3', 'TAGLIATELLE CIPOLLA E SALSICCIA', null, '7', '9999993', '1');
INSERT INTO `alimento` VALUES ('59', '3', 'SPAGHETTI ALLA CIPOLLA DI CERTALDO', null, '5', '9999997', '1');
INSERT INTO `alimento` VALUES ('60', '4', 'LESSO RIFATTO (FRANCESINA)', null, '7.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('61', '4', 'ROSTICCIANA AL FORNO', null, '7', '9999998', '1');
INSERT INTO `alimento` VALUES ('62', '4', 'TAGLIATA CON RUCOLA (PER 2 PERS.)', null, '16', '9999999', '1');
INSERT INTO `alimento` VALUES ('63', '4', 'TAGLIATA CON CIPOLLA (PER 2 PERS.)', null, '16', '9999997', '1');
INSERT INTO `alimento` VALUES ('64', '4', 'SALSICCIA E FAGIOLI', null, '5', '9999999', '1');
INSERT INTO `alimento` VALUES ('65', '4', 'TORTINO DI CIPOLLA IN SALSA', null, '4.5', '9999995', '1');
INSERT INTO `alimento` VALUES ('66', '4', 'ARISTA', null, '5', '9999999', '1');
INSERT INTO `alimento` VALUES ('67', '5', 'CIPOLLE CARAMELLATE', null, '3.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('68', '5', 'CIPOLLE DI CERTALDO FRITTE', null, '3.5', '9999999', '1');
INSERT INTO `alimento` VALUES ('69', '5', 'INSALATA MISTA', null, '3', '9999999', '1');
INSERT INTO `alimento` VALUES ('70', '5', 'PATATE FRITTE', null, '3', '9999999', '1');
INSERT INTO `alimento` VALUES ('71', '7', 'CRUDO E CIPOLLA', null, '6', '9999999', '1');
INSERT INTO `alimento` VALUES ('72', '7', 'VEGETARIANA', null, '5', '9999999', '1');
INSERT INTO `alimento` VALUES ('73', '7', 'SALAMINO PICCANTE', null, '6', '9999999', '1');
INSERT INTO `alimento` VALUES ('74', '7', 'MARGHERITA', null, '5', '9999998', '1');
INSERT INTO `alimento` VALUES ('75', '7', 'FUNGHI', null, '5', '9999999', '1');
INSERT INTO `alimento` VALUES ('76', '7', 'CIPOLLA E SALSICCIA', null, '6', '9999999', '1');
INSERT INTO `alimento` VALUES ('77', '7', 'PROSCIUTTO COTTO', null, '5', '9999999', '1');
INSERT INTO `alimento` VALUES ('78', '7', 'CIPOLLA DI CERTALDO', null, '5', '9999999', '1');
INSERT INTO `alimento` VALUES ('79', '8', 'CANTUCCIO E VIN SANTO', null, '4', '9999999', '1');
INSERT INTO `alimento` VALUES ('80', '8', 'COVACCINO ALLA NUTELLA', null, '4', '9999999', '1');
INSERT INTO `alimento` VALUES ('81', '8', 'FRUTTA AL BICCHIERE', null, '2.5', '9999999', '1');

-- ----------------------------
-- Table structure for `categoria`
-- ----------------------------
DROP TABLE IF EXISTS `categoria`;
CREATE TABLE `categoria` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `Descrizione` varchar(150) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of categoria
-- ----------------------------
INSERT INTO `categoria` VALUES ('1', 'Bevande');
INSERT INTO `categoria` VALUES ('2', 'Antipasti');
INSERT INTO `categoria` VALUES ('3', 'Primi');
INSERT INTO `categoria` VALUES ('4', 'Secondi');
INSERT INTO `categoria` VALUES ('5', 'Contorni');
INSERT INTO `categoria` VALUES ('6', 'Dolci');
INSERT INTO `categoria` VALUES ('7', 'Pizze');
INSERT INTO `categoria` VALUES ('8', 'Pizze Dolci');
INSERT INTO `categoria` VALUES ('9', 'Vini');
INSERT INTO `categoria` VALUES ('10', 'Coperto');

-- ----------------------------
-- Table structure for `comanda`
-- ----------------------------
DROP TABLE IF EXISTS `comanda`;
CREATE TABLE `comanda` (
  `id_comanda` int(11) NOT NULL AUTO_INCREMENT,
  `data` date DEFAULT NULL,
  `n_coperti` int(11) DEFAULT NULL,
  `totalecomanda` double(11,2) DEFAULT NULL,
  `sconto` double(11,2) DEFAULT '0.00',
  PRIMARY KEY (`id_comanda`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of comanda
-- ----------------------------
INSERT INTO `comanda` VALUES ('1', '2014-08-29', '2', '10.00', '4.00');
INSERT INTO `comanda` VALUES ('2', '2014-08-29', '1', '33.00', '0.00');
INSERT INTO `comanda` VALUES ('3', '2014-08-29', '1', '37.00', '10.00');
INSERT INTO `comanda` VALUES ('4', '2014-08-29', '2', '8.00', '1.00');

-- ----------------------------
-- Table structure for `comanda_alimento`
-- ----------------------------
DROP TABLE IF EXISTS `comanda_alimento`;
CREATE TABLE `comanda_alimento` (
  `id_comanda` int(11) NOT NULL,
  `id_alimento` int(11) NOT NULL,
  `qta` int(11) NOT NULL,
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `prezzo` double(11,2) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `comandafk` (`id_comanda`),
  KEY `alimentofk` (`id_alimento`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of comanda_alimento
-- ----------------------------
INSERT INTO `comanda_alimento` VALUES ('1', '55', '2', '1', '12.00');
INSERT INTO `comanda_alimento` VALUES ('1', '10', '2', '2', '2.00');
INSERT INTO `comanda_alimento` VALUES ('2', '58', '2', '3', '14.00');
INSERT INTO `comanda_alimento` VALUES ('2', '51', '3', '4', '18.00');
INSERT INTO `comanda_alimento` VALUES ('2', '10', '1', '5', '1.00');
INSERT INTO `comanda_alimento` VALUES ('3', '63', '2', '6', '32.00');
INSERT INTO `comanda_alimento` VALUES ('3', '52', '2', '7', '14.00');
INSERT INTO `comanda_alimento` VALUES ('3', '10', '1', '8', '1.00');
INSERT INTO `comanda_alimento` VALUES ('4', '61', '1', '9', '7.00');
INSERT INTO `comanda_alimento` VALUES ('4', '10', '2', '10', '2.00');

-- ----------------------------
-- Table structure for `sconti`
-- ----------------------------
DROP TABLE IF EXISTS `sconti`;
CREATE TABLE `sconti` (
  `id_comanda` int(11) DEFAULT NULL,
  `sconto` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of sconti
-- ----------------------------
