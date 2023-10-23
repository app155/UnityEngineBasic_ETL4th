using UnityEngine;

namespace Platformer.FSM.Character
{
    public class Fall : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.Fall;
        public override bool canExecute => base.canExecute &&
                                            (machine.currentStateID == CharacterStateID.Idle ||
                                             machine.currentStateID == CharacterStateID.Move ||
                                             machine.currentStateID == CharacterStateID.Jump ||
                                             machine.currentStateID == CharacterStateID.DoubleJump ||
                                             machine.currentStateID == CharacterStateID.DownJump ||
                                             machine.currentStateID == CharacterStateID.WallSlide);

        private float _landingDistance;
        private float _fallStartPosY;

        // 기반타입이 생성자 오버로드를 가지면,
        // 하위타입에서도 해당 오버로드에 인자를 전달할 수 있도록 파라미터를 가지는 오버로드가 필요
        public Fall(CharacterMachine machine, float landingDistance) : base(machine)
        {
            _landingDistance = landingDistance;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = true;
            controller.isMovable = false;
            _fallStartPosY = transform.position.y;
            animator.Play("Fall");
        }

        public override CharacterStateID OnStateUpdate()
        {
            CharacterStateID nextID = base.OnStateUpdate();

            if (nextID == CharacterStateID.None)
                return id;

            if (controller.isGrounded)
            {
                if (_fallStartPosY - transform.position.y < _landingDistance)
                    nextID = CharacterStateID.Idle;

                else
                    nextID = CharacterStateID.Land;
            }
                

            return nextID;
        }
    }
}
