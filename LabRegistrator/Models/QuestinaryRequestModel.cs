using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabRegistrator.Models
{
    public class QuestinaryRequestModel
    {
        public string contract { get; set; }
        public analyticsrequests[] analyticsrequests { get; set; }
    }

    public class analyticsrequests
    {
        public string id { get; set; }
        public string specimen_code { get; set; }
        public string bodysite_code { get; set; }
        public string container_type { get; set; }

    }
}
