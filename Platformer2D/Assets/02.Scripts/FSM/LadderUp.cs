using Unity.VisualScripting;
using UnityEngine;

namespace Platformer.FSM.Character
{
    // animator.speed 애니메이션 속도
    // 사다리 타는 동안만 애니메이션 재생

    public class LadderUp : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.LadderUp;
        public override bool canExecute => base.canExecute &&
                                            (machine.currentStateID == CharacterStateID.Fall ||
                                            machine.currentStateID == CharacterStateID.Jump ||
                                            machine.currentStateID == CharacterStateID.DoubleJump ||
                                            machine.currentStateID == CharacterStateID.Idle ||
                                            machine.currentStateID == CharacterStateID.Move);

        
        public LadderUp(CharacterMachine machine)
            : base(machine)
        {
            
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = false;
            controller.isMovable = false;
            controller.Stop();

            if (controller.transform.position.y < controller.upLadder.upEnter.y)
                transform.position = controller.upLadder.upEnter;

            animator.Play("Ladder");
        }

        public override CharacterStateID OnStateUpdate()
        {
            CharacterStateID nextID = base.OnStateUpdate();

            if (nextID == CharacterStateID.None)
                return id;

            if (controller.vertical > 0.0f)
            {
                animator.speed = 1;
                transform.position += Vector3.up * 0.5f * Time.deltaTime;
            }

            else
            {
                animator.speed = 0;
            }


            return nextID;
        }
    }
}
