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

        protected static string Headtoken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IjU2NjU0MzNCMjg2ODM1QjFERDg2OTRDRTUzRkYzQUE1RTYyNDFBNUQiLCJ0eXAiOiJKV1QiLCJ4NXQiOiJWbVZET3lob05iSGRocFRPVV84NnBlWWtHbDAifQ.eyJuYmYiOjE1MDg0MTA4NzYsImV4cCI6MTUwODQ0Njg3NiwiaXNzIjoiaHR0cHM6Ly9hdXRoLXN0YWdlLm1lZGxpbngub25saW5lIiwiYXVkIjpbImh0dHBzOi8vYXV0aC1zdGFnZS5tZWRsaW54Lm9ubGluZS9yZXNvdXJjZXMiLCJmaGlyQVBJIl0sImNsaWVudF9pZCI6InRlc3RwZXB5YWthIiwic3ViIjoiZDE3OTBmODEtODQyMi00OWI1LWJkZWYtZjFhMjgwYTZlMWM1IiwiYXV0aF90aW1lIjoxNTA4NDEwODc2LCJpZHAiOiJsb2NhbCIsImZoaXItZHN0dTIiOiJmaGlyLyovJCoiLCJzY29wZSI6WyJtaXMiXSwiYW1yIjpbInB3ZCJdfQ.mwutt8eFCaxgQhicyWMOAO6FD0Pt0XAV2mYI4z5CBnKEUgeGGkNZQkXFEo77LcKmDbygcBICZqhVK8DxvpKwIkAz5IqaDoHD6EZl9itF1ytkn46oIXa06r_HKypQFb2wwxC8RcRcD36Q9M0RKXAChzWgpbV4z1_0GOm0X1zrTTeRLisQ-24BAPhDrt8q70KZWLiRuFVMGnZfxe8bZxPkH1f74O79EBqX74LSGUSTISJJnVkVXxjokdKlHKHaRlI7IGzpb_5JnW9cbVCSsTcsfWdabOxTsm1JCaza4pDhbYZYAPi9tDeZZ5b2tcy_zyzmkuLtJtASnLiO54F8tgk0sQ";
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
       
   

       

