using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SatriaKelana
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] GameObject _activatedGO;
        [SerializeField] GameObject _deactivatedGO;
        [SerializeField] UnityEvent<bool> _onToggled;
        [SerializeField] bool _activated = false;

        public bool Valid => _activatedGO != null && _deactivatedGO != null;
        public bool Active => _activated;
        public event Action<ToggleButton> OnActivated;
        public event Action<bool> OnToggled;

        Toggle _toggle;

        void Awake()
        {
            TryGetComponent(out _toggle);
            _toggle.onValueChanged.AddListener(OnValueChanged);
            _toggle.isOn = _activated;
        }

        void OnValueChanged(bool value)
        {
            SetState(value);
        }

        public void SetState(bool activated)
        {
            _activatedGO.SetActive(activated);
            _deactivatedGO.SetActive(!activated);
            _onToggled?.Invoke(activated);
            OnToggled?.Invoke(activated);
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