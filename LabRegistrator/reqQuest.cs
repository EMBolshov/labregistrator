using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace LabRegistrator
{
    class reqQeust : Request
    {

        public WebRequest QuestRequest(string ConstUrl, string headtoken, NomenFieldsForReq nomen)
        {

            return null;
        }

    }

    class NomenFieldsForReq
    {

        public string id;
        public string spicemancode;
        public string bodysitecode;
        public string containertype;
    }
}