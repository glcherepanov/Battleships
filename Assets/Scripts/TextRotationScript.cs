using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRotationScript : MonoBehaviour
{
	public TextMesh Text;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if(Text && Camera.main)
		{
			Text.transform.rotation = Camera.main.transform.rotation;
		}
	}
}
