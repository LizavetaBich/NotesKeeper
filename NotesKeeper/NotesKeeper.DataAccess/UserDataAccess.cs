using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NotesKeeper.Common.Models;

namespace NotesKeeper.DataAccess
{
    public class UserDataAccess : IRepository
    {
        private readonly User _currentUser;

        private NotesKeeperContext _userContext;

        public UserDataAccess(User user)
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
                            this._userContext.Notes.Update(note);
                            break;
                        case Tag tag:
                            var resTag = this._userContext.Tags.Find(tag.Id);
                            resTag.DeletionDate = DateTime.Now;
                            resTag.IsDeleted = true;
                            this._userContext.Tags.Update(tag);
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
            throw new NotImplementedException();
        }

        public ConfiguredTaskAwaitable<IEnumerable<BaseItem>> GetAsync(Func<BaseItem, bool> filter)
        {
            throw new NotImplementedException();
        }
    }
}
