using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesKeeper.BusinessLayer.Interfaces;
using NotesKeeper.Common.Models;
using NotesKeeper.DataAccess;

namespace NotesKeeper.BusinessLayer.Services
{
    public class NoteService : INoteService
    {
        private readonly IRepository _repository;

        public NoteService(IRepository repository)
        {
            this._repository = repository;
        }

        public async Task<Note> AddAsync(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException();
            }

            var addedNote = await this._repository.AddAsync(note);

            return (Note)addedNote;
        }

        public Task<Note> GetAsync(Guid id)
        {
            return this._repository.GetQueryable<Note>()
                .Where(item => item.Id == id)
                .Cast<Note>()
                .SingleOrDefaultAsync();
        }

        public async Task<Note> UpdateAsync(Note note)
        {
            var updated = await this._repository.UpdateAsync(note);
            return (Note)updated;
        }

        public async Task Delete(Guid id)
        {
            var result = await this._repository.DeleteAsync(new Note { Id = id });

            if (!result)
            {
                throw new Exception("Deletion failed.");
            }
        }
    }
}