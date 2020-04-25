using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common.Models
{
    public class UserConfig : BaseModel
    {
        public UserConfig(Guid id,
            Guid userId,
            int yearsForward,
            int yearsBehind)
        {
            UserId = userId;
            YearsForward = yearsForward;
            YearsBehind = yearsBehind;
        }

        public Guid UserId { get; }

        public int YearsForward { get; }

        public int YearsBehind { get; }
    }
}
