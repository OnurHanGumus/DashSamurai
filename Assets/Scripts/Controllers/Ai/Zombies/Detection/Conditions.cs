using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Conditions
{
    private ICondition[] _conditions;

    [Inject]
    public Conditions(params ICondition[] conditions)
    {
        _conditions = conditions;
    }

    public void IsSatisfied()
    {
        foreach (var i in _conditions)
        {
            i.IsSatisfied();
        }
    }
}
