using UnityEngine;

namespace Source.Scripts.Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public virtual void SetVelocity(Vector3 velocity) { }

        public virtual void Get() { }

        public virtual void Release() { }
    }
}
