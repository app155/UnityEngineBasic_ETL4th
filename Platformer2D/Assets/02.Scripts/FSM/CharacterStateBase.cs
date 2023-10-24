﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Rendering;
using UnityEngine;
using CharacterController = Platformer.Controllers.CharacterController;

namespace Platformer.FSM
{
    public enum CharacterStateID
    {
        None,
        Idle,
        Move,
        Jump,
        DownJump,
        DoubleJump,
        Fall,
        Land,
        Crouch,
        Dash,
        Hurt,
        Die,
        Attack,
        WallSlide,
        Edge,
        EdgeClimb,
        LadderUp,
        sfasdf,
        LadderDown,
        Slide,
    }

    public abstract class CharacterStateBase : StateBase<CharacterStateID>
    {
        protected StateMachine<CharacterStateID> machine;
        protected CharacterController controller;
        protected Transform transform;
        protected Rigidbody2D rigidbody;
        protected Animator animator;
        protected CapsuleCollider2D trigger;
        protected CapsuleCollider2D collision;

        public CharacterStateBase(CharacterMachine machine)
            : base(machine)
        {
            this.machine = machine;
            this.controller = machine.owner;
            this.transform = machine.owner.transform;
            this.rigidbody = machine.owner.GetComponent<Rigidbody2D>();
            this.animator = machine.owner.GetComponentInChildren<Animator>();
            this.trigger = machine.owner.GetComponent<CapsuleCollider2D>();
            this.collision = machine.owner.GetComponentsInChildren<CapsuleCollider2D>().FirstOrDefault(x => x.isTrigger == false);
        }
    }
}
