using NotesKeeper.Common;
using System.Threading.Tasks;

namespace NotesKeeper.DataAccess.Interfaces.Factories
{
    public interface IContextFactory
    {
        Task<IContext<T>> CreateContext<T>() where T : BaseModel;
    }
}
