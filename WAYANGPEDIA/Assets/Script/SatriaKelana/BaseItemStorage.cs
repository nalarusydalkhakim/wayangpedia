using System.Collections.Generic;
using UnityEngine;

namespace SatriaKelana
{
    public abstract class BaseItemStorage : ScriptableObject
    {
        [SerializeField] protected List<BaseItem> _items;
        public virtual IList<BaseItem> Items => _items;

        public virtual BaseItem Get(int index)
        {
            if (index < 0 || index >= _items.Count) return null;
            return _items[index];
        }
    }
}