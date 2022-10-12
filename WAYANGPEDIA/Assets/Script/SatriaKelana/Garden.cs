using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using SatriaKelana.UI;
using Unity.VisualScripting;
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
        [SerializeField] private SelectPlant _selectPlant;

        private Area _selectedArea;

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
            _selectedArea = area;
            _selectPlant.Show();
        }

        private void OnCollect(Area area)
        {
            var position = Camera.main.WorldToScreenPoint(area.transform.position);
            var coin = Instantiate(_coin, position, Quaternion.identity);
            coin.transform.SetParent(_coinBar.root);
            coin.transform.localScale = Vector3.one * 0.5f;
            coin.transform
                .DOMove(_coinBar.position, .5f)
                .SetEase(Ease.InOutSine)
                .OnComplete(() => Destroy(coin));
            coin.transform
                .DOScale(Vector3.one, .25f)
                .SetLoops(2, LoopType.Yoyo);
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
            _selectPlant.OnSelect += OnSelect;
        }

        private void OnSelect(SelectPlant select)
        {
            var plant = select.SelectedPlant;
            _selectedArea.SetPlant(plant);
        }
    }
}