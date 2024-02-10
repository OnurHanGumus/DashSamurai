using System;
using UnityEngine;
using Zenject;

public class EnemyWaterDetector : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables
    [Inject] private PoolSignals _poolSignals { get; set; }
    [Inject] private AudioSignals _audioSignals { get; set; }
    [Inject] private EnemyInternalSignals _enemyInternalSignals { get; set; }
    #endregion

    #region Public Variables

    #endregion
    #region Serialized Variables
    [SerializeField] private float raycastDistance = 1.5f;
    #endregion
    #region Private Variables
    private bool _isDropable = true;
    private Ray ray;
    private RaycastHit hit;
    private bool _isDead = false;
    #endregion
    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        SubscribeSignals();
    }

    private void SubscribeSignals()
    {
        _enemyInternalSignals.onDeath += OnDead;
    }

    void Update()
    {
        if (!_isDead)
        {
            return;
        }

        if (!_isDropable)
        {
            return;
        }

        ray = new Ray(transform.position, transform.forward);

        //Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.blue);

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            if (hit.collider != null)
            {
                Vector3 hitPoint = hit.point;
                if (!hit.transform.CompareTag("Water"))
                {
                    return;
                }

                _audioSignals.onPlaySound?.Invoke(Enums.AudioSoundEnums.WaterSplash);
                GameObject particle = _poolSignals.onGetObject(PoolEnums.WaterSplash, hitPoint);
                particle.SetActive(true);
                _isDropable = false;
            }
        }
    }

    private void OnEnable()
    {
        _isDropable = true;
        _isDead = false;
    }

    private void OnDead(IAttackable attackable)
    {
        _isDead = true;
    }
}