using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NotesKeeper.DataAccess.Models;

namespace NotesKeeper.DataAccess
{
    public interface IRepository
    {
        void ConnectToDb();

        Task<BaseItem> AddAsync(BaseItem item);

        ConfiguredTaskAwaitable<bool> DeleteAsync(BaseItem item);

        ConfiguredTaskAwaitable<BaseItem> UpdateAsync(BaseItem item);

        ConfiguredTaskAwaitable<IEnumerable<BaseItem>> GetAsync(Func<BaseItem, bool> filter);
    }
}
