using UnityEngine;

namespace SatriaKelana
{
    [CreateAssetMenu(fileName = "New item", menuName = "Item/Item")]
    public class Item : BaseItem
    {
        [SerializeField] private int _price;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _sprite;
        public override int Price => _price;
        public override string Description => _description;
        public override Sprite Sprite => _sprite;
    }
}