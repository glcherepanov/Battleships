using TMPro;
using UnityEngine;

public class LobbyMessageLine : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI label = null;

	public void SetContent(string content)
	{
		label.text = content;
	}
}
