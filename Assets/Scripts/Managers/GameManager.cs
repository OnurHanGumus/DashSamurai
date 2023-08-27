using System;
using Enums;
using Signals;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    #region Self Variables

    //[Inject(Id = "DenemeController")] IDeneme DenemeController;
    //[Inject(Id = "DenemeController2")] IDeneme DenemeController2;
    #region Public Variables

    public GameStates States;

    #endregion

    #endregion

    private void Awake()
    {
        Application.targetFrameRate = 120;
        //DenemeController.Dene();
        //DenemeController2.Dene();
    }


    private void OnEnable()
    {
        SubscribeEvents();
    }


    private void SubscribeEvents()
    {
    }

    private void UnsubscribeEvents()
    {
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
}