using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesKeeper.DataAccess.Models;

namespace NotesKeeper.DataAccess.Services.DbContexts.Configuration
{
    internal class CalendarEventConfiguration : IEntityTypeConfiguration<CalendarEvent>
    {
        public void Configure(EntityTypeBuilder<CalendarEvent> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.Tags);
            builder.HasMany(e => e.EventDateTimes).WithOne(x => x.Event);
        }
    }
}
