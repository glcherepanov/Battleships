using System;
using UnityEngine;

public class LevelSelectionRequest
{
	public int LevelId { get; set; }
}

public class LevelSelectionScreen : MonoBehaviour
{
	public event Action<LevelSelectionRequest> LevelSelectionRequested;
}
