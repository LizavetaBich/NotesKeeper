using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common.Interfaces
{
    public interface IRepository<TModel>
    {
        IEnumerable<TModel> Read();

        void Save(IEnumerable<TModel> models);

        bool Delete(IEnumerable<Guid> modelIds);

        bool IsExists(IEnumerable<Guid> modelIds);
    }
}
