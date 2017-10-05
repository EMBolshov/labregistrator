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
    class Response
    {
        public T ResponseToModelConverter<T>(WebRequest HttpResponse)
        {
            var ResponseStreamReader = ResponseStreamGetter(HttpResponse);
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var str = ResponseStreamReader.ReadToEnd().Replace(@"""..._code"":null""", @"""undefined""");
            var jsonResponse = serializer.Deserialize<T>(str);
            return jsonResponse;
        }

        private StreamReader ResponseStreamGetter(WebRequest HttpResponse)
        {
            try
            {
                var getResponse = (HttpWebResponse) HttpResponse.GetResponse();
                Stream getResponseToStream = getResponse.GetResponseStream();
                StreamReader ResponseStreamReader = new StreamReader(getResponseToStream);
                return ResponseStreamReader;
            }
            catch (NullReferenceException e)
            {
                //TODO: Nlog report + allert
                return null;
            }
        }
    }
}


