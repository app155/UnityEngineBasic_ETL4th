using Platformer.FSM;
using UnityEngine;

namespace Platformer.Controllers
{
    public class PiranhaController : EnemyController
    {
        protected override void Awake()
        {
            base.Awake();

            machine = new EnemyMachine(this);
            var machineData = StateMachineDataSheet.GetPiranhaPlantData(machine);
            machine.Init(machineData);
            onHpDepleted += (amount) => machine.ChangeState(CharacterStateID.Hurt);
            onHpMin += () => machine.ChangeState(CharacterStateID.Die);
        }
    }
}