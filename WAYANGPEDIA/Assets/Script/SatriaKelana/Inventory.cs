using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SatriaKelana
{
    [CreateAssetMenu(fileName = "New inventory", menuName = "Storage/Inventory", order = 0)]
    public class Inventory : ScriptableObject, IPersistent
    {
        [SerializeField] private List<Item> _items = new();
        [SerializeField] private ItemStorage _storage;
        private SaveManager _saveManager;

        public IReadOnlyList<Item> Items => _items;

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