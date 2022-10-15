using System.Collections.Generic;
using UnityEngine;

namespace SatriaKelana
{
    [CreateAssetMenu(fileName = "New item storage", menuName = "Storage/Item", order = 0)]
    public class ItemStorage : ScriptableObject, IItemStorage
    {
        [SerializeField] private List<Item> _items;
        public IList<Item> Items => _items;

        public Item Get(int index)
        {
            if (index < 0 || index >= _items.Count) return null;
            return _items[index];
        }
    }
}