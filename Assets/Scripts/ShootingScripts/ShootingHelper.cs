using Photon.Pun;
using System.Linq;
using UnityEngine;

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
        var tower = GameObject.FindGameObjectsWithTag( player ).Where( item => item.GetComponent<IslandScript>().IsAlive ).First();

        tower.GetComponent<IslandScript>().Crash();
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
