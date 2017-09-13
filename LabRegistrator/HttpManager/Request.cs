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

        protected static string Headtoken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjU2NjU0MzNCMjg2ODM1QjFERDg2OTRDRTUzRkYzQUE1RTYyNDFBNUQiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJWbVZET3lob05iSGRocFRPVV84NnBlWWtHbDAifQ.eyJuYmYiOjE1MDUyMDgyNjQsImV4cCI6MTUwNTI0NDI2NCwiaXNzIjoiaHR0cHM6Ly9hdXRoLXN0YWdlLm1lZGxpbngub25saW5lIiwiYXVkIjpbImh0dHBzOi8vYXV0aC1zdGFnZS5tZWRsaW54Lm9ubGluZS9yZXNvdXJjZXMiLCJmaGlyQVBJIl0sImNsaWVudF9pZCI6InRlc3RwZXB5YWthIiwic3ViIjoiZDE3OTBmODEtODQyMi00OWI1LWJkZWYtZjFhMjgwYTZlMWM1IiwiYXV0aF90aW1lIjoxNTA1MjA4MjYxLCJpZHAiOiJsb2NhbCIsImZoaXItZHN0dTIiOiJmaGlyLyovJCoiLCJzY29wZSI6WyJtaXMiXSwiYW1yIjpbInB3ZCJdfQ.nUi3g3qDE9hooNiIHfkqI7wRpUcmm4Bs_yNyOqZYBkvsMU07yt8OovV2ActPENhAzQzDpisN5NL6zTf-IQn8NRsrLcQUBqOt63bOew5Dfe22zX18X81rBxKWful6QAhG-Efxck3BY3TCTxcE3SLTU0wEKZLkjmu2rkwDwYgalbBKfd52jUH_zQzsLvxJ0f7sxB2yiiJFnuydpFtKHiyK0Z8G7ByVaCN9a6HN9JuAL7OMatLWxKrljR5tx02u_GpLDHvwspE_4h9_p659u_-cyFUqR-hAWRBKP7T-nX6FKeGFq_p9703bMmIDXJtDMF1ZuzJmNcOVcSFt7CMT7_5pYQ";
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
       
   

       

