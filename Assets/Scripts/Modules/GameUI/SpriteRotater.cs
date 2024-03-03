using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotater : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables
    #endregion
    #region Public Variables

    #endregion

    #region Serialized Variables
    #endregion

    #region Private Variables

    #endregion

    #endregion

    protected virtual void Awake() //This script should at the back of parent object while parent object rotation is 0 and look forward.
    {
        Init();
    }

    protected virtual void Init()
    {

    }

    protected virtual void Update()
    {
        RotateSprite();
    }

    protected virtual void RotateSprite()
    {
        transform.eulerAngles = new Vector3(60, 0, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
