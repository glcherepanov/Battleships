using Photon.Pun;
using System.Linq;
using UnityEngine;

public class GameManagerScript : MonoBehaviourPunCallbacks
{
	public ShipMovingHelper ShipMovingHelper;
	[SerializeField]
	private PlayerIndicator hostIndicator = null;
	[SerializeField]
	private PlayerIndicator playerIndicator = null;

	public void Start()
	{
		if(PhotonNetwork.IsMasterClient)
		{
			ShipMovingHelper.SpawnShips();
			PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.CreateNewExample.ToString(), true } });
			PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.MoveShips.ToString(), true } });
			
			hostIndicator.SetPlayerName(PlayerNameUtility.GetName(PhotonNetwork.LocalPlayer));
			playerIndicator.SetPlayerName(PlayerNameUtility.GetName(PhotonNetwork.CurrentRoom.Players.First(p => !p.Value.IsMasterClient).Value));
		}
		else
		{
			hostIndicator.SetPlayerName(PlayerNameUtility.GetName(PhotonNetwork.CurrentRoom.GetPlayer(PhotonNetwork.CurrentRoom.MasterClientId)));
			playerIndicator.SetPlayerName(PlayerNameUtility.GetName(PhotonNetwork.LocalPlayer));
		}
	}
}
