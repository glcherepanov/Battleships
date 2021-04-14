using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
	private string winLabel = "you won";
	[SerializeField]
	private string lossLabel = "you lost";
	[SerializeField]
	private string tieLabel = "tied";

	[SerializeField]
	private TextMeshProUGUI winStatus = null;
	[SerializeField]
	private Button quitButton = null;

	public event Action QuitRequested;

	private void Awake()
	{
		quitButton.onClick.AddListener(OnQuitClicked);

		Hide();
	}

	private void OnQuitClicked()
	{
		QuitRequested?.Invoke();
	}

	public void Show(EndGameDisplayResult result)
	{
		winStatus.text = GetLabelForStatus(result.Status);
		gameObject.SetActive(true);
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
		gameObject.SetActive(false);
	}
}
