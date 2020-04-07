using System.Threading.Tasks;

namespace NotesKeeper.DataAccess.Interfaces.Factories
{
    public interface IContextFactory
    {
        Task<IContext<T>> CreateContext<T>();
    }
}
