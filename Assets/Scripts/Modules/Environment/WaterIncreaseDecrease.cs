using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterIncreaseDecrease : MonoBehaviour
{
    [SerializeField] private Ease riseEase, rotateEase;
    [SerializeField] private float rotateAmount = 10;
    [SerializeField] private float riseTime = 1.5f, rotateTime = 1.5f;
    [SerializeField] private float riseMax = 0.1f, riseMin = -0.4f;
    private void Start()
    {
        Rise();
        Rotate();
    }

    private void Rise()
    {
        transform.DOMoveY(riseMax, riseTime).SetEase(riseEase).OnComplete(() =>
        {
            transform.DOMoveY(riseMin, riseTime).SetEase(riseEase).OnComplete(() =>
            {
                Rise();
            });
        });
    }

    private void Rotate()
    {
        transform.DOLocalRotate(new Vector3(0, 0, rotateAmount), rotateTime).SetEase(rotateEase).OnComplete(() =>
        {
            transform.DOLocalRotate(new Vector3(0, 0, -rotateAmount), rotateTime).SetEase(rotateEase).OnComplete(() =>
            {
                Rotate();
            });
        });
    }
}
