using NotesKeeper.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotesKeeper.DataAccess.Interfaces
{
    public interface IDocumentContext<T> where T : BaseModel
    {
        Task OpenConnection();

        Task<IEnumerable<T>> Create(IEnumerable<T> items);

        Task<IEnumerable<T>> Read(IEnumerable<Guid> ids);

        Task<IEnumerable<T>> Read(Func<T, bool> filter = null);

        Task Update(IEnumerable<T> items);

        Task Delete(IEnumerable<Guid> ids = null);
    }
}
