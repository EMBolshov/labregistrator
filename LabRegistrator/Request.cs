using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace LabRegistrator
{
    /// <summary>
    /// Базовый класс для различных запросов, хранит в себе адрес к которому должны обращаться все запросы и токен авторизации
    /// </summary>
    class Request
    {
        protected static string headtoken = "0000";
        protected static string url = "https://api-stage.medlinx.online/";
        /// <summary>
        /// Метод для запроса номенклатуры
        /// передаем описанные ниже параметры, если все ок - возвращаем HttpWebREquest webRequest
        /// </summary>
        /// <param name="ConstUrl">Url для запроса, https://api-stage.medlinx.online/ из базового класса, плюс /nomenclature (потом будет config можно будет настроить там)</param>
        /// <param name="Contract">Номер контракта</param>
        /// <param name="headtoken">Токен авторизации</param>
        /// <returns></returns>
        public WebRequest basicRequest(string ConstUrl, string Contract, string headtoken)
        {
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url + ConstUrl + "?contract=" + Contract);
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
    }
}
