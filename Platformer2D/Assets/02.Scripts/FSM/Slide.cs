using Unity.VisualScripting;
using UnityEngine;

namespace Platformer.FSM.Character
{
    // Crouch + Dash

    public class Slide : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.Slide;
        public override bool canExecute => base.canExecute &&
                                            (machine.currentStateID == CharacterStateID.Idle ||
                                             machine.currentStateID == CharacterStateID.Move ||
                                             machine.currentStateID == CharacterStateID.Crouch) &&
                                             controller.isGrounded;

        private float _distance;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private Vector2 _originalColliderOffset;
        private Vector2 _originalColliderSize;
        private Vector2 _slideColliderOffset;
        private Vector2 _slideColliderSize;

        public Slide(CharacterMachine machine, Vector2 slideColliderOffset, Vector2 slideColliderSize, float distance)
            : base(machine)
        {
            _originalColliderOffset = trigger.offset;
            _originalColliderSize = trigger.size;
            _slideColliderOffset = slideColliderOffset;
            _slideColliderSize = slideColliderSize;
            _distance = distance;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = false;
            controller.isMovable = false;
            controller.Stop();
            collision.offset = trigger.offset = _slideColliderOffset;
            collision.size = trigger.size = _slideColliderSize;
            rigidbody.bodyType = RigidbodyType2D.Kinematic;
            _startPosition = transform.position;
            _targetPosition = transform.position + Vector3.right * controller.direction * _distance;
            
            animator.Play("Slide");
        }

        public override void OnStateExit()
        {
            base.OnStateExit();

            collision.offset = trigger.offset = _originalColliderOffset;
            collision.size = trigger.size = _originalColliderSize;

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
