using System;
using UnityEngine;
using UnityEngine.UI;

public class PreLobbyMenu : Element
{
	[SerializeField]
	private TMPro.TMP_InputField userName = null;
	[SerializeField]
	private TMPro.TMP_InputField lobbyName = null;

	[SerializeField]
	private Button joinButton = null;
	[SerializeField]
	private Button createButton = null;

	public string Username => userName.text;
	public string LobbyName => lobbyName.text;

	public event Action RequestedJoin;
	public event Action RequestedCreate;

	private void OnEnable()
	{
		userName.text = string.Empty;
		lobbyName.text = string.Empty;

		userName.onValueChanged.AddListener(OnUsernameChanged);
		lobbyName.onValueChanged.AddListener(OnLobbyNameChanged);

		joinButton.onClick.AddListener(OnJoinClicked);
		createButton.onClick.AddListener(OnCreateClicked);
	}

	private void OnDisable()
	{
		userName.onValueChanged.RemoveListener(OnUsernameChanged);
		lobbyName.onValueChanged.RemoveListener(OnLobbyNameChanged);

		joinButton.onClick.RemoveListener(OnJoinClicked);
		createButton.onClick.RemoveListener(OnCreateClicked);
	}

	private void OnUsernameChanged(string newName)
	{

	}

	private void OnLobbyNameChanged(string newName)
	{

	}

	private void OnJoinClicked()
	{
		RequestedJoin?.Invoke();
	}

	private void OnCreateClicked()
	{
		RequestedCreate?.Invoke();
	}
}
