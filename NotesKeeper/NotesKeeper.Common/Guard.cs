using System;

namespace NotesKeeper.Common
{
    public static class Guard
    {
        public static void IsNotNull<T>(T item) where T: class
        {
            if (item == null)
            {
                throw new ArgumentNullException($"Variable of type {typeof(T).Name} couldn't bew NULL!");
            }
        }
    }
}
