using UnityEngine;

public abstract class Element : MonoBehaviour
{
	public virtual void Show(bool immediately)
	{
		gameObject.SetActive(true);
	}

	public virtual void Hide(bool immediately)
	{
		gameObject.SetActive(false);
	}
}
