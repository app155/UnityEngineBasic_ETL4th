using Platformer.FSM;
using Platformer.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Platformer.Controllers
{
    // 1. MiniStatusUI Script
    // transform.root.GetComponent<IHp>()
    // HPBar �����ϴ� ��� ���� ü�º�ȭ�ø��� ü�� ����.

    // 2. �̵��Ұ����� ����
    // Enemycontroller fixedupdate override
    // �̵��Ϸ��� ��ġ�� ��ȿ�� ��ġ �ƴ϶�� ���ڸ�.

    // 3. EnemySpawner
    // Ư�� �� ��ȯ�ϴ� Ŭ���� �ۼ�
    // ��ȯ ���� ���� (Box ����)
    // ��ȯ �ֱ⸶�� ��ȯ ���� �� ���� ��ġ ����
    // �ش� ��ġ���� �Ʒ��� Raycast -> ��ȿ�� �� �߽߰� ��ȯ

    public enum AI
    {
        None,
        Think,
        ExecuteRandomBehaviour,
        WaitUntilBehaviour,
        Follow,
    }

    public class EnemyController : CharacterController
    {
        public override float horizontal => _horizontal;

        public override float vertical => _vertical;

        private float _horizontal;
        private float _vertical;

        [SerializeField] private AI _ai;
        private Transform _target;
        [SerializeField] private float _targetDetectRange;
        [SerializeField] private bool _autoFollow;
        [SerializeField] private bool _attackEnable;
        [SerializeField] private float _attackRange;
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private List<CharacterStateID> _behaviours;
        [SerializeField] private float _behaviourTimeMin;
        [SerializeField] private float _behaviourTimeMax;
        private float _behaviourTimer;

        private CapsuleCollider2D _trigger;

        protected override void Awake()
        {
            base.Awake();
            _trigger = GetComponent<CapsuleCollider2D>();
        }

        protected override void Update()
        {
            UpdateAI();

            base.Update();
        }

        private void UpdateAI()
        {
            if (_autoFollow)
            {
                if (_target == null)
                {
                    Collider2D col = Physics2D.OverlapCircle(rigidbody.position, _targetDetectRange, _targetMask);

                    if (col)
                        _target = col.transform;
                }
            }

            if (_target)
            {
                _ai = AI.Follow;
            }

            switch (_ai)
            {
                case AI.Think:
                    break;
                case AI.ExecuteRandomBehaviour:
                    break;
                case AI.WaitUntilBehaviour:
                    break;
                case AI.Follow:
                    if (_target == null)
                    {
                        _ai = AI.Think;
                        return;
                    }

                    if (_attackEnable && Vector2.Distance(transform.position, _target.position) <= _attackRange)
                    {
                        machine.ChangeState(CharacterStateID.Attack);
                    }
                    else
                    {
                        machine.ChangeState(CharacterStateID.Move);
                    }

                    if (transform.position.x < _target.position.x - _trigger.size.x)
                    {
                        _horizontal = 1.0f;
                    }
                    else if (transform.position.x > _target.position.x + _trigger.size.x)
                    {
                        _horizontal = -1.0f;
                    }

                    break;
                default:
                    break;
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void DepleteHp(object subject, float amount)
        {
            base.DepleteHp(subject, amount);

            if (subject.GetType().Equals(typeof(Transform)))
                Knockback(Vector2.right * (((Transform)subject).position.x - transform.position.x < 0 ? 1.0f : -1.0f) * 1.0f);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if ((1 << collision.gameObject.layer & _targetMask) > 0)
            {
                if (collision.TryGetComponent(out IHp target))
                {
                    if (target.invincible == false)
                        target.DepleteHp(transform, Random.Range(damageMin, damageMax));
                }
            }
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            DrawDetectTargetGizmos();
        }

        void DrawDetectTargetGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _targetDetectRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }

    }
}
