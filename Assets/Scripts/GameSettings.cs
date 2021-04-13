using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
	public UISettings UISettings => uiSettings;
	[SerializeField]
	private UISettings uiSettings = null;
}