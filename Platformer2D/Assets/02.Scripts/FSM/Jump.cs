﻿using UnityEngine;

namespace Platformer.FSM.Character
{
    public class Jump : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.Jump;
        public override bool canExecute => base.canExecute &&
                                            controller.hasJumped == false &&
                                            machine.currentStateID == CharacterStateID.WallSlide ||
                                            machine.currentStateID == CharacterStateID.UpLadderClimb ||
                                            machine.currentStateID == CharacterStateID.DownLadderClimb ||
                                            ((machine.currentStateID == CharacterStateID.Idle ||
                                              machine.currentStateID == CharacterStateID.Move) &&
                                              controller.isGrounded);
        private float _jumpForce;

        // 기반타입이 생성자 오버로드를 가지면,
        // 하위타입에서도 해당 오버로드에 인자를 전달할 수 있도록 파라미터를 가지는 오버로드가 필요
        public Jump(CharacterMachine machine, float jumpForce)
            : base(machine)
        {
            _jumpForce = jumpForce;
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = true;
            controller.isMovable = false;
            controller.hasJumped = true;
            controller.hasDoubleJumped = false;
            animator.Play("Jump");

            float velocityX = (machine.previousStateID == CharacterStateID.WallSlide ||
                               machine.previousStateID == CharacterStateID.UpLadderClimb ||
                               machine.previousStateID == CharacterStateID.DownLadderClimb) ?
                               controller.horizontal * controller.moveSpeed : rigidbody.velocity.x;

            rigidbody.velocity = new Vector2(velocityX, 0.0f);
            rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
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
    }
}
