using System.Collections;
using System.Collections.Generic;
using CharacterController = RPG.Controllers.CharacterController;
using Unity.Netcode;
using UnityEngine;

public class ClientBehaviour : NetworkBehaviour
{
    NetworkVariable<int> count = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            transform.position = GameObject.Find("SpawnPoint").transform.position;
        }
    }

    private void Update()
    {
        Debug.Log($"{OwnerClientId}'s count = {count.Value}");

        if (!IsOwner)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SubmitJumpServerRpc();
        }
    }

    [ServerRpc]
    private void SubmitJumpServerRpc(ServerRpcParams rpcParams = default)
    {
        ulong clientID = rpcParams.Receive.SenderClientId;

        if (TryJump(clientID))
        {
            MakeJumpClientRpc(clientID);
        }
    }

    private bool TryJump(ulong clientID)
    {
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(clientID, out NetworkClient client))
        {
            if (client.PlayerObject.GetComponent<CharacterController>().ChangeState(RPG.Controllers.State.Jump))
            {
                return true;
            }
        }
        return false;
    }

    [ClientRpc]
    private void MakeJumpClientRpc(ulong clientID, ClientRpcParams rpcParams = default)
    {
        if (IsClient)
        {
            if (OwnerClientId == clientID)
            {
                if (GetComponent<CharacterController>().ChangeState(RPG.Controllers.State.Jump))
                {

                }
            }
        }
    }
}
