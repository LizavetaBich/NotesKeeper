using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotesKeeper.WebApi.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Email is not specified.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is not specified.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is not specified.")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
