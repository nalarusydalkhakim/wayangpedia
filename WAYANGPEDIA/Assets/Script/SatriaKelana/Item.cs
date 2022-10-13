using System;
using UnityEngine;

namespace SatriaKelana
{
    [Serializable]
    public abstract class Item : ScriptableObject
    {
        public abstract int Price { get; }
        public abstract string Description { get; }
        public abstract Sprite Sprite { get; }

        public virtual void Buy(Inventory inventory)
        {
            inventory.Add(this);
        }
    }
}