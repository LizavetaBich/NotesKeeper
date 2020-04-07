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

            var context = await _contextFactory.CreateContext<T>();
            
            await context.OpenConnection();

            result = await context.Create(items);

            return result;
        }

        public Task Delete<T>(Guid id)
        {
            return Delete<T>(new List<Guid>() { id });
        }

        public async Task Delete<T>(IEnumerable<Guid> ids)
        {
            var context = await _contextFactory.CreateContext<T>();
            
            await context.OpenConnection();

            await context.Delete(ids);
        }

        public async Task<T> Read<T>(Guid id)
        {
            var context = await _contextFactory.CreateContext<T>();
            
            await context.OpenConnection();
            var items = await context.Read(new List<Guid>() { id });
            return items.Single();
        }

        public async Task<IEnumerable<T>> ReadAll<T>()
        {
            var context = await _contextFactory.CreateContext<T>();
            await context.OpenConnection();
            return await context.Read();
        }

        public async Task<T> Update<T>(T item)
        {
            var result = await Update(new List<T>() { item });
            return result.Single();
        }

        public async Task<IEnumerable<T>> Read<T>(IEnumerable<Guid> ids)
        {
            var context = await _contextFactory.CreateContext<T>();
            await context.OpenConnection();
            return await context.Read(ids);
        }

        public async Task<IEnumerable<T>> Read<T>(Func<T, bool> filter = null)
        {
            var context = await _contextFactory.CreateContext<T>();
            await context.OpenConnection();
            return await context.Read(filter);
        }

        public async Task<IEnumerable<T>> Update<T>(IEnumerable<T> items)
        {
            var context = await _contextFactory.CreateContext<T>();
            await context.OpenConnection();
            await context.Update(items);
            return items;
        }
    }
}
