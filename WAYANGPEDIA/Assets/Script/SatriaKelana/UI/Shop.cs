using System;
using UnityEngine;

namespace SatriaKelana.UI
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ItemStorage _storage;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private ShopItemButton _itemButton;
        [SerializeField] private GameObject _itemContainer;
        [SerializeField] private SaveManager _saveManager;
        [SerializeField] private Selector _selector;

        private void Awake()
        {
            _inventory.Init(_saveManager);
            foreach (var item in _storage.Items)
            {
                var button = Instantiate(_itemButton, _itemContainer.transform);
                button.SetItem(item);
                button.OnItemClick += OnItemClick;
            }
        }

        private void OnItemClick(Item item)
        {
            _selector.Show(_storage.Items, "Beli", _storage.Items.IndexOf(item),true);
        }
    }
}