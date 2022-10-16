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
            public BaseItem Item { get; set; }
            public int Stack { get; set; }
        }

        [SerializeField] private ItemStorage _storage;
        [SerializeField] private SaveManager _saveManager;
        public List<ItemData> Items { get; } = new();

        public BaseItem Get(int index)
        {
            if (index < 0 || index >= Items.Count) return null;
            return Items[index].Item;
        }

        public void Init()
        {
            Items.Clear();
            _saveManager.Register(this);
            Load();
        }

        public void Add(BaseItem item)
        {
            var existing = Items.FirstOrDefault(i => i.Item == item);
            if (existing != null)
            {
                existing.Stack++;
                return;
            }
            Items.Add(new ItemData
            {
                Id = _storage.Items.IndexOf(item),
                Item = item,
                Stack = 1
            });
        }

        public void Save()
        {
            _saveManager.BinarySave(Items, "Inventory");
        }

        public void Load()
        {
            if (!_saveManager.BinaryLoad("Inventory", out List<ItemData> items))
            {
                return;
            }

            foreach (var item in items)
            {
                Items.Add(new ItemData()
                {
                    Id = item.Id,
                    Item = _storage.Get(item.Id),
                    Stack = item.Stack
                });
            }
        }
    }
}