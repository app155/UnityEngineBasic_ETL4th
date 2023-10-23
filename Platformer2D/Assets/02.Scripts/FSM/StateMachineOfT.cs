using System;
using UnityEngine;
using Platformer.Controllers;
using CharacterController = Platformer.Controllers.CharacterController;
using System.Collections.Generic;
using System.Linq;

namespace Platformer.FSM
{
    public class StateMachine<T>
        where T : Enum
    {
        public T currentStateID;
        public T previousStateID;
        protected Dictionary<T, IState<T>> states;
        private bool _isDirty;

        public void Init(IDictionary<T, IState<T>> copy)
        {
            states = new Dictionary<T, IState<T>>(copy);
            currentStateID = states.First().Key;
            states[currentStateID].OnStateEnter();
        }

        public void UpdateState()
        {
            ChangeState(states[currentStateID].OnStateUpdate());
        }

        public void FixedUpdateState()
        {
            states[currentStateID].OnStateFixedUpdate();
        }

        public void LateUpdateState()
        {
            _isDirty = false;
        }

        public bool ChangeState(T newStateID)
        {
            if (_isDirty)
                return false;

            // 현재 상태와 동일한 상태로 바꿀 필요 없음.
            if (Comparer<T>.Default.Compare(newStateID, currentStateID) == 0)
                return false;

            if (states[newStateID].canExecute == false)
                return false;

            _isDirty = true;
            states[currentStateID].OnStateExit();
            previousStateID = currentStateID;
            currentStateID = newStateID;
            states[currentStateID].OnStateEnter();
            return true;
        }
    }
}