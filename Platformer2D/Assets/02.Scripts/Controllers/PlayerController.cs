using Platformer.FSM;
using System.Linq;
using UnityEngine;

namespace Platformer.Controllers
{
    public class PlayerController : CharacterController
    {
        public override float horizontal => Input.GetAxis("Horizontal");

        public override float vertical => Input.GetAxis("Vertical");

        protected override void Start()
        {
            base.Start();

            machine = new PlayerMachine(this);
            var machineData = StateMachineDataSheet.GetPlayerData(machine);
            machine.Init(machineData);
            onHpDepleted += (amount) => machine.ChangeState(CharacterStateID.Hurt);
            onHpMin += () => machine.ChangeState(CharacterStateID.Die);
        }

        protected override void Update()
        {
            base.Update();

            if (isUpLadderDetected && Input.GetKey(KeyCode.UpArrow))
            {
                machine.ChangeState(CharacterStateID.LadderUp);
            }

            if (Input.GetKey(KeyCode.LeftAlt))
            {
                if (machine.ChangeState(CharacterStateID.DownJump) == false &&
                    machine.ChangeState(CharacterStateID.Jump) == false &&
                    Input.GetKeyDown(KeyCode.LeftAlt))
                {
                    machine.ChangeState(CharacterStateID.DoubleJump);
                }
            }

            if (Input.GetKey(KeyCode.RightArrow) ||
                Input.GetKey(KeyCode.LeftArrow))
            {
                machine.ChangeState(CharacterStateID.WallSlide);
            }

            else if (machine.currentStateID == CharacterStateID.WallSlide)
            {
                machine.ChangeState(CharacterStateID.Idle);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                machine.ChangeState(CharacterStateID.Crouch);
            }

            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                if (machine.currentStateID == CharacterStateID.Crouch)
                    machine.ChangeState(CharacterStateID.Idle);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
                machine.ChangeState(CharacterStateID.Dash);

            //if (Input.GetKeyDown(KeyCode.LeftAlt))
            //{
            //    if (machine.ChangeState(CharacterStateID.Jump) == false &&
            //        machine.ChangeState(CharacterStateID.DoubleJump) == false)
            //    {

            //    }
            //}
        }
    }
}
