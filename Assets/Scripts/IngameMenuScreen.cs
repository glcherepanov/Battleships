using System;
using UnityEngine;
using UnityEngine.UI;

public class IngameMenuScreen : MonoBehaviour
{
	[SerializeField]
	private Button exitButton = null;

	public event Action ExitRequested;

	public void Awake()
	{
		exitButton.onClick.AddListener(OnExitClicked);
	}

	private void OnExitClicked()
	{
		ExitRequested?.Invoke();
	}
}
