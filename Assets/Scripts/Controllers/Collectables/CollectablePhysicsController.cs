using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CollectablePhysicsController : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables
    [Inject] private AbilitySignals _abilitySignals { get; set; }
    [Inject] private PoolSignals _poolSignals { get; set; }

    #endregion

    #region Public Variables
    public CollectableEnums CollectableEnum;
    #endregion
    #region Serialized Variables
    [SerializeField] private PoolEnums collectParticle;

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
        GameObject particle = _poolSignals.onGetObject(collectParticle, new Vector3(transform.position.x, 2, transform.position.z));
        particle.SetActive(true);
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
