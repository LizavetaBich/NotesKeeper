using NotesKeeper.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotesKeeper.Common.Interfaces
{
    public interface IConfigProvider
    {
        Task<UserConfig> GetUserConfig();
    }
}
