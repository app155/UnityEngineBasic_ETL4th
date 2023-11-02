using Platformer.Animations;
using Platformer.FSM;
using Platformer.GameElements;
using Platformer.GameElements.Pool;
using UnityEngine;

namespace Platformer.Controllers
{
    public class DarkPiranhaPlantController : EnemyController
    {
        protected override void Awake()
        {
            base.Awake();

            machine = new EnemyMachine(this);
            var machineData = StateMachineDataSheet.GetDarkPiranhaPlantData(machine);
            machine.Init(machineData);
            onHpDepleted += (amount) => machine.ChangeState(CharacterStateID.Hurt);
            onHpMin += () => machine.ChangeState(CharacterStateID.Die);

            GetComponentInChildren<CharacterAnimationEvents>().onLaunchProjectile += () =>
            {
                ProjectileWithDamage projectile = 
                    PoolManager.instance.Get<ProjectileWithDamage>(PoolTag.Projectile_DarkPiranhaPlant);

                projectile.transform.position = transform.position
                                                + new Vector3(direction * 0.1f, 0.2f, 0.0f);

                projectile.owner = transform;
                projectile.damage = Random.Range(damageMin, damageMax);
                projectile.targetMask = _targetMask;
                projectile.velocity = Vector3.right * direction * 5.0f;
            };
        }
    }
}