using UnityEngine;
using TMPro;

public enum PlayerCategory
{
	Owner,
	Normal
}

public class PlayerInfoBox : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI playerCategoryLabel = null;
	[SerializeField]
	private TextMeshProUGUI playerNameLabel = null;

	public void Awake()
	{
		playerCategoryLabel.text = "";
		playerNameLabel.text = "";
	}

	public void SetPlayerInfo(string playerName, PlayerCategory playerCategory)
	{
		playerCategoryLabel.text = FormatPlayerCategoryLabel(playerCategory);
		playerNameLabel.text = playerName;
	}

	private string FormatPlayerCategoryLabel(PlayerCategory playerCategory)
	{
		string label = PlayerCategoryToLabel(playerCategory);
		if(label.Length != 0)
		{
			label += ":";
		}

		return label;
	}

	private string PlayerCategoryToLabel(PlayerCategory playerCategory)
	{
		switch(playerCategory)
		{
			case PlayerCategory.Owner:
				return "room owner";
			default:
				return "player";
			
		}
	}
}
