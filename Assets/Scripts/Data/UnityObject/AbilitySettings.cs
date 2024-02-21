using Components.Players;
using Components.Enemies;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AbilitySettings", menuName = "ZenjectExample/AbilitySettings", order = 0)]
public class AbilitySettings : ScriptableObject
{
    public List<AbilityData> AbilityDatas;


}