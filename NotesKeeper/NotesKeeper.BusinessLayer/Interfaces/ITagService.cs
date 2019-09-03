using NotesKeeper.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotesKeeper.BusinessLayer.Interfaces
{
    public interface ITagService
    {
        Task<Tag> AddAsync(Tag tag);

        Task<Tag> GetAsync(Guid id);

        Task<List<Tag>> GetAsync(string text);

        Task<Tag> UpdateAsync(Tag note);

        Task Delete(Guid id);
    }
}
