using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NotesKeeper.Common.Models;

namespace NotesKeeper.WebApi.IdentityModels
{
    public class NotesKeeperMasterContext : IdentityDbContext<User>
    {
    }
}
