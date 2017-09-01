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
    //TODO Interface

    interface IRequestToMDO
    {
        WebRequest getNomenclature();

        WebRequest getQuestinary();
        // WebRequest sendToPreanalitic();


    }

    class RequestToMDO : IRequestToMDO
    {

        public analyticsrequests kek = new analyticsrequests {id = "001.028", specimen_code = "122555007", bodysite_code = "null", container_type = "null"};
        public  QuestinaryRequestModel testRequest = new QuestinaryRequestModel
        {
            contract = "C000035569",
            analytics = new []
            {
                new analyticsrequests{"001.028","122555007","null","null" }
            } 
           
        };

        protected static string headtoken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjU2NjU0MzNCMjg2ODM1QjFERDg2OTRDRTUzRkYzQUE1RTYyNDFBNUQiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJWbVZET3lob05iSGRocFRPVV84NnBlWWtHbDAifQ.eyJuYmYiOjE1MDQyNjY4ODQsImV4cCI6MTUwNDMwMjg4NCwiaXNzIjoiaHR0cHM6Ly9hdXRoLXN0YWdlLm1lZGxpbngub25saW5lIiwiYXVkIjpbImh0dHBzOi8vYXV0aC1zdGFnZS5tZWRsaW54Lm9ubGluZS9yZXNvdXJjZXMiLCJmaGlyQVBJIl0sImNsaWVudF9pZCI6InRlc3RwZXB5YWthIiwic3ViIjoiZDE3OTBmODEtODQyMi00OWI1LWJkZWYtZjFhMjgwYTZlMWM1IiwiYXV0aF90aW1lIjoxNTA0MjY2ODgyLCJpZHAiOiJsb2NhbCIsImZoaXItZHN0dTIiOiJmaGlyLyovJCoiLCJzY29wZSI6WyJtaXMiXSwiYW1yIjpbInB3ZCJdfQ.mzZwZ-b9UKW0m-HtNaxCUIcZ1FcSbQqnlIH5Tz9Y3CZNOFY9Cg7rfho5GpNp-VX9b7bIszsmLdsaloMG1HvkTaljIbGtF5hWvM9RBIA-K246hIQ1OkdGPWc1AljgLY3CdkN1E3J0ZnkiZNf7dN5NJzQRY2TmKdiKkyxgH1-QRRK9LREaGZGEWoAeqi8k7OSRMsJUNvXLVB-f0HaWBM699p3C569_HeolNoa3nF-XwigVlBu14eb1d_nfPt2FiVe9J-9J3dnt3gcR7UOsHWK62U4P6-O-H4wJxG96Vz5M9QJu_R8fHsE3rm_-wPKrPw2OFXtM1Wo7hIojGAdjznhL8w";
        protected static string url = "https://api-stage.medlinx.online/";
        private string _contract = "C000035569";

       

        public WebRequest getNomenclature()
        {
            try
            {
                var webRequest = (HttpWebRequest) WebRequest.Create(url + "nomenclature" + "?contract=" + this._contract);
                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.Timeout = 20000;
                    webRequest.ContentType = "application/json";
                    webRequest.Headers.Add("Authorization", "Bearer " + headtoken);
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
                var webRequest = (HttpWebRequest) WebRequest.Create(url + "questionnaire");
                if (webRequest != null)
                {
                    webRequest.Method = "POST";
                    webRequest.ContentType = "application/json";
                    webRequest.Timeout = 20000;
                    webRequest.Headers.Add("Authorization", "Bearer " + headtoken);

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
       
   

       

