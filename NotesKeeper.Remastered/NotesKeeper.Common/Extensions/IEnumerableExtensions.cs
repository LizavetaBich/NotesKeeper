using System.Collections.Generic;
using System.Linq;

namespace NotesKeeper.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> models)
        {
            return models == null || !models.Any();
        }
    }
}
