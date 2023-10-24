using Unity.VisualScripting;
using UnityEngine;
using Ladder = Platformer.GameElements.Ladder;

namespace Platformer.FSM.Character
{
    // animator.speed 애니메이션 속도
    // 사다리 타는 동안만 애니메이션 재생

    public class LadderDown : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.LadderDown;
        public override bool canExecute => base.canExecute &&
                                            controller.isDownLadderDetected == true &&
                                            (machine.currentStateID == CharacterStateID.Fall ||
                                            machine.currentStateID == CharacterStateID.Jump ||
                                            machine.currentStateID == CharacterStateID.DoubleJump ||
                                            machine.currentStateID == CharacterStateID.LadderUp ||
                                            machine.currentStateID == CharacterStateID.Idle ||
                                            machine.currentStateID == CharacterStateID.Move);

        private Ladder _downLadder;
        
        public LadderDown(CharacterMachine machine)
            : base(machine)
        {
            
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            _downLadder = controller.downLadder;
            controller.isDirectionChangeable = false;
            controller.isMovable = false;

            controller.Stop();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;

            float posX = _downLadder.downEnter.x;
            float posY = transform.position.y > _downLadder.downEnter.y ? _downLadder.downEnter.y : transform.position.y;

            transform.position = new Vector3(posX, posY, transform.position.z);

            animator.Play("Ladder");
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            _downLadder = null;
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        public override CharacterStateID OnStateUpdate()
        {
            CharacterStateID nextID = base.OnStateUpdate();

            if (nextID == CharacterStateID.None)
                return id;

            if (controller.vertical < 0.0f)
                animator.speed = 1;

            else if (controller.vertical > 0.0f)
            {
                controller.upLadder = _downLadder;
                nextID = CharacterStateID.LadderUp;
            }

            

            else
                animator.speed = 0;


            if (controller.isGrounded)
                nextID = CharacterStateID.Idle;
                

            return nextID;
        }

        public override void OnStateFixedUpdate()
        {
            base.OnStateFixedUpdate();

            if (controller.vertical < 0.0f)
                transform.position += Vector3.down * 0.5f * Time.fixedDeltaTime;


            if (transform.position.y < _downLadder.downExit.y)
                transform.position = _downLadder.downExit;
        }
    }
}
