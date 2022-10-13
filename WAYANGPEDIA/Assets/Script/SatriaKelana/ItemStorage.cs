using System.Collections.Generic;
using UnityEngine;

namespace SatriaKelana
{
    [CreateAssetMenu(fileName = "New item storage", menuName = "Storage/Item", order = 0)]
    public class ItemStorage : ScriptableObject
    {
        [SerializeField] private List<Item> _items;
        public List<Item> Items => _items;
    }
}