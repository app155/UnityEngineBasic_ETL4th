using Unity.VisualScripting;
using UnityEngine;

namespace Platformer.FSM.Character
{
    // LShift
    // Idle, Move, Jump, DownJump, DoubleJump, Fall -> Dash
    // 현재 바라보는 방향 AddForce
    // 애니메이션 종료시 Idle

    // + a
    // 생성자에서 대시 거리 받고, 애니메이션 시간에 걸쳐 해당 거리 이동
    // animator.GetCurrentAnimatorClipinfo(0)

    public class Dash : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.Dash;
        public override bool canExecute => base.canExecute &&
                                            (machine.currentStateID == CharacterStateID.Idle ||
                                             machine.currentStateID == CharacterStateID.Move) &&
                                            controller.isGrounded;

        private int _step;
        private Vector2 _dashDistance;

        public Dash(CharacterMachine machine, float dashDistance)
            : base(machine)
        {
            _dashDistance = new Vector2(dashDistance, 0.0f);
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = false;
            controller.isMovable = false;
            controller.Stop();
            _step = 0;
            // rigidbody.AddForce(_dashDistance, ForceMode2D.Impulse);
            animator.Play("Dash");
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            controller.Stop();
        }

        public override CharacterStateID OnStateUpdate()
        {
            CharacterStateID nextID = base.OnStateUpdate();

            if (nextID == CharacterStateID.None)
                return id;

            switch (_step)
            {
                case 1:
                    nextID = CharacterStateID.Idle;
                    break;

                default:
                    break;
            }
            return nextID;
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();

            switch (_step)
            {
                case 0:
                    if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                        rigidbody.position += Vector2.right * controller.direction * _dashDistance / animator.GetCurrentAnimatorStateInfo(0).length * Time.fixedDeltaTime;
                    else
                        _step++;
                    break;

                default:
                    break;
            }
        }
    }
}
