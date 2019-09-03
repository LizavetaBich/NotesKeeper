using NotesKeeper.Common.Models;
using System;
using System.Threading.Tasks;

namespace NotesKeeper.BusinessLayer.Interfaces
{
    public interface INoteService
    {
        Task<Note> AddAsync(Note note);

        Task<Note> GetAsync(Guid id);

        Task<Note> UpdateAsync(Note note);

        Task Delete(Guid id);
    }
}
