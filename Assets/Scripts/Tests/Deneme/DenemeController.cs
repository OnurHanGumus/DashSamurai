using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DenemeController : IDeneme, IInitializable
{
    public void Dene()
    {
        Debug.Log("dene");
    }

    public void Initialize()
    {
        Debug.Log("DenemeController Initialized");
    }
}
