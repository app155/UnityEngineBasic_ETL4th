using System;

namespace Platformer.FSM
{
    public abstract class StateBase<T> : IState<T>
        where T : Enum
    {
        public abstract T id { get; }

        public virtual bool canExecute => true;

        private bool _hasFixedUpdated;

        public StateBase(StateMachine<T> machine)
        {

        }

        public virtual void OnStateEnter()
        {
            _hasFixedUpdated = false;
        }

        public virtual void OnStateExit()
        {

        }

        public virtual void OnStateFixedUpdate()
        {
            if (_hasFixedUpdated == false)
                _hasFixedUpdated = true;
        }

        public virtual T OnStateUpdate()
        {
            return _hasFixedUpdated ? id : default(T); // default -> None 반환
        }
    }
}
