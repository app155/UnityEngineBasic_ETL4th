using Platformer.FSM;
using UnityEngine;

namespace Platformer.Controllers
{
    public class SlugController : EnemyController
    {
        [SerializeField] private Collider2D _groundForward;
        [SerializeField] private Vector2 _detectGroundForwardOffset;
        [SerializeField] private Vector2 _detectGroundForwardSize;

        protected override void Start()
        {
            base.Start();

            machine = new EnemyMachine(this);
            var machineData = StateMachineDataSheet.GetSlugData(machine);
            machine.Init(machineData);
            onHpDepleted += (amount) => machine.ChangeState(CharacterStateID.Hurt);
            onHpMin += () => machine.ChangeState(CharacterStateID.Die);
        }

        protected override void FixedUpdate()
        {


            base.FixedUpdate();
        }

        void DetectGroundForward()
        {

        }
    }
}