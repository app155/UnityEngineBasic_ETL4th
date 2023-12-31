﻿using Unity.VisualScripting;
using UnityEngine;

namespace Platformer.FSM.Character
{
    // OnHpMin에 전환 구독

    public class Die : CharacterStateBase
    {
        public override CharacterStateID id => CharacterStateID.Die;
        public override bool canExecute => base.canExecute;

        
        public Die(CharacterMachine machine)
            : base(machine)
        {

        }

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            controller.isDirectionChangeable = false;
            controller.isMovable = false;
            controller.Stop();
            trigger.enabled = false;
            animator.Play("Die");
        }

        public override CharacterStateID OnStateUpdate()
        {
            CharacterStateID nextID = base.OnStateUpdate();

            if (nextID == CharacterStateID.None)
                return id;

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                transform.gameObject.SetActive(false);

            return nextID;
        }
    }
}
