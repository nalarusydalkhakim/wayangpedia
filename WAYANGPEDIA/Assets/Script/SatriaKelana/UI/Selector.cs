using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SatriaKelana.UI
{
    public class Selector : MonoBehaviour
    {
        [SerializeField] private BaseItemStorage _storage;
        [SerializeField] private Image _image, _background;
        [SerializeField] private TextMeshProUGUI _title, _okText;
        [SerializeField] private Button _next, _previous, _select, _close;
        [SerializeField] private TextMeshProUGUI _description;

        private int _index;
        private CanvasGroup _group, _bgGroup;
        private GameObject _descriptionGO;
        private IList<BaseItem> _items;

        public BaseItem SelectedItem { get; private set; }

        public event Action<Selector> OnSelect;

        private void Awake()
        {
            if (_storage != null)
            {
                _items = _storage.Items;
                SetItem(0);
            }

            _descriptionGO = _description.transform.parent.gameObject;
            TryGetComponent(out _group);
            _background.TryGetComponent(out _bgGroup);
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
            _image.transform.DOComplete(true);
            _index--;
            if (_index < 0) _index = _items.Count - 1;
            var sequence = DOTween.Sequence();
            sequence.Append(_image.transform.DOMove(_next.transform.position, .1f));
            sequence.AppendCallback(() => _image.transform.position = _previous.transform.position);
            sequence.AppendCallback(() => SetItem(_index));
            sequence.Append(_image.transform.DOLocalMove(Vector3.zero, .1f));
            var scaleSequence = DOTween.Sequence();
            scaleSequence.Append(_image.transform.DOScale(Vector3.one * .5f, .1f));
            scaleSequence.Append(_image.transform.DOScale(Vector3.one, .1f));
        }

        private void OnNext()
        {
            _image.transform.DOComplete(true);
            _index = (_index + 1) % _items.Count;
            var sequence = DOTween.Sequence();
            sequence.Append(_image.transform.DOMove(_previous.transform.position, .1f));
            sequence.AppendCallback(() => _image.transform.position = _next.transform.position);
            sequence.AppendCallback(() => SetItem(_index));
            sequence.Append(_image.transform.DOLocalMove(Vector3.zero, .1f));
            var scaleSequence = DOTween.Sequence();
            scaleSequence.Append(_image.transform.DOScale(Vector3.one * .5f, .1f));
            scaleSequence.Append(_image.transform.DOScale(Vector3.one, .1f));
        }

        private void SetItem(int index)
        {
            if (index < 0 || index >= _items.Count) return;
            _index = index;
            var item = _items[index];
            _image.sprite = item.Sprite;
            _title.text = item.Name;
            _description.text = item.Description;
            SelectedItem = item;
        }

        public void Show()
        {
            if (_group == null) TryGetComponent(out _group);
            var rectTransform = transform as RectTransform;
            if (rectTransform == null)
            {
                Debug.LogWarning("Select item isn't rect transform");
                return;
            }

            gameObject.SetActive(true);
            _background.gameObject.SetActive(true);
            _group.alpha = 0;
            _bgGroup.alpha = 0;
            transform.position += Vector3.down * rectTransform.rect.height;
            transform.DOLocalMove(Vector3.zero, .25f);
            _group.DOFade(1f, .25f);
            _bgGroup.DOFade(1f, .25f);
        }

        public void Show(IList<BaseItem> items, string okText, int index = 0, bool showDescription = false)
        {
            _items = items;
            SetItem(index);
            _okText.text = okText;
            if (showDescription)
            {
                _descriptionGO = _description.transform.parent.gameObject;
                _descriptionGO.SetActive(true);
                var rect = _descriptionGO.transform as RectTransform;
                if (rect != null)
                {
                    rect.anchoredPosition += Vector2.down * rect.rect.height;
                    DOTween.To(() => rect.anchoredPosition.y,
                        y => rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, y), 0f, .25f);
                }
            }

            Show();
        }

        private void Close()
        {
            var rectTransform = transform as RectTransform;
            if (rectTransform == null)
            {
                Debug.LogWarning("Select item isn't rect transform");
                return;
            }

            _bgGroup.DOFade(0f, .25f)
                .OnComplete(() => _bgGroup.gameObject.SetActive(false));
            transform.DOLocalMove(Vector3.down * rectTransform.rect.height, .25f);
            _group.DOFade(0f, .25f)
                .OnComplete(() => gameObject.SetActive(false));

            var descriptionRect = _descriptionGO.transform as RectTransform;
            if (descriptionRect == null) return;
            var target = descriptionRect.anchoredPosition + Vector2.down * descriptionRect.rect.height;
            DOTween.To(() => descriptionRect.anchoredPosition.y,
                y => descriptionRect.anchoredPosition = new Vector2(descriptionRect.anchoredPosition.x, y), target.y,
                .25f);
        }
    }
}