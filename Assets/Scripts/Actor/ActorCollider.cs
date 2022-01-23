using Magnet;
using SoundEffect;
using UnityEngine;

// ReSharper disable All

namespace Actor
{
    public class ActorCollider : MonoBehaviour
    {
        private Actor _actor;

        private void Start()
        {
            _actor = GetComponent<Actor>();
        }

        public void OnTriggerEnter(Collider other)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                MagneticPole enemyPole = enemy.magneticPole;
                bool canBeatenActor = _actor.CanBeaten(enemyPole);
                if (canBeatenActor)
                {
                    _actor.GethealthObject().Open = false;
                    SoundManager.instance.PlaySoundEffect("背包被破壞");
                }
                else
                {
                    // 產生衝擊波
                    _actor.Sp_C(enemy.magneticPole);
                    // 產生反擊子彈
                    Enemy_Backtrack enemy_Backtrack = Instantiate(AttackMagnet._.enemy_Backtrack, enemy.transform.position, Quaternion.identity);
                    SoundManager.instance.PlaySoundEffect("反擊");
                    enemy_Backtrack.magneticPole = enemy.magneticPole;
                    enemy_Backtrack.End_Act += () => {
                        if (enemy.magneticPole == MagneticPole.North)
                        {
                            AttackMagnet._.HP_N--;
                        }
                        else
                        {
                            AttackMagnet._.HP_S--;
                        }
                        Destroy(enemy_Backtrack.gameObject);
                    };
                    enemy_Backtrack.Move(AttackMagnet._.transform);
                }
                Destroy(enemy.gameObject);
                return;
            }

            AddHP addHp = other.GetComponent<AddHP>();
            if (addHp != null)
            {
                SoundManager.instance.PlaySoundEffect("撿起卡爾");
                if (_actor._currentMagneticPole != addHp.magneticPole)
                {
                    HealthObject healthObject = _actor.GethealthObject(false);
                    if (healthObject != null) healthObject.Open = true;
                }
                Destroy(addHp.gameObject);
                return;
            }
        }
    }
}