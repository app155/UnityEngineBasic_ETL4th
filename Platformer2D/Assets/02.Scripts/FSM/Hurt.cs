using Unity.VisualScripting;
using UnityEngine;

namespace Platformer.FSM.Character
{
    // OnHpDeplete에 전환 구독

    public class Hurt : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.Hurt;
        public override bool canExecute => base.canExecute;

        public Hurt(CharacterMachine machine)
            : base(machine)
        {

        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = false;
            controller.isMovable = false;
            controller.hasJumped = true;
            controller.hasDoubleJumped = true;
            controller.Stop();
            animator.Play("Hurt");
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
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
