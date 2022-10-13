using System.Collections.Generic;
using UnityEngine;

namespace SatriaKelana
{
    [CreateAssetMenu(menuName = "Storage/Plant", fileName = "New plant storage")]
    public class PlantStorage : ScriptableObject
    {
        [SerializeField] private List<Plant> _plants = new();

        public IReadOnlyList<Plant> Plants => _plants;

        public Plant Get(int index)
        {
            if (index < 0 || index >= _plants.Count) return null;
            return _plants[index];
        }
    }
}