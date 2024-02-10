using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Zenject;
using UnityEngine.UI;

public class SliderIncreaseAutomatically
{
    #region Self Variables
    #region Inject Variables

    #endregion
    #region Public Variables
    #endregion
    #region SerializeField Variables

    #endregion
    #region Private Variables
    private bool _isIncreasing = false;

    #endregion
    #endregion

    public async Task IncreaseValue(Slider slider, int _maksValue)
    {
        if (_isIncreasing)
        {
            return;
        }
        _isIncreasing = true;

        while (slider.value < _maksValue)
        {
            await Task.Delay(System.TimeSpan.FromSeconds(0.01f));
            slider.value += 1;
        }

        _isIncreasing = false;
    }
}
