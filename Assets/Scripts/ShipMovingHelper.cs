using Photon.Pun;
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

    private List<Vector3> Positions = new List<Vector3>()
    {
        new Vector3( -77, 0, -40 ),
        new Vector3( -20, 0, 0 ),
        new Vector3( -33, 0, -23 ),
        new Vector3( -70, 0, 6 ),
        new Vector3( -35, 0, 35 ),
    };
    private List<GameObject> _ships = new List<GameObject>();

    void Start()
    {
        _ships = GameObject.FindGameObjectsWithTag( "ship" ).ToList();
        int i = 0;
        _ships.ForEach( s => {
            Positions[ i ] = new Vector3( Positions[i].x, s.GetComponent<Transform>().position.y, Positions[i].z);
            i++;
        });

    }

    // Update is called once per frame
    void Update()
    {
        PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue( CustomProperties.MoveShips.ToString(), out object isMoving );
        foreach ( var ship in _ships )
        {
            if ( !ship.GetComponent<Animator>().GetBool( "IsHited" ) && isMoving.Equals(true))
            {
                var transform = ship.GetComponent<Transform>();
                transform.position = new Vector3( transform.position.x + shipSpeed, transform.position.y, transform.position.z );

                if ( transform.position.x >= endPoint )
                {
                    PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.CrashTower.ToString(), true } } );
                    PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.CreateNewExample.ToString(), true } } );
                    RespawnShips();
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

            ship.GetComponent<Transform>().position = new Vector3( Positions[i].x, Positions[ i ].y, Positions[i].z );
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
