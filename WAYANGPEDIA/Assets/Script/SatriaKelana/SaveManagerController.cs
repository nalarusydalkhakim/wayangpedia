using System;
using UnityEngine;

namespace SatriaKelana
{
    public class SaveManagerController : MonoBehaviour
    {
        [SerializeField] private SaveManager _manager;

        private void OnApplicationQuit()
        {
            _manager.SaveAll();
        }
    }
}