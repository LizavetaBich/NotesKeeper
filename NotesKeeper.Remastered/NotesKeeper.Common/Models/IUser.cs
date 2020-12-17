using System;

namespace NotesKeeper.Common.Models
{
    public interface IUser
    {
        Guid Id { get; set; }

        string UserId { get; set; }

        string Email { get; set; }

        string PasswordHash { get; set; }
    }
}
