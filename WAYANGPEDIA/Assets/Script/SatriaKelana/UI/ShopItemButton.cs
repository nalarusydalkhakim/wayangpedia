using System;
using UnityEngine;
using UnityEngine.UI;

namespace SatriaKelana.UI
{
    [RequireComponent(typeof(Button))]
    public class ShopItemButton : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private Item _item;
        private Button _button;

        public event Action<Item> OnItemClick;

        private void Awake()
        {
            TryGetComponent(out _button);
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            OnItemClick?.Invoke(_item);
        }

        public void SetItem(Item item)
        {
            _item = item;
            _image.sprite = _item.Sprite;
        }
    }
}