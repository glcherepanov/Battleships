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
    public TMPro.TMP_InputField UserName;
    public TMPro.TMP_InputField LobbyNameField;
    public TMPro.TextMeshProUGUI HostName;
    public TMPro.TextMeshProUGUI PlayerName;

    public string levelJson;
    private int _selected = 0;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(0, 10);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Log( "connectedToMaster" );
    }

    public void CreateRoom()
    {
        PhotonNetwork.NickName = UserName.text;
        PhotonNetwork.CreateRoom( LobbyNameField.textComponent.text, new Photon.Realtime.RoomOptions { MaxPlayers = 3 } );

        Log( "CreatedRoom" );
    }

    public void JoinRoom()
    {
        PhotonNetwork.NickName = UserName.text;
        Log( "TryJoinRoom: " + LobbyNameField?.text ?? "empty" );
        if ( PhotonNetwork.NetworkClientState == Photon.Realtime.ClientState.ConnectedToMasterServer )
        {
            PhotonNetwork.JoinRoom( LobbyNameField.textComponent.text );
        }
        else
        {
            Log( "Can`t join, status: " + PhotonNetwork.NetworkClientState.ToString() );
        }
    }

    public void LeaveLobby()
    {
        if ( PhotonNetwork.CurrentRoom != null )
        {
            PhotonNetwork.LeaveRoom();
            Log( "Leave room: " + PhotonNetwork.CurrentRoom.Name );
        }
        else
        {
            Log( "Not in room" );
        }
    }

    public override void OnJoinedRoom()
    {
        Log( "joined to room: " + PhotonNetwork.CurrentRoom.Name );

        if ( PhotonNetwork.IsMasterClient )
        {
            HostName.text += PhotonNetwork.NickName;
        }
        else
        {
            HostName.text = "host: " + PhotonNetwork.MasterClient.NickName;
            PlayerName.text = "player: " + PhotonNetwork.NickName;
        }
    }

    public override void OnPlayerEnteredRoom( Player newPlayer )
    {
        PlayerName.text += newPlayer.NickName;
    }

    public override void OnJoinRoomFailed( short returnCode, string message )
    {
        Log( "failed: " + message + " " + returnCode );
    }

    public override void OnRoomPropertiesUpdate( ExitGames.Client.Photon.Hashtable propertiesThatChanged )
    {
        var Bool = PhotonNetwork.CurrentRoom.CustomProperties[ CustomProperties.StartGame.ToString() ];
        Debug.Log(Bool.Equals( true ));
        if ( Bool.Equals( true ) )
        {
            // string properties = JsonUtility.ToJson( level ); 
            PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.LevelProperties.ToString(), levelJson } } );
            PhotonNetwork.LoadLevel( "Level" );
        }
    }

    public void StartGame()
    {
        PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.StartGame.ToString(), true } } );
    }

    private void Log( string message )
    {
        StatusText.text += "\n";
        StatusText.text += message;
    }

    public void SelectLevel( int id )
    {
        levelJson = LevelsData.SetGameLevel(id);
        Debug.Log(levelJson);
    }
}
