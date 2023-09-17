using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CollectablePhysicsController : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables
    [Inject] protected AbilitySignals _abilitySignals { get; set; }


    #endregion

    #region Public Variables
    public CollectableEnums CollectableEnum;
    #endregion
    #region Serialized Variables

    #endregion
    #region Private Variables


    #endregion
    #endregion

    public void Awake()
    {
        //
    }

    public void OnTriggered()
    {
        Debug.Log(CollectableEnum + " is collected.");
        _abilitySignals.onActivateAbility?.Invoke(CollectableEnum);
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggered();
        }
    }
}
