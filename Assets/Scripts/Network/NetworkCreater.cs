using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkCreater : MonoBehaviourPunCallbacks
{
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

		menuController.LobbyScreen.LogLobbyCreated(request.LobbyName);
		Debug.Log($"Room created: {request.LobbyName}");
	}

	private void OnLobbyJoinRequested(LobbyJoinRequest request)
	{
		PhotonNetwork.NickName = request.PlayerName;
		Debug.Log($"Attempting to join room: {request.LobbyName}");

		if(PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
		{
			PhotonNetwork.JoinRoom(request.LobbyName);
			menuController.LobbyScreen.LogLobbyJoined(request.LobbyName);
		}
		else
		{
			Debug.Log($"Failed to join room, status: {PhotonNetwork.NetworkClientState}");
			menuController.LobbyScreen.LogLobbyFailed(request.LobbyName);
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
			Debug.Log($"Left room: {PhotonNetwork.CurrentRoom.Name}");
		}
		else
		{
			Debug.Log("Can't leave room, not in any room");
		}
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("Connected to master");
	}

	public override void OnJoinedRoom()
	{
		Debug.Log($"Joined room: {PhotonNetwork.CurrentRoom.Name}");

		menuController.LobbyScreen.SetLobbyName(PhotonNetwork.CurrentRoom.Name);
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
		var playerCategory = newPlayer.IsMasterClient ? PlayerCategory.Owner : PlayerCategory.Normal;
		menuController.LobbyScreen.AddPlayer(newPlayer.ActorNumber, newPlayer.NickName, playerCategory);
		menuController.LobbyScreen.LogPlayerJoined(newPlayer.NickName);
	}

	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
		menuController.LobbyScreen.RemovePlayer(otherPlayer.ActorNumber);
		menuController.LobbyScreen.LogPlayerLeft(otherPlayer.NickName);
	}

	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		Debug.Log($"Failed to join room: {message} {returnCode}");
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
}
