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
        public Plant Plant => _plant;
        public event Action<Area> OnCollect;
        public event Action<Area> OnPickPlant;
        private State _state = State.Idle;

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
            var length = _plant.Sprites.Count - 1;
            var percentage = 1d / length;
            var duration = _plant.Duration;
            var currentDuration = (DateTime.Now - CurrentConstraint.StartTime).TotalSeconds;
            var currentPercentage = currentDuration / duration;
            var index = Mathf.Clamp((int)Math.Floor(currentPercentage / percentage), 0, length - 1);
            _renderer.sprite = _plant.Sprites[index];
            if (currentDuration >= duration)
            {
                _state = State.Ripening;
            }
        }

        private void HandleIdle()
        {
            _plant = null;
            _renderer.sprite = null;
            CurrentConstraint = null;
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
                case State.Growing:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Collect()
        {
            if (_state != State.Ripening) return;
            ResetState();
            OnCollect?.Invoke(this);
        }

        private void ResetState()
        {
            _state = State.Idle;
            // var start = DateTime.Now;
            // var end = start.AddSeconds(_totalDuration);
            // CurrentConstraint = new TimeConstraint
            // {
            //     StartTime = start,
            //     EndTime = end
            // };
            // Done = false;
        }

        public void SetPlant(Plant plant)
        {
            _plant = plant;
            CurrentConstraint = new TimeConstraint()
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddSeconds(_plant.Duration)
            };
            _state = State.Growing;
        }

        public void Load(Plant plant, TimeConstraint timeConstraint)
        {
            _plant = plant;
            CurrentConstraint = timeConstraint;
            _state = State.Growing;
        }
    }
}