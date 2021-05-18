using UnityEngine;

public class GameInterfaceController : MonoBehaviour
{
	public ExampleScreen ExampleScreen => exampleScreen;
	[SerializeField]
	private ExampleScreen exampleScreen = null;

	public IngameMenuScreen IngameMenuScreen => ingameMenuScreen;
	[SerializeField]
	private IngameMenuScreen ingameMenuScreen = null;

	public EndGameScreen EndGameScreen => endGameScreen;
	[SerializeField]
	private EndGameScreen endGameScreen = null;
}
