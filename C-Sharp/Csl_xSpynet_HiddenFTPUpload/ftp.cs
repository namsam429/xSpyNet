using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;

namespace xSpyNet
{
    public static class ftp
    {
        public static bool UploadFileToServerStore(string server, string user, string password, string fileurl, string filenameX)
        {
            //try {
                WebClient client = new WebClient();
                client.Credentials = new NetworkCredential(user, password);
                client.UploadFile("ftp://" + server + "/htdocs/filestore/" + filenameX, "STOR", fileurl);
                return true;
            //}
            //catch
            //{
                //return false;
            //}
        }
        public static bool CheckIfFileExistsOnStoreServer(string server, string user, string password, string fileName)
        {
            var request = (FtpWebRequest)WebRequest.Create("ftp://" + server + "/htdocs/filestore/" + fileName);
            request.Credentials = new NetworkCredential(user, password);
            request.Method = WebRequestMethods.Ftp.GetFileSize;
            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return false;
        }  
    }
}
