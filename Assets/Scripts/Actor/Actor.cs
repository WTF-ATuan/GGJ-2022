using System.Collections.Generic;
using System.Linq;
using Extra;
using Magnet;
using UnityEngine;

namespace Actor
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private float defaultMoveSpeed = 5;
        [SerializeField] private MagneticPole defaultMagneticPole = MagneticPole.North;
        [SerializeField] private int defaultHealth = 3;
        [SerializeField] private List<GameObject> healthObjects;

        private float _moveSpeed;
        private Rigidbody2D _rigidbody2D;
        private MagneticPole _currentMagneticPole;
        private int _health;


        public readonly DomainEvent actorDead = new DomainEvent();

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
            if (amount < 0 || amount > 100) return;
            _moveSpeed += amount;
        }

        public void SwitchMagneticPole()
        {
            var isNorth = _currentMagneticPole == MagneticPole.North;
            _currentMagneticPole = isNorth ? MagneticPole.South : MagneticPole.North;
            var spriteRender = GetComponent<SpriteRenderer>();
            spriteRender.color = isNorth ? Color.blue : Color.red;
        }

        public bool CanBeaten(MagneticPole magneticPole)
        {
            var isSame = _currentMagneticPole == magneticPole;
            return !isSame;
        }

        public void Beaten()
        {
            _health -= 1;
            foreach (var healthObject in healthObjects.Where(healthObject => healthObject != null))
            {
                healthObject.SetActive(false);
            }

            if (_health > 0) return;
            Debug.Log("Actor Die");
            actorDead.InvokeEvent();
        }

        // ReSharper disable once IdentifierTypo
        public AttackableComponent GetCurrentAttackableComponent()
        {
            var healthIndex = 0;
            var healPosition = Vector3.zero;
            for (var index = 0; index < healthObjects.Count; index++)
            {
                var healthObject = healthObjects[index];
                if (healthObject == null) continue;
                healthIndex = index;
                healPosition = healthObject.transform.position;
                break;
            }

            var component = new AttackableComponent
            {
                AttackbleIndex = healthIndex,
                ComponentPosition = healPosition
            };
            return component;
        }
    }
}