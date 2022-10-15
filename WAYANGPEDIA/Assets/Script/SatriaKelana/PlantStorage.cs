using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SatriaKelana
{
    [CreateAssetMenu(menuName = "Storage/Plant", fileName = "New plant storage")]
    public class PlantStorage : ScriptableObject, IItemStorage
    {
        [SerializeField] private List<Plant> _plants = new();

        public IReadOnlyList<Plant> Plants => _plants;

        public IList<Item> Items => _plants.Cast<Item>().ToList();
        Item IItemStorage.Get(int index)
        {
            return Get(index);
        }

        public Plant Get(int index)
        {
            if (index < 0 || index >= _plants.Count) return null;
            return _plants[index];
        }
    }
}