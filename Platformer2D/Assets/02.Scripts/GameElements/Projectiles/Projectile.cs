using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.GameElements
{
    public class Projectile : MonoBehaviour
    {
        [HideInInspector] public Transform owner;
        [HideInInspector] public Vector3 velocity;
        [HideInInspector] public LayerMask targetMask;
        private LayerMask _boundMask;


        private void Awake()
        {
            _boundMask = 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Ground");
        }

        private void FixedUpdate()
        {
            Vector3 expected = transform.position + velocity * Time.fixedDeltaTime;

            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                                                 expected - transform.position,
                                                 Vector3.Distance(transform.position, expected),
                                                 _boundMask | targetMask);

            if (hit.collider != null)
            {
                int layerFlag = 1 << hit.collider.gameObject.layer;

                if ((layerFlag & _boundMask) > 0)
                {
                    OnHitBound(hit);
                }

                else if ((layerFlag & targetMask) > 0)
                {
                    OnHitTarget(hit);
                }
            }

            transform.position = expected;
        }

        protected virtual void OnHitBound(RaycastHit2D hit)
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnHitTarget(RaycastHit2D hit)
        {
            gameObject.SetActive(false);
        }
    }
}