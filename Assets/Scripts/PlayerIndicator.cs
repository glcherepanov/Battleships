using UnityEngine;
using TMPro;

public class PlayerIndicator : MonoBehaviour
{
	[SerializeField]
	private TextMeshPro playerNameLabel = null;

	public void SetPlayerName(string playerName)
	{
		playerNameLabel.text = playerName;
	}
}
