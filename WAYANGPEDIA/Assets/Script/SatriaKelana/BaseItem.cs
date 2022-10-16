using System;
using UnityEngine;

namespace SatriaKelana
{
    public abstract class BaseItem : ScriptableObject
    {
        public virtual string Name => name;
        public abstract int Price { get; }
        public abstract string Description { get; }
        public abstract Sprite Sprite { get; }

        public virtual void Buy(Inventory inventory)
        {
            inventory.Add(this);
        }
    }
}