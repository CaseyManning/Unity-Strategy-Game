using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class NetworkLobbyHook : LobbyHook {

	public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer) {
		LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer> ();
//		SetUpLocalPlayer localPlayer = gamePlayer.GetComponent<SetUpLocalPlayer> ();
//
//		localPlayer.pname = lobby.name;
//		localPlayer.playerColor = lobby.playerColor;
		//gamePlayer.GetComponent<PlayerScript>().team = lobby.playerName;
	}

}
