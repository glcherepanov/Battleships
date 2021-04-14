using Photon.Pun;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShootingHelper : MonoBehaviourPunCallbacks
{
    private GameObject _ship, _tower;

    public void Shoot( string player, string target )
    {
        _ship = GameObject.FindGameObjectsWithTag( "ship" ).Where( item => item.GetComponentInChildren<TextMesh>().text == target ).First();
        _tower = GameObject.FindGameObjectsWithTag( player ).Where( item => item.GetComponent<IslandScript>().IsAlive ).First();

        _tower.GetComponentInChildren<ShootScript>().MakeShoot( _ship );
    }

    private void CrashTower( string player )
    {
        if (GameObject.FindGameObjectsWithTag( player ).Where( item => item.GetComponent<IslandScript>().IsAlive ).ToList().Count == 1)
        {
            GameObject.FindGameObjectsWithTag( player );
            PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.MoveShips.ToString(), false } } );
            //_winWindow.SetActive(true);
            var hostName = GameObject.FindGameObjectsWithTag( "hostName" ).First().GetComponent<TextMesh>().text;
            var noHostName = GameObject.FindGameObjectsWithTag( "playerName" ).First().GetComponent<TextMesh>().text;

            string message = String.Format("Победил игрок - {0}\nПроиграл игрок - {1}", player == "host" ? hostName : noHostName, player != "host" ? hostName : noHostName);
            Debug.Log(message);
        }
        
        var tower = GameObject.FindGameObjectsWithTag( player ).Where( item => item.GetComponent<IslandScript>().IsAlive ).First();
        tower.GetComponent<IslandScript>().Crash();
    }

    private void CrashBothPlayerTowers()
    {
        if ( GameObject.FindGameObjectsWithTag( "hostTower" ).Where( item => item.GetComponent<IslandScript>().IsAlive ).ToList().Count == 1
            && GameObject.FindGameObjectsWithTag( "playerTower" ).Where( item => item.GetComponent<IslandScript>().IsAlive ).ToList().Count == 1 ) 
        {
            PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.MoveShips.ToString(), false } } );
            Debug.Log( "ничья" );
        }
        var tower = GameObject.FindGameObjectsWithTag( "hostTower" ).Where( item => item.GetComponent<IslandScript>().IsAlive ).First();
        tower.GetComponent<IslandScript>().Crash();
        tower = GameObject.FindGameObjectsWithTag( "playerTower" ).Where( item => item.GetComponent<IslandScript>().IsAlive ).First();
        tower.GetComponent<IslandScript>().Crash();
    }

    public void Respanw()
    {
        _tower.GetComponentInChildren<ShootScript>().Respawn();
    }

    public override void OnRoomPropertiesUpdate( ExitGames.Client.Photon.Hashtable propertiesThatChanged )
    {
        PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue( CustomProperties.AnswerDone.ToString(), out object answerDone);

        if ( propertiesThatChanged.TryGetValue( CustomProperties.CheckAnswer.ToString(), out object answer ) && answerDone.Equals(false) )
        {
            var answerObject = JsonUtility.FromJson<AnswerObject>( answer.ToString() );

            if ( answerObject.Correct )
            {
                Shoot( answerObject.Player + "Tower", answerObject.Target );
            }
            else
            {
                CrashTower( answerObject.Player + "Tower" );
            }
        }
        if ( propertiesThatChanged.TryGetValue( CustomProperties.CrashTower.ToString(), out object crash ) )
        {
            if ( crash.Equals( true ) )
            {
                CrashBothPlayerTowers();
            }
        }
    }
}
