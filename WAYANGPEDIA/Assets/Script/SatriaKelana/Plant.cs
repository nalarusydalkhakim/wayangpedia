using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SatriaKelana
{
    [CreateAssetMenu(menuName = "Item/Plant", fileName = "New plant")]
    public class Plant : Item
    {
        [SerializeField] private string _name;
        [SerializeField] private List<Sprite> _sprites = new List<Sprite>();
        [SerializeField] private int _duration;
        [SerializeField] private int _harvestCoin;
        [SerializeField] private Sprite _seedSprite;
        [SerializeField] private int _price;
        [SerializeField] private string _description;

        public string Name => _name;
        public IReadOnlyList<Sprite> Sprites => _sprites;
        public int Duration => _duration;
        public int HarvestCoin => _harvestCoin;
        public Sprite SeedSprite => _seedSprite;
        public override int Price => _price;
        public override string Description => _description;
        public override Sprite Sprite => _seedSprite;
    }
}