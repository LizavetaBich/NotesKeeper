using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common.Models.AccountModels
{
    public class RefreshToken : BaseModel
    {
        public string Token { get; set; }

        public DateTime ExpirationTime { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
