using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NotesKeeper.Common.Models;

namespace NotesKeeper.DataAccess
{
    public interface IRepository
    {
        void ConnectToDb();

        Task<BaseItem> AddAsync(BaseItem item);

        ConfiguredTaskAwaitable<bool> DeleteAsync(BaseItem item);

        ConfiguredTaskAwaitable<BaseItem> UpdateAsync(BaseItem item);

        IQueryable<BaseItem> GetQueryable<T>();
    }
}
