using UnityEngine;
using UnityEngine.SceneManagement;

public class OrientationControl
{
	private enum Orientation
	{
		Landscape,
		Portrait
	}

	private const string LevelScene = "Level";

	[RuntimeInitializeOnLoadMethod]
	public static void Initialize()
	{
		UpdateOrientation(SceneManager.GetActiveScene());
		SceneManager.sceneLoaded += SceneLoaded;
	}

	private static void SceneLoaded(Scene scene, LoadSceneMode mode)
	{
		UpdateOrientation(scene);
	}

	private static void UpdateOrientation(Scene scene)
	{
		if(scene.name == LevelScene)
		{
			ForceOrientation(Orientation.Landscape);
		}
		else
		{
			ForceOrientation(Orientation.Portrait);
		}
	}

	private static void ForceOrientation(Orientation orientation)
	{
		switch(orientation)
		{
			case Orientation.Landscape:
				Screen.autorotateToPortrait = false;
				Screen.autorotateToPortraitUpsideDown = false;
				Screen.autorotateToLandscapeLeft = true;
				Screen.autorotateToLandscapeRight = true;
				Screen.orientation = ScreenOrientation.Landscape;
				break;
			case Orientation.Portrait:
				Screen.autorotateToPortrait = true;
				Screen.autorotateToPortraitUpsideDown = true;
				Screen.autorotateToLandscapeLeft = false;
				Screen.autorotateToLandscapeRight = false;
				Screen.orientation = ScreenOrientation.Portrait;
				break;
		}
	}
}
