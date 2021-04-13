using UnityEngine;

[CreateAssetMenu]
public class UISettings : ScriptableObject
{
	public PreLobbyMenu PreLobbyMenu => preLobbyMenu;
	[SerializeField]
	private PreLobbyMenu preLobbyMenu = null;

	public LobbyMenu LobbyMenu => lobbyMenu;
	[SerializeField]
	private LobbyMenu lobbyMenu = null;

}
