using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotesKeeper.DataAccess.Models;
using System;
using System.Security.Cryptography.X509Certificates;

namespace NotesKeeper.DataAccess.Services.DbContexts.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => new { x.Id, x.UserId });
        }
    }
}
