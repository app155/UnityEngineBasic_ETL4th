using Platformer.FSM.Character;
using System.Collections.Generic;

namespace Platformer.FSM
{
    public static class StateMachineDataSheet
    {
        public static IDictionary<CharacterStateID, IState<CharacterStateID>> GetPlayerData(CharacterMachine machine)
        {
            return new Dictionary<CharacterStateID, IState<CharacterStateID>>()
            {
                { CharacterStateID.Idle, new Idle(machine) },
                { CharacterStateID.Move, new Move(machine) },
                { CharacterStateID.Fall, new Fall(machine, 0.75f) },
                { CharacterStateID.Jump, new Jump(machine, 2.8f) },
                { CharacterStateID.DoubleJump, new DoubleJump(machine, 2.8f) },
                { CharacterStateID.DownJump, new DownJump(machine) },
                { CharacterStateID.Crouch, new Crouch(machine) },
                { CharacterStateID.Land, new Land(machine) },
            };
        }
    }
}
