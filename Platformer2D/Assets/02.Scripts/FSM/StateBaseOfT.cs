using System;

namespace Platformer.FSM
{
    public abstract class StateBase<T> : IState<T>
        where T : Enum
    {
        public abstract T id { get; }

        public virtual bool canExecute => true;

        public StateBase(StateMachine<T> machine)
        {

        }

        public virtual void OnStateEnter()
        {

        }

        public virtual void OnStateExit()
        {

        }

        public virtual void OnStateFixedUpdate()
        {

        }

        public virtual T OnStateUpdate()
        {
            return id;
        }
    }
}
