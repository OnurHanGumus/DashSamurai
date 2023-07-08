using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;


public class HealthBarManager : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables
    [Inject] private PlayerSignals PlayerSignals { get; set; }
    #endregion
    #region Public Variables
    public TextMeshPro HealthText;

    #endregion

    #region Serialized Variables
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform healthBar;

    [SerializeField] private GameObject holder;
    #endregion

    #region Private Variables

    #endregion

    #endregion

    #region Event Subscription
    private void Start()
    {
        SubscribeEvent();
    }

    private void SubscribeEvent()
    {
        PlayerSignals.onHitted += SetHealthBarScale;
    }

    private void UnSubscribeEvent()
    {
        PlayerSignals.onHitted -= SetHealthBarScale;
    }

    private void OnDisable()
    {
        UnSubscribeEvent();
    }

    #endregion


    private void Awake()
    {
        Init();
    }

    private void Init()
    {

    }

    private void Update()
    {
        transform.eulerAngles = new Vector3(60, 0, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void SetHealthBarScale(int currentValue)//HealthBar increase or decrease with this method. This method can also listen a signal.
    {
        healthBar.localScale = new Vector3((float)currentValue / 100, 1, 1);
    }
}
