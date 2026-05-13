using System;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public abstract class SearchStrategy : MonoBehaviour
    {
        public abstract void CheckForTarget();
        public abstract event Action<Transform> OnTargetFound; 
    }
}