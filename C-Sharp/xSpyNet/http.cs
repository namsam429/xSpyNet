using System;
using System.Text;
using System.Collections.Specialized;
using System.Net;

namespace xSpyNet
{
    public class http
    {
        public static byte[] Post(string uri, NameValueCollection pairs)
        {
            byte[] response = null;
            using (WebClient client = new WebClient())
            {
                response = client.UploadValues(uri, pairs);
            }
            return response;
        }
    }
}
