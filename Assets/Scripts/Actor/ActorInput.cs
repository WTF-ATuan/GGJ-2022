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
            var horizontalValue = Input.GetAxis("Horizontal");
            var canSwitchMagnetPole = Input.GetKeyDown(KeyCode.Space);
            _actor.Move(horizontalValue);
            if (canSwitchMagnetPole)
            {
                _actor.SwitchMagneticPole();
            }
        }
    }
}