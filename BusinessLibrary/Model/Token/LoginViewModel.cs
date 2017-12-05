using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Model.Token
{
  public  class LoginViewModel
    {
        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string refresh_token { get; set; }
    }
}
