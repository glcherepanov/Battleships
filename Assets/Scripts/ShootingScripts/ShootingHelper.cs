using Photon.Pun;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShootingHelper : MonoBehaviourPunCallbacks
{
	private GameObject _ship, _tower;

	public bool AnyHostTowersAlive { get; private set; }
	public bool AnyPlayerTowersAlive { get; private set; }

	public event Action TowersChanged;

	public void Shoot(string player, string target)
	{
		_ship = GameObject.FindGameObjectsWithTag("ship").Where(item => item.GetComponentInChildren<TextMeshPro>().text == target).First();
		_tower = GameObject.FindGameObjectsWithTag(player).Where(item => item.GetComponent<IslandScript>().IsAlive).First();

		_tower.GetComponentInChildren<ShootScript>().MakeShoot(_ship);
	}

	private void CrashTower(string player)
	{
		if(GameObject.FindGameObjectsWithTag(player).Where(item => item.GetComponent<IslandScript>().IsAlive).ToList().Count == 1)
		{
			GameObject.FindGameObjectsWithTag(player);
			PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.MoveShips.ToString(), false } });
		}

		var tower = GameObject.FindGameObjectsWithTag(player).Where(item => item.GetComponent<IslandScript>().IsAlive).FirstOrDefault();
		if(tower != null) tower.GetComponent<IslandScript>().Crash();

		OnTowersChanged();
	}

	private void CrashBothPlayerTowers()
	{
		if(GameObject.FindGameObjectsWithTag("hostTower").Where(item => item.GetComponent<IslandScript>().IsAlive).ToList().Count == 1
			&& GameObject.FindGameObjectsWithTag("playerTower").Where(item => item.GetComponent<IslandScript>().IsAlive).ToList().Count == 1)
		{
			PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.MoveShips.ToString(), false } });
		}

		var tower = GameObject.FindGameObjectsWithTag("hostTower").Where(item => item.GetComponent<IslandScript>().IsAlive).FirstOrDefault();
		if(tower != null) tower.GetComponent<IslandScript>().Crash();
		tower = GameObject.FindGameObjectsWithTag("playerTower").Where(item => item.GetComponent<IslandScript>().IsAlive).FirstOrDefault();
		if(tower != null) tower.GetComponent<IslandScript>().Crash();

		OnTowersChanged();
	}

	public void Respawn()
	{
		_tower.GetComponentInChildren<ShootScript>().Respawn();
	}

	public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
	{
		PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(CustomProperties.AnswerDone.ToString(), out object answerDone);

		if(propertiesThatChanged.TryGetValue(CustomProperties.CheckAnswer.ToString(), out object answer) && answerDone.Equals(false))
		{
			var answerObject = JsonUtility.FromJson<AnswerObject>(answer.ToString());

			if(answerObject.Correct)
			{
				Shoot(answerObject.Player + "Tower", answerObject.Target);
			}
			else
			{
				CrashTower(answerObject.Player + "Tower");
			}
		}
		if(propertiesThatChanged.TryGetValue(CustomProperties.CrashTower.ToString(), out object crash))
		{
			if(crash.Equals(true))
			{
				CrashBothPlayerTowers();
			}
		}
	}

	private void OnTowersChanged()
	{
		int hostAliveTowerCount = GameObject.FindGameObjectsWithTag("hostTower").Where(item => item.GetComponent<IslandScript>().IsAlive).Count();
		int playerAliveTowerCount = GameObject.FindGameObjectsWithTag("playerTower").Where(item => item.GetComponent<IslandScript>().IsAlive).Count();

		AnyHostTowersAlive = hostAliveTowerCount > 0;
		AnyPlayerTowersAlive = playerAliveTowerCount > 0;

		TowersChanged?.Invoke();
	}
}
