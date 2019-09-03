using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesKeeper.BusinessLayer.Interfaces;
using NotesKeeper.Common.Models;
using NotesKeeper.DataAccess;

namespace NotesKeeper.BusinessLayer.Services
{
    public class TagService : ITagService
    {
        private readonly IRepository _repository;

        public TagService(IRepository repository)
        {
            this._repository = repository;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            var addedGroup = await this._repository.AddAsync(tag);

            return (Tag)addedGroup;
        }

        public async Task Delete(Guid id)
        {
            var result = await this._repository.DeleteAsync(new Tag { Id = id });

            if (!result)
            {
                throw new Exception("Deletion failed.");
            }
        }

        public Task<Tag> GetAsync(Guid id)
        {
            return this._repository.GetQueryable<Tag>()
                .Where(item => item.Id == id)
                .Cast<Tag>()
                .SingleOrDefaultAsync();
        }

        public Task<List<Tag>> GetAsync(string text)
        {
            return this._repository.GetQueryable<Tag>()
                .Where(item => ((Tag)item).Content == text)
                .Cast<Tag>()
                .ToListAsync();
        }

        public async Task<Tag> UpdateAsync(Tag note)
        {
            var updated = await this._repository.UpdateAsync(note);
            return (Tag)updated;
        }
    }
}