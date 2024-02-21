using System;
using Controllers;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ZenjectExample/PlayerSettings", order = 0)]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] public Vector3 CameraOffset;
    [SerializeField] public float Speed = 1f;
    [SerializeField] public AnimationCurve SpeedCurve;
    [SerializeField] public Quaternion InitialQuaternion;
}