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

        if ( propertiesThatChanged.TryGetValue( CustomProperties.CheckAnswer.ToString(), out object answer ) )
        {
            var answerObject = JsonUtility.FromJson<AnswerObject>( answer.ToString() );

            if ( answerObject.Correct )
            {
                _shipMovingHelper.crushShip( answerObject.Target );
            }
            else
            {
                _shipMovingHelper.hitShip( ( string ) propertiesThatChanged[ "AnswerNumber" ] );
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
