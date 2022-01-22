using Magnet;
using UnityEngine;

namespace Actor
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private float defaultMoveSpeed = 5;
        [SerializeField] private MagneticPole defaultMagneticPole = MagneticPole.North;
        [SerializeField] private int defaultHealth = 3;

        private float _moveSpeed;
        private Rigidbody2D _rigidbody2D;
        private MagneticPole _currentMagneticPole;
        private int _health;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _moveSpeed = defaultMoveSpeed;
            _currentMagneticPole = defaultMagneticPole;
            _health = defaultHealth;
        }

        public void Move(float horizontal)
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

        public void SwitchMagneticPole(MagneticPole magneticPole)
        {
            var isSame = _currentMagneticPole == magneticPole;
            if (isSame) return;
            _currentMagneticPole = magneticPole;
        }

        public void Beaten()
        {
            _health -= 1;
            if (_health <= 0)
            {
                Debug.Log("Actor Die");
            }
        }
    }
}