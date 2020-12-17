using NotesKeeper.Common.Models;
using System.Collections.Generic;

namespace NotesKeeper.Common.Comparers
{
    public class UserEqualityComparer : IEqualityComparer<IUser>
    {
        public bool Equals(IUser x, IUser y)
        {
            return x!= null && y != null && x.Id == y.Id && x.UserId == y.UserId;
        }

        public int GetHashCode(IUser obj)
        {
            return obj == null ? 0 : obj.Id.GetHashCode();
        }
    }
}
