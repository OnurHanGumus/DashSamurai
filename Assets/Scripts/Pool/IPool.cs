using UnityEngine;
using UnityEngine.Events;
using Zenject;

public interface IPool
{
    GameObject OnCreate();
}