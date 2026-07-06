using Source.Scripts.Enemies;
using UnityEngine;

namespace Source.Scripts.Other
{
    public class EnemySpawner : ObjectsPool<Enemy>
    {
        [SerializeField] private int _defaultMaxHealth = 10;

        [SerializeField] private float _defaultMoveSpeed = 3f;
        [SerializeField] private float _defaultRotationSpeed = 180f;
        [SerializeField] private int _defaultDamage = 5;

        private int _currentMaxHealth;
        private float _currentMoveSpeed;
        private float _currentRotationSpeed;
        private int _currentDamage;
        
        
        public Enemy SpawnEnemy(Vector3 position, int? maxHealth = null, float? moveSpeed = null,
            float? rotationSpeed = null, int? damage = null)
        {
            _currentMaxHealth = maxHealth ?? _defaultMaxHealth;
            _currentMoveSpeed = moveSpeed ?? _defaultMoveSpeed;
            _currentRotationSpeed = rotationSpeed ?? _defaultRotationSpeed;
            _currentDamage = damage ?? _defaultDamage;

            Enemy enemy = GetObject(); 
            enemy.transform.position = position;
            return enemy;
        }
       
        protected override void OnGet(Enemy obj)
        {
            base.OnGet(obj);
            obj.OnDie += ReleaseObject; 
        }
        
        protected override void OnRelease(Enemy obj)
        {
            obj.OnDie -= ReleaseObject;
            base.OnRelease(obj);
        }
    } 
}