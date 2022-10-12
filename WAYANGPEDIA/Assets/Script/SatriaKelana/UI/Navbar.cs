using System;
using UnityEngine;

namespace SatriaKelana.UI
{
    public class Navbar : MonoBehaviour
    {
        [SerializeField] private ToggleButton _store, _collection, _adventure;
        [SerializeField] private Menu _storeMenu, _collectionMenu, _adventureMenu;

        private void Awake()
        {
            _store.OnToggled += StoreOnToggled;
            _collection.OnToggled += CollectionOnToggled;
            _adventure.OnToggled += AdventureOnToggled;
        }

        private void StoreOnToggled(bool value)
        {
            _storeMenu.Toggle(value);
        }
        
        private void CollectionOnToggled(bool value)
        {
            _collectionMenu.Toggle(value);
        }
        
        private void AdventureOnToggled(bool value)
        {
            _adventureMenu.Toggle(value);
        }
    }
}