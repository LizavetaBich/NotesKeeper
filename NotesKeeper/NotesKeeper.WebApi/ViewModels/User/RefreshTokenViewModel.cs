using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesKeeper.WebApi.ViewModels
{
    public class RefreshTokenViewModel
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
