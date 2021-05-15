using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NetworkCreater : MonoBehaviourPunCallbacks
{
	public TMPro.TextMeshProUGUI StatusText;

	public string levelJson;

	[SerializeField]
	private MenuController menuController = null;

	public void Start()
	{
		PhotonNetwork.NickName = "Player" + Random.Range(0, 10);
		PhotonNetwork.ConnectUsingSettings();

		menuController.PreLobbyScreen.LobbyCreateRequested += OnLobbyCreateRequested;
		menuController.PreLobbyScreen.LobbyJoinRequested += OnLobbyJoinRequested;

		menuController.LevelSelectionScreen.LevelSelectionRequested += OnLevelSelectionRequested;
		
		menuController.LobbyScreen.GameStartRequested += OnGameStartRequested;
		menuController.LobbyScreen.LobbyExitRequested += OnLobbyExitRequested;
	}

	private void OnLobbyCreateRequested(LobbyCreateRequest request)
	{
		PhotonNetwork.NickName = request.PlayerName;
		PhotonNetwork.CreateRoom(request.LobbyName, new RoomOptions { MaxPlayers = 3 });

		Log("CreatedRoom");
	}

	private void OnLobbyJoinRequested(LobbyJoinRequest request)
	{
		PhotonNetwork.NickName = request.PlayerName;
		Log("TryJoinRoom: " + request.LobbyName ?? "empty");
		if(PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
		{
			PhotonNetwork.JoinRoom(request.LobbyName);
		}
		else
		{
			Log("Can`t join, status: " + PhotonNetwork.NetworkClientState.ToString());
		}
	}

	private void OnLevelSelectionRequested(LevelSelectionRequest request)
	{
		levelJson = LevelsData.SetGameLevel(request.LevelId);
		Debug.Log(levelJson);
	}

	private void OnGameStartRequested()
	{
		PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.StartGame.ToString(), true } });
	}

	private void OnLobbyExitRequested()
	{
		if(PhotonNetwork.CurrentRoom != null)
		{
			PhotonNetwork.LeaveRoom();
			Log("Leave room: " + PhotonNetwork.CurrentRoom.Name);
		}
		else
		{
			Log("Not in room");
		}
	}

	public override void OnConnectedToMaster()
	{
		Log("connectedToMaster");
	}

	public override void OnJoinedRoom()
	{
		Log("joined to room: " + PhotonNetwork.CurrentRoom.Name);

		menuController.LobbyScreen.SetLobbyControl(allow: PhotonNetwork.IsMasterClient);
		menuController.LobbyScreen.AddPlayer(PhotonNetwork.LocalPlayer.ActorNumber, PhotonNetwork.LocalPlayer.NickName, PhotonNetwork.LocalPlayer.IsMasterClient ? PlayerCategory.Owner : PlayerCategory.Normal);
	}

	public override void OnLeftRoom()
	{
		menuController.LobbyScreen.SetLobbyControl(allow: false);
		menuController.LobbyScreen.RemovePlayer(PhotonNetwork.LocalPlayer.ActorNumber);
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		menuController.LobbyScreen.AddPlayer(newPlayer.ActorNumber, newPlayer.NickName, newPlayer.IsMasterClient ? PlayerCategory.Owner : PlayerCategory.Normal);
	}

	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		menuController.LobbyScreen.RemovePlayer(otherPlayer.ActorNumber);
	}

	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		Log("failed: " + message + " " + returnCode);
	}

	public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
	{
		var Bool = PhotonNetwork.CurrentRoom.CustomProperties[CustomProperties.StartGame.ToString()];
		Debug.Log(Bool.Equals(true));
		if(Bool.Equals(true))
		{
			// string properties = JsonUtility.ToJson( level ); 
			if(string.IsNullOrEmpty(levelJson))
			{
				levelJson = LevelsData.SetGameLevel(1);
			}
			PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.LevelProperties.ToString(), levelJson } });
			PhotonNetwork.LoadLevel("Level");
		}
	}

	private void Log(string message)
	{
		StatusText.text += "\n";
		StatusText.text += message;
	}
}
