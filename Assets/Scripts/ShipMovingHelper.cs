using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShipMovingHelper : MonoBehaviour
{
    public float shipSpeed = 0.001F;
    public float startPoint = 0;
    public float endPoint = 0;

    private readonly List<Vector2> Positions = new List<Vector2>()
    {
        new Vector2( -77, -40 ),
        new Vector2( -20, 0 ),
        new Vector2( -33, -23 ),
        new Vector2( -70, 6 ),
        new Vector2( -35, 35 ),
    };
    private List<GameObject> _ships = new List<GameObject>();

    void Start()
    {
        _ships = GameObject.FindGameObjectsWithTag( "ship" ).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        foreach ( var ship in _ships )
        {
            if ( !ship.GetComponent<Animator>().GetBool( "IsHited" ) )
            {
                var transform = ship.GetComponent<Transform>();
                transform.position = new Vector3( transform.position.x + shipSpeed, transform.position.y, transform.position.z );

                if ( transform.position.x >= endPoint )
                {
                    transform.position = new Vector3( startPoint, transform.position.y, transform.position.z );
                }
            }
        }
    }

    public void RespawnShips()
    {
        HashSet<int> usedPositions = new HashSet<int>();
        foreach ( var ship in _ships )
        {
            int i = Random.Range( 0, Positions.Count );
            while ( usedPositions.Any( item => item == i ) ) 
            {
                i = Random.Range( 0, Positions.Count );
            }

            ship.GetComponent<Transform>().position = new Vector3( Positions[i].x, ship.GetComponent<Transform>().position.y, Positions[i].y );
            ship.GetComponent<Transform>().rotation = Quaternion.Euler( 0, 270, 0);
            ship.GetComponent<Animator>().SetBool( "IsAlive", true );
            ship.GetComponent<Animator>().SetBool( "IsHited", false );
            usedPositions.Add( i );
        }
    }

    public void crushShip( string number )
    {
        var ship = _ships.First( item => item.GetComponentInChildren<TextMesh>().text == number );

        ship.GetComponent<Animator>().SetBool( "IsHited", true );
        ship.GetComponent<Animator>().SetBool( "IsAlive", false );
    }

    public void hitShip( string number )
    {
        var ship = _ships.First( item => item.GetComponentInChildren<TextMesh>().text == number );

        ship.GetComponentInChildren<TextMesh>().text = "error";
    }

    public void SetShips( List<GameObject> newShips )
    {
        _ships = newShips;
    }
}
