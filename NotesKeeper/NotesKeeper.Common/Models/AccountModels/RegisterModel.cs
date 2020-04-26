using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common.Models.AccountModels
{
    public class RegisterModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
