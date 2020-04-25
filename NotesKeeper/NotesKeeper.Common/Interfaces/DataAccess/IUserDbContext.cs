using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace NotesKeeper.Common.Interfaces.DataAccess
{
    public interface IUserDbContext
    {
        DbSet<CustomEvent> Events { get; set; }

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
