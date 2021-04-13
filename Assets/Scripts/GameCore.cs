using UnityEngine;

public class GameCore
{
	private const string GameSettingsPrefabName = "Game Settings";

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void Initialize()
	{
		var settings = Resources.Load<GameSettings>(GameSettingsPrefabName);
		if(settings == null)
		{
			Debug.LogError($"Missing game settings. Make sure to create a GameSettings scriptable object called {GameSettingsPrefabName} and put it in the Resources folder");
			return;
		}


	}
}
