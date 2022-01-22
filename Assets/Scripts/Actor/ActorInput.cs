using System;
using UnityEngine;
using Magnet;

namespace Actor
{
    public class ActorInput : MonoBehaviour
    {
        private Actor _actor;
        private Animator m_animator;

        private void Start()
        {
            _actor = GetComponent<Actor>();
            m_animator = GetComponent<Animator>();
        }

        private void Update()
        {
            int _nowSpeed = 0;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _nowSpeed = -1;
                _actor.Move(_nowSpeed);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _nowSpeed = 1;
                _actor.Move(_nowSpeed);
            }
            m_animator.SetInteger("NowSpeed ", _nowSpeed);

            var canSwitchMagnetPole = Input.GetKeyDown(KeyCode.Space);
            if (canSwitchMagnetPole)
            {
                _actor.currentMagneticPole = _actor.currentMagneticPole == MagneticPole.North ? MagneticPole.South : MagneticPole.North;
            }
        }
    }
}