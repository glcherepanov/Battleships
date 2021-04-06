using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ExampleCreatorScript : MonoBehaviour
{
    public Text ExampleText;

    public int Result { get; private set; }
    private int _first = 0;
    private int _second = 0;

    // Start is called before the first frame update
    void Start()
    {
        CreateExample();
    }

    public void CreateExample()
    {
        _first = Random.Range( 10, 50 );
        _second = Random.Range( 10, 50 );
        string operation = GetRandomOperationSymbol();
        Result = GetResultByOperationSymbol( operation );

        ExampleText.text = string.Format( "{0} {1} {2} = ?", _first, operation, _second );

        var answers = GetAnswerOptionsList( Result );
        FillButtons( answers );
        FillShips( answers );
    }

    private string GetRandomOperationSymbol()
    {
        switch ( Random.Range( 0, 20 ) & 10 )
        { 
            case 0: return "+";
            case 1: return "-";
            default: return "+";
        }
    }

    private int GetResultByOperationSymbol( string operation )
    {
        switch ( operation )
        {
            case "+": return _first + _second;
            case "-": return _first - _second;
            default: return 0;
        }
    }

    private List<int> GetAnswerOptionsList( int correctAnswer )
    {
        List<int> result = new List<int>();
        result.Add( correctAnswer );

        while ( result.Count < 5 )
        {
            int newRand;
            while ( ( newRand = Random.Range( Result - Result / 2, Result + Result / 2 ) ) == correctAnswer ) { }
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
