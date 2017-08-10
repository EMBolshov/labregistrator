using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows;

namespace LabRegistrator
{

    /// <summary>
    /// Базовый класс для ответов.
    /// </summary>
    class Response
    {
        public T ResponseBasicHandler<T>(WebRequest HttpResponse)
        {
            var getResponse = (HttpWebResponse) HttpResponse.GetResponse();
            Stream getResponseToStream = getResponse.GetResponseStream();
            StreamReader ResponseStreamReader = new StreamReader(getResponseToStream);
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var str = ResponseStreamReader.ReadToEnd().Replace(@"""..._code"":null""", @"""undefined""");
            // var str = ResponseStreamReader.ReadToEnd();
            var jsonResponse = serializer.Deserialize<T>(str);
            return jsonResponse;
  
        }   
    }
}


