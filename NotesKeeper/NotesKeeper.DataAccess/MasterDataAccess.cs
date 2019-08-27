using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotesKeeper.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NotesKeeper.DataAccess
{
    public class MasterDataAccess : IRepository
    {
        private NotesKeeperMasterContext _masterContext;

        public void ConnectToDb()
        {
            // TODO: add dependency injection
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("MasterDbConfiguration");
            var config = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<NotesKeeperMasterContext>();
            var options = optionsBuilder
                .UseSqlServer(config.GetConnectionString("DefaultConnection"))
                .Options;
            this._masterContext = new NotesKeeperMasterContext(options);
        }

        public async Task<BaseItem> AddAsync(BaseItem item)
        {
            if (!(item is User))
            {
                throw new ArgumentNullException("Argument can't be null.");
            }

            BaseItem updatedItem;
            using (var transaction = await this._masterContext.Database.BeginTransactionAsync())
            {
                var createdItem = await this._masterContext.Users.AddAsync((User)item).ConfigureAwait(false);
                await this._masterContext.SaveChangesAsync(true);

                this.AddConnectionString(createdItem.Entity);

                updatedItem = this._masterContext.Users.Update(createdItem.Entity).Entity;
                await this._masterContext.SaveChangesAsync();

                transaction.Commit();
            }
            

            return updatedItem;
        }

        public ConfiguredTaskAwaitable<bool> DeleteAsync(BaseItem item)
        {
            if (!(item is User))
            {
                throw new ArgumentNullException("Argument can't be null.");
            }

            return (new Task<bool>(() =>
            {
                try
                {
                    var user = this._masterContext.Users.Find(item.Id);
                    user.DeletionDate = DateTime.Now;
                    user.IsDeleted = true;
                    this._masterContext.Users.Update(user);
                    this._masterContext.SaveChanges();
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
            if (!(item is User))
            {
                throw new ArgumentNullException("Argument can't be null.");
            }

            return (new Task<BaseItem>(() =>
            {
                var updatedItem = this._masterContext.Users.Update((User)item);
                this._masterContext.SaveChanges();
                return updatedItem.Entity;
            })).ConfigureAwait(false);
        }

        public ConfiguredTaskAwaitable<IEnumerable<BaseItem>> GetAsync(Func<BaseItem,bool> filter)
        {
            return (new Task<IEnumerable<BaseItem>>(() => this._masterContext.Users.Where(user => filter(user))))
                .ConfigureAwait(false);
        }

        private void AddConnectionString(User user)
        {
            user.ConnectionString = $"Server=(localdb)\\mssqllocaldb;Database=User_{user.Id};Trusted_Connection=True;";
        }
    }
}
