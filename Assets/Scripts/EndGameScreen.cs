using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI winStatus = null;
	[SerializeField]
	private Button quitButton = null;

	private void Awake()
	{
		quitButton.onClick.AddListener(OnQuitClicked);
	}

	private void OnQuitClicked()
	{

	}
}
