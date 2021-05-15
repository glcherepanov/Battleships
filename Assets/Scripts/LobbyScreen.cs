using System;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScreen : MonoBehaviour
{
	[SerializeField]
	private RoomInfoPanel roomInfoPanel = null;

	[SerializeField]
	private Button startGameButton = null;
	[SerializeField]
	private Button selectLevelButton = null;
	[SerializeField]
	private Button exitButton = null;

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

		SetLobbyControl(false);
	}

	public void AddPlayer(int playerId, string playerName, PlayerCategory playerCategory)
	{
		roomInfoPanel.AddPlayer(playerId, playerName, playerCategory);
	}

	public void RemovePlayer(int playerId)
	{
		roomInfoPanel.RemovePlayer(playerId);
	}

	public void SetLobbyControl(bool allow)
	{
		allowLobbyControl = allow;

		if(allowLobbyControl)
		{
			startGameButton.gameObject.SetActive(true);
			selectLevelButton.gameObject.SetActive(true);
		}
		else
		{
			startGameButton.gameObject.SetActive(false);
			selectLevelButton.gameObject.SetActive(false);
		}
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
