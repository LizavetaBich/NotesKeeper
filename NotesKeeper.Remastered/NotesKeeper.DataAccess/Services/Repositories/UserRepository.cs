using NotesKeeper.Common.Comparers;
using NotesKeeper.Common.Extensions;
using NotesKeeper.Common.Interfaces;
using NotesKeeper.Common.Models;
using NotesKeeper.DataAccess.Interfaces.DbContexts;
using NotesKeeper.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NotesKeeper.DataAccess.Services.Repositories
{
    public class UserRepository : IRepository<IUser>
    {
        private readonly IMainDbContext _mainDbContext;

        public UserRepository(IMainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        protected IQueryable<IUser> Users => _mainDbContext.Users;

        protected virtual IEqualityComparer<IUser> EqualityComparer => new UserEqualityComparer();

        public bool Delete(IEnumerable<Guid> modelIds)
        {
            throw new NotImplementedException();
        }

        public bool IsExists(IEnumerable<Guid> modelIds)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> Read()
        {
            throw new NotImplementedException();
        }

        public void Save(IEnumerable<IUser> models)
        {
            if (models.IsNullOrEmpty())
            {
                throw new ArgumentNullException($"Object {nameof(models)} is null or empty.");
            }

            // Update existing items
            var existingModels = _mainDbContext.Users.Intersect(models.AsQueryable(), EqualityComparer);

            // Save new items
            var newModels = models.Except(existingModels, EqualityComparer);

            _mainDbContext.Users.AddRange(newModels as IEnumerable<User>);
        }

        private void Update(IEnumerable<IUser> models)
        {

        }

        private void Store(IEnumerable<IUser> models)
        {
            _mainDbContext.Users.AddRange((IEnumerable<Models.User>)models);

        }
    }
}
