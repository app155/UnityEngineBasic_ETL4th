using RPG.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]
public class ChatManager : NetworkBehaviour
{
    public static ChatManager instance;

    private List<string> _chatLogs = new List<string>();
    private const int _chatLogMax = 10;
    public event Action<IEnumerable<string>> onChatLogChanged;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var ui = UIManager.instance.Get<ChatEnterUI>();
            if (ui.gameObject.activeSelf)
            {
                if (string.IsNullOrEmpty(ui.message) == false)
                {
                    SendChatMessageServerRpc(ui.message);
                }
                ui.Hide();
            }

            else
            {
                ui.Show();
            }
                
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SendChatMessageServerRpc(string message, ServerRpcParams rpcParams = default)
    {
        _chatLogs.Add(message);

        if (_chatLogs.Count > _chatLogMax)
        {
            _chatLogs.RemoveAt(0);
        }

        onChatLogChanged?.Invoke(_chatLogs);

        ReceiveChatMessageClientRpc(message);
    }

    [ClientRpc]
    private void ReceiveChatMessageClientRpc(string message, ClientRpcParams rpcParams = default)
    {
        _chatLogs.Add(message);

        if (_chatLogs.Count > _chatLogMax)
        {
            _chatLogs.RemoveAt(0);
        }

        onChatLogChanged?.Invoke(_chatLogs);

        var playerStatusUI = UIManager.instance.Get<PlayerStatusUI>();
        playerStatusUI.ShowChatBalloon(OwnerClientId, message);
    }
}
