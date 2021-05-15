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
		playerCategoryLabel.text = PlayerCategoryToLabel(playerCategory);
		playerNameLabel.text = playerName;
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
