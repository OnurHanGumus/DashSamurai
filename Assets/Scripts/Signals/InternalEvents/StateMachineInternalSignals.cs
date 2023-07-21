using Components.Enemies;
using Enums;
using System.Threading.Tasks;
using UnityEngine.Events;

public class StateMachineInternalSignals
{
    public UnityAction<EnemyStateEnums, bool> onChangeState = delegate { };
}