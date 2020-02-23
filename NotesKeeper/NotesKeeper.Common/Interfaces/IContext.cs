using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotesKeeper.DataAccess.Interfaces
{
    public interface IContext<T> : IDisposable
    {
        Task OpenConnection();

        Task Flush();

        Task<IEnumerable<T>> Create(IEnumerable<T> items);

        Task<IEnumerable<T>> Read(IEnumerable<Guid> ids, Func<bool> filter = null);

        Task Update(IEnumerable<T> items);

        Task Delete(IEnumerable<Guid> ids);
    }
}
