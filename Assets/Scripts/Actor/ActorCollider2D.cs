﻿using UnityEngine;

namespace Actor
{
    public class ActorCollider2D : MonoBehaviour
    {
        private Actor _actor;

        private void Start()
        {
            _actor = GetComponent<Actor>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            var enemy = other.gameObject.GetComponent<EnemyMagnet>();
            var enemyPole = enemy.m_MagneticPole;
            var canBeatenActor = _actor.CanBeaten(enemyPole);
            if (canBeatenActor)
            {
                _actor.Beaten();
            }
            else
            {
                enemy.FightBack();
            }
        }
    }
}