using System.Collections.Generic;
using UnityEngine;

public class LobbyLogPanel : MonoBehaviour
{
	[SerializeField]
	private RectTransform messageLineParent = null;
	[SerializeField]
	private LobbyMessageLine messageLinePrefab = null;

	private List<LobbyMessageLine> lines = new List<LobbyMessageLine>();

	public void Log(string line)
	{
		LobbyMessageLine lineObject = Instantiate(messageLinePrefab, messageLineParent);
		lineObject.SetContent(line);
		lines.Add(lineObject);
	}

	public void ClearLogs()
	{
		for(int i = lines.Count - 1; i >= 0; --i)
		{
			LobbyMessageLine line = lines[i];
			if(line != null)
			{
				Destroy(line);
			}

			lines.RemoveAt(i);
		}
	}
}
