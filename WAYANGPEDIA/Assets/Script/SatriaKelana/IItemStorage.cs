using System.Collections.Generic;

namespace SatriaKelana
{
    public interface IItemStorage
    {
        IList<Item> Items { get; }
        Item Get(int index);
    }
}