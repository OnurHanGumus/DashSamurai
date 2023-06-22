using System;
using Controllers;
using Data.MetaData;
using Events.External;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ZenjectExample/PlayerSettings", order = 0)]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] public PlayerCameraController.Settings PlayerCameraControllerSettings;
    [SerializeField] public PlayerShootController.Settings PlayerShootManagerSettings;
    [SerializeField] public PlayerMovementController.Settings PlayerMovementSettings;
}