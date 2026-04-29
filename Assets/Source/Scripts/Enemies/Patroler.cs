using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class Patroler : MoveStrategy
    {
        [SerializeField] private Collider _moveZone;
        [SerializeField] private float _safetyMargin;
        [SerializeField] private float _waitBeforeMove;
        
        private EnemyRotater _rotater;
        private EnemyWaypointsMover _mover;
        
        private Rigidbody _rigidbody;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            
            _rotater = new EnemyRotater(_transform);
            _mover = new EnemyWaypointsMover(_transform, _rigidbody, _moveZone, _safetyMargin, _waitBeforeMove);
            
            _mover.SetSpeed(Speed);
        }


        public override void HandleMovement()
        {
           _mover.Move();
           _rotater.Rotate(_mover.Target.position);
        }

        public override void SetTarget(Transform target)
        {
            _mover.SetTarget(target);
        }
    }
}