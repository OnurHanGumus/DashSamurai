using Enums;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class UISignals
{
    public UnityAction<UIPanels> onOpenPanel;
    public UnityAction<UIPanels> onClosePanel;
    public UnityAction<bool> onCloseSuccessfulPanel;
    public UnityAction<bool> onLockJoystick;

    public UnityAction<ScoreTypeEnums, int> onSetChangedText;
}