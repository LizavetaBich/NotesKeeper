using Microsoft.EntityFrameworkCore;
using NotesKeeper.BusinessLayer.Interfaces;
using NotesKeeper.Common.Models;
using NotesKeeper.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesKeeper.BusinessLayer.Services
{
    public class GroupService : IGroupService
    {
        private readonly IRepository _repository;

        public GroupService(IRepository repository)
        {
            this._repository = repository;
        }

        public async Task<Group> AddAsync(Group group)
        {
            var addedGroup = await this._repository.AddAsync(group);

            return (Group)addedGroup;
        }

        public async Task Delete(Guid id)
        {
            var result = await this._repository.DeleteAsync(new Group {Id = id});

            if (!result)
            {
                throw new Exception("Deletion failed.");
            }
        }

        public Task<List<Group>> GetAllAsync()
        {
            return this._repository.GetQueryable<Group>()
                .Cast<Group>()
                .ToListAsync();
        }

        public Task<Group> GetAsync(Guid id)
        {
            return this._repository.GetQueryable<Group>()
                .Where(item => item.Id == id)
                .Cast<Group>()
                .SingleOrDefaultAsync();
        }

        public async Task<Group> UpdateAsync(Group group)
        {
            var updated = await this._repository.UpdateAsync(group);
            return (Group)updated;
        }
    }
}
