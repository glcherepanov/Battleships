using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Linq;
using UnityEngine;

public class GameManagerScript : MonoBehaviourPunCallbacks
{
    public GameObject ShipMovingHelper;
    public TextMesh Host;
    public TextMesh Player;

    public GameObject FirstShip;
    public GameObject SecondShip;
    public GameObject ThirdShip;
    public GameObject FourthShip;
    public GameObject FifthShip;

    private GameObject _firstShip;
    private GameObject _secondShip;
    private GameObject _thirdShip;
    private GameObject _fourthShip;
    private GameObject _fifthShip;

    public void InitGameObjects()
    {
        if ( PlayerManager.LocalPlayerInstance == null )
        {
            if ( PhotonNetwork.IsMasterClient )
            {
                InitShips();
                ShipMovingHelper.GetComponent<ShipMovingHelper>().SetShips( new System.Collections.Generic.List<GameObject>() { _firstShip, _secondShip, _thirdShip, _fourthShip, _fifthShip } );
                Host.text = PhotonNetwork.LocalPlayer.NickName;
                Player.text = PhotonNetwork.CurrentRoom.Players.Last( p => !p.Value.IsMasterClient ).Value.NickName;
            }
            else
            {
                Host.text = PhotonNetwork.CurrentRoom.Players.First( p => p.Value.IsMasterClient).Value.NickName;
                Player.text = PhotonNetwork.LocalPlayer.NickName; 
            }
        }
    }

    private void InitShips()
    {
        _firstShip = PhotonNetwork.Instantiate( "Ship", FirstShip.transform.position, FirstShip.transform.rotation, 0 );
        _secondShip = PhotonNetwork.Instantiate( "Ship", SecondShip.transform.position, SecondShip.transform.rotation, 0 );
        _thirdShip = PhotonNetwork.Instantiate( "Ship", ThirdShip.transform.position, ThirdShip.transform.rotation, 0 );
        _fourthShip = PhotonNetwork.Instantiate( "Ship", FourthShip.transform.position, FourthShip.transform.rotation, 0 );
        _fifthShip = PhotonNetwork.Instantiate( "Ship", FifthShip.transform.position, FifthShip.transform.rotation, 0 );

        _firstShip.tag = "ship";
        _secondShip.tag = "ship";
        _thirdShip.tag = "ship";
        _fourthShip.tag = "ship";
        _fifthShip.tag = "ship";
    }
}
