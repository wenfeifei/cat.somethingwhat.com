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

 Date: 14/11/2018 15:10:48
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for book_chapter_read_record
-- ----------------------------
DROP TABLE IF EXISTS `book_chapter_read_record`;
CREATE TABLE `book_chapter_read_record`  (
  `Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Book_Read_Record_Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Openid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Chapter_Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Chapter_Link` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Number_Of_Words` int(11) NULL DEFAULT NULL,
  `Duration` int(11) NULL DEFAULT NULL,
  `Create_Time` datetime(0) NULL DEFAULT NULL,
  `Update_Time` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for book_read_record
-- ----------------------------
DROP TABLE IF EXISTS `book_read_record`;
CREATE TABLE `book_read_record`  (
  `Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Openid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Rule` int(11) NULL DEFAULT NULL,
  `Author` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Book_Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Book_Classify` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Book_Link` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Cover_Image` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Book_Intro` varchar(2000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `IsHidden` tinyint(1) NULL DEFAULT NULL,
  `Create_Time` datetime(0) NULL DEFAULT NULL,
  `Update_Time` datetime(0) NULL DEFAULT NULL,
  `Last_Reading_Record_Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `AlreadyCollected` tinyint(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for book_user
-- ----------------------------
DROP TABLE IF EXISTS `book_user`;
CREATE TABLE `book_user`  (
  `Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Appid` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `User_Id` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Openid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `NickName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `AvatarUrl` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Gender` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Country` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Province` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `City` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Language` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Create_Time` datetime(0) NULL DEFAULT NULL,
  `Update_Time` datetime(0) NULL DEFAULT NULL,
  `Currency` int(11) NULL DEFAULT NULL,
  `Read_Minute` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for book_user_consume
-- ----------------------------
DROP TABLE IF EXISTS `book_user_consume`;
CREATE TABLE `book_user_consume`  (
  `Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Book_Chapter_Read_Record_Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Amount` int(11) NULL DEFAULT NULL,
  `Remark` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Create_Time` datetime(0) NULL DEFAULT NULL,
  `Openid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for book_user_message
-- ----------------------------
DROP TABLE IF EXISTS `book_user_message`;
CREATE TABLE `book_user_message`  (
  `Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Openid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Content` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Create_Time` datetime(0) NULL DEFAULT NULL,
  `Update_Time` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for book_user_message_readrecord
-- ----------------------------
DROP TABLE IF EXISTS `book_user_message_readrecord`;
CREATE TABLE `book_user_message_readrecord`  (
  `Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Book_User_Message_Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Openid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Create_Time` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for book_user_preference
-- ----------------------------
DROP TABLE IF EXISTS `book_user_preference`;
CREATE TABLE `book_user_preference`  (
  `Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Openid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `FontSize` int(11) NULL DEFAULT NULL,
  `FontColor` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `FontFamily` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `BackgroundColor` varchar(10) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `ScreenBrightness` int(11) NULL DEFAULT NULL,
  `KeepScreenOn` tinyint(1) NULL DEFAULT NULL,
  `Create_Time` datetime(0) NULL DEFAULT NULL,
  `Update_Time` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for book_user_recharge
-- ----------------------------
DROP TABLE IF EXISTS `book_user_recharge`;
CREATE TABLE `book_user_recharge`  (
  `Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Openid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Recharge_Type` int(11) NULL DEFAULT NULL,
  `Recharge_Currency` int(11) NULL DEFAULT NULL,
  `Remark` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Create_Time` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_account
-- ----------------------------
DROP TABLE IF EXISTS `sys_account`;
CREATE TABLE `sys_account`  (
  `Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '后台用户信息表，主键',
  `Authority` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '权限分组，多个用逗号分割',
  `User_Id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `NickName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Password` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Password_Salt` varchar(36) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Disable` bit(1) NULL DEFAULT NULL,
  `Avatar` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '头像',
  `Create_Time` datetime(0) NULL DEFAULT NULL,
  `Update_Time` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_interface_blacklist
-- ----------------------------
DROP TABLE IF EXISTS `sys_interface_blacklist`;
CREATE TABLE `sys_interface_blacklist`  (
  `Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Ip` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Appid` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Groups` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '黑名单所属分组，多个用逗号分割',
  `Expiry_Time` datetime(0) NULL DEFAULT NULL COMMENT '黑名单的有效期',
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Create_Time` datetime(0) NULL DEFAULT NULL,
  `Update_Time` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统接口黑名单表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_interface_whitelist
-- ----------------------------
DROP TABLE IF EXISTS `sys_interface_whitelist`;
CREATE TABLE `sys_interface_whitelist`  (
  `Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Appid` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Validity_Time` datetime(0) NULL DEFAULT NULL COMMENT '白名单的有效期',
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Create_Time` datetime(0) NULL DEFAULT NULL,
  `Update_Time` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '接口白名单表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for sys_login_log
-- ----------------------------
DROP TABLE IF EXISTS `sys_login_log`;
CREATE TABLE `sys_login_log`  (
  `Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `User_Id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Client_IP` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Country` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Province` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `City` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `District` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `TraceIdentifier` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Create_Time` datetime(0) NULL DEFAULT NULL,
  `IsSuccessed` tinyint(1) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for wechat_app_config
-- ----------------------------
DROP TABLE IF EXISTS `wechat_app_config`;
CREATE TABLE `wechat_app_config`  (
  `Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `Mark_Key` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Appid` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Secret` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Mch_id` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Mch_id_secret` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Create_Time` datetime(0) NULL DEFAULT NULL,
  `Update_Time` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for wechat_payorder
-- ----------------------------
DROP TABLE IF EXISTS `wechat_payorder`;
CREATE TABLE `wechat_payorder`  (
  `Id` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `FromKey` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Appid` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Openid` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `MchId` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `OutTradeNo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Body` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `RequestData` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL,
  `TotalFee` int(11) NULL DEFAULT NULL,
  `IsPaySuccessed` tinyint(1) NULL DEFAULT NULL,
  `PayResult` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `PrepayId` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL,
  `Create_Time` datetime(0) NULL DEFAULT NULL,
  `Update_Time` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
