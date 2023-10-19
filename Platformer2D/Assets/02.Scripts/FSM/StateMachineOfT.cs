using System;
using UnityEngine;
using Platformer.Controllers;
using CharacterController = Platformer.Controllers.CharacterController;
using System.Collections.Generic;

namespace Platformer.FSM
{
    public class StateMachine<T>
        where T : Enum
    {
        public T currentStateID;
        protected Dictionary<T, IState<T>> states;

        public void Init(IDictionary<T, IState<T>> copy)
        {
            states = new Dictionary<T, IState<T>>(copy);
        }

        public void UpdateState()
        {
            ChangeState(states[currentStateID].OnStateUpdate());
        }

        public bool ChangeState(T newStateID)
        {
            // 현재 상태와 동일한 상태로 바꿀 필요 없음.
            if (Comparer<T>.Default.Compare(newStateID, currentStateID) == 0)
                return false;

            if (states[newStateID].canExecute == false)
                return false;

            states[currentStateID].OnStateExit();
            currentStateID = newStateID;
            states[currentStateID].OnStateEnter();
            return true;
        }
    }
}