using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using LabRegistrator.Models;

namespace LabRegistrator
{
   class RequestToMDO : IRequestToMDO
    {

        public  QuestinaryRequestModel testRequest = new QuestinaryRequestModel
        {
            contract = "C000035569",
            analyticsrequests = new []
            {
                new analyticsrequests {id = "001.028",specimen_code = "122555007",bodysite_code = "null",container_type = "null" },
            } 
           
        };

        protected static string Headtoken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjU2NjU0MzNCMjg2ODM1QjFERDg2OTRDRTUzRkYzQUE1RTYyNDFBNUQiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJWbVZET3lob05iSGRocFRPVV84NnBlWWtHbDAifQ.eyJuYmYiOjE1MDg4MjU5MDAsImV4cCI6MTUwODg2MTkwMCwiaXNzIjoiaHR0cHM6Ly9hdXRoLXN0YWdlLm1lZGxpbngub25saW5lIiwiYXVkIjpbImh0dHBzOi8vYXV0aC1zdGFnZS5tZWRsaW54Lm9ubGluZS9yZXNvdXJjZXMiLCJmaGlyQVBJIl0sImNsaWVudF9pZCI6InRlc3RwZXB5YWthIiwic3ViIjoiMjRmMTFmYjAtNDBhZC00MWUxLWIwYjQtZDM1MDI3NGE2MTgwIiwiYXV0aF90aW1lIjoxNTA4NzQ2MzQ0LCJpZHAiOiJsb2NhbCIsImZoaXItZHN0dTIiOlsiZmhpci8qLyQqIiwiZmhpci8kKiJdLCJzY29wZSI6WyJtaXMiXSwiYW1yIjpbInB3ZCJdfQ.QDWKksJK25GapR2utkV0saDDUXdIlLD0MYkE41gqZDXuxUxVhlIJc4dtS9K6Da4L5Fe5PKm8PghiXEqBsGq_UNfx_Ylu86zrbQWjgA1pRKDD6TNqjxCP8ukEbRAMVE9WTwHBlv28v35IkeAXN1ts9fGEFiGaa8spCRoBQ0jGNCi6dLyXmEyUB5ZSvg-AZ1zPjqYKrjtVvDcOaK-Nzim3MWltsvRPrRujShr2nP9MOHieYDgBk3K5TnNfimAkWPnPC9TZuLIyHTsPOTOenS34Xras5MMItMiTzfcBwxOduSWv2aJ7e_JFtMIOELrjhsFzW4p_ViuMa3FB-5D6E3_FxA\r\n";
        protected static string Url = "https://api-stage.medlinx.online/";
        private string _contract = "C000035569";
        //TODO: Опросник идёт с бандлом
        public WebRequest getNomenclature()
        {
            try
            {
                var webRequest = (HttpWebRequest) WebRequest.Create(Url + "nomenclature" + "?contract=" + this._contract);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 20000;
                    webRequest.ContentType = "application/json";
                    webRequest.Headers.Add("Authorization", "Bearer " + Headtoken);
                    return webRequest;
                }
            }
            catch (WebException)
            {
                return null;
            }
            return null;
        }
        
        public WebRequest getQuestinary()
        {
            var serializedModel = QuestinaryModelToJson();
            try
            {
                var webRequest = (HttpWebRequest) WebRequest.Create(Url + "questionnaire");
                if (webRequest != null)
                {
                    webRequest.Method = "POST";
                    webRequest.ContentType = "application/json";
                    webRequest.Timeout = 20000;
                    webRequest.Headers.Add("Authorization", "Bearer " + Headtoken);

                    using (var streamWritter = new StreamWriter(webRequest.GetRequestStream()))
                    {
                        streamWritter.Write(serializedModel);
                        streamWritter.Flush();
                        streamWritter.Close();
                    }
                    return webRequest;

                }
            }
            catch (WebException)
            {

                return null;
            }
            return null;
        }


        private object QuestinaryModelToJson()
        {
            var json = new JavaScriptSerializer().Serialize(testRequest);
            return json;
        }
    }

}
       
   

       

