using Platformer.GameElements.Pool;
using Platformer.Stats;
using UnityEngine;

namespace Platformer.GameElements
{
    public class ProjectileWithDamage : Projectile
    {
        [HideInInspector] public float damage;

        protected override void OnHitTarget(RaycastHit2D hit)
        {
            base.OnHitTarget(hit);

            if (hit.collider.TryGetComponent(out IHp target))
            {
                target.DepleteHp(owner, damage);

                ParticleSystem system =
                    ParticleSystemPoolManager.instance.Get(PoolTag.Particle_ProjectileExplosion);

                system.transform.position = transform.position;
                system.transform.rotation = Quaternion.Euler(0, Mathf.Sign(velocity.x) * -90.0f, 0);
            }
        }
    }
}