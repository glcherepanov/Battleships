using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class MPAnswerCheker : MonoBehaviourPunCallbacks
{
    public GameObject ShipMovingHelper;
    private ShipMovingHelper _shipMovingHelper;
    //private TowerShootingHelper _towerShootingHelper;

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
