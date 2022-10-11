using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SatriaKelana
{
    [CreateAssetMenu(menuName = "Garden/Plant", fileName = "New plant")]
    public class Plant : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private List<Sprite> _sprites = new List<Sprite>();
        [SerializeField] private int _duration;

        public string Name => _name;
        public IReadOnlyList<Sprite> Sprites => _sprites;
        public int Duration => _duration;
    }
}