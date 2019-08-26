using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using NotesKeeper.DataAccess.Models;

namespace NotesKeeper.DataAccess
{
    public class MasterDataAccess : IMasterDataAccess
    {
        private NotesKeeperMasterContext _masterContext;

        public void ConnectToMasterDb()
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

        public ConfiguredTaskAwaitable<EntityEntry<User>> AddUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("Argument can't be null.");
            }
            
            this.AddConnectionString(user);

            return this._masterContext.Users.AddAsync(user).ConfigureAwait(false);
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await this._masterContext.Users.FindAsync(id).ConfigureAwait(false);
            this._masterContext.Users.Remove(user);
        }

        public ConfiguredTaskAwaitable<User> GetUser(Guid id)
        {
            return this._masterContext.Users.FindAsync(id).ConfigureAwait(false);
        }

        public Task<User> GetUser(string email)
        {
            return new Task<User>(() =>
            {
                return this._masterContext.Users.SingleOrDefault(user => user.Email == email);
            });
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        private void AddConnectionString(User user)
        {
            user.ConnectionString = $"Server=(localdb)\\mssqllocaldb;Database=User_{user.FirstName.Replace(" ", string.Empty)}{user.LastName.Replace(" ", string.Empty)};Trusted_Connection=True;";
        }
    }
}
