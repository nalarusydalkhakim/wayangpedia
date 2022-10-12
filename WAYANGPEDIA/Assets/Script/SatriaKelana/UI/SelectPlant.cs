using System;
using UnityEngine;
using SatriaKelana;
using TMPro;
using UnityEngine.UI;

namespace SatriaKelana.UI
{
    public class SelectPlant : MonoBehaviour
    {
        [SerializeField] private PlantStorage _storage;
        [SerializeField] private Image _seedImage;
        [SerializeField] private TextMeshProUGUI _seedTitle;
        [SerializeField] private Button _next, _previous, _select, _close;

        private Plant _selectedPlant;
        private int _index;

        public Plant SelectedPlant => _selectedPlant;
        public event Action<SelectPlant> OnSelect;

        private void Awake()
        {
            SetPlant(0);
            _next.onClick.AddListener(OnNext);
            _previous.onClick.AddListener(OnPrevious);
            _select.onClick.AddListener(OnSelectClick);
            _close.onClick.AddListener(Close);
        }

        private void OnSelectClick()
        {
            Close();
            OnSelect?.Invoke(this);
        }

        private void OnPrevious()
        {
            _index--;
            var sequence = LeanTween.sequence();
            sequence.append(_seedImage.transform.LeanMoveX(_previous.transform.position.x, .25f));
            sequence.append(() => _seedImage.transform.position = _next.transform.position);
            sequence.append(_seedImage.transform.LeanMoveLocal(Vector3.zero, .25f));
            var scaleSequence = LeanTween.sequence();
            scaleSequence.append(_seedImage.transform.LeanScale(Vector3.one * .5f, .25f));
            scaleSequence.append(_seedImage.transform.LeanScale(Vector3.one, .25f));
        }

        private void OnNext()
        {
            _index++;
        }

        private void SetPlant(int index)
        {
            _index = index;
            var plant = _storage.Get(index);
            if (plant == null) return;
            _seedImage.sprite = plant.SeedSprite;
            _seedTitle.text = plant.Name;
            _selectedPlant = plant;
        }

        public void Show()
        {
            var rectTransform = transform as RectTransform;
            if (rectTransform == null)
            {
                Debug.LogWarning("Select plant isn't rect transform");
                return;
            }
            gameObject.SetActive(true);
            transform.position += Vector3.down * rectTransform.rect.height;
            transform.LeanMoveLocal(Vector3.zero, .25f);
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }
    }
}