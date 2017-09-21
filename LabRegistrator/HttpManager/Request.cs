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

        protected static string Headtoken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjU2NjU0MzNCMjg2ODM1QjFERDg2OTRDRTUzRkYzQUE1RTYyNDFBNUQiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJWbVZET3lob05iSGRocFRPVV84NnBlWWtHbDAifQ.eyJuYmYiOjE1MDU3MzYxODYsImV4cCI6MTUwNTc3MjE4NiwiaXNzIjoiaHR0cHM6Ly9hdXRoLXN0YWdlLm1lZGxpbngub25saW5lIiwiYXVkIjpbImh0dHBzOi8vYXV0aC1zdGFnZS5tZWRsaW54Lm9ubGluZS9yZXNvdXJjZXMiLCJmaGlyQVBJIl0sImNsaWVudF9pZCI6InRlc3RwZXB5YWthIiwic3ViIjoiZDE3OTBmODEtODQyMi00OWI1LWJkZWYtZjFhMjgwYTZlMWM1IiwiYXV0aF90aW1lIjoxNTA1NzM2MTg0LCJpZHAiOiJsb2NhbCIsImZoaXItZHN0dTIiOiJmaGlyLyovJCoiLCJzY29wZSI6WyJtaXMiXSwiYW1yIjpbInB3ZCJdfQ.rqelTCfehbMQIl9kwCwkkIJgwQ0LVhlJEIz_URiVnTCb6FlshAG_KPg5BRtDCyvuD7pmZ_ohvxGzU8w0pa9zEkmP9BtkFIzARSWjnYmjkA3Tt4TV-LbrvdUemeXdCT7IDBMzzR8X5A2UyRwaW07vLzvH_v_xTp5VX-RV60AcltJWzwVZi785szbqfF8PkrVcy1F8GPiG5ZYoSRFquXZEAvGTdvBoBng5Iuq79p1HZfzjwFkqS6YrB7FcfEoXkQRoBw9ETjA8EtfLBCx7SoHglMxTPfYFzDduLmIYvFXwQYZAf9-NzDxOXKFEooyCW4XyFZvdtxj10P8yXftJNVrHXA";
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
       
   

       

