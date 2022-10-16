using System;
using UnityEngine;

namespace SatriaKelana
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private Inventory _inventory;

        private void Awake()
        {
            _inventory.Init();
        }
    }
}