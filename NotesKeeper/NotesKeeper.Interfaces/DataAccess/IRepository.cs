using NotesKeeper.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesKeeper.DataAccess.Interfaces
{
    public interface IRepository
    {
        Task<T> Create<T>(T item) where T : BaseModel;

        Task<T> Read<T>(Guid id) where T : BaseModel;

        Task<IEnumerable<T>> ReadAll<T>() where T : BaseModel;

        Task<T> Update<T>(T item) where T : BaseModel;

        Task Delete<T>(Guid id) where T : BaseModel;

        Task<IEnumerable<T>> Create<T>(IEnumerable<T> items) where T : BaseModel;

        Task<IEnumerable<T>> Read<T>(IEnumerable<Guid> ids) where T : BaseModel;

        Task<IEnumerable<T>> Read<T>(Func<T, bool> filter = null) where T : BaseModel;

        Task<IEnumerable<T>> Update<T>(IEnumerable<T> items) where T : BaseModel;

        Task Delete<T>(IEnumerable<Guid> ids) where T : BaseModel;
    }
}
