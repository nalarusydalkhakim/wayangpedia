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
        [SerializeField] private Selector _selector;
        [SerializeField] private CoinManager _coinManager;

        private void Awake()
        {
            foreach (var item in _storage.Items)
            {
                var button = Instantiate(_itemButton, _itemContainer.transform);
                button.SetItem(item);
                button.OnItemClick += OnItemClick;
            }

            _selector.OnSelect += OnItemSelect;
        }

        private void OnItemClick(BaseItem item)
        {
            _selector.Show(_storage.Items, "Beli", _storage.Items.IndexOf(item),true);
        }

        private void OnItemSelect(Selector selector)
        {
            if (_coinManager.Coin < selector.SelectedItem.Price)
            {
                Debug.Log("Not enough money");
                return;
            }
            _coinManager.Subtract(selector.SelectedItem.Price);
            _inventory.Add(selector.SelectedItem);
        }
    }
}