using UnityEngine;

namespace Source.Scripts.Enemies
{
    public abstract class Mover
    {
        protected float Speed;
        
        protected Rigidbody Rigidbody;
        protected Transform Transform;
        
        public Transform Target { get; protected set; }
        
        public abstract void Move();
        public abstract void SetSpeed(float speed); 
        public abstract void SetTarget(Transform target);
            
    }
}