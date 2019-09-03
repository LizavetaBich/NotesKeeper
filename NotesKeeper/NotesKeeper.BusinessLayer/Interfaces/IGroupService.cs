using NotesKeeper.Common.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NotesKeeper.BusinessLayer.Interfaces
{
    public interface IGroupService
    {
        Task<Group> AddAsync(Group group);

        Task<Group> GetAsync(Guid id);

        Task<List<Common.Models.Group>> GetAllAsync();

        Task<Group> UpdateAsync(Group group);

        Task Delete(Guid id);
    }
}
