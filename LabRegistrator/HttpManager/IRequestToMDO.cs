using System.Collections.Generic;
using System.Net;
using LabRegistrator.Models;

namespace LabRegistrator
{
    interface IRequestToMDO
    {
        WebRequest getNomenclature();
        WebRequest getQuestinary(List<analyticsrequests> req);
        //TODO: getPreanalytics()
        //TODO: sendBundle()
        //TODO: ExceptionsClassForWebRequest
    }
}