using System;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMenu : Element
{
	[SerializeField]
	private Button leaveButton = null;
	[SerializeField]
	private Button selectLevelButton = null;
	[SerializeField]
	private Button startGameButton = null;

	public event Action LeaveRequested;
	public event Action StartGameRequested;

	private void OnEnable()
	{
		leaveButton.onClick.AddListener(OnLeaveClicked);
		selectLevelButton.onClick.AddListener(OnSelectLevelClicked);
		startGameButton.onClick.AddListener(OnStartGameClicked);
	}

	private void OnDisable()
	{
		leaveButton.onClick.RemoveListener(OnLeaveClicked);
		selectLevelButton.onClick.RemoveListener(OnSelectLevelClicked);
		startGameButton.onClick.RemoveListener(OnStartGameClicked);
	}

	private void OnLeaveClicked()
	{
		LeaveRequested?.Invoke();
	}

	private void OnSelectLevelClicked()
	{

	}

	private void OnStartGameClicked()
	{
		StartGameRequested?.Invoke();
	}
}
