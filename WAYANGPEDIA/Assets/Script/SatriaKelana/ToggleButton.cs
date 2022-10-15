using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SatriaKelana
{
    [RequireComponent(typeof(Button))]
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] GameObject _activatedGO;
        [SerializeField] GameObject _deactivatedGO;
        [SerializeField] UnityEvent<bool> _onToggled;
        [SerializeField] bool _activated = false;

        public bool Valid => _activatedGO != null && _deactivatedGO != null;
        public bool Active => _activated;
        public UnityEvent<bool> OnToggled => _onToggled;
        public event Action<ToggleButton> OnActivated;

        Button _button;

        void Awake()
        {
            TryGetComponent(out _button);
            _button.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            Toggle();
        }

        private void Toggle()
        {
            _activated = !_activated;
            SetState(_activated);
        }

        public void SetState(bool activated)
        {
            _activatedGO.SetActive(activated);
            _deactivatedGO.SetActive(!activated);
            _onToggled?.Invoke(activated);
            if (activated)
            {
                OnActivated?.Invoke(this);
            }
        }

        void OnValidate()
        {
            if (!Valid) return;
            SetState(_activated);
            EditorUtility.SetDirty(this);
        }
    }
}