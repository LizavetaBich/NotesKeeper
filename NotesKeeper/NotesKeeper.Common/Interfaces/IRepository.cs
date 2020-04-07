using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesKeeper.DataAccess.Interfaces
{
    public interface IRepository
    {
        Task<T> Create<T>(T item);

        Task<T> Read<T>(Guid id);

        Task<IEnumerable<T>> ReadAll<T>();

        Task<T> Update<T>(T item);

        Task Delete<T>(Guid id);

        Task<IEnumerable<T>> Create<T>(IEnumerable<T> items);

        Task<IEnumerable<T>> Read<T>(IEnumerable<Guid> ids);

        Task<IEnumerable<T>> Read<T>(Func<T, bool> filter = null);

        Task<IEnumerable<T>> Update<T>(IEnumerable<T> items);

        Task Delete<T>(IEnumerable<Guid> ids);
    }
}
