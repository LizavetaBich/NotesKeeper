using NotesKeeper.Common.Interfaces;

namespace NotesKeeper.DataAccess.Services
{
    public class DbConnectionStringProvider : IDbConnectionStringProvider
    {
        public string GetMainDbConnectionString()
        {
            throw new System.NotImplementedException();
        }

        public string GetUserDbConnectionString()
        {
            throw new System.NotImplementedException();
        }
    }
}
