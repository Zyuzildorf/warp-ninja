using UnityEngine;

namespace Source.Scripts.Enemies
{
    public abstract class Mover
    {
        protected float Speed;
        
        public Transform Target { get; protected set; }
        
        public abstract void Move();
        public abstract void SetSpeed(float speed); 
        public abstract void SetTarget(Transform target);
            
    }
}