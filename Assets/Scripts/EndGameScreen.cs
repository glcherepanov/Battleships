using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum EndGameStatus
{
	Won,
	Lost,
	Tied
}

public class EndGameDisplayResult
{
	public EndGameStatus Status { get; set; }
}

public class EndGameScreen : MonoBehaviour
{
	[SerializeField]
	private RectTransform panelRoot = null;

	[SerializeField]
	private string winLabel = "you won";
	[SerializeField]
	private string lossLabel = "you lost";
	[SerializeField]
	private string tieLabel = "tied";

	[SerializeField]
	private TextMeshProUGUI winStatusLabel = null;
	[SerializeField]
	private Button exitButton = null;

	public event Action ExitRequested;

	public void Awake()
	{
		exitButton.onClick.AddListener(OnExitClicked);

		Hide();
	}

	private void OnExitClicked()
	{
		ExitRequested?.Invoke();
	}

	public void Show(EndGameDisplayResult result)
	{
		winStatusLabel.text = GetLabelForStatus(result.Status);
		panelRoot.gameObject.SetActive(true);
	}

	private string GetLabelForStatus(EndGameStatus status)
	{
		switch(status)
		{
			case EndGameStatus.Won:
				return winLabel;
			case EndGameStatus.Lost:
				return lossLabel;
			case EndGameStatus.Tied:
				return tieLabel;
			default:
				return "???";
		}
	}

	public void Hide()
	{
		panelRoot.gameObject.SetActive(false);
	}
}
