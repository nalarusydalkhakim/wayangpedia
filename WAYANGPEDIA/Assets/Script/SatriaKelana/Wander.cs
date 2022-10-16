using System;
using System.Linq;
using SatriaKelana.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SatriaKelana
{
    public class Wander : MonoBehaviour
    {
        public enum State
        {
            Idle,
            Wandering
        }

        [Serializable]
        public class WanderData
        {
            public State State { get; set; }
            public DateTime TimeStart { get; set; }
            public DateTime TimeEnd { get; set; }
        }
        
        [Serializable]
        public class Selection
        {
            [SerializeField] private Button _button;
            [SerializeField] private GameObject _text;
            [SerializeField] private Image _image;
            public Button Button => _button;
            public Image Image => _image;
            public GameObject Text => _text;
            public BaseItem Item { get; set; }
        }

        [Header("Selection")] [SerializeField] private Selection _food;
        [SerializeField] private Selection _item;
        [SerializeField] private Selection _accessories;

        [Header("Components")] [SerializeField]
        private Inventory _inventory;

        [SerializeField] private Button _depart;
        [SerializeField] private Selector _selector;
        [SerializeField] private TextMeshProUGUI _time;

        private WanderData _currentStatus;

        private void Awake()
        {
            _food.Button.onClick.AddListener(() => AttachSelect(OnFoodSelect));
            _item.Button.onClick.AddListener(() => AttachSelect(OnItemSelect));
            _accessories.Button.onClick.AddListener(() => AttachSelect(OnAccessoriesSelect));
            _depart.onClick.AddListener(Depart);
            _currentStatus = new WanderData
            {
                State = State.Idle
            };
        }

        private void Update()
        {
            if (_currentStatus == null) return;
            switch (_currentStatus.State)
            {
                case State.Idle:
                    HandleIdle();
                    break;
                case State.Wandering:
                    HandleWandering();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleIdle()
        {
            _time.text = "||";
        }

        private void HandleWandering()
        {
            var timeLeft = (_currentStatus.TimeEnd - DateTime.Now);
            _time.text = $"{timeLeft:mm\\:ss}";
            
            if (!(timeLeft.TotalSeconds <= 0)) return;
            _currentStatus.State = State.Idle;
            SetButtonState(true);
        }

        private void Depart()
        {
            _currentStatus = new WanderData
            {
                State = State.Wandering,
                TimeStart = DateTime.Now,
                TimeEnd = DateTime.Now.AddMinutes(5)
            };
            SetButtonState(false);
        }

        private void SetButtonState(bool active)
        {
            _depart.enabled = active;
            _food.Button.enabled = active;
            _item.Button.enabled = active;
            _accessories.Button.enabled = active;
        }

        private void OnFoodSelect(Selector selector)
        {
            selector.OnSelect -= OnFoodSelect;

            SetItem(_food, selector.SelectedItem);
        }

        private void OnItemSelect(Selector selector)
        {
            selector.OnSelect -= OnItemSelect;

            SetItem(_item, selector.SelectedItem);
        }

        private void OnAccessoriesSelect(Selector selector)
        {
            selector.OnSelect -= OnAccessoriesSelect;

            SetItem(_accessories, selector.SelectedItem);
        }

        private void SetItem(Selection selection, BaseItem item)
        {
            selection.Text.SetActive(false);
            selection.Image.gameObject.SetActive(true);
            selection.Image.sprite = item.Sprite;
            selection.Item = item;
        }

        private void AttachSelect(Action<Selector> select)
        {
            if (_inventory.Items.Count <= 0)
            {
                Debug.Log("No items found in inventory", this);
                return;
            }

            var items = _inventory.Items.Select(i => i.Item).ToList();
            _selector.Show(items, "Pilih");
            _selector.OnSelect += select;
        }
    }
}