using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SatriaKelana
{
    [CreateAssetMenu(fileName = "New inventory", menuName = "Storage/Inventory", order = 0)]
    public class Inventory : ScriptableObject, IPersistent
    {
        [Serializable]
        public class ItemData
        {
            public int Id { get; set; }
            [field: NonSerialized]
            public Item Item { get; set; }
            public int Stack { get; set; }
        }
        
        private readonly List<ItemData> _items = new();
        [SerializeField] private ItemStorage _storage;
        private SaveManager _saveManager;

        public Item Get(int index)
        {
            if (index < 0 || index >= _items.Count) return null;
            return _items[index].Item;
        }

        public void Init(SaveManager saveManager)
        {
            _items.Clear();
            _saveManager = saveManager;
            _saveManager.Register(this);
            Load();
        }

        public void Add(Item item)
        {
            var existing = _items.FirstOrDefault(i => i.Item == item);
            if (existing != null)
            {
                existing.Stack++;
                return;
            }
            _items.Add(new ItemData
            {
                Id = _storage.Items.IndexOf(item),
                Item = item,
                Stack = 1
            });
        }

        public void Save()
        {
            _saveManager.BinarySave(_items, "Inventory");
        }

        public void Load()
        {
            if (!_saveManager.BinaryLoad("Inventory", out List<ItemData> items))
            {
                return;
            }

            foreach (var item in items)
            {
                _items.Add(new ItemData()
                {
                    Id = item.Id,
                    Item = _storage.Get(item.Id),
                    Stack = item.Stack
                });
            }
        }
    }
}