using UnityEngine;

public class DebugLoader
{
	[RuntimeInitializeOnLoadMethod]
	public static void Initialize()
	{
		GameObject consolePrefab = Resources.Load<GameObject>("IngameDebugConsole");
		GameObject.Instantiate(consolePrefab);
	}
}
