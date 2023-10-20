using UnityEngine;

namespace Platformer.FSM.Character
{
    public class DownJump : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.DownJump;
        public override bool canExecute => base.canExecute &&
                                            controller.hasJumped == false &&
                                            (machine.currentStateID == CharacterStateID.Crouch) &&
                                            controller.isGrounded &&
                                            controller.isGroundBelowExist;
        private float _jumpForce;
        private float _groundIgnoreTime;
        private float _elapsedTime; // 무시 시작하고 경과한 시간

        public DownJump(CharacterMachine machine, float jumpForce = 1.0f, float groundIgnoreTime = 0.5f)
            : base(machine)
        {
            _jumpForce = jumpForce;
            _groundIgnoreTime = groundIgnoreTime;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = true;
            controller.isMovable = false;
            controller.hasJumped = true;
            controller.hasDoubleJumped = false;
            animator.Play("Jump");
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);
            rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            Physics2D.IgnoreCollision(collision, controller.ground, true);
            _elapsedTime = 0.0f;
        }

        public override CharacterStateID OnStateUpdate()
        {
            CharacterStateID nextID = base.OnStateUpdate();

            if (nextID == CharacterStateID.None)
                return id;

            if (rigidbody.velocity.y <= 0.0f)
                nextID = CharacterStateID.Fall;

            return nextID;
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();

            if (_elapsedTime > _groundIgnoreTime)
                Physics2D.IgnoreCollision(collision, controller.ground, false);
                
            else
                _elapsedTime += Time.fixedDeltaTime;
        }
    }
}
