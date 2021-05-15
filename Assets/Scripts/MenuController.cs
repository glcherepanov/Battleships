using UnityEngine;

public enum MenuLocation
{
	None,
	PreLobby,
	Lobby,
	LevelSelection
}

public class MenuController : MonoBehaviour
{
	public PreLobbyScreen PreLobbyScreen => preLobbyScreen;
	[SerializeField]
	private PreLobbyScreen preLobbyScreen = null;

	public LobbyScreen LobbyScreen => lobbyScreen;
	[SerializeField]
	private LobbyScreen lobbyScreen = null;

	public LevelSelectionScreen LevelSelectionScreen => levelSelectionScreen;
	[SerializeField]
	private LevelSelectionScreen levelSelectionScreen = null;

	private MenuLocation currentLocation = MenuLocation.None;

	public void Awake()
	{
		preLobbyScreen.gameObject.SetActive(false);
		lobbyScreen.gameObject.SetActive(false);
		levelSelectionScreen.gameObject.SetActive(false);

		GoToLocation(MenuLocation.PreLobby);
	}

	public void GoToLocation(MenuLocation location)
	{
		if(currentLocation == location)
		{
			return;
		}

		if(currentLocation != MenuLocation.None)
		{
			ExitLocation(currentLocation);
		}

		if(location != MenuLocation.None)
		{
			EnterLocation(location);
		}

		currentLocation = location;
	}

	public void EnterLocation(MenuLocation location)
	{
		switch(location)
		{
			case MenuLocation.PreLobby:
				preLobbyScreen.gameObject.SetActive(true);
				break;
			case MenuLocation.Lobby:
				lobbyScreen.gameObject.SetActive(true);
				break;
			case MenuLocation.LevelSelection:
				levelSelectionScreen.gameObject.SetActive(true);
				break;
			default:
				break;
		}
	}

	public void ExitLocation(MenuLocation location)
	{
		switch(location)
		{
			case MenuLocation.PreLobby:
				preLobbyScreen.gameObject.SetActive(false);
				break;
			case MenuLocation.Lobby:
				lobbyScreen.gameObject.SetActive(false);
				break;
			case MenuLocation.LevelSelection:
				levelSelectionScreen.gameObject.SetActive(false);
				break;
			default:
				break;
		}
	}
}
