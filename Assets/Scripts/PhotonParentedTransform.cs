using Photon.Pun;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(PhotonTransformView))]
public class PhotonParentedTransform : MonoBehaviour, IPunInstantiateMagicCallback
{
	public PhotonTransformView TransformView
	{
		get
		{
			if(transformView == null)
			{
				transformView = GetComponent<PhotonTransformView>();
			}

			return transformView;
		}
	}
	private PhotonTransformView transformView = null;

	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		var instantiationData = info.photonView.InstantiationData;
		if(instantiationData != null && instantiationData.Length != 0)
		{
			string parentId = (string)instantiationData[0];
			var parents = GameObject.FindObjectsOfType<PhotonNetworkedParent>();
			PhotonNetworkedParent parent = null;

			for(int i = 0; i < parents.Length; ++i)
			{
				if(parents[i].Id == parentId)
				{
					parent = parents[i];
					break;
				}
			}

			if(parent != null)
			{
				// Transform coordinates just in case PhotonTransformView already synced local position before this callback.
				Vector3 localPosition = TransformView.transform.localPosition;
				Quaternion localRotation = TransformView.transform.localRotation;
				TransformView.transform.parent = parent.transform;
				TransformView.transform.localPosition = localPosition;
				TransformView.transform.localRotation = localRotation;
			}
		}
	}

	public static GameObject InstantiateParented(string prefabName, Vector3 position, Quaternion rotation, PhotonNetworkedParent parent)
	{
		return PhotonNetwork.Instantiate(prefabName, position, rotation, data: new object[] { parent.Id });
	}

	private static string GetHierarchyPath(Transform t)
	{
		var hierarchy = new List<Transform>();

		Transform current = t;
		hierarchy.Add(current);

		while(current.parent != null)
		{
			current = current.parent;
			hierarchy.Add(current);
		}

		var path = new StringBuilder();
		for(int i = hierarchy.Count - 1; i >= 0; --i)
		{
			path.Append(hierarchy[i].gameObject.name);
			if(i >= 0)
			{
				path.Append("/");
			}
		}

		return path.ToString();
	}
}
