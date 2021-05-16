using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionRequest
{
	public int LevelId { get; set; }
}

public enum LevelType
{
	None,
	Add,
	Substract
}

public class LevelSelectionScreen : MonoBehaviour
{
	private class LevelInfo
	{
		public int LevelId { get; set; }
		public LevelType LevelType { get; set; }
		public int UpperBound { get; set; }
	}

	private readonly LevelInfo[] LevelInfos = new[]
	{
		new LevelInfo
		{
			LevelId = 1,
			LevelType = LevelType.Add,
			UpperBound = 10
		},
		new LevelInfo
		{
			LevelId = 5,
			LevelType = LevelType.Substract,
			UpperBound = 10
		},
		new LevelInfo
		{
			LevelId = 2,
			LevelType = LevelType.Add,
			UpperBound = 20
		},
		new LevelInfo
		{
			LevelId = 6,
			LevelType = LevelType.Substract,
			UpperBound = 20
		},
		new LevelInfo
		{
			LevelId = 3,
			LevelType = LevelType.Add,
			UpperBound = 60
		},
		new LevelInfo
		{
			LevelId = 7,
			LevelType = LevelType.Substract,
			UpperBound = 60
		},
		new LevelInfo
		{
			LevelId = 4,
			LevelType = LevelType.Add,
			UpperBound = 100
		},
		new LevelInfo
		{
			LevelId = 8,
			LevelType = LevelType.Substract,
			UpperBound = 100
		},
	};

	[SerializeField]
	private RectTransform levelButtonParent = null;

	[SerializeField]
	private LevelButton levelButtonPrefab = null;

	private Dictionary<int, LevelInfo> buttonToLevelInfo = new Dictionary<int, LevelInfo>();
	private MenuController menuController = null;

	public event Action<LevelSelectionRequest> LevelSelectionRequested;

	public void Awake()
	{
		menuController = GetComponentInParent<MenuController>();

		for(int i = 0; i < LevelInfos.Length; ++i)
		{
			LevelInfo info = LevelInfos[i];
			LevelButton button = Instantiate(levelButtonPrefab, levelButtonParent);

			int buttonId = i;
			button.SetId(buttonId);
			button.SetLevelInfo(levelType: info.LevelType, upperBound: info.UpperBound);
			
			buttonToLevelInfo.Add(buttonId, info);
			button.Clicked += LevelButtonClicked;
		}
	}

	private void LevelButtonClicked(LevelButton button)
	{
		LevelInfo info = buttonToLevelInfo[button.LevelButtonId];
		var request = new LevelSelectionRequest
		{
			LevelId = info.LevelId
		};
		LevelSelectionRequested?.Invoke(request);

		menuController.GoToLocation(MenuLocation.Lobby);
	}
}
