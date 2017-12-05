using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLibrary.Model.Token
{
  public  class JsonWebToken
    {
        public string access_token { get; set; }

        public string token_type { get; set; } = "bearer";

        public int expires_in { get; set; }

        public string refresh_token { get; set; }
    }
}
