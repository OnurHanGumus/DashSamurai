using System;
using Controllers;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BomberUISettings", menuName = "ZenjectExample/BomberUISettings", order = 0)]
public class BomberUiSettings : ScriptableObject
{
    [SerializeField] public int TextAppearedAtSecond = 5;
    [SerializeField] public float TextScaleAnimationDelay = 0.2f;

}