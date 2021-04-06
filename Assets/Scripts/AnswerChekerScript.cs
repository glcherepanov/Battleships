using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class AnswerChekerScript : MonoBehaviour
{
    public GameObject ExampleCreator;
    public GameObject ShipMovingHelper;

    private ExampleCreatorScript _exampleCreator;
    private ShipMovingHelper _shipMovingHelper;

    void Start()
    {
        _exampleCreator = ExampleCreator.GetComponent<ExampleCreatorScript>();
        _shipMovingHelper = ShipMovingHelper.GetComponent<ShipMovingHelper>();
    }

    public void CheckAnswer( Text answer )
    {
        if ( _exampleCreator.Result == int.Parse( answer.text ) )
        {
            _shipMovingHelper.crushShip( answer.text );
        }
        else
        {
            _shipMovingHelper.hitShip( answer.text );
        }
    }
}
