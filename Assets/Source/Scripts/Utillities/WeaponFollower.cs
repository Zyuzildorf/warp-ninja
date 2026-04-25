using Source.Scripts.Player;
using Source.Scripts.Weapons;
using UnityEngine;

namespace Source.Scripts.Utillities
{
    public class WeaponFollower : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Weapon _weapon;

        private void Update()
        {
            transform.position = new Vector3(_weapon.transform.position.x, _weapon.transform.position.y, transform.position.z);
        }
    }
}
