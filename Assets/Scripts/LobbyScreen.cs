using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScreen : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI titleLabel = null;

	[SerializeField]
	private RoomInfoPanel roomInfoPanel = null;

	[SerializeField]
	private Button startGameButton = null;
	[SerializeField]
	private Button selectLevelButton = null;
	[SerializeField]
	private Button exitButton = null;

	[SerializeField]
	private LobbyLogPanel lobbyLogPanel = null;

	private MenuController menuController = null;
	private bool allowLobbyControl = false;

	public event Action GameStartRequested;
	public event Action LobbyExitRequested;

	public void Awake()
	{
		menuController = GetComponentInParent<MenuController>();

		startGameButton.onClick.AddListener(StartGameClicked);
		selectLevelButton.onClick.AddListener(SelectLevelClicked);
		exitButton.onClick.AddListener(ExitClicked);

		SetLobbyName("Комната");
		SetLobbyControl(false);
	}

	public void SetLobbyName(string lobbyName)
	{
		titleLabel.text = lobbyName;
	}

	public void AddPlayer(int playerId, string playerName, PlayerCategory playerCategory)
	{
		roomInfoPanel.AddPlayer(playerId, playerName, playerCategory);
	}

	public void RemovePlayer(int playerId)
	{
		roomInfoPanel.RemovePlayer(playerId);
	}

	public void ClearPlayers()
	{
		roomInfoPanel.ClearPlayers();
	}

	public void SetLobbyControl(bool allow)
	{
		allowLobbyControl = allow;

		if(allowLobbyControl)
		{
			if(startGameButton != null)
			{
				startGameButton.gameObject.SetActive(true);
			}
			if(selectLevelButton != null)
			{
				selectLevelButton.gameObject.SetActive(true);
			}
		}
		else
		{
			if(startGameButton != null)
			{
				startGameButton.gameObject.SetActive(false);
			}
			if(selectLevelButton != null)
			{
				selectLevelButton.gameObject.SetActive(false);
			}
		}
	}

	public void LogLobbyJoined(string lobbyName)
	{
		lobbyLogPanel.Log($"Подключение к комнате: {lobbyName}");
	}

	public void LogLobbyCreated(string lobbyName)
	{
		lobbyLogPanel.Log($"Комната создана: {lobbyName}");
	}

	public void LogLobbyFailed(string lobbyName)
	{
		lobbyLogPanel.Log($"Не удалось подключиться к комнате: {lobbyName}");
	}

	public void LogPlayerJoined(string playerName)
	{
		lobbyLogPanel.Log($"Подключился игрок - {playerName}");
	}

	public void LogPlayerLeft(string playerName)
	{
		lobbyLogPanel.Log($"{playerName} покинул комнату");
	}

	public void ClearLogs()
	{
		lobbyLogPanel.ClearLogs();
	}

	private void StartGameClicked()
	{
		GameStartRequested?.Invoke();
	}

	private void SelectLevelClicked()
	{
		menuController.GoToLocation(MenuLocation.LevelSelection);
	}

	private void ExitClicked()
	{
		LobbyExitRequested?.Invoke();
		menuController.GoToLocation(MenuLocation.PreLobby);
	}
}
