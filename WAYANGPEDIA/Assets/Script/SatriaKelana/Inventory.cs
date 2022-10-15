using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SatriaKelana
{
    [CreateAssetMenu(fileName = "New inventory", menuName = "Storage/Inventory", order = 0)]
    public class Inventory : ScriptableObject, IPersistent, IItemStorage
    {
        private readonly List<Item> _items = new();
        [SerializeField] private ItemStorage _storage;
        private SaveManager _saveManager;
        IList<Item> IItemStorage.Items => _items;

        public IReadOnlyList<Item> Items => _items;

        public Item Get(int index)
        {
            if (index < 0 || index >= _items.Count) return null;
            return _items[index];
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
            _items.Add(item);
        }

        public void Save()
        {
            var items = _items.Select(i => _storage.Items.IndexOf(i)).ToList();
            _saveManager.BinarySave(items, "Inventory");
        }

        public void Load()
        {
            if (!_saveManager.BinaryLoad("Inventory", out List<int> itemIds))
            {
                return;
            }

            var length = _storage.Items.Count;
            var validIds = itemIds.Where(i => i >= 0 && i < length);
            foreach (var id in validIds)
            {
                _items.Add(_storage.Items[id]);
            }
        }
    }
}