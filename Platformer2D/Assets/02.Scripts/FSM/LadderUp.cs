using Unity.VisualScripting;
using UnityEngine;
using Ladder = Platformer.GameElements.Ladder;

namespace Platformer.FSM.Character
{
    // animator.speed 애니메이션 속도
    // 사다리 타는 동안만 애니메이션 재생

    public class LadderUp : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.LadderUp;
        public override bool canExecute => base.canExecute &&
                                            controller.isUpLadderDetected == true &&
                                            (machine.currentStateID == CharacterStateID.Fall ||
                                            machine.currentStateID == CharacterStateID.Jump ||
                                            machine.currentStateID == CharacterStateID.DoubleJump ||
                                            machine.currentStateID == CharacterStateID.LadderDown ||
                                            machine.currentStateID == CharacterStateID.Idle ||
                                            machine.currentStateID == CharacterStateID.Move);

        private Ladder _upLadder;
        
        public LadderUp(CharacterMachine machine)
            : base(machine)
        {
            
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            _upLadder = controller.upLadder;
            controller.isDirectionChangeable = false;
            controller.isMovable = false;

            controller.Stop();
            rigidbody.bodyType = RigidbodyType2D.Kinematic;

            float posX = _upLadder.upEnter.x;
            float posY = transform.position.y < _upLadder.upEnter.y ? _upLadder.upEnter.y : transform.position.y;

            transform.position = new Vector3(posX, posY, transform.position.z);

            animator.Play("Ladder");
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            _upLadder = null;
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        public override CharacterStateID OnStateUpdate()
        {
            CharacterStateID nextID = base.OnStateUpdate();

            if (nextID == CharacterStateID.None)
                return id;

            if (controller.vertical > 0.0f)
                animator.speed = 1;

            else if (controller.vertical < 0.0f)
            {
                controller.downLadder = _upLadder;
                nextID = CharacterStateID.LadderDown;
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

            if (controller.vertical > 0.0f)
                transform.position += Vector3.up * 0.5f * Time.fixedDeltaTime;


            if (transform.position.y > _upLadder.upExit.y)
                transform.position = _upLadder.top;
        }
    }
}
