using UI.Joystick;
using UnityEngine;

namespace Core.Entities
{
    public sealed class CharacterController : PersonController
    {
        public override void Move(Vector3 direction)
        {
            Agent.velocity = direction;
        }

        private void FixedUpdate()
        {
            Move(Joystick.Instance.GetHandleDirection());
        }
    }
}