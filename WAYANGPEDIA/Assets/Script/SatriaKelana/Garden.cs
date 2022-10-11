using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SatriaKelana
{
    public class Garden : MonoBehaviour, IPersistent
    {
        [SerializeField] Plant[] _plants;
        [SerializeField] SaveManager _saveManager;

        public void Load()
        {
            var states = _saveManager.BinaryLoad<List<Plant.State>>(name);
            for (int i = 0; i < _plants.Length; i++)
            {
                var plant = _plants[i];
                var state = states[i];
                plant.Load(state);
            }
        }

        public void Save()
        {
            var states = _plants.Select(p => p.CurrentState).ToList();
            _saveManager.BinarySave(states, name);
        }

        void Awake()
        {
            Load();
        }
    }
}