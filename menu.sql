/*
Navicat MySQL Data Transfer

Source Server         : Locale
Source Server Version : 50144
Source Host           : localhost:3306
Source Database       : sagra

Target Server Type    : MYSQL
Target Server Version : 50144
File Encoding         : 65001

Date: 2014-09-03 16:56:10
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `menu`
-- ----------------------------
DROP TABLE IF EXISTS `menu`;
CREATE TABLE `menu` (
  `descrizioneMenu` char(90) DEFAULT NULL,
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `prezzo` double(11,0) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of menu
-- ----------------------------
INSERT INTO `menu` VALUES ('Menu beneficenza 23', '1', '23');
INSERT INTO `menu` VALUES ('Menu Bimbo', '2', '10');
INSERT INTO `menu` VALUES ('Menu Tosco Tempura', '3', '25');

-- ----------------------------
-- Table structure for `menualimento`
-- ----------------------------
DROP TABLE IF EXISTS `menualimento`;
CREATE TABLE `menualimento` (
  `idmenu` int(11) NOT NULL,
  `idalimento` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of menualimento
-- ----------------------------
INSERT INTO `menualimento` VALUES ('1', '52');
INSERT INTO `menualimento` VALUES ('1', '57');
INSERT INTO `menualimento` VALUES ('1', '59');
INSERT INTO `menualimento` VALUES ('1', '61');
INSERT INTO `menualimento` VALUES ('1', '64');
INSERT INTO `menualimento` VALUES ('1', '68');
INSERT INTO `menualimento` VALUES ('1', '81');
INSERT INTO `menualimento` VALUES ('1', '80');
INSERT INTO `menualimento` VALUES ('2', '56');
INSERT INTO `menualimento` VALUES ('2', '61');
INSERT INTO `menualimento` VALUES ('1', '80');
INSERT INTO `menualimento` VALUES ('3', '82');
INSERT INTO `menualimento` VALUES ('3', '57');
INSERT INTO `menualimento` VALUES ('3', '83');
INSERT INTO `menualimento` VALUES ('3', '84');
INSERT INTO `menualimento` VALUES ('3', '66');
INSERT INTO `menualimento` VALUES ('3', '80');
INSERT INTO `menualimento` VALUES ('3', '81');
