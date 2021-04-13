using System.Collections;
using UnityEngine;
using System.Linq;

public class MainMenu : MonoBehaviour
{
	private enum MainMenuState
	{
		None,
		PreLobby,
		Lobby
	}

	private enum MainMenuEvent
	{
		None,
		PreLobbyJoin,
		PreLobbyCreate,
		LobbyLeave,
		LobbyStartGame
	}

	[SerializeField]
	private UISettings uiSettings = null;
	[SerializeField]
	private Canvas canvas = null;

	private PreLobbyMenu preLobbyMenu = null;
	private LobbyMenu lobbyMenu = null;

	private MainMenuState state = MainMenuState.None;

	private void Awake()
	{
		preLobbyMenu = Instantiate(uiSettings.PreLobbyMenu, Vector3.zero, Quaternion.identity, canvas.transform);
		lobbyMenu = Instantiate(uiSettings.LobbyMenu, Vector3.zero, Quaternion.identity, canvas.transform);

		preLobbyMenu.RequestedCreate += PreLobbyRequestedCreate;
		preLobbyMenu.RequestedJoin += PreLobbyRequestedJoin;

		lobbyMenu.LeaveRequested += LobbyLeaveRequested;
		lobbyMenu.StartGameRequested += LobbyStartGameRequested;

		TransitionToState(MainMenuState.PreLobby, immediately: true);
	}

	private void LobbyStartGameRequested()
	{
		HandleEvent(MainMenuEvent.LobbyStartGame);
	}

	private void LobbyLeaveRequested()
	{
		HandleEvent(MainMenuEvent.LobbyLeave);
	}

	private void PreLobbyRequestedJoin()
	{
		HandleEvent(MainMenuEvent.PreLobbyJoin);
	}

	private void PreLobbyRequestedCreate()
	{
		HandleEvent(MainMenuEvent.PreLobbyCreate);
	}

	private void HandleEvent(MainMenuEvent e)
	{
		switch(state)
		{
			case MainMenuState.PreLobby:


				break;
			case MainMenuState.Lobby:
				if(e == MainMenuEvent.LobbyLeave)
				{
					TransitionToState(MainMenuState.PreLobby);
				}

				break;
		}
	}

	private void TransitionToState(MainMenuState state, bool immediately = false)
	{
		switch(state)
		{
			case MainMenuState.PreLobby:
				ShowOnly(preLobbyMenu, immediately);
				break;
			case MainMenuState.Lobby:
				ShowOnly(lobbyMenu, immediately);
				break;
		}
	}

	private void ShowOnly(Element subMenu, bool immediately)
	{
		foreach(Element e in AllSubMenus())
		{
			if(e == subMenu)
			{
				e.Show(immediately: immediately);
			}
			else
			{
				e.Hide(immediately: immediately);
			}
		}
	}

	private IEnumerable AllSubMenus()
	{
		yield return preLobbyMenu;
		yield return lobbyMenu;
	}
}
