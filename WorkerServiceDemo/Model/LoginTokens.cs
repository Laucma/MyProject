using System;
using System.Collections.Generic;
using System.Text;

namespace WorkerServiceDemo.Model
{
    class LoginTokens
    {
        //public string access_token { get; set; }
        public string refresh_token { get; set; }
    }

    class LoginPara
    {
        public string client_id { get; set; } = "Xinning_Api";
        public string client_secret { get; set; } = "Xinning@2021*";
        public string grant_type { get; set; } = "password";
        public string username { get; set; }
        public string password { get; set; }
    }
}
