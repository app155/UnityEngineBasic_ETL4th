using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Multiplayer;

public class HostingState : NetworkBehaviour
{
    public const int MAX_CONNNECT_PAYLOAD = 1024;

    private void Setup()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
        NetworkManager.Singleton.StartHost();
    }

    public void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        var connectionData = request.Payload;
        
        if (connectionData.Length > MAX_CONNNECT_PAYLOAD)
        {
            response.Approved = false;
            return;
        }
    }
}
