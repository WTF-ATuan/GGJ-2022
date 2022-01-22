using System;
using UnityEngine;
using Magnet;

namespace Actor
{
    public class ActorInput : MonoBehaviour
    {
        private Actor _actor;

        private void Start()
        {
            _actor = GetComponent<Actor>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _actor.Move(-1);
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _actor.Move(1);
            }
            var canSwitchMagnetPole = Input.GetKeyDown(KeyCode.Space);
            if (canSwitchMagnetPole)
            {
                _actor.currentMagneticPole = _actor.currentMagneticPole == MagneticPole.North ? MagneticPole.South : MagneticPole.North;
            }
        }
    }
}