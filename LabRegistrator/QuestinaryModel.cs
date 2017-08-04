using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabRegistrator
{
    class QuestianaryModel
    {

        public class Rootobject
        {
            public string resourceType { get; set; }
            public string status { get; set; }
            public Item item { get; set; }
        }

        public class Item
        {
            public string linkId { get; set; }
            public string text { get; set; }
            public bool required { get; set; }
            public bool repeats { get; set; }
            public string type { get; set; }
            public Item1[] item { get; set; }
        }

        public class Item1
        {
            public string linkId { get; set; }
            public string text { get; set; }
            public bool required { get; set; }
            public bool repeats { get; set; }
            public Item2[] item { get; set; }
            public string type { get; set; }
        }

        public class Item2
        {
            public string linkId { get; set; }
            public string text { get; set; }
            public string type { get; set; }
            public bool required { get; set; }
            public bool repeats { get; set; }
            public Option[] option { get; set; }
        }

        public class Option
        {
            public string system { get; set; }
            public string code { get; set; }
        }

    }
}
