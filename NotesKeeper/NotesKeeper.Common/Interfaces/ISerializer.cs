using System;
using System.Collections.Generic;
using System.Text;

namespace NotesKeeper.Common.Interfaces
{
    public interface ISerializer<T>
    {
        string Serialize(T item);

        T Deserialize(string serializedItem);
    }
}
