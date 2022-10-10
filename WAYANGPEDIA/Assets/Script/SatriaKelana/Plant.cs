using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Plant : MonoBehaviour
{
    [Serializable]
    struct Stage
    {
        [SerializeField] string _name;
        [SerializeField] Sprite _sprite;
        [SerializeField] float _duration;
        public string Name => _name;
        public Sprite Sprite => _sprite;
        public float Duration => _duration;
    }

    [SerializeField] List<Stage> _stages = new List<Stage>();
    SpriteRenderer _renderer;
    
    // Start stage from the beginning
    int _stageIndex = 0;

    private void Awake()
    {
        TryGetComponent(out _renderer);
        // Load state
        // If there are no state, then start from the beginning
        // Set targetted time to current time + duration
    }

    private void Update()
    {
        // Update progress
        // If current time is higher than targeted time, then
        // go to the next stage
        // If reached the last stage, then
        // Wait for user click
        // And when the user click, start a coin collection animation
        // Increase user's coin
    }
}
