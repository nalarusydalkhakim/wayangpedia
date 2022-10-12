using System;
using DG.Tweening;
using UnityEngine;

namespace SatriaKelana.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _group;

        public void Toggle(bool active)
        {
            if (active)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
            var t = transform;
            var rect = t as RectTransform;
            if (rect == null)
            {
                Debug.LogWarning("This menu isn't a rect transform!", this);
                return;
            }

            t.position += Vector3.down * rect.rect.height;
            t.DOLocalMove(Vector3.zero, .25f);
            if (_group == null) return;
            _group.alpha = 0;
            _group.DOFade(1f, .25f);
        }

        public void Hide()
        {
            var t = transform;
            var rect = t as RectTransform;
            if (rect == null)
            {
                Debug.LogWarning("This menu isn't a rect transform!", this);
                return;
            }

            t.DOLocalMove(Vector3.down * rect.rect.height, .25f);
            if (_group == null) return;
            _group.alpha = 1;
            _group.DOFade(0f, .25f);
        }
    }
}