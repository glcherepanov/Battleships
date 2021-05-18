using ThisOtherThing.UI.Shapes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Button))]
[DefaultExecutionOrder(-10)]
public class StyledButton : MonoBehaviour
{
	private enum ButtonState
	{
		None,
		Normal
	}

	public Button ControlledButton
	{
		get
		{
			if(controlledButton == null)
			{
				controlledButton = GetComponent<Button>();
			}

			return controlledButton;
		}
	}
	private Button controlledButton = null;

	[SerializeField]
	private Rectangle targetBackground = null;

	[SerializeField]
	private Color normalColor = Color.white;
	[SerializeField]
	private Color shadowColor = Color.black;

	public void OnEnable()
	{
		SetDirty();
	}

	public void OnValidate()
	{
		SetDirty();
	}

	private void SetDirty()
	{
		ControlledButton.transition = Selectable.Transition.None;

		if(targetBackground != null)
		{
			targetBackground.ShapeProperties.FillColor = normalColor;
			for(int i = 0; i < targetBackground.ShadowProperties.Shadows.Length; ++i)
			{
				var shadow = targetBackground.ShadowProperties.Shadows[i];
				shadow.Color = shadowColor;
			}
			targetBackground.ForceMeshUpdate();
		}
	}
}
