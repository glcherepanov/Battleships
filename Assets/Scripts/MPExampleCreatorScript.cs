using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MPExampleCreatorScript : MonoBehaviourPunCallbacks
{
    public Text ExampleText;

    private List<int> _answers = new List<int>();
    private int _result;

    private List<GameObject> _ships;

    public void CreateExample()
    {
        LevelProperties properties = JsonUtility.FromJson<LevelProperties>( PhotonNetwork.CurrentRoom.CustomProperties[ CustomProperties.LevelProperties.ToString() ].ToString() );
        Debug.Log(properties.Opperation);
        //int result, first, second;
        string opperation = "+";
        int low = 0;
        int top = 10;
        int result = Random.Range( properties.From, properties.To );
        int first = result - ( opperation == "+" ? Random.Range( properties.From, properties.To / 2 ) : Random.Range( properties.From, properties.To ) );
        int second = opperation == "+" ? result - first : result + first;

        var str = string.Format( "{0} {1} {2} = ?", first, opperation, second );
        var answers = GetAnswerOptionsList( low, top, result );

        PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { "Example", str }, { "Answers", answers.ToArray() }, { "CorrectAnswer", result }, { "NewExample", true } } );
    }

    public override void OnRoomPropertiesUpdate( ExitGames.Client.Photon.Hashtable propertiesThatChanged )
    {
        if ( propertiesThatChanged.TryGetValue( "Answers", out object answers ) )
        {
            _answers = ( ( int[] ) answers ).ToList();
        }
        if ( propertiesThatChanged.TryGetValue( "CorrectAnswer", out object answer ) )
        {
            _result = ( int ) answer;
        }

        if ( propertiesThatChanged.TryGetValue( "NewExample", out object isNewExample ) )
        {
            if ( ( bool ) isNewExample )
            {
                FillButtons( _answers );
                FillShips( _answers );
                if ( propertiesThatChanged.TryGetValue( "Example", out object example ) )
                {
                    ExampleText.text = ( string ) example;
                }
            }
        }
    }

    public void SetShips( List<GameObject> ships )
    {
        _ships = ships;
    }

    private void SetVariables( LevelProperties properties, int result, int first, int second )
    {
        result = Random.Range( properties.From, properties.To );
        switch (properties.Opperation)
        {
            case LevelProperties.OpperationEnum.Minus:
                first = result - Random.Range( properties.From, properties.To / 2 );
                second = result - first;
                break;
            case LevelProperties.OpperationEnum.Plus:
                first = result - Random.Range( properties.From, properties.To / 2 );
                second = result - first;
                break;
        }
    }

    private List<int> GetAnswerOptionsList( int low, int top, int correctAnswer )
    {
        List<int> result = new List<int>();
        result.Add( correctAnswer );

        while ( result.Count < 5 )
        {
            int newRand;
            while ( ( newRand = Random.Range( low, top ) ) == correctAnswer && !result.Contains( newRand ) ) { }
            result.Add( newRand );
        }

        return result.OrderBy( x => Random.value ).ToList();
    }

    private void FillButtons( List<int> answerOptionsList )
    {
        answerOptionsList = new List<int>( answerOptionsList );
        foreach ( var button in GameObject.FindGameObjectsWithTag( "answerButton" ) )
        {
            button.GetComponentInChildren<Text>().text = answerOptionsList.First().ToString();
            answerOptionsList.Remove( answerOptionsList.First() );
        }
    }

    private void FillShips( List<int> answerOptionsList )
    {
        answerOptionsList = new List<int>( answerOptionsList );
        foreach ( var text in GameObject.FindGameObjectsWithTag( "answerShipText" ) )
        {
            text.GetComponent<TextMesh>().text = answerOptionsList.First().ToString();
            answerOptionsList.Remove( answerOptionsList.First() );
        }
    }
}
