using UnityEngine;
using Vuforia;

[DefaultExecutionOrder(100)]
public class DesktopTest : MonoBehaviour
{
	private TrackableBehaviour trackableBehaviour = null;

	private void Awake()
	{
		if(!Application.isEditor && Application.isMobilePlatform)
		{
			Destroy(this);
			return;
		}

		trackableBehaviour = GetComponent<TrackableBehaviour>();
	}

	private void Update()
	{
		var rendererComponents = trackableBehaviour.GetComponentsInChildren<Renderer>(true);
		var colliderComponents = trackableBehaviour.GetComponentsInChildren<Collider>(true);
		var canvasComponents = trackableBehaviour.GetComponentsInChildren<Canvas>(true);

		// Enable rendering:
		foreach(var component in rendererComponents)
			component.enabled = true;

		// Enable colliders:
		foreach(var component in colliderComponents)
			component.enabled = true;

		// Enable canvas':
		foreach(var component in canvasComponents)
			component.enabled = true;
	}
}