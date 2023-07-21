using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[BurstCompiler]
public class MovePositions
{
    public Position position;

    //[BurstCompiler]
    public void OnUpdate()
    {
        ++position.x;
        --position.y;
    }
}
