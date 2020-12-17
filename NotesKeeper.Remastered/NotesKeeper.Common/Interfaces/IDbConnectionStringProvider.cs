using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common.Interfaces
{
    public interface IDbConnectionStringProvider
    {
        string GetUserDbConnectionString();

        string GetMainDbConnectionString();
    }
}
