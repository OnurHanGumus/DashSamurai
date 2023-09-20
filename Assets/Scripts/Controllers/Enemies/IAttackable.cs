using UnityEngine.Events;

public interface IAttackable
{
    void OnWeaponTriggerEnter(int value);
    bool IsMoving { get; }
}