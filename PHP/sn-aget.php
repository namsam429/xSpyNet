<?php
require_once("sn-config.php");
require_once("sn-function.php");
if(isset($_POST["act"])) {
	if($_POST["act"] == "getftpserverinfo" && check_token($_POST["token"]) == true) {
		writelog("GET_FTPSERVER:".$_POST["serverid"],$user, $cpuid);
		die($_POST["serverid"]."|".$StoreServerFTP[$_POST["serverid"]]["server"]."|".$StoreServerFTP[$_POST["serverid"]]["user"]."|".$StoreServerFTP[$_POST["serverid"]]["password"]);
	}
	elseif($_POST["act"] == "getquery" && check_token($_POST["token"]) == true) {
		if($_POST["user"]!= NULL && $_POST["cpuid"]!=NULL) {
			$user    =mysqli_real_escape_string($conn,htmlentities($_POST["user"], ENT_QUOTES));
			$cpuid   =mysqli_real_escape_string($conn,htmlentities($_POST["cpuid"], ENT_QUOTES));
			$sql="SELECT * from lenh where user='$user' and cpuid='$cpuid' and result=''";
			$query=mysqli_query($conn, $sql);
			while ($row = mysqli_fetch_array($query, MYSQLI_BOTH)) {
				writelog("GET_QUERY:".$row["id"]."|".urlencode($row["code"])."|".$row["type"]."/",$user,$cpuid);
				echo $row["id"]."|".urlencode($row["code"])."|".$row["type"]."/";
			}
		}
	}
}
require_once("sn-cc.php");
?>