using System.Collections.Generic;
using UnityEngine;

namespace SatriaKelana
{
    public abstract class BaseItemStorage : ScriptableObject
    {
        [SerializeField] protected List<Item> _items;
        public virtual IList<Item> Items => _items;

        public virtual Item Get(int index)
        {
            if (index < 0 || index >= _items.Count) return null;
            return _items[index];
        }
    }
}