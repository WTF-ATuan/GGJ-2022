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
        private Rigidbody _rigidbody;
        private MagneticPole _currentMagneticPole;
        private int _health;


        public readonly DomainEvent actorDead = new DomainEvent();

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _moveSpeed = defaultMoveSpeed;
            _currentMagneticPole = defaultMagneticPole;
            _health = defaultHealth;
            SwitchMagneticPole();
        }

        public void Move(float horizontal)
        {
            print(horizontal);
            if (horizontal > 0)
            {
                transform.position = transform.position + new Vector3(1, 0, 0) * _moveSpeed * Time.deltaTime;
            }
            else if (horizontal < 0)
            {
                transform.position = transform.position + new Vector3(-1, 0, 0) * _moveSpeed * Time.deltaTime;
            }
            //var currentVelocity = _rigidbody.velocity;
            //var movementOffsetX = horizontal * _moveSpeed;
            //var nextVelocity = new Vector2(movementOffsetX, currentVelocity.y);
            //_rigidbody.velocity = nextVelocity;
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
        public List<AttackableComponent> GetCurrentAttackableComponentList()
        {
            var components = new List<AttackableComponent>();

            for (var index = 0; index < healthObjects.Count; index++)
            {
                var healthObject = healthObjects[index];
                if (healthObject == null) continue;
                var healthIndex = index;
                var component = new AttackableComponent
                {
                    AttackbleIndex = healthIndex,
                    ComponentObject = healthObject
                };
                components.Add(component);
            }

            return components;
        }
    }
}