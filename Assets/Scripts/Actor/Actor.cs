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
        public List<HealthObject> healthObjects;

        private Animator m_animator;
        [SerializeField] private Sprite[] backpack_Sprites, n_Sprites, s_Sprites;
        [SerializeField] SpriteRenderer NowBackpack_Sprite, NowMagnetic;

        // 衝擊波爆出來的位置
        public Transform C;
        public Actor_C CPref;
        private float _moveSpeed;
        public MagneticPole currentMagneticPole
        {
            get => _currentMagneticPole;
            set
            {
                // ========改變磁極的特效寫這邊=========
                var spriteRender = GetComponent<SpriteRenderer>();
                //spriteRender.color = value == MagneticPole.North ? Color.red : Color.blue;
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
            currentMagneticPole = defaultMagneticPole;
            m_animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_health != health)
            {
                if (_health > health)
                {
                    m_animator.SetTrigger("Hit");
                }
                _health = health;
                if(_health <= 0)
                {
                    Debug.Log("Actor Die");
                    actorDead.InvokeEvent();
                }
            }

            //改變背包上的磁極 (硬A
            Sprite nowSprite = null;
            for (int i = 0; i < backpack_Sprites.Length; i++)
            {
                if (NowBackpack_Sprite.sprite == backpack_Sprites[i])
                {
                    nowSprite = currentMagneticPole == MagneticPole.North ? n_Sprites[i] : s_Sprites[i];
                    break;
                }
            }                
            NowMagnetic.sprite = nowSprite;
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
        }

        public void Sp_C(MagneticPole m)
        {
            Instantiate<Actor_C>(CPref, C.transform.position, Quaternion.identity, C).Open(m);
        }

        public void ModifyMoveSpeed(float amount)
        {
            if (amount < 0 || amount > 100) return;
            _moveSpeed += amount;
        }

        public bool CanBeaten(MagneticPole magneticPole)
        {
            var isSame = _currentMagneticPole == magneticPole;
            return !isSame;
        }

        /// <summary>
        /// 取得一個生命物件（能選擇是活的還死的）
        /// </summary>
        public HealthObject GethealthObject(bool GetOpen = true)
        {

            List<HealthObject> Objs = healthObjects.FindAll(x => x.Open == GetOpen);
            if (Objs == null || Objs.Count <= 0) return null;
            return Objs[Random.Range(0, Objs.Count)];
        }
    }
}