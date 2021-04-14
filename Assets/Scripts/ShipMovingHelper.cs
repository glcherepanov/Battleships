using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipMovingHelper : MonoBehaviourPunCallbacks
{
	private const string ShipPrefabName = "Ship";

	private class Ship
	{
		public GameObject GameObject;
		public Animator Animator;
		public TextMesh Text;
	}

	[SerializeField]
	private float shipSpeed = 1.0f;
	[SerializeField]
	private float startPoint = 0.0f;
	[SerializeField]
	private float endPoint = 0.0f;

	[SerializeField]
	private PhotonNetworkedParent shipParent = null;
	[SerializeField]
	private List<Transform> positionPoints = new List<Transform>();

	//private List<Vector3> Positions = new List<Vector3>()
	//{
	//	new Vector3( -77, 0, -40 ),
	//	new Vector3( -20, 0, 0 ),
	//	new Vector3( -33, 0, -23 ),
	//	new Vector3( -70, 0, 6 ),
	//	new Vector3( -35, 0, 35 ),
	//};

	private List<Ship> ships = new List<Ship>();
	private System.Random random = null;

	private void Awake()
	{
		random = new System.Random(unchecked(System.Environment.TickCount * 31));
	}

	void Update()
	{
		if(!PhotonNetwork.IsMasterClient)
		{
			return;
		}

		PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(CustomProperties.MoveShips.ToString(), out object isMoving);
		foreach(var ship in ships)
		{
			if(isMoving != null)
			{
				if(!ship.Animator.GetBool("IsHited") && isMoving.Equals(true))
				{
					ship.GameObject.transform.localPosition += Vector3.right * shipSpeed * Time.deltaTime;

					if(ship.GameObject.transform.localPosition.x >= endPoint)
					{
						PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.CrashTower.ToString(), true } });
						PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.CreateNewExample.ToString(), true } });
						RespawnShips();
					}
				}
			}
		}
	}

	public void RespawnShips()
	{
		var positions = new List<Vector3>();
		for(int i = 0; i < positionPoints.Count; ++i)
		{
			positions.Add(positionPoints[i].position);
		}

		RandomUtility.Shuffle(positions, random);

		for(int i = 0; i < ships.Count; ++i)
		{
			Vector3 position = positions[i];
			Ship ship = ships[i];
			ship.GameObject.transform.localPosition = position;
			ship.GameObject.transform.localRotation = Quaternion.LookRotation(Vector3.left, Vector3.up);
			ship.Animator.SetBool("IsAlive", true);
			ship.Animator.SetBool("IsHited", false);
		}
	}

	public void crushShip(string number)
	{
		Ship ship = ships.First(s => s.Text.text == number);

		ship.Animator.SetBool("IsHited", true);
		ship.Animator.SetBool("IsAlive", false);
	}

	public void hitShip(string number)
	{
		// var ship = _ships.First( item => item.GetComponentInChildren<TextMesh>().text == number );

		// ship.GetComponentInChildren<TextMesh>().text = "error";
	}

	public void SpawnShips()
	{
		for(int i = 0; i < positionPoints.Count; ++i)
		{
			Transform positionPoint = positionPoints[i];
			GameObject shipObject = PhotonParentedTransform.InstantiateParented(ShipPrefabName, positionPoint.position, Quaternion.LookRotation(Vector3.left, Vector3.up), shipParent);
			var ship = new Ship
			{
				GameObject = shipObject,
				Animator = shipObject.GetComponent<Animator>(),
				Text = shipObject.GetComponentInChildren<TextMesh>()
			};
			ships.Add(ship);
		}
	}

	public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
	{
		if(propertiesThatChanged.TryGetValue(CustomProperties.RespawnShips.ToString(), out object Bool))
		{
			if(Bool.Equals(true))
			{
				RespawnShips();
			}
		}
	}
}
