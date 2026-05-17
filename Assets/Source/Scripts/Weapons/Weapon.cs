using System;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public event Action<IHealthObject> OnHitHealthObject;

        public virtual void SetVelocity(Vector3 velocity) { }

        public virtual void Get() { }

        public virtual void Release() { }

        protected void InvokeOnHitHealthObject(IHealthObject healthObject)
        {
            if (OnHitHealthObject != null)
            {
                OnHitHealthObject?.Invoke(healthObject);
            }
        }
    }
}
