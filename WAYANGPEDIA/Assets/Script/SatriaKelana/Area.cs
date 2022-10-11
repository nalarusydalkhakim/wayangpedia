using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SatriaKelana
{

    [RequireComponent(typeof(SpriteRenderer))]
    public class Area : MonoBehaviour
    {

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Plant _plant = null;

        [Serializable]
        public class TimeConstraint
        {
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
        }

        public enum State
        {
            Idle,
            Growing,
            Ripening
        }

        public TimeConstraint CurrentConstraint { get; private set; }
        public bool Done { get; private set; }
        public event Action<Area> OnCollect;
        public event Action<Area> OnPickPlant;
        private bool _loaded = false;
        private State _state = State.Idle;

        private void Start()
        {
            if (_loaded) return;
            ResetState();
        }

        private void Update()
        {
            switch (_state)
            {
                case State.Idle:
                    HandleIdle();
                    break;
                case State.Growing:
                    HandleGrowing();
                    break;
                case State.Ripening:
                    HandleRipening();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleRipening()
        {
            if (_plant == null) return;
            var ripeningSprite = _plant.Sprites[^1];
            _renderer.sprite = ripeningSprite;
        }

        private void HandleGrowing()
        {
            if (_plant == null) return;
            var length = _plant.Sprites.Count;
            var percentage = 1d / length;
            var duration = _plant.Duration;
        }

        private void HandleIdle()
        {
            _renderer.sprite = null;
        }

        private void OnMouseDown()
        {
            switch (_state)
            {
                case State.Idle:
                    OnPickPlant?.Invoke(this);
                    break;
                case State.Ripening:
                    Collect();
                    break;
            }
        }
        
        public void Collect()
        {
            ResetState();
            OnCollect?.Invoke(this);
        }

        private void ResetState()
        {
            // var start = DateTime.Now;
            // var end = start.AddSeconds(_totalDuration);
            // CurrentConstraint = new TimeConstraint
            // {
            //     StartTime = start,
            //     EndTime = end
            // };
            // Done = false;
        }

        public void Load(TimeConstraint timeConstraint)
        {
            // CurrentConstraint = timeConstraint;
            // _loaded = true;
        }
    }
}