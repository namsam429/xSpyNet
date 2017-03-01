<?php
@session_start();
date_default_timezone_set('Asia/Saigon');
// MySQL
$DB["SERVER"]   = "localhost";
$DB["DB"]       = "spynet";
$DB["user"]     = "root";
$DB["password"] = "";
$conn=mysqli_connect($DB["SERVER"], $DB["user"], $DB["password"], $DB["DB"]) or die("<h1>Can't connect to database server!</h1>");
/* chuyển định dạng của CSDL sang utf8 */
if (!mysqli_set_charset($conn, "utf8"))
	printf("Error loading character set utf8: %s\n", mysqli_error($conn));
//FTP Server
$StoreServerFTP[0]["server"]   = "ftp.freevnn.com";
$StoreServerFTP[0]["user"]     = "freev_16503716";
$StoreServerFTP[0]["password"] = "aspirine";
$StoreServerFTP[0]["link"]     = "spynetsore01.freevnn.com";
//Configure Server Activity
$FTPServerNum = 0; // Server sẽ được sử dụng để chứa dữ liệu

?>