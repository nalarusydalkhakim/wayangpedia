using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

namespace SatriaKelana
{
    public class Garden : MonoBehaviour, IPersistent
    {
        [SerializeField] private Area[] _areas;
        [SerializeField] private SaveManager _saveManager;
        [SerializeField] private GameObject _coin;
        [SerializeField] private RectTransform _coinBar;
        [SerializeField] private PlantStorage _storage;

        public void Load()
        {
            var success = _saveManager.BinaryLoad<List<AreaRecord>>(name, out var states);
            if (!success) return;
            foreach (var state in states)
            {
                var plant = _storage.Get(state.PlantIndex);
                if (plant == null) continue;
                
                var area = _areas[state.Index];
                var constraint = state.Constraint;
                area.Load(plant, constraint);
            }

            foreach (var area in _areas)
            {
                area.OnCollect += OnCollect;
                area.OnPickPlant += OnPickPlant;
            }
        }

        private void OnPickPlant(Area area)
        {
            area.SetPlant(_storage.Get(0));
        }

        private void OnCollect(Area area)
        {
            var position = Camera.main.WorldToScreenPoint(area.transform.position);
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
            var records = _areas.Select((a, i) => new AreaRecord
            {
                Index = i,
                Constraint = a.CurrentConstraint,
                PlantIndex = _storage.Plants.IndexOf(a.Plant)
            }).ToList();
            _saveManager.BinarySave(records, name);
        }

        private void Awake()
        {
            _saveManager.Register(this);
            Load();
        }
    }
}