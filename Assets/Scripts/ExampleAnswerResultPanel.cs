using TMPro;
using UnityEngine;

public class ExampleAnswerResultPanel : MonoBehaviour
{
	[SerializeField]
	private RectTransform panelRoot = null;
	[SerializeField]
	private TextMeshProUGUI resultLabel = null;

	public void Awake()
	{
		HideResult();
	}

	public void ShowResult(string playerName, bool isCorrect)
	{
		resultLabel.text = FormatResult(playerName, isCorrect);
		panelRoot.gameObject.SetActive(true);
	}

	public void HideResult()
	{
		resultLabel.text = string.Empty;
		panelRoot.gameObject.SetActive(false);
	}

	private string FormatResult(string playerName, bool isCorrect)
	{
		return $"{playerName} ответил {(isCorrect ? "верно" : "неверно")}";
	}
}
