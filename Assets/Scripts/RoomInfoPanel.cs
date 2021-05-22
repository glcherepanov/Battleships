using Photon.Pun.Demo.Cockpit;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfoPanel : MonoBehaviour
{
	private class PlayerInfo
	{
		public int PlayerId { get; set; }
		public PlayerCategory PlayerCategory { get; set; }
		public PlayerInfoBox InfoBox { get; set; }
	}

	private class PlayerInfoComparer : IComparer<PlayerInfo>
	{
		public int Compare(PlayerInfo x, PlayerInfo y)
		{
			if(x.PlayerCategory == y.PlayerCategory)
			{
				return 0;
			}

			if(x.PlayerCategory == PlayerCategory.Owner)
			{
				return -1;
			}

			if(y.PlayerCategory == PlayerCategory.Owner)
			{
				return 1;
			}

			return 0;
		}
	}

	[SerializeField]
	private PlayerInfoBox playerInfoBoxPrefab = null;
	[SerializeField]
	private RectTransform playerInfoBoxParent = null;

	private List<PlayerInfo> infos = new List<PlayerInfo>();

	public void AddPlayer(int playerId, string playerName, PlayerCategory playerCategory)
	{
		if(infos.FindIndex(i => i.PlayerId == playerId) != -1)
		{
			return;
		}

		PlayerInfoBox infoBox = Instantiate(playerInfoBoxPrefab, playerInfoBoxParent);
		infoBox.SetPlayerInfo(playerName, playerCategory);

		var info = new PlayerInfo
		{
			PlayerId = playerId,
			PlayerCategory = playerCategory,
			InfoBox = infoBox
		};

		infos.Add(info);
		SortInfo();
	}

	public void RemovePlayer(int playerId)
	{
		int i = infos.FindIndex(info => info.PlayerId == playerId);
		if(i != -1)
		{
			var info = infos[i];
			if(info.InfoBox != null)
			{
				Destroy(info.InfoBox);
			}
			infos.RemoveAt(i);
			SortInfo();
		}
	}

	public void ClearPlayers()
	{
		for(int i = infos.Count - 1; i >= 0; --i)
		{
			var info = infos[i];
			if(info.InfoBox != null)
			{
				Destroy(info.InfoBox.gameObject);
			}
			infos.RemoveAt(i);
		}
	}

	private void SortInfo()
	{
		infos.Sort(new PlayerInfoComparer());
		int aliveIndex = 0;

		for(int i = 0; i < infos.Count; ++i)
		{
			var box = infos[i].InfoBox;
			if(box != null)
			{
				box.transform.SetSiblingIndex(aliveIndex++);
			}
		}
	}
}
