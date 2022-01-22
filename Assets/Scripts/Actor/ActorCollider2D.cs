using UnityEngine;

namespace Actor
{
    public class ActorCollider2D : MonoBehaviour
    {
        private Actor _actor;

        private void Start()
        {
            _actor = GetComponent<Actor>();
        }

        public void OnCollisionEnter2D(Collision2D other)
        {

        }
    }
}