using UnityEngine.Events;

public interface IAttackable
{
    int Health { get; set; }
    void OnWeaponTriggerEnter(int value);
    bool IsMoving { get; }
}