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
            var enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy == null) return;
            var enemyPole = enemy.magneticPole;
            var canBeatenActor = _actor.CanBeaten(enemyPole);
            if (canBeatenActor)
            {
                _actor.Beaten();
            }
            else
            {
                var bossPosition = enemy.StartPosition;
                enemy.Move(1, bossPosition);
            }
        }
    }
}