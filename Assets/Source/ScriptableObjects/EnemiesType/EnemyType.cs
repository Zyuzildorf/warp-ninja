using Source.Scripts.Enemies;
using UnityEngine;

namespace Source.ScriptableObjects.EnemiesType
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/Create new enemy")]
    public class EnemyType : ScriptableObject
    {
        [SerializeField] private int _maxHealth;
    }
}