using Unity.VisualScripting;
using UnityEngine;

namespace Platformer.FSM.Character
{
    // LShift
    // Idle, Move, Jump, DownJump, DoubleJump, Fall -> Dash
    // 현재 바라보는 방향 AddForce
    // 애니메이션 종료시 Idle

    // + a
    // 생성자에서 대시 거리 받고, 애니메이션 시간에 거쳐 해당 거리 이동
    // animator.GetCurrentAnimatorClipinfo(0)

    public class Dash : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.Dash;
        public override bool canExecute => base.canExecute &&
                                            machine.currentStateID == CharacterStateID.Fall &&
                                            controller.isGrounded;

        private Vector2 _dashDistance;

        public Dash(CharacterMachine machine, Vector2 dashDistance)
            : base(machine)
        {
            _dashDistance = dashDistance;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = false;
            controller.isMovable = false;
            controller.Stop();
            // rigidbody.AddForce(_dashDistance, ForceMode2D.Impulse);
            animator.Play("Dash");
        }

        public override CharacterStateID OnStateUpdate()
        {
            CharacterStateID nextID = base.OnStateUpdate();

            if (nextID == CharacterStateID.None)
                return id;

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {

                return id;
            }


            nextID = CharacterStateID.Idle;

            return nextID;
        }
    }
}
