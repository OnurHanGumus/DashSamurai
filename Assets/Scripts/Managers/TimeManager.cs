using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TimeManager : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private LevelSignals LevelSignals { get; set; }
    [Inject] private SaveSignals SaveSignals { get; set; }
    #endregion
    #region Public Variables


    #endregion

    #region Serialized Variables

    #endregion

    #region Private Variables

    #endregion

    #endregion

}
