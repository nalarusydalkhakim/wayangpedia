using System;
using TMPro;
using UnityEngine;

namespace SatriaKelana.UI
{
    public class CoinBar : MonoBehaviour
    {
        [SerializeField] private Garden _garden;
        [SerializeField] private TextMeshProUGUI _coinText;

        private void Awake()
        {
            _garden.OnAreaCollect += OnAreaCollect;
            UpdateCoin();
        }

        private void OnAreaCollect(Area area)
        {
            UpdateCoin();
        }

        private void UpdateCoin()
        {
            var coin = PlayerPrefs.GetInt("Coin", 0);
            _coinText.text = coin.ToString();
        }
    }
}