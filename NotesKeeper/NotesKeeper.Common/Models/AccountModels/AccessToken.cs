using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common.Models.AccountModels
{
    public class AccessToken
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
