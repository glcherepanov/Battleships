using UnityEngine;

public class DebugLoader
{
	[RuntimeInitializeOnLoadMethod]
	public static void Initialize()
	{
		if(!Application.isEditor && Debug.isDebugBuild)
		{
			GameObject consolePrefab = Resources.Load<GameObject>("IngameDebugConsole");
			GameObject.Instantiate(consolePrefab);
		}
	}
}
