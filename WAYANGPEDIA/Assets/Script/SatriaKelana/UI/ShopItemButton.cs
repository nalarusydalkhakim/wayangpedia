using System;
using UnityEngine;
using UnityEngine.UI;

namespace SatriaKelana.UI
{
    [RequireComponent(typeof(Button))]
    public class ShopItemButton : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private BaseItem _item;
        private Button _button;

        public event Action<BaseItem> OnItemClick;

        private void Awake()
        {
            TryGetComponent(out _button);
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            OnItemClick?.Invoke(_item);
        }

        public void SetItem(BaseItem item)
        {
            _item = item;
            _image.sprite = _item.Sprite;
        }
    }
}