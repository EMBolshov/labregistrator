using System.Net;

namespace LabRegistrator
{
    interface IRequestToMDO
    {
        WebRequest getNomenclature();
        WebRequest getQuestinary();
        //TODO: getPreanalytics()
        //TODO: sendBundle()
        //TODO: ExceptionsClassForWebRequest
    }
}