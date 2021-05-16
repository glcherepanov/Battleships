using UnityEngine;

public class LobbyLogPanel : MonoBehaviour
{
	[SerializeField]
	private RectTransform messageLineParent = null;
	[SerializeField]
	private LobbyMessageLine messageLinePrefab = null;

	public void Log(string line)
	{
		LobbyMessageLine lineObject = Instantiate(messageLinePrefab, messageLineParent);
		lineObject.SetContent(line);
	}
}
