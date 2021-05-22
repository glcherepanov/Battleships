using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCreateRequest
{
	public string PlayerName { get; set; }
	public string LobbyName { get; set; }
}

public class LobbyJoinRequest
{
	public string PlayerName { get; set; }
	public string LobbyName { get; set; }
}

public class PreLobbyScreen : MonoBehaviour
{
	[SerializeField]
	private TMP_InputField playerNameInput = null;
	[SerializeField]
	private TMP_InputField lobbyNameInput = null;

	[SerializeField]
	private Button joinButton = null;
	[SerializeField]
	private Button createButton = null;

	public event Action<LobbyCreateRequest> LobbyCreateRequested;
	public event Action<LobbyJoinRequest> LobbyJoinRequested;

	private MenuController menuController = null;

	public void Awake()
	{
		menuController = GetComponentInParent<MenuController>();

		joinButton.onClick.AddListener(JoinClicked);
		createButton.onClick.AddListener(CreateClicked);
	}

	private void JoinClicked()
	{
		var request = new LobbyJoinRequest
		{
			PlayerName = playerNameInput.text,
			LobbyName = lobbyNameInput.text
		};

		menuController.GoToLocation(MenuLocation.Lobby);
		LobbyJoinRequested?.Invoke(request);
	}

	private void CreateClicked()
	{
		var request = new LobbyCreateRequest
		{
			PlayerName = playerNameInput.text,
			LobbyName = lobbyNameInput.text
		};

		menuController.GoToLocation(MenuLocation.Lobby);
		LobbyCreateRequested?.Invoke(request);
	}
}
