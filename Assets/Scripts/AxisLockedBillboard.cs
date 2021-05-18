using UnityEngine;

public class AxisLockedBillboard : MonoBehaviour
{
	private Camera Camera
	{
		get
		{
			if(camera == null)
			{
				camera = Camera.main;
			}
			return camera;
		}
	}
	private new Camera camera;

	private void LateUpdate()
	{
		if(Camera == null)
		{
			return;
		}

		Vector3 projected = Camera.transform.forward;
		projected.y = 0;
		projected.Normalize();
		transform.rotation = Quaternion.LookRotation(-projected, Vector3.up);
	}
}
