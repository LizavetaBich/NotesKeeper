using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NotesKeeper.DataAccess.Models;

namespace NotesKeeper.DataAccess
{
    public interface IMasterDataAccess
    {
        void ConnectToMasterDb();

        ConfiguredTaskAwaitable<EntityEntry<User>> AddUser(User user);

        Task DeleteUser(Guid id);

        User UpdateUser(User user);

        ConfiguredTaskAwaitable<User> GetUser(Guid id);

        Task<User> GetUser(string email);
    }
}
