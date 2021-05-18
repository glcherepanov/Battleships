using System;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScreen : MonoBehaviour
{
	[SerializeField]
	private ExampleExpressionBox exampleExpressionBox = null;
	[SerializeField]
	private ExampleAnswerPanel exampleAnswerPanel = null;
	[SerializeField]
	private ExampleAnswerResultPanel exampleAnswerResultPanel = null;

	public event Action<ExampleAnswerResponse> ExampleAnswered;

	public void Awake()
	{
		exampleAnswerPanel.ExampleAnswered += OnExampleAnswered;

		SetExampleExpression("");
	}

	private void OnExampleAnswered(ExampleAnswerResponse response)
	{
		ExampleAnswered?.Invoke(response);
	}

	public void SetExampleExpression(string expression)
	{
		exampleExpressionBox.SetExpression(expression);
	}

	public void SetAnswersVisible(bool isVisible)
	{
		exampleAnswerPanel.SetAnswersVisible(isVisible);
	}

	public void SetAvailableAnswers(List<int> answers)
	{
		exampleAnswerPanel.SetAvailableAnswers(answers);
	}

	public void ShowResult(string playerName, bool isCorrect)
	{
		exampleAnswerResultPanel.ShowResult(playerName, isCorrect);
	}

	public void HideResult()
	{
		exampleAnswerResultPanel.HideResult();
	}
}
