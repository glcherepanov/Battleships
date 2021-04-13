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
            var tower = GameObject.FindGameObjectsWithTag( player ).Where( item => item.GetComponent<IslandScript>().IsAlive ).First();
            tower.GetComponent<IslandScript>().Crash();
            GameObject.FindGameObjectsWithTag( player );
            //_winWindow.SetActive(true);
            var hostName = GameObject.FindGameObjectsWithTag( "hostName" ).First().GetComponent<Text>().text;
            var noHostName = GameObject.FindGameObjectsWithTag( "playerName" ).First().GetComponent<Text>().text;

            string message = String.Format($"Победил игрок - {0}\nПроиграл игрок - {1}", player == "host" ? hostName : noHostName, player != "host" ? hostName : noHostName);
            Debug.Log(message);
        }
        else
        {
            var tower = GameObject.FindGameObjectsWithTag( player ).Where( item => item.GetComponent<IslandScript>().IsAlive ).First();
            tower.GetComponent<IslandScript>().Crash();
        }
    }

    public void Respanw()
    {
        _tower.GetComponentInChildren<ShootScript>().Respawn();
    }

    public override void OnRoomPropertiesUpdate( ExitGames.Client.Photon.Hashtable propertiesThatChanged )
    {
        if ( propertiesThatChanged.TryGetValue( CustomProperties.CheckAnswer.ToString(), out object answer ) )
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
    }
}
