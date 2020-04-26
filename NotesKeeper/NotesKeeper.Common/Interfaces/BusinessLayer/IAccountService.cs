using NotesKeeper.Common.Models;
using NotesKeeper.Common.Models.AccountModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotesKeeper.Common.Interfaces.BusinessLayer
{
    public interface IAccountService
    {
        Task<ApplicationUser> RegisterUser(RegisterModel registerModel);

        Task<ApplicationUser> LoginUser(LoginModel loginModel);
    }
}
