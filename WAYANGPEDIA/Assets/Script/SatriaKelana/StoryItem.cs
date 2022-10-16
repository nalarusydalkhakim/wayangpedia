using System;
using UnityEngine;

namespace SatriaKelana
{
    [Serializable]
    [CreateAssetMenu(fileName = "New story item", menuName = "Item/Story", order = 0)]
    public class StoryItem : BaseItem
    {
        [SerializeField] private int _price;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _sprite;
        public override int Price => _price;
        public override string Description => _description;
        public override Sprite Sprite => _sprite;
    }
}