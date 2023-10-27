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
                                             machine.currentStateID == CharacterStateID.Move ||
                                             machine.currentStateID == CharacterStateID.Jump ||
                                             machine.currentStateID == CharacterStateID.DoubleJump ||
                                             machine.currentStateID == CharacterStateID.DownJump ||
                                             machine.currentStateID == CharacterStateID.Fall);

        private float _distance;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;

        public Dash(CharacterMachine machine, float distance)
            : base(machine)
        {
            _distance = distance;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = false;
            controller.isMovable = false;
            controller.Stop();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _startPosition = transform.position;
            _targetPosition = transform.position + Vector3.right * controller.direction * _distance;
            
            animator.Play("Dash");
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        public override CharacterStateID OnStateUpdate()
        {
            CharacterStateID nextID = base.OnStateUpdate();

            if (nextID == CharacterStateID.None)
                return id;

            float t = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            t = Mathf.Log10(10 + t * 90) - 1;

            Vector3 expected = 
                Vector3.Lerp(_startPosition, _targetPosition, t);

            // expected 위치 유효한지 확인
            bool isValid = true;
            if (Physics2D.OverlapCapsule((Vector2)expected + trigger.offset,
                                          trigger.size,
                                          trigger.direction,
                                          0.0f,
                                          1 << LayerMask.NameToLayer("Wall")))
            {
                _startPosition = transform.position;
                _targetPosition = transform.position;
                isValid = false;
            }

            if (isValid)
                transform.position = expected;

            if (t >= 1.0f)
                nextID = CharacterStateID.Idle;

            return nextID;
        }

    }
}
