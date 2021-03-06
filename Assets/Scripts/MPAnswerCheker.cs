using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine;

public class MPAnswerCheker : MonoBehaviourPunCallbacks
{
	public GameObject ShipMovingHelper;
	[SerializeField]
	private GameInterfaceController gameInterfaceController = null;

	private ShipMovingHelper _shipMovingHelper;
	//private TowerShootingHelper _towerShootingHelper;
	private float _timer = 0;
	private bool _wait = false;

	public void Start()
	{
		_shipMovingHelper = ShipMovingHelper.GetComponent<ShipMovingHelper>();

		gameInterfaceController.ExampleScreen.ExampleAnswered += OnExampleAnswered;
	}

	private void OnExampleAnswered(ExampleAnswerResponse response)
	{
		PhotonNetwork.CurrentRoom.SetCustomProperties(
			new ExitGames.Client.Photon.Hashtable
			{
				{
					CustomProperties.CheckAnswer.ToString(),
					GetAnswerObjectString(response.Answer)
				}
			}
		);
	}

	void Update()
	{
		if(PhotonNetwork.IsMasterClient)
		{
			if(_wait && _timer > 10)
			{
				PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.RespawnShips.ToString(), true } });
				PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.CreateNewExample.ToString(), true } });
				PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.MoveShips.ToString(), true } });
				_wait = false;
			}
			_timer += Time.deltaTime;
		}
	}

	public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
	{
		PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(CustomProperties.AnswerDone.ToString(), out object answerDone);

		Debug.Log("Check answer func");
		Debug.Log(answerDone);
		if(propertiesThatChanged.TryGetValue(CustomProperties.CheckAnswer.ToString(), out object answer))
		{
			var answerObject = JsonUtility.FromJson<AnswerObject>(answer.ToString());

			if(!((bool)answerDone))
			{
				gameInterfaceController.ExampleScreen.SetAnswersVisible(false);

				Player player = null;
				if(answerObject.Player == "host")
				{
					player = PhotonNetwork.CurrentRoom.GetPlayer(PhotonNetwork.CurrentRoom.MasterClientId);
				}
				else
				{
					player = PhotonNetwork.CurrentRoom.Players.Values.First(p => !p.IsMasterClient);
				}

				string playerName = PlayerNameUtility.GetName(player);

				if(answerObject.Correct)
				{
					gameInterfaceController.ExampleScreen.ShowResult(playerName, isCorrect: true);

					if(PhotonNetwork.IsMasterClient)
					{
						_wait = true;
						_timer = 0;
						_shipMovingHelper.CrushShip(answerObject.Target);
						PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.AnswerDone.ToString(), true } });
						PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable { { CustomProperties.MoveShips.ToString(), false } });
					}
				}
				else
				{
					propertiesThatChanged.TryGetValue( "onlePlayerAlreadyLose", out object onlePlayerAlreadyLose );
					if ( onlePlayerAlreadyLose?.ToString() == "host" || onlePlayerAlreadyLose?.ToString() == "player" )
					{
						PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.AnswerDone.ToString(), true } } );
						PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.MoveShips.ToString(), false } } );

						//PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { CustomProperties.CreateNewExample.ToString(), true } } );
					}
					else
					{
						PhotonNetwork.CurrentRoom.SetCustomProperties( new ExitGames.Client.Photon.Hashtable { { "onlePlayerAlreadyLose", answerObject.Player } } );
					}
				}
			}
		}
	}

	private string GetAnswerObjectString(int answer)
	{
		AnswerObject answerObject = new AnswerObject()
		{
			Player = PhotonNetwork.LocalPlayer.IsMasterClient ? "host" : "player",
			Target = PhotonNetwork.CurrentRoom.CustomProperties["CorrectAnswer"].ToString(),
			Correct = ((int)PhotonNetwork.CurrentRoom.CustomProperties["CorrectAnswer"]) == answer
		};
		string json = JsonUtility.ToJson(answerObject);

		return json;
	}
}
