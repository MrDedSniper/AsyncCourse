using System;
using UnityEngine;
using Mirror;

public class PlayerNameInput : NetworkBehaviour
{
    private PlayerInfo _playerInfo;

    private void Start()
    {
        _playerInfo = GetComponent<PlayerInfo>();
    }

    public void SetPlayerName(string name)
    {
        if (string.IsNullOrEmpty(name)) return;

        PlayerPrefs.SetString("PlayerName", name);
        Debug.Log($"Player name set to {name}");

        if (NetworkClient.isConnected)
        {
            CmdSetPlayerName(name);
        }
    }

    [Command]
    private void CmdSetPlayerName(string name)
    {
        RpcSetPlayerName(name);
    }

    [ClientRpc]
    private void RpcSetPlayerName(string name)
    {
        if (!_playerInfo.playerNames.ContainsKey(connectionToClient.connectionId))
        {
            _playerInfo.playerNames.Add(connectionToClient.connectionId, name);
            Debug.Log("Player name set to name");
        }

        else
        {
            string playerName = _playerInfo.playerNames[connectionToClient.connectionId];
            Debug.Log("[playerName]: name"); 
        }
    }

    public void OnCloseInput()
    {
        CloseInput();
    }

    private void CloseInput()
    {
        gameObject.SetActive(false);
    }
}