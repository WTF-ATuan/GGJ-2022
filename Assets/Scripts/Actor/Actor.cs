using System;
using UnityEngine;

namespace Actor
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private float defaultMoveSpeed;
        private float _moveSpeed;
        private Rigidbody2D _rigidbody2D;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _moveSpeed = defaultMoveSpeed;
        }

        public void Movement(float horizontal)
        {
            var currentVelocity = _rigidbody2D.velocity;
            var movementOffsetX = horizontal * _moveSpeed;
            var nextVelocity = new Vector2(movementOffsetX, currentVelocity.y);
            _rigidbody2D.velocity = nextVelocity;
        }

        public void ModifyMoveSpeed(float amount)
        {
            if(amount < 0 || amount > 100) return;
            _moveSpeed += amount;
        }
    }
}