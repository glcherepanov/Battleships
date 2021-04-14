using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviourPunCallbacks
{
	private enum EndGameState
	{
		None,
		Playing,
		End,
		Quitting
	}

	private const string MainMenuScene = "MainMenu";

	[SerializeField]
	private ShootingHelper shootingHelper = null;

	[SerializeField]
	private EndGameScreen endGameScreen = null;

	private EndGameState currentState = EndGameState.None;

	private bool towersChanged = false;
	private bool quitRequested = false;

	private void Awake()
	{
		shootingHelper.TowersChanged += OnTowersChanged;
		endGameScreen.QuitRequested += OnQuitRequested;

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
					endGameScreen.Show(new EndGameDisplayResult
					{
						Status = EndGameStatus.Tied
					});
				}
				else
				{
					bool isThisTowerAlive = IsThisPlayerHost() ? shootingHelper.AnyHostTowersAlive : shootingHelper.AnyPlayerTowersAlive;
					if(isThisTowerAlive)
					{
						endGameScreen.Show(new EndGameDisplayResult
						{
							Status = EndGameStatus.Won
						});
					}
					else
					{
						endGameScreen.Show(new EndGameDisplayResult
						{
							Status = EndGameStatus.Lost
						});
					}
				}
				break;
			case EndGameState.Quitting:
				endGameScreen.Hide();
				PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
				PhotonNetwork.LeaveRoom();
				break;
		}
	}

	private void ExitState(EndGameState nextState)
	{
		switch(nextState)
		{
			case EndGameState.Playing:
				break;
			case EndGameState.End:
				break;
			case EndGameState.Quitting:
				break;
		}
	}

	private void UpdateState(EndGameState state)
	{
		switch(state)
		{
			case EndGameState.Playing:
				if(towersChanged)
				{
					if(!shootingHelper.AnyHostTowersAlive || !shootingHelper.AnyPlayerTowersAlive)
					{
						TransitionToState(EndGameState.End);
					}
				}
				break;
			case EndGameState.End:
				if(quitRequested)
				{
					TransitionToState(EndGameState.Quitting);
				}
				break;
			case EndGameState.Quitting:
				break;
		}

		towersChanged = false;
		quitRequested = false;
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

	private void OnQuitRequested()
	{
		quitRequested = true;
		UpdateState(currentState);
	}

	private bool IsThisPlayerHost()
	{
		return PhotonNetwork.LocalPlayer.IsMasterClient;
	}

	public override void OnLeftRoom()
	{
		SceneManager.LoadSceneAsync(MainMenuScene);
	}
}
