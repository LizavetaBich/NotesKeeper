using NotesKeeper.Common.Interfaces.DataAccess;
using SimpleInjector;

namespace NotesKeeper.DataAccess.EntityFramework
{
    public static class DataAccessEFBootstraper
    {
        public static void Bootstrap(Container container)
        {
            container.Register<IDbContext, MainDbContext>(Lifestyle.Scoped);
            container.Register<IUserDbContext, UserDbContext>(Lifestyle.Scoped);
        }
    }
}
