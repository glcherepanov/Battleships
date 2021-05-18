using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExampleAnswerButton : MonoBehaviour
{
	[SerializeField]
	private Button controlledButton = null;

	[SerializeField]
	private TextMeshProUGUI answerLabel = null;

	public int ButtonId { get; private set; }

	public event Action<ExampleAnswerButton> Clicked;

	public void Awake()
	{
		controlledButton.onClick.AddListener(ButtonClicked);
	}

	public void SetId(int id)
	{
		ButtonId = id;
	}

	public void SetAnswer(int answer)
	{
		answerLabel.text = answer.ToString();
	}

	private void ButtonClicked()
	{
		Clicked?.Invoke(this);
	}
}
