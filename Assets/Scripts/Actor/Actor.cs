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
        [SerializeField] public MagneticPole defaultMagneticPole = MagneticPole.North;
        [SerializeField] private int defaultHealth = 3;
        public List<HealthObject> healthObjects;

        private float _moveSpeed;
        public MagneticPole currentMagneticPole
        {
            get => _currentMagneticPole;
            set
            {
                // ========改變磁極的特效寫這邊=========
                var spriteRender = GetComponent<SpriteRenderer>();
                spriteRender.color = value == MagneticPole.North ? Color.red : Color.blue;
                _currentMagneticPole = value;
            }
        }
        public MagneticPole _currentMagneticPole;
        public int health
        {
            get
            {
                int n = 0;
                foreach(HealthObject i in healthObjects)
                {
                    if (i.Open) n++;
                }
                return n;
            }
        }
        private int _health;

        public readonly DomainEvent actorDead = new DomainEvent();

        private void Start()
        {
            _moveSpeed = defaultMoveSpeed;
            _currentMagneticPole = defaultMagneticPole;
            //SwitchMagneticPole();
        }

        private void Update()
        {
            if(_health != health)
            {
                _health = health;
                if(_health <= 0)
                {
                    Debug.Log("Actor Die");
                    actorDead.InvokeEvent();
                }
            }
        }

        public void Move(float horizontal)
        {
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

        //public void SwitchMagneticPole()
        //{
        //    var isNorth = _currentMagneticPole == MagneticPole.North;
        //    _currentMagneticPole = isNorth ? MagneticPole.South : MagneticPole.North;
        //    var spriteRender = GetComponent<SpriteRenderer>();
        //    spriteRender.color = isNorth ? Color.blue : Color.red;
        //}

        public bool CanBeaten(MagneticPole magneticPole)
        {
            var isSame = _currentMagneticPole == magneticPole;
            return !isSame;
        }

        //public void Beaten()
        //{
        //    //_health -= 1;
        //    foreach (var healthObject in healthObjects.Where(healthObject => healthObject != null))
        //    {
        //        healthObject.gameObject.SetActive(false);
        //    }

        //    if (health > 0) return;
        //    Debug.Log("Actor Die");
        //    actorDead.InvokeEvent();
        //}

        /// <summary>
        /// 取得一個生命物件（能選擇是活的還死的）
        /// </summary>
        public HealthObject GethealthObject(bool GetOpen = true)
        {

            List<HealthObject> Objs = healthObjects.FindAll(x => x.Open == GetOpen);
            if (Objs == null || Objs.Count <= 0) return null;
            return Objs[Random.Range(0, Objs.Count)];
        }

        //// ReSharper disable once IdentifierTypo
        //public List<AttackableComponent> GetCurrentAttackableComponentList()
        //{
        //    var components = new List<AttackableComponent>();

        //    for (var index = 0; index < healthObjects.Count; index++)
        //    {
        //        var healthObject = healthObjects[index];
        //        if (healthObject == null) continue;
        //        var healthIndex = index;
        //        var component = new AttackableComponent
        //        {
        //            AttackbleIndex = healthIndex,
        //            ComponentObject = healthObject.gameObject;
        //        };
        //        components.Add(component);
        //    }

        //    return components;
        //}
    }
}