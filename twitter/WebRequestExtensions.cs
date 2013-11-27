namespace audit.twitter
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    using audit.twitter.OAuth;

    public static class WebRequestExtensions
    {
        public static void AddPostData(this WebRequest request,  List<KeyValuePair<string, string>> postData)
        {
            IEnumerable<string> urlParmetersList = postData.Select(k => Rfc3986.EscapeUriDataString(k.Key) + "=" + Rfc3986.EscapeUriDataString(k.Value));
            string poststring = string.Join("&", urlParmetersList);

            byte[] byteArray = Encoding.UTF8.GetBytes(poststring);
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
        }
    }
}