using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviourPunCallbacks
{
	private enum EndGameState
	{
		None,
		Playing,
		End,
		Exiting
	}

	private const string MainMenuScene = "MainMenu";

	[SerializeField]
	private ShootingHelper shootingHelper = null;

	[SerializeField]
	private GameInterfaceController gameInterfaceController = null;

	private EndGameState currentState = EndGameState.None;

	private bool towersChanged = false;
	private bool exitRequested = false;

	public void Awake()
	{
		shootingHelper.TowersChanged += OnTowersChanged;
		gameInterfaceController.EndGameScreen.ExitRequested += OnExitRequested;
		gameInterfaceController.IngameMenuScreen.ExitRequested += OnExitRequested;

		TransitionToState(EndGameState.Playing);
	}

	private void EnterState(EndGameState nextState)
	{
		switch(nextState)
		{
			case EndGameState.Playing:
				break;
			case EndGameState.End:
				if(!shootingHelper.AnyHostTowersAlive && !shootingHelper.AnyPlayerTowersAlive)
				{
					gameInterfaceController.EndGameScreen.Show(new EndGameDisplayResult
					{
						Status = EndGameStatus.Tied
					});
				}
				else
				{
					bool isThisTowerAlive = IsThisPlayerHost() ? shootingHelper.AnyHostTowersAlive : shootingHelper.AnyPlayerTowersAlive;
					if(isThisTowerAlive)
					{
						gameInterfaceController.EndGameScreen.Show(new EndGameDisplayResult
						{
							Status = EndGameStatus.Won
						});
					}
					else
					{
						gameInterfaceController.EndGameScreen.Show(new EndGameDisplayResult
						{
							Status = EndGameStatus.Lost
						});
					}
				}
				break;
			case EndGameState.Exiting:
				gameInterfaceController.EndGameScreen.Hide();
				PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
				PhotonNetwork.Disconnect();
				break;
		}
	}

	private void ExitState(EndGameState previousState)
	{
		switch(previousState)
		{
			case EndGameState.Playing:
				break;
			case EndGameState.End:
				break;
			case EndGameState.Exiting:
				break;
		}
	}

	private void UpdateState(EndGameState state)
	{
		switch(state)
		{
			case EndGameState.Playing:
				// This transition happens when we click exit in the ingame menu.
				if(exitRequested)
				{
					exitRequested = false;
					TransitionToState(EndGameState.Exiting);
				}
				else if(towersChanged)
				{
					towersChanged = false;
					if(!shootingHelper.AnyHostTowersAlive || !shootingHelper.AnyPlayerTowersAlive)
					{
						TransitionToState(EndGameState.End);
					}
				}
				break;
			case EndGameState.End:
				if(exitRequested)
				{
					TransitionToState(EndGameState.Exiting);
				}
				break;
			case EndGameState.Exiting:
				break;
		}

		towersChanged = false;
		exitRequested = false;
	}

	private void TransitionToState(EndGameState nextState)
	{
		if(currentState != EndGameState.None)
		{
			ExitState(currentState);
		}

		if(nextState != EndGameState.None)
		{
			EnterState(nextState);
		}

		currentState = nextState;
	}

	private void OnTowersChanged()
	{
		towersChanged = true;
		UpdateState(currentState);
	}

	private void OnExitRequested()
	{
		exitRequested = true;
		UpdateState(currentState);
	}

	private bool IsThisPlayerHost()
	{
		return PhotonNetwork.LocalPlayer.IsMasterClient;
	}

	public override void OnDisconnected(DisconnectCause cause)
	{
		SceneManager.LoadSceneAsync(MainMenuScene);
	}
}
