using UnityEngine.Events;

namespace Components.Enemies
{
    public interface IAttackable
    {
        void OnWeaponTriggerEnter(int value);
        bool IsMoving { get;}
    }
}