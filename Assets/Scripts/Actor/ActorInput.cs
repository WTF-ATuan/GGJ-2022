using System;
using UnityEngine;

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
                _actor.SwitchMagneticPole();
            }
        }
    }
}