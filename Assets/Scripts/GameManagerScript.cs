using Photon.Pun;
using System.Linq;
using UnityEngine;

public class GameManagerScript : MonoBehaviourPunCallbacks
{
	public ShipMovingHelper ShipMovingHelper;
	public TextMesh Host;
	public TextMesh Player;

	public void Start()
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
}
