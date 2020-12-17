using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesKeeper.DataAccess.Models;
using System;

namespace NotesKeeper.DataAccess.Services.DbContexts.Configuration
{
    internal class EventDateTimeConfiguration : IEntityTypeConfiguration<EventDateTime>
    {
        public void Configure(EntityTypeBuilder<EventDateTime> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
