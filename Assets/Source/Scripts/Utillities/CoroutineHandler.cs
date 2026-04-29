using UnityEngine;

namespace Source.Scripts.Utillities
{
    public class CoroutineHandler : MonoBehaviour
    {
        public static CoroutineHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}