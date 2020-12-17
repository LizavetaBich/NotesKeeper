using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesKeeper.DataAccess.Models;

namespace NotesKeeper.DataAccess.Services.DbContexts.Configuration
{
    internal class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(n => n.Id);
            builder.HasIndex(n => new { n.Title, n.Content });
            builder.HasMany(n => n.Tags);
        }
    }
}
