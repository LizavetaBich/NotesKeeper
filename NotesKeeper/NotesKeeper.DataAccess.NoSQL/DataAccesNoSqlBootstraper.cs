using NotesKeeper.Common.Interfaces.Factories;
using NotesKeeper.DataAccess.Interfaces;
using SimpleInjector;

namespace NotesKeeper.DataAccess.NoSQL
{
    public class DataAccesNoSqlBootstraper
    {
        public static void Bootstrap(Container container)
        {
            container.Register<IRepository, DocumentRepository>();
            container.Register<IContextFactory, NoSqlContextFactory>();
        }
    }
}
