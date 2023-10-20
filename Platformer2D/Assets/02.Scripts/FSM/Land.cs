using Unity.VisualScripting;
using UnityEngine;

namespace Platformer.FSM.Character
{
    public class Land : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.Land;
        public override bool canExecute => base.canExecute &&
                                            machine.currentStateID == CharacterStateID.Fall &&
                                            controller.isGrounded;

        
        public Land(CharacterMachine machine)
            : base(machine)
        {
            
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = false;
            controller.isMovable = false;
            controller.Stop();
            animator.Play("Land");
        }

        public override CharacterStateID OnStateUpdate()
        {
            CharacterStateID nextID = base.OnStateUpdate();

            if (nextID == CharacterStateID.None)
                return id;

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                nextID = CharacterStateID.Idle;

            return nextID;
        }
    }
}
