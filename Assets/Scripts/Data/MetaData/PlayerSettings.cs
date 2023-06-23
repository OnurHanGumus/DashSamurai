using System;
using Controllers;
using Data.MetaData;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ZenjectExample/PlayerSettings", order = 0)]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] public PlayerCameraController.Settings PlayerCameraControllerSettings;
    [SerializeField] public PlayerMovementController.Settings PlayerMovementSettings;
}