/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 50721
 Source Host           : localhost:3306
 Source Schema         : cat.book

 Target Server Type    : MySQL
 Target Server Version : 50721
 File Encoding         : 65001

 Date: 14/11/2018 15:11:12
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Records of sys_account
-- ----------------------------
INSERT INTO `sys_account` VALUES ('139c630fa2c7a1d8', 'guest', 'test', '喵喵测试号', 'AF457C812B882C2C3EEC83E6DF9CB0B0', '7628687e-161d-4cb3-a93e-c327c72d8d6a', b'0', 'upload/images/20180925003600_6667218_710d70ec54e736d13e9b28c197504fc2d4626976.jpg', '2018-09-19 16:55:05', '2018-09-27 02:56:30');
INSERT INTO `sys_account` VALUES ('4659954737641156790', 'administrator', 'admin', '喵喵管理员', '1A0435A56150FB1128BD86B5B44C9EFE', '15fecb29-8a04-4943-bb49-bed4b4ba8904', b'0', 'upload/images/20180925003600_6667218_710d70ec54e736d13e9b28c197504fc2d4626976.jpg', '2018-11-14 15:08:51', '2018-11-14 15:10:21');
INSERT INTO `sys_account` VALUES ('5657725263875604292', 'guest', 'test02', '青蛙02', '8FFDF037C5B64FF00A9FEDB0473370B6', '67d10c7b-a9d5-479a-a8fe-a29d02f4e223', b'1', 'upload/images/20180925003638_9551176_fd98083b5bb5c9eaf140a2d9d939b6003af3b359.jpg', '2018-09-22 00:14:53', '2018-09-29 10:55:51');

SET FOREIGN_KEY_CHECKS = 1;
