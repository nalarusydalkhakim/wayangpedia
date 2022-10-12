using System;
using DG.Tweening;
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
        private CanvasGroup _group;

        public Plant SelectedPlant => _selectedPlant;
        public event Action<SelectPlant> OnSelect;

        private void Awake()
        {
            SetPlant(0);
            TryGetComponent(out _group);
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
            _seedImage.transform.DOComplete(true);
            _index--;
            if (_index < 0) _index = _storage.Plants.Count - 1;
            var sequence = DOTween.Sequence();
            sequence.Append(_seedImage.transform.DOMove(_next.transform.position, .1f));
            sequence.AppendCallback(() => _seedImage.transform.position = _previous.transform.position);
            sequence.AppendCallback(() => SetPlant(_index));
            sequence.Append(_seedImage.transform.DOLocalMove(Vector3.zero, .1f));
            var scaleSequence = DOTween.Sequence();
            scaleSequence.Append(_seedImage.transform.DOScale(Vector3.one * .5f, .1f));
            scaleSequence.Append(_seedImage.transform.DOScale(Vector3.one, .1f));
        }

        private void OnNext()
        {
            _seedImage.transform.DOComplete(true);
            _index = (_index + 1) % _storage.Plants.Count;
            var sequence = DOTween.Sequence();
            sequence.Append(_seedImage.transform.DOMove(_previous.transform.position, .1f));
            sequence.AppendCallback(() => _seedImage.transform.position = _next.transform.position);
            sequence.AppendCallback(() => SetPlant(_index));
            sequence.Append(_seedImage.transform.DOLocalMove(Vector3.zero, .1f));
            var scaleSequence = DOTween.Sequence();
            scaleSequence.Append(_seedImage.transform.DOScale(Vector3.one * .5f, .1f));
            scaleSequence.Append(_seedImage.transform.DOScale(Vector3.one, .1f));
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
            if (_group == null) TryGetComponent(out _group);
            var rectTransform = transform as RectTransform;
            if (rectTransform == null)
            {
                Debug.LogWarning("Select plant isn't rect transform");
                return;
            }
            gameObject.SetActive(true);
            _group.alpha = 0;
            transform.position += Vector3.down * rectTransform.rect.height;
            transform.DOLocalMove(Vector3.zero, .25f);
            _group.DOFade(1f, .25f);
        }

        private void Close()
        {
            var rectTransform = transform as RectTransform;
            if (rectTransform == null)
            {
                Debug.LogWarning("Select plant isn't rect transform");
                return;
            }

            transform.DOLocalMove(Vector3.down * rectTransform.rect.height, .25f);
            _group.DOFade(0f, .25f)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}