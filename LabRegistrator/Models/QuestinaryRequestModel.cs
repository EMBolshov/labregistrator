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
        public analyticsrequests[] analytics { get; set; }
    }

    public class analyticsrequests: IEnumerable<analyticsrequests>
    {
        public string id { get; set; }
        public string specimen_code { get; set; }
        public string bodysite_code { get; set; }
        public string container_type { get; set; }

        public IEnumerator<analyticsrequests> GetEnumerator()
        {
            return analyticsrequests.GetEnumerator();
        }
    }
}
