using NotesKeeper.Common;
using NotesKeeper.Common.Interfaces;
using NotesKeeper.Common.Models.Configuration;
using NotesKeeper.Common.Serializers;
using NotesKeeper.DataAccess.Interfaces;
using NotesKeeper.Common.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace NotesKeeper.DataAccess.NoSQL
{
    public class NoSqlContextFactory : IContextFactory
    {
        private readonly IConfiguration _configuration;

        public NoSqlContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<IDocumentContext<T>> CreateContext<T>() where T : BaseModel
        {
            return Task.Run(async () =>
            {
                var serializer = new JsonCustomSerializer<T>();
                IDocumentContext<T> context = new NoSqlContext<T>(serializer, _configuration);
                return context;
            });
        }
    }
}
