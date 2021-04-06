﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NetworkCreater : MonoBehaviourPunCallbacks
{
    public Text StatusText;
    public InputField UserName;
    public InputField LobbyNameField;
    public Text HostName;
    public Text PlayerName;

    private Dictionary<int, LevelProperties> levels = new Dictionary<int, LevelProperties>()
    {
        //new {1, new LevelProerties {
            //to = 1,    
            //from = 1,
            //opperation = enum.plus
        //},
        //new {2,  },
    };
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
        PhotonNetwork.CreateRoom( LobbyNameField.textComponent.text, new Photon.Realtime.RoomOptions { MaxPlayers = 2 } );

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
        if ( Bool.Equals( true ) )
        {
            string properties = JsonUtility.ToJson( new LevelProperties
            {
                From = 0,
                To = 10,
                Opperation = LevelProperties.OpperationEnum.Plus
            }); 
            PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.LevelProperties.ToString(), properties } } );
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
        _selected = id;
    }
}
