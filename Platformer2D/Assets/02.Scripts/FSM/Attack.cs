using Platformer.Animations;
using Platformer.Stats;
using Platformer.Datum;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.FSM.Character
{
    public class Attack : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.Attack;

        public override bool canExecute
        {
            get
            {
                if (base.canExecute == false)
                    return false;

                float elapsedTime = Time.time - _exitTimeMark;

                if (_comboStack > 0 && elapsedTime >= _comboResetTime)
                {
                    _comboStack = 0;
                    return false;
                }

                if (_comboStack > _comboStackMax)
                    return false;

                if ((_comboStack == 0 || (_comboStack > 0 && _hasHit)) &&
                    (machine.currentStateID == CharacterStateID.Idle ||
                     machine.currentStateID == CharacterStateID.Move ||
                     machine.currentStateID == CharacterStateID.Crouch ||
                     machine.currentStateID == CharacterStateID.Jump ||
                     machine.currentStateID == CharacterStateID.DownJump ||
                     machine.currentStateID == CharacterStateID.DoubleJump ||
                     machine.currentStateID == CharacterStateID.Fall))
                {
                    return true;
                }

                return false;
            }
        }

        private int _comboStackMax;
        private int _comboStack;
        private float _comboResetTime;
        private float _exitTimeMark;
        private bool _hasHit;

        private SkillCastSetting[] _attackSettings;
        private List<IHp> _targets = new List<IHp>();
        private CharacterAnimationEvents _animationEvents;


        public Attack(CharacterMachine machine, float comboResetTime, SkillCastSetting[] attackSettings)
            : base(machine)
        {
            _attackSettings = attackSettings;
            _comboStackMax = attackSettings.Length - 1;
            _comboResetTime = comboResetTime;

            _animationEvents = animator.GetComponent<CharacterAnimationEvents>();
            _animationEvents.onHit = () =>
            {
                foreach (var target in _targets)
                {
                    if (target == null)
                        continue;

                    float damage = Random.Range(controller.damageMin, controller.damageMax) * _attackSettings[_comboStack - 1].damageGain;

                    target.DepleteHp(transform, damage);
                }

                _hasHit = true;
            };
        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();

            controller.isDirectionChangeable = false;
            controller.isMovable = controller.isGrounded;
            _hasHit = false;

            SkillCastSetting setting = _attackSettings[_comboStack];
            RaycastHit2D[] hits =
                Physics2D.BoxCastAll(origin: rigidbody.position + new Vector2(setting.castCenter.x * controller.direction, setting.castCenter.y),
                                     size: setting.castSize,
                                     angle: 0.0f,
                                     direction: Vector2.right * controller.direction,
                                     distance: setting.castDistance,
                                     layerMask: setting.targetMask);

            Vector2 origin = rigidbody.position + new Vector2(setting.castCenter.x * controller.direction, setting.castCenter.y);
            Vector2 size = setting.castSize;
            float distance = setting.castDistance;

            _targets.Clear();

            for (int i = 0; i < hits.Length; i++)
            {
                if (_targets.Count >= setting.targetMax)
                    break;

                if (hits[i].collider.TryGetComponent(out IHp target))
                    _targets.Add(target);
            }

            animator.SetFloat("comboStack", _comboStack++);
            animator.Play("Attack");

            Debug.DrawLine(origin + new Vector2(-size.x / 2.0f * controller.direction, +size.y / 2.0f),
               origin + new Vector2(+size.x / 2.0f * controller.direction, +size.y / 2.0f) + Vector2.right * controller.direction * distance,
               Color.red,
               animator.GetCurrentAnimatorStateInfo(0).length);

            Debug.DrawLine(origin + new Vector2(-size.x / 2.0f * controller.direction, -size.y / 2.0f),
                           origin + new Vector2(+size.x / 2.0f * controller.direction, -size.y / 2.0f) + Vector2.right * controller.direction * distance,
                           Color.red,
                           animator.GetCurrentAnimatorStateInfo(0).length);

            Debug.DrawLine(origin + new Vector2(-size.x / 2.0f * controller.direction, +size.y / 2.0f),
                           origin + new Vector2(-size.x / 2.0f * controller.direction, -size.y / 2.0f),
                           Color.red,
                           animator.GetCurrentAnimatorStateInfo(0).length);

            Debug.DrawLine(origin + new Vector2(+size.x / 2.0f * controller.direction, +size.y / 2.0f) + Vector2.right * controller.direction * distance,
                           origin + new Vector2(+size.x / 2.0f * controller.direction, -size.y / 2.0f) + Vector2.right * controller.direction * distance,
                           Color.red,
                           animator.GetCurrentAnimatorStateInfo(0).length);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            _exitTimeMark = Time.time;
        }

        public override CharacterStateID OnStateUpdate()
        {
            CharacterStateID nextID = base.OnStateUpdate();

            if (nextID == CharacterStateID.None)
                return id;

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                nextID = CharacterStateID.Idle;

            if (controller.isGrounded)
                controller.move = new Vector2(controller.horizontal * controller.moveSpeed * 0.1f, 0.0f);

            return nextID;
        }
    }
}
