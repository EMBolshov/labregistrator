using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabRegistrator
{
    class Token
    {
        private string _token;

        public string Value
        {
           get { return _token; }
           set { this._token = value; }
        }
    }
}