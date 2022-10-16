using System;
using UnityEngine;

namespace SatriaKelana
{
    [CreateAssetMenu(fileName = "New coin event", menuName = "Manager/Coin", order = 0)]
    public class CoinManager : ScriptableObject
    {
        public int Coin { get; private set; }

        public event Action<int> OnCoinChanged;

        private void Awake()
        {
            Coin = PlayerPrefs.GetInt(Constants.CoinKey, 0);
        }

        public void Add(int coin)
        {
            Coin += coin;
            PlayerPrefs.SetInt(Constants.CoinKey, Coin);
            OnCoinChanged?.Invoke(Coin);
        }

        public void Subtract(int coin)
        {
            Coin -= coin;
            PlayerPrefs.SetInt(Constants.CoinKey, Coin);
            OnCoinChanged?.Invoke(Coin);
        }
    }
}