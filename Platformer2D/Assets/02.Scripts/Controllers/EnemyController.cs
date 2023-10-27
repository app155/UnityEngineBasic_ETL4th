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
    // HPBar 갱신하는 기능 구독 체력변화시마다 체력 갱신.

    // 2. 이동불가영역 감지
    // Enemycontroller fixedupdate override
    // 이동하려는 위치가 유효한 위치 아니라면 제자리.

    // 3. EnemySpawner
    // 특정 적 소환하는 클래스 작성
    // 소환 범위 지정 (Box 범위)
    // 소환 주기마다 소환 범위 내 랜덤 위치 선정
    // 해당 위치에서 아래로 Raycast -> 유효한 땅 발견시 소환

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

        public bool isGroundForwardExist
        {
            get
            {
                Vector2 detectGroundForwardStartPos
                    = rigidbody.position + new Vector2(_detectGroundForwardOffset.x * direction, _detectGroundForwardOffset.y);

                RaycastHit2D hit = Physics2D.Raycast(detectGroundForwardStartPos, Vector2.down, _detectGroundForwardDistance, groundMask);

                _groundForward = hit.collider;

                return _groundForward;
            }
        }

        private float _horizontal;
        private float _vertical;

        [SerializeField] private Collider2D _groundForward;
        [SerializeField] private Vector2 _detectGroundForwardOffset;
        [SerializeField] private float _detectGroundForwardDistance;

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
        [SerializeField] private float _slopeAngle = 45.0f;

        private CapsuleCollider2D _trigger;

        protected override void Awake()
        {
            base.Awake();
            _trigger = GetComponent<CapsuleCollider2D>();
            _ai = AI.Think;
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
                    {
                        _ai = AI.ExecuteRandomBehaviour;
                    }

                    break;
                case AI.ExecuteRandomBehaviour:
                    {
                        var nextID = _behaviours[Random.Range(0, _behaviours.Count)];

                        if (machine.ChangeState(nextID))
                        {
                            _behaviourTimer = Random.Range(_behaviourTimeMin, _behaviourTimeMax);
                            _horizontal = Random.Range(-1.0f, 1.0f);
                            _ai = AI.WaitUntilBehaviour;
                        }

                        else
                        {
                            _ai = AI.Think;
                        }
                    }
                    
                    break;
                case AI.WaitUntilBehaviour:
                    if (_behaviourTimer <= 0)
                    {
                        _ai = AI.Think;
                    }

                    else
                    {
                        _behaviourTimer -= Time.deltaTime;
                    }

                    break;
                case AI.Follow:
                    {
                        if (_target == null)
                        {
                            _ai = AI.Think;
                            return;
                        }

                        if (Vector2.Distance(transform.position, _target.position) > _targetDetectRange)
                        {
                            _target = null;
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
                    }
                default:
                    break;
            }
        }

        protected override void FixedUpdate()
        {
            if (isGroundForwardExist == false)
                return;

            base.FixedUpdate();

            //if (machine.currentStateID != CharacterStateID.Move)
            //{
            //    base.FixedUpdate();
            //}

            //else
            //{
            //    machine.FixedUpdateState();

            //    Vector2 expected = rigidbody.position + move * Time.fixedDeltaTime;
            //    float distanceX = Mathf.Abs(expected.x - rigidbody.position.x);
            //    float height = distanceX * Mathf.Tan(_slopeAngle * Mathf.Deg2Rad);
            //    Vector2 origin = expected + Vector2.up * height;
            //    RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, height * 2, groundMask);

            //    Debug.DrawRay(origin, Vector2.down * height * 2, Color.red);
            //    Debug.Log(hit);

            //    if (hit.collider)
            //    {
            //        rigidbody.position = hit.point;
            //        Debug.Log(hit.point);
            //    }
            //}
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
            DrawDetectGroundForwardGizmos();
        }

        void DrawDetectTargetGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _targetDetectRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }

        void DrawDetectGroundForwardGizmos()
        {
            Gizmos.color = Color.blue;

            Vector2 detectGroundForwardStartPos
                = rigidbody.position + new Vector2(_detectGroundForwardOffset.x * direction, _detectGroundForwardOffset.y);

            Gizmos.DrawLine(detectGroundForwardStartPos, detectGroundForwardStartPos + Vector2.down * _detectGroundForwardDistance);
        }
    }
}
