using Unity.VisualScripting;
using UnityEngine;

namespace Platformer.FSM.Character
{
    // OnHpMin에 전환 구독

    public class Die : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.Die;
        public override bool canExecute => base.canExecute;

        
        public Die(CharacterMachine machine)
            : base(machine)
        {
            controller.onHpMin += ChangeStateToDie;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = false;
            controller.isMovable = false;
            controller.hasJumped = true;
            controller.hasDoubleJumped = true;
            controller.Stop();
            animator.Play("Die");
        }

        public void ChangeStateToDie()
        {
            machine.ChangeState(CharacterStateID.Die);
        }
    }
}
