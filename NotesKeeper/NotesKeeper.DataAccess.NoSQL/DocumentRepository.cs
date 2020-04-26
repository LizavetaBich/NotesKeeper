using NotesKeeper.Common;
using NotesKeeper.DataAccess.Interfaces;
using NotesKeeper.Common.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesKeeper.DataAccess.NoSQL
{
    public class DocumentRepository : IRepository
    {
        private readonly IContextFactory _contextFactory;

        public DocumentRepository(IContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create<T>(T item) where T : BaseModel
        {
            Guard.IsNotNull(item);

            return (await Create<T>(new List<T> { item }).ConfigureAwait(false)).SingleOrDefault();
        }

        public async Task<IEnumerable<T>> Create<T>(IEnumerable<T> items) where T : BaseModel
        {
            Guard.IsNotNull(items);

            IEnumerable<T> result = default;

            var context = await _contextFactory.CreateContext<T>().ConfigureAwait(false);
            
            await context.OpenConnection().ConfigureAwait(false);

            result = await context.Create(items).ConfigureAwait(false);

            return result;
        }

        public Task Delete<T>(Guid id) where T : BaseModel
        {
            return Delete<T>(new List<Guid>() { id });
        }

        public async Task Delete<T>(IEnumerable<Guid> ids) where T : BaseModel
        {
            var context = await _contextFactory.CreateContext<T>().ConfigureAwait(false);
            
            await context.OpenConnection().ConfigureAwait(false);

            await context.Delete(ids).ConfigureAwait(false);
        }

        public async Task<T> Read<T>(Guid id) where T : BaseModel
        {
            var context = await _contextFactory.CreateContext<T>().ConfigureAwait(false);
            
            await context.OpenConnection().ConfigureAwait(false);
            var items = await context.Read(new List<Guid>() { id }).ConfigureAwait(false);
            return items.Single();
        }

        public async Task<IEnumerable<T>> ReadAll<T>() where T : BaseModel
        {
            var context = await _contextFactory.CreateContext<T>().ConfigureAwait(false);
            await context.OpenConnection().ConfigureAwait(false);
            return await context.Read().ConfigureAwait(false);
        }

        public async Task<T> Update<T>(T item) where T : BaseModel
        {
            var result = await Update(new List<T>() { item }).ConfigureAwait(false);
            return result.Single();
        }

        public async Task<IEnumerable<T>> Read<T>(IEnumerable<Guid> ids) where T : BaseModel
        {
            var context = await _contextFactory.CreateContext<T>().ConfigureAwait(false);
            await context.OpenConnection().ConfigureAwait(false);
            return await context.Read(ids).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> Read<T>(Func<T, bool> filter = null) where T : BaseModel
        {
            var context = await _contextFactory.CreateContext<T>().ConfigureAwait(false);
            await context.OpenConnection().ConfigureAwait(false);
            return await context.Read(filter).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> Update<T>(IEnumerable<T> items) where T : BaseModel
        {
            var context = await _contextFactory.CreateContext<T>().ConfigureAwait(false);
            await context.OpenConnection().ConfigureAwait(false);
            await context.Update(items).ConfigureAwait(false);
            return items;
        }
    }
}
