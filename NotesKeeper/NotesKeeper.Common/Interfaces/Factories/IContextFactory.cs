using NotesKeeper.DataAccess.Interfaces;
using System.Threading.Tasks;

namespace NotesKeeper.Common.Interfaces.Factories
{
    public interface IContextFactory
    {
        Task<IDocumentContext<T>> CreateContext<T>() where T : BaseModel;
    }
}
