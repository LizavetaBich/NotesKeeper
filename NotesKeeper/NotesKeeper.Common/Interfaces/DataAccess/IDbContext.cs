using Microsoft.EntityFrameworkCore;
using NotesKeeper.Common.Models;
using NotesKeeper.Common.Models.AccountModels;
using System.Threading;
using System.Threading.Tasks;

namespace NotesKeeper.Common.Interfaces.DataAccess
{
    public interface IDbContext
    {
        DbSet<ApplicationUser> Users { get; set; }

        DbSet<RefreshToken> RefreshTokens { get; set; }

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
