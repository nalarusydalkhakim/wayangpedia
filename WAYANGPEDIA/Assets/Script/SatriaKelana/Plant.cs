using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SatriaKelana
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class Plant : MonoBehaviour
    {
        [Serializable]
        struct Stage
        {
            [SerializeField] string _name;
            [SerializeField] Sprite _sprite;
            [SerializeField] int _duration;
            public string Name => _name;
            public Sprite Sprite => _sprite;
            public int Duration => _duration;
        }

        [SerializeField] SpriteRenderer _renderer;
        [SerializeField] List<Stage> _stages = new List<Stage>();

        [Serializable]
        public class State
        {
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
        }

        public State CurrentState { get; private set; }
        public bool Done { get; private set; }
        bool _loaded = false;

        private void Start()
        {
            if (_loaded) return;
            var start = DateTime.Now;
            var end = start.AddSeconds(_stages.Sum(s => s.Duration));
            CurrentState = new State
            {
                StartTime = start,
                EndTime = end
            };
        }

        private void Update()
        {
            var currentDuration = (DateTime.Now - CurrentState.StartTime).TotalSeconds;
            var countTime = 0;
            foreach (var stage in _stages)
            {
                countTime += stage.Duration;
                if (currentDuration <= countTime)
                {
                    _renderer.sprite = stage.Sprite;
                    break;
                }
            }
            Done = currentDuration >= countTime;
            // If reached the last stage, then
            // Wait for user click
            // And when the user click, start a coin collection animation
            // Increase user's coin
        }
        
        public void Load(State state)
        {
            CurrentState = state;
            _loaded = true;
        }
    }
}
