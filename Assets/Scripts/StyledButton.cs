using ThisOtherThing.UI.Shapes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Button))]
public class StyledButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private enum ButtonState
	{
		None,
		Normal,
		Pressed
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
	private Color pressedColor = Color.grey;

	private ButtonState currentState = ButtonState.None;

	public void OnEnable()
	{
		TransitionToState(ButtonState.Normal);
	}

	public void OnValidate()
	{
		SetDirty();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if(currentState == ButtonState.Normal)
		{
			TransitionToState(ButtonState.Pressed);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if(currentState == ButtonState.Pressed)
		{
			TransitionToState(ButtonState.Normal);
		}
	}

	private void EnterState(ButtonState nextState)
	{
		SetDirty();
	}

	private void ExitState(ButtonState nextState)
	{
		
	}

	private void TransitionToState(ButtonState nextState)
	{
		if(currentState != ButtonState.None)
		{
			ExitState(currentState);
		}

		if(nextState != ButtonState.None)
		{
			EnterState(nextState);
		}

		currentState = nextState;
	}

	private Color GetColorForState(ButtonState state)
	{
		if(state == ButtonState.Pressed)
		{
			return pressedColor;
		}

		return normalColor;
	}

	private void SetDirty()
	{
		ControlledButton.transition = Selectable.Transition.None;

		if(targetBackground != null)
		{
			targetBackground.ShapeProperties.FillColor = GetColorForState(currentState);
			targetBackground.ForceMeshUpdate();
		}
	}
}
