using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DenemeController2 : IDeneme, IInitializable
{
    public void Dene()
    {
        Debug.Log("dene2");
    }

    public void Initialize()
    {
        Debug.Log("DenemeController2 Initialized");
    }
}
