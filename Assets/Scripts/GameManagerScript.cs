using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Linq;
using UnityEngine;

public class GameManagerScript : MonoBehaviourPunCallbacks
{
	public ShipMovingHelper ShipMovingHelper;
	public TextMesh Host;
	public TextMesh Player;

	private void Start()
	{
		if(PhotonNetwork.IsMasterClient)
		{
			ShipMovingHelper.SpawnShips();
			PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.CreateNewExample.ToString(), true } });
			PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.MoveShips.ToString(), true } });
			Host.text = PhotonNetwork.LocalPlayer.NickName;
			Player.text = PhotonNetwork.CurrentRoom.Players.Last(p => !p.Value.IsMasterClient).Value.NickName;
		}
		else
		{
			Host.text = PhotonNetwork.CurrentRoom.Players.First(p => p.Value.IsMasterClient).Value.NickName;
			Player.text = PhotonNetwork.LocalPlayer.NickName;
		}
	}

	public void InitGameObjects()
	{
		return;

		// if ( PlayerManager.LocalPlayerInstance == null )
		// {
		if(PhotonNetwork.IsMasterClient)
		{
			ShipMovingHelper.SpawnShips();
			PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.CreateNewExample.ToString(), true } });
			PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.MoveShips.ToString(), true } });
			Host.text = PhotonNetwork.LocalPlayer.NickName;
			Player.text = PhotonNetwork.CurrentRoom.Players.Last(p => !p.Value.IsMasterClient).Value.NickName;
		}
		// else
		// {
		// }
		// }
		if(!PhotonNetwork.IsMasterClient)
		{
			Host.text = PhotonNetwork.CurrentRoom.Players.First(p => p.Value.IsMasterClient).Value.NickName;
			Player.text = PhotonNetwork.LocalPlayer.NickName;
		}
	}

	private void InitShips()
	{
		//_firstShip = PhotonNetwork.Instantiate( "Ship", FirstShip.transform.position, FirstShip.transform.rotation, 0);
		//_secondShip = PhotonNetwork.Instantiate( "Ship", SecondShip.transform.position, SecondShip.transform.rotation, 0 );
		//_thirdShip = PhotonNetwork.Instantiate( "Ship", ThirdShip.transform.position, ThirdShip.transform.rotation, 0 );
		//_fourthShip = PhotonNetwork.Instantiate( "Ship", FourthShip.transform.position, FourthShip.transform.rotation, 0 );
		//_fifthShip = PhotonNetwork.Instantiate( "Ship", FifthShip.transform.position, FifthShip.transform.rotation, 0 );

		//_firstShip.tag = "ship";
		//_secondShip.tag = "ship";
		//_thirdShip.tag = "ship";
		//_fourthShip.tag = "ship";
		//_fifthShip.tag = "ship";
	}
}
