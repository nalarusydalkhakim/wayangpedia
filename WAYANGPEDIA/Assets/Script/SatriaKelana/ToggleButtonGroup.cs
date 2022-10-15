using System.Collections.Generic;
using UnityEngine;

namespace SatriaKelana
{
    public class ToggleButtonGroup : MonoBehaviour
    {
        [SerializeField] List<ToggleButton> _toggles = new List<ToggleButton>();
        ToggleButton _activeToggle = null;

        public List<ToggleButton> Toggles => _toggles;

        void Awake()
        {
            foreach (var toggle in _toggles)
            {
                toggle.OnActivated += OnActivated;
                if (toggle.Active)
                {
                    _activeToggle = toggle;
                }
            }
        }

        void OnActivated(ToggleButton toggle)
        {
            _activeToggle.SetState(false);
            _activeToggle = toggle;
        }
    }
}