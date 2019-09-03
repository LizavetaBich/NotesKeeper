using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NotesKeeper.Common.Models;

namespace NotesKeeper.DataAccess
{
    public class UserDataManager : IRepository
    {
        private readonly User _currentUser;

        private NotesKeeperContext _userContext;

        public UserDataManager(User user)
        {
            this._currentUser = user;
        }

        public void ConnectToDb()
        {
            // TODO: add dependency injection
            this._userContext = new NotesKeeperContext(this._currentUser.ConnectionString);
        }

        public async Task<BaseItem> AddAsync(BaseItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Argument can't be null.");
            }
            
            BaseItem createdItem = null;

            switch (item)
            {
                case Note note:
                    var noteEntity = await this._userContext.Notes.AddAsync(note).ConfigureAwait(false);
                    createdItem = noteEntity.Entity;
                    break;
                case Tag tag:
                    var tagEntity = await this._userContext.Tags.AddAsync(tag).ConfigureAwait(false);
                    createdItem = tagEntity.Entity;
                    break;
                case Group group:
                    var groupEntity = await this._userContext.Groups.AddAsync(group).ConfigureAwait(false);
                    createdItem = groupEntity.Entity;
                    break;
            }

            await this._userContext.SaveChangesAsync(true).ConfigureAwait(false);

            return createdItem;
        }

        public ConfiguredTaskAwaitable<bool> DeleteAsync(BaseItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Argument can't be null.");
            }

            return (new Task<bool>(() =>
            {
                try
                {
                    switch (item)
                    {
                        case Note note:
                            var resNote = this._userContext.Notes.Find(note.Id);
                            resNote.DeletionDate = DateTime.Now;
                            resNote.IsDeleted = true;
                            this._userContext.Notes.Update(resNote);
                            break;
                        case Tag tag:
                            var resTag = this._userContext.Tags.Find(tag.Id);
                            resTag.DeletionDate = DateTime.Now;
                            resTag.IsDeleted = true;
                            this._userContext.Tags.Update(resTag);
                            break;
                        case Group group:
                            var resGroup = this._userContext.Groups.Find(group.Id);
                            resGroup.DeletionDate = DateTime.Now;
                            resGroup.IsDeleted = true;
                            this._userContext.Groups.Update(resGroup);
                            break;
                    }

                    this._userContext.SaveChanges();
                }
                catch
                {
                    return false;
                }

                return true;
            })).ConfigureAwait(false);
        }

        public ConfiguredTaskAwaitable<BaseItem> UpdateAsync(BaseItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Argument can't be null.");
            }

            return (new Task<BaseItem>(() =>
            {
                BaseItem updated = null;

                try
                {
                    switch (item)
                    {
                        case Note note:
                            updated = (this._userContext.Notes.Update(note)).Entity;
                            break;
                        case Tag tag:
                            updated = (this._userContext.Tags.Update(tag)).Entity;
                            break;
                        case Group group:
                            updated = (this._userContext.Groups.Update(group)).Entity;
                            break;
                    }

                    this._userContext.SaveChanges();
                }
                catch
                {
                    return null;
                }

                return updated;
            })).ConfigureAwait(false);
        }

        public IQueryable<BaseItem> GetQueryable<T>()
        {
            var currentType = typeof(T);

            if (currentType == typeof(Note))
            {
                return this._userContext.Notes.AsTracking();
            }

            if (currentType == typeof(Group))
            {
                return this._userContext.Groups.AsTracking();
            }

            if (currentType == typeof(Tag))
            {
                return this._userContext.Tags.AsTracking();
            }

            return null;
        }
    }
}
