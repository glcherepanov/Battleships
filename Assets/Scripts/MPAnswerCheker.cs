using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class MPAnswerCheker : MonoBehaviourPunCallbacks
{
    public GameObject ShipMovingHelper;
    private ShipMovingHelper _shipMovingHelper;
    //private TowerShootingHelper _towerShootingHelper;
    private float _timer = 0;
    private bool _wait = false;

    void Start()
    {
        _shipMovingHelper = ShipMovingHelper.GetComponent<ShipMovingHelper>();
    }

    public void CheckAnswer( Text answer )
    {
        PhotonNetwork.CurrentRoom.SetCustomProperties(
            new ExitGames.Client.Photon.Hashtable
            {
                {
                    CustomProperties.CheckAnswer.ToString(),
                    GetAnswerObjectString( answer.text )
                }
            }
        );
    }

    void Update() 
    {
        if ( _wait && _timer > 10)
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.RespawnShips.ToString(), true } } );
            PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.CreateNewExample.ToString(), true } } );
            PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.MoveShips.ToString(), true } } );
            _wait = false;
        }
        _timer += Time.deltaTime;
    } 
    public override void OnRoomPropertiesUpdate( ExitGames.Client.Photon.Hashtable propertiesThatChanged )
    {
        PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue( CustomProperties.AnswerDone.ToString(), out object answerDone);

        Debug.Log("Check answer func");
        Debug.Log(answerDone);
        if ( propertiesThatChanged.TryGetValue( CustomProperties.CheckAnswer.ToString(), out object answer ) )
        {
            var answerObject = JsonUtility.FromJson<AnswerObject>( answer.ToString() );

            if (answerDone.ToString().Equals("False") )
            {
                if ( answerObject.Correct )
                {
                    _shipMovingHelper.crushShip( answerObject.Target );
                    PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.AnswerDone.ToString(), true } } );
                    Debug.Log(answerDone);
                    _wait = true;
                    _timer = 0;
                    PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.MoveShips.ToString(), false } } );
                }
                else
                {
                    _shipMovingHelper.hitShip( ( string ) propertiesThatChanged[ "AnswerNumber" ] );
                }
            }
        }
    }

    private string GetAnswerObjectString( string answer )
    {
        AnswerObject answerObject = new AnswerObject()
        {
            Player = PhotonNetwork.LocalPlayer.IsMasterClient ? "host" : "player",
            Target = PhotonNetwork.CurrentRoom.CustomProperties[ "CorrectAnswer" ].ToString(),
            Correct = ( ( int ) PhotonNetwork.CurrentRoom.CustomProperties[ "CorrectAnswer" ] ) == int.Parse( answer )
        };
        string json = JsonUtility.ToJson( answerObject );

        return json;
    }
}
