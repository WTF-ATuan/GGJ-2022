using Magnet;
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
                }
                else
                {
                    // 產生反擊子彈
                    Enemy_Backtrack enemy_Backtrack = Instantiate(AttackMagnet._.enemy_Backtrack, enemy.transform.position, Quaternion.identity);
                    enemy_Backtrack.End_Act += () => {
                        AttackMagnet._.HP--;
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