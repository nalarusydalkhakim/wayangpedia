using System;
using System.Linq;
using SatriaKelana.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SatriaKelana
{
    public class Wander : MonoBehaviour, IPersistent
    {
        public enum State
        {
            Idle,
            Wandering,
            Done
        }

        [Serializable]
        public class WanderData
        {
            public State State { get; set; }
            public DateTime TimeStart { get; set; }
            public DateTime TimeEnd { get; set; }
            public int Food { get; set; } = -1;
            public int Item { get; set; } = -1;
            public int Accessory { get; set; } = -1;
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
        [SerializeField] private Selection _accessory;

        [Header("Components")] [SerializeField]
        private Inventory _inventory;

        [SerializeField] private Button _depart;
        [SerializeField] private Selector _selector;
        [SerializeField] private TextMeshProUGUI _time, _departText;
        [SerializeField] private SaveManager _saveManager;
        [SerializeField] private ItemStorage _storage;

        private WanderData _currentStatus;

        private void Awake()
        {
            _food.Button.onClick.AddListener(() => AttachSelect(OnFoodSelect));
            _item.Button.onClick.AddListener(() => AttachSelect(OnItemSelect));
            _accessory.Button.onClick.AddListener(() => AttachSelect(OnAccessorySelect));
            _depart.onClick.AddListener(Depart);
            _saveManager.Register(this);
            Load();
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
                case State.Done:
                    HandleDone();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleDone()
        {
            _time.text = "00:00";
        }

        private void EnterState(State state)
        {
            switch (state)
            {
                case State.Idle:
                    _currentStatus = new WanderData();
                    SetButtonState(true);
                    _departText.text = "Berangkat!";
                    break;
                case State.Wandering:
                    SetButtonState(false);
                    _departText.text = "Sedang berkelana";
                    break;
                case State.Done:
                    SetButtonState(false);
                    _depart.enabled = true;
                    _departText.text = "Ambil hasil";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            LoadAllItem();
            _currentStatus.State = state;
        }

        private void LoadAllItem()
        {
            LoadItem(_food, _currentStatus.Food);
            LoadItem(_item, _currentStatus.Item);
            LoadItem(_accessory, _currentStatus.Accessory);
        }

        private void LoadItem(Selection selection, int index)
        {
            var item = _storage.Get(index);
            SetItem(selection, item);
        }

        private void HandleIdle()
        {
            _time.text = "||";
        }

        private void HandleWandering()
        {
            var timeLeft = (_currentStatus.TimeEnd - DateTime.Now);
            _time.text = $"{timeLeft:mm\\:ss}";

            if (timeLeft.TotalSeconds > 0) return;
            EnterState(State.Done);
        }

        private void Depart()
        {
            switch (_currentStatus.State)
            {
                case State.Idle:
                    _currentStatus.TimeStart = DateTime.Now;
                    _currentStatus.TimeEnd = DateTime.Now.AddSeconds(5);
                    EnterState(State.Wandering);
                    break;
                case State.Wandering:
                    break;
                case State.Done:
                    EnterState(State.Idle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetButtonState(bool active)
        {
            _depart.enabled = active;
            _food.Button.enabled = active;
            _item.Button.enabled = active;
            _accessory.Button.enabled = active;
        }

        private void OnFoodSelect(Selector selector)
        {
            selector.OnSelect -= OnFoodSelect;
            _currentStatus.Food = _storage.Items.IndexOf(selector.SelectedItem);
            SetItem(_food, selector.SelectedItem);
        }

        private void OnItemSelect(Selector selector)
        {
            selector.OnSelect -= OnItemSelect;
            _currentStatus.Item = _storage.Items.IndexOf(selector.SelectedItem);
            SetItem(_item, selector.SelectedItem);
        }

        private void OnAccessorySelect(Selector selector)
        {
            selector.OnSelect -= OnAccessorySelect;
            _currentStatus.Accessory = _storage.Items.IndexOf(selector.SelectedItem);
            SetItem(_accessory, selector.SelectedItem);
        }

        private void SetItem(Selection selection, BaseItem item)
        {
            var itemNull = item == null;
            selection.Text.SetActive(itemNull);
            selection.Image.gameObject.SetActive(!itemNull);
            if (itemNull) return;
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

        public void Save()
        {
            _saveManager.BinarySave(_currentStatus, "Wandering");
        }

        public void Load()
        {
            var success = _saveManager.BinaryLoad("Wandering", out _currentStatus);
            if (success)
            {
                EnterState(_currentStatus.State);
                return;
            }

            _currentStatus = new WanderData();
            EnterState(State.Idle);
        }
    }
}