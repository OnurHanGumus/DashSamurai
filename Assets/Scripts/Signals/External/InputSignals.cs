using UnityEngine;
using UnityEngine.Events;

public class InputSignals
{
    public UnityAction onEnableInput = delegate { };
    public UnityAction onDisableInput = delegate { };
    public UnityAction onFirstTimeTouchTaken = delegate { };
    public UnityAction onInputTaken = delegate { };
    public UnityAction<Vector3> onClicking = delegate { };
    public UnityAction onInputReleased = delegate { };
    public UnityAction onClicked = delegate { };
    public UnityAction<Vector3> onInputDragged = delegate { };

}