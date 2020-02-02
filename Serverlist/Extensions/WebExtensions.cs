using GameCore;
using MEC;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Serverlist.Extensions
{
	public static class WebExtensions
	{
        /// <summary>
        /// Sends a port request to url with parameters and values
        /// </summary>
        public static string WebRequest(string url, string[] postParams, string[] postValues)
        {
            if (url == null || url.Length == 0)
            {
                throw new ApplicationException("Specify the URI of the resource to retrieve.");
            }
            WebClient client = new WebClient();

            // Add a user agent header in case the 
            // requested URI contains a query.

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            var _postParams = new System.Collections.Specialized.NameValueCollection();

            for (int i = 0; i < postParams.Length; i++)
            {
                _postParams.Add(postParams[i], postValues[i]);
            }
            byte[] responsebytes = client.UploadValues(url, "POST", _postParams);
            string responsebody = Encoding.UTF8.GetString(responsebytes);
            return responsebody;
        }

        /// <summary>
        /// Sends a port request to url with parameters and values
        /// </summary>
        public static string WebRequest(string url, List<string> postParams, List<string> postValues)
        {
            if (url == null || url.Length == 0)
            {
                throw new ApplicationException("Specify the URI of the resource to retrieve.");
            }
            WebClient client = new WebClient();

            // Add a user agent header in case the 
            // requested URI contains a query.

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            var _postParams = new System.Collections.Specialized.NameValueCollection();

            for (int i = 0; i < postParams.Count; i++)
            {
                _postParams.Add(postParams[i], postValues[i]);
            }
            byte[] responsebytes = client.UploadValues(url, "POST", _postParams);
            string responsebody = Encoding.UTF8.GetString(responsebytes);
            return responsebody;
        }
    }
}