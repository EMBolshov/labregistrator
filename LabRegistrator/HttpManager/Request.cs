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
            contract = "C000003409",
            analyticsrequests = new []
            {
                new analyticsrequests {id = "001.028",specimen_code = "122555007",bodysite_code = "null",container_type = "null" },
            } 
           
        };

        protected static string Headtoken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjU2NjU0MzNCMjg2ODM1QjFERDg2OTRDRTUzRkYzQUE1RTYyNDFBNUQiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJWbVZET3lob05iSGRocFRPVV84NnBlWWtHbDAifQ.eyJuYmYiOjE1MDk0NjEyNTEsImV4cCI6MTUwOTQ5NzI1MSwiaXNzIjoiaHR0cHM6Ly9hdXRoLXN0YWdlLm1lZGxpbngub25saW5lIiwiYXVkIjpbImh0dHBzOi8vYXV0aC1zdGFnZS5tZWRsaW54Lm9ubGluZS9yZXNvdXJjZXMiLCJmaGlyQVBJIl0sImNsaWVudF9pZCI6InRlc3RwZXB5YWthIiwic3ViIjoiZDE3OTBmODEtODQyMi00OWI1LWJkZWYtZjFhMjgwYTZlMWM1IiwiYXV0aF90aW1lIjoxNTA5NDU4NjAxLCJpZHAiOiJsb2NhbCIsImZoaXItZHN0dTIiOiJmaGlyLyovJCoiLCJzY29wZSI6WyJtaXMiXSwiYW1yIjpbInB3ZCJdfQ.Dea5D6Sapyq734G4hP3EE_i3CTMdIkWiY7NyotcDTguOf64JSEJhwz6lbWn4T1qF2T1UJghSzRdlIfJ4slpX9uY5wAKlK_cMuUnuUE0slRHtJSnK3jcB7OrOI2eBmjnxAlwSFI4TN6D-SK_DYLcVUPN8usAOPR8Q5GKMcXRzRtg8im7xA3iINKR3Lm-wvPML0cLPYhqibmk1ES1SqOGT9Y6jIgqQVvCr311RJngRNo_Xfx-sb5VUVBcet078a_A5TFw2_77p_669yFcqZFrJ-3WKrn24j6jS5cTVkJU3IMNQVcYo3fJoSRd0w5CAtWHNOzqk8ESQufVvV0_wMY5Upw\r\n";
        protected static string Url = "https://api-stage.medlinx.online/";
        private string _contract = "C000003409";

        //TODO: Опросник идёт с бандлом
        public WebRequest getNomenclature()
        {
            try
            {
                var webRequest = (HttpWebRequest) WebRequest.Create(Url + "nomenclature" + "?contract=" + this._contract);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout= 100000;
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
       
   

       

