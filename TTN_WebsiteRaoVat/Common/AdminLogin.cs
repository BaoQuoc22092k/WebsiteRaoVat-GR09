using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTN_WebsiteRaoVat.Common
{
    [Serializable]
    public class AdminLogin
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}