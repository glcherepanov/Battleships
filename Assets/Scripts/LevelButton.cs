using System;
using ThisOtherThing.UI.Shapes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
	[SerializeField]
	private Button controlledButton = null;

	[SerializeField]
	private Ellipse levelTypeBackground = null;

	[SerializeField]
	private Color addBackgroundColor = Color.white;
	[SerializeField]
	private Color substractBackgroundColor = Color.white;

	[SerializeField]
	private TextMeshProUGUI levelTypeLabel = null;
	[SerializeField]
	private TextMeshProUGUI levelDescriptionLabel = null;

	public int LevelButtonId { get; private set; }

	public event Action<LevelButton> Clicked;

	public void Awake()
	{
		controlledButton.onClick.AddListener(ButtonClicked);
		levelTypeLabel.text = "?";
		levelDescriptionLabel.text = "?";
	}

	public void SetId(int id)
	{
		LevelButtonId = id;
	}

	public void SetLevelInfo(LevelType levelType, int upperBound)
	{
		levelTypeLabel.text = LevelTypeToLabel(levelType);
		levelDescriptionLabel.text = FormatLevelDescription(upperBound);
		
		levelTypeBackground.ShapeProperties.FillColor = GetBackgroundColorForType(levelType);
		levelTypeBackground.ForceMeshUpdate();
	}

	private void ButtonClicked()
	{
		Clicked?.Invoke(this);
	}

	private string LevelTypeToLabel(LevelType levelType)
	{
		switch(levelType)
		{
			case LevelType.Add:
				return "+";
			case LevelType.Substract:
				return "-";
			default:
				return "?";
		}
	}

	private Color GetBackgroundColorForType(LevelType levelType)
	{
		switch(levelType)
		{
			case LevelType.Add:
				return addBackgroundColor;
			case LevelType.Substract:
				return substractBackgroundColor;
			default:
				return addBackgroundColor;
		}
	}

	private string FormatLevelDescription(int upperBound)
	{
		return $"to {upperBound}";
	}
}
