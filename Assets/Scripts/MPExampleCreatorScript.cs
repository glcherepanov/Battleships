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
        Debug.Log(properties.From);
        Debug.Log(properties.To);

        string operation = properties.Opperation == LevelProperties.OpperationEnum.Plus ? "+" : "-";
        // int low = 0;
        // properties.From = low;
        // int top = 10;
        // properties.To = top;
        int result = Random.Range( properties.From, properties.To );
        int first = properties.Opperation == LevelProperties.OpperationEnum.Plus ? Random.Range(properties.From, result) : Random.Range(result, properties.To);
        int second = properties.Opperation == LevelProperties.OpperationEnum.Plus ? result - first : first - result;
        var str = string.Format( "{0} {1} {2} = ?", first, operation, second );
        var answers = GetAnswerOptionsList(properties.From, properties.To, result );

        PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { "Example", str }, { "Answers", answers.ToArray() }, { "CorrectAnswer", result }, { "NewExample", true }, {CustomProperties.AnswerDone.ToString(), false} } );
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

        if ( propertiesThatChanged.TryGetValue( CustomProperties.CreateNewExample.ToString(), out object isNew ) )
        {
            if ( isNew.Equals( true ) )
            {
                CreateExample();
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
            newRand = Random.Range( low, top );
            if ((newRand != correctAnswer) && !result.Contains( newRand ) ) 
            {
                result.Add( newRand );
            }
        }

        return result.OrderBy( x => x ).ToList();
    }

    private void FillButtons( List<int> answerOptionsList )
    {
        if ( PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.CurrentRoom.Players.Last().Value.NickName && PhotonNetwork.CurrentRoom.PlayerCount > 2 )
        {
            GameObject.FindGameObjectsWithTag( "answerButton" ).ToList().ForEach( b =>
            {
                b.SetActive( false );
            });
        }
        else
        {
            answerOptionsList = new List<int>( answerOptionsList );
            foreach ( var button in GameObject.FindGameObjectsWithTag( "answerButton" ) )
            {
                button.GetComponentInChildren<Text>().text = answerOptionsList.First().ToString();
                answerOptionsList.Remove( answerOptionsList.First() );
            }
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
