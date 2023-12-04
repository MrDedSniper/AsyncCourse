using UnityEngine;
using Mirror;

public class PlayerNameSetter : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        string playerName = PlayerPrefs.GetString("PlayerName");
        if (!string.IsNullOrEmpty(playerName))
        {
            CmdSetPlayerName(playerName);
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
        gameObject.name = name;
    }
}