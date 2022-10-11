using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SatriaKelana
{
    public class Garden : MonoBehaviour, IPersistent
    {
        [SerializeField] Area[] _plants;
        [SerializeField] SaveManager _saveManager;
        [SerializeField] GameObject _coin;
        [SerializeField] RectTransform _coinBar;

        public void Load()
        {
            var success = _saveManager.BinaryLoad<List<Area.TimeConstraint>>(name, out var states);
            if (!success) return;
            for (int i = 0; i < _plants.Length; i++)
            {
                var plant = _plants[i];
                var state = states[i];
                plant.Load(state);
                plant.OnCollect += OnCollect;
            }
        }
        
        public void OnCollect(Area plant)
        {
            var position = Camera.main.WorldToScreenPoint(plant.transform.position);
            var coin = Instantiate(_coin, position, Quaternion.identity);
            coin.transform.SetParent(_coinBar.root);
            coin.transform.localScale = Vector3.one * 0.5f;
            coin
                .LeanMove(_coinBar.position, .5f)
                .setEaseInOutSine()
                .setOnComplete(() => Destroy(coin));
            coin
                .LeanScale(Vector3.one, .25f)
                .setLoopPingPong();
        }

        public void Save()
        {
            var states = _plants.Select(p => p.CurrentConstraint).ToList();
            _saveManager.BinarySave(states, name);
        }

        void Awake()
        {
            _saveManager.Register(this);
            Load();
        }
    }
}