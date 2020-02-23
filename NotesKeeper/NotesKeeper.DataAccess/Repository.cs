using NotesKeeper.DataAccess.Interfaces;
using NotesKeeper.DataAccess.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesKeeper.DataAccess
{
    public class Repository : IRepository
    {
        private readonly IContextFactory _contextFactory;

        public Repository(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create<T>(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException();
            }

            return (await Create<T>(new List<T> { item })).SingleOrDefault();
        }

        public async Task<IEnumerable<T>> Create<T>(IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException();
            }

            IEnumerable<T> result = default;

            using (var context = await _contextFactory.CreateContext<T>())
            {
                await context.OpenConnection();

                result = await context.Create(items);

                await context.Flush();
            }

            return result;
        }

        public Task Delete<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Delete<T>(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<T> Read<T>(Guid id, Func<bool> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Read<T>(IEnumerable<Guid> ids, Func<bool> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task Update<T>(T item)
        {
            throw new NotImplementedException();
        }

        public Task Update<T>(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
