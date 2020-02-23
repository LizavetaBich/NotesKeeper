using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotesKeeper.DataAccess.Interfaces.Factories
{
    public interface IContextFactory
    {
        Task<IContext<T>> CreateContext<T>();
    }
}
