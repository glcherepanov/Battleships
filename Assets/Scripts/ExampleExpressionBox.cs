using TMPro;
using UnityEngine;

public class ExampleExpressionBox : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI expressionLabel = null;

	public void Awake()
	{
		SetExpression("");
	}

	public void SetExpression(string expression)
	{
		expressionLabel.text = expression;
	}
}
