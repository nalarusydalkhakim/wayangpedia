using System;
using TMPro;
using UnityEngine;

namespace SatriaKelana.UI
{
    public class CoinBar : MonoBehaviour
    {
        [SerializeField] private CoinManager _manager;
        [SerializeField] private TextMeshProUGUI _coinText;

        private void Awake()
        {
            _manager.OnCoinChanged += UpdateCoin;
            UpdateCoin(_manager.Coin);
        }

        private void UpdateCoin(int coin)
        {
            _coinText.text = coin.ToString();
        }
    }
}