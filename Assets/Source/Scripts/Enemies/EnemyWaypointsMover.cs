using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class EnemyWaypointsMover : EnemyMover, IMovePattern
    {
        [SerializeField] private Transform[] _waypoints;
        [SerializeField] private float _safetyMargin = 0.5f;
        
        private Transform _currentWaypoint;

        private void Awake()
        {
            _currentWaypoint = _waypoints[0];
        }

        public override void Move()
        {
            Vector3 direction = _currentWaypoint.position - transform.position;
            
            Rigidbody.velocity = direction.normalized * _speed;

            if (CheckDestination())
            {
                ChangeWaypoint();
            }
        }

        private bool CheckDestination()
        {
            if (transform.position.x - _currentWaypoint.position.x < _safetyMargin)
            {
                return true;
            }
            
            return false;
        }

        private void ChangeWaypoint()
        {
            foreach (var waypoint in _waypoints)
            {
                if (_currentWaypoint != waypoint)
                {
                    _currentWaypoint = waypoint;
                    break;
                }
            }
        }
    }
}