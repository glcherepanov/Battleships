using System;
using System.Collections.Generic;
using UnityEngine;

public class ExampleAnswerResponse
{
	public int Answer { get; set; }
}

public class ExampleAnswerPanel : MonoBehaviour
{
	private struct AnswerInfo
	{
		public int Answer { get; set; }
	}

	[SerializeField]
	private ExampleAnswerButton exampleAnswerButtonPrefab = null;

	[SerializeField]
	private RectTransform exampleAnswerButtonParent = null;

	private List<ExampleAnswerButton> answerButtons = new List<ExampleAnswerButton>();
	private Dictionary<int, AnswerInfo> buttonToAnswer = new Dictionary<int, AnswerInfo>();

	public bool ShowAnswers { get; private set; } = false;

	public event Action<ExampleAnswerResponse> ExampleAnswered;

	public void Awake()
	{
		for(int i = 0; i < 5; ++i)
		{
			ExampleAnswerButton button = Instantiate(exampleAnswerButtonPrefab, exampleAnswerButtonParent);
			int buttonId = i;
			button.SetId(i);

			answerButtons.Add(button);
			buttonToAnswer.Add(buttonId, new AnswerInfo { Answer = 0 });
			button.Clicked += OnAnswerButtonClicked;
		}

		SetAnswersVisible(false);
	}

	public void SetAnswersVisible(bool isVisible)
	{
		for(int i = 0; i < answerButtons.Count; ++i)
		{
			ExampleAnswerButton button = answerButtons[i];
			button.gameObject.SetActive(isVisible);
		}
	}

	public void SetAvailableAnswers(List<int> answers)
	{
		for(int i = 0; i < answers.Count; ++i)
		{
			ExampleAnswerButton button = answerButtons[i];
			button.SetAnswer(answers[i]);
		}
	}

	private void OnAnswerButtonClicked(ExampleAnswerButton button)
	{
		AnswerInfo info = buttonToAnswer[button.ButtonId];
		var response = new ExampleAnswerResponse { Answer = info.Answer };
		ExampleAnswered?.Invoke(response);
	}
}
