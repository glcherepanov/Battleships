using Photon.Realtime;

public static class PlayerNameUtility
{
	public static string GetName(Player player)
	{
		if(player.NickName.Length != 0)
		{
			return player.NickName;
		}

		return player.IsMasterClient ? "host" : "player";
	}
}
