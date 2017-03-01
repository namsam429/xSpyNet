<?php
require_once("sn-config.php");
function check_token($token) {
	for($i=0;$i<=10;$i++)
		if(md5("876d4ce235fe510a3141db3158339c5e".$i)==$token)
			return true;
	return false;	
}
function writelog($content, $user, $cpuid="none") {
	if(!file_exists("log"))
		mkdir("log");
	$fp = fopen("log/".date('Y-m-d H', time()).".txt", "a");
	fwrite($fp, date('Y-m-d H:i:s', time())." - $user - $cpuid -".$content."
");
	fclose($fp);
}

// FTP
function fileExistsFTPStore($ftp_server, $user, $pass, $file) {
// set up a connection to the server we chose or die and show an error
        $conn_id = ftp_connect($ftp_server) or writelog("Couldn't connect to $ftp_server");
        ftp_login($conn_id,$user,$pass);
// check if a file exist
        $path = "/htdocs/filestore/"; //the path where the file is located
        $check_file_exist = $path.$file; //combine string for easy use
        $contents_on_server = ftp_nlist($conn_id, $path); //Returns an array of filenames from the specified directory on success or FALSE on error. 
// Test if file is in the ftp_nlist array
        if (in_array($check_file_exist, $contents_on_server)) 
        	return true;
        else
        {
            return false;
        };

        // output $contents_on_server, shows all the files it found, helps for debugging, you can use print_r() as well
        //var_dump($contents_on_server);

		// remember to always close your ftp connection
        ftp_close($conn_id);	
}
?>