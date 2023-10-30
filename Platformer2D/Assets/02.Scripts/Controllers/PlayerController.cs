using Platformer.Effects;
using Platformer.FSM;
using Platformer.GameElements;
using System.Linq;
using UnityEngine;

namespace Platformer.Controllers
{
    public class PlayerController : CharacterController
    {
        public override float horizontal => Input.GetAxis("Horizontal");

        public override float vertical => Input.GetAxis("Vertical");

        private float _invincibleTimer;

        public void SetInvincible(float duration)
        {
            if (duration < _invincibleTimer)
                return;

            _invincibleTimer = duration;
            invincible = true;
        }

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

            if (_invincibleTimer > 0)
            {
                _invincibleTimer -= Time.deltaTime;

                if (_invincibleTimer <= 0)
                    invincible = false;
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

            if (Input.GetKey(KeyCode.UpArrow))
            {
                machine.ChangeState(CharacterStateID.UpLadderClimb);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                machine.ChangeState(CharacterStateID.DownLadderClimb);
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
            {
                machine.ChangeState(CharacterStateID.Dash);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                machine.ChangeState(CharacterStateID.Slide);
            }
                
            if (Input.GetKey(KeyCode.LeftControl))
            {
                machine.ChangeState(CharacterStateID.Attack);
            }
        }

        public override void DepleteHp(object subject, float amount)
        {
            base.DepleteHp(subject, amount);

            SetInvincible(0.7f);

            if (subject.GetType().Equals(typeof(Transform)))
                Knockback(Vector2.right * (((Transform)subject).position.x - transform.position.x < 0 ? 1.0f : -1.0f) * 1.0f
                          + Vector2.up * 1.0f);

            DamagePopUp damagePopUp = PoolManager<DamagePopUp>.instance.GetPool<DamagePopUp>(PoolTag.DamagePopUp_Player).Get();
            damagePopUp.transform.position = transform.position + Vector3.up * 0.2f;
            damagePopUp.Show(amount);
        }
    }
}
