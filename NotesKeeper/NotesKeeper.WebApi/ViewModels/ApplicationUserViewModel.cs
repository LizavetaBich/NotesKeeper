using NotesKeeper.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesKeeper.WebApi.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Token { get; set; }

        public Role Role { get; set; }
    }
}
