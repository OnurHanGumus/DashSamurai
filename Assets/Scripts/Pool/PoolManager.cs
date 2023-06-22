using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Events.External;
using Enums;
using Signals;

public class PoolManager : MonoBehaviour
{
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }

    [Inject] private BulletManager.Factory bulletFactory;
    [Inject] private EnemyManager.Factory enemyFactory;
    [Inject] private ExplosionManager.Factory explosionFactory;
    #region Serialized Variables

    [SerializeField] private Dictionary<PoolEnums, List<GameObject>> poolDictionary;
    [SerializeField] private List<IPool> factoryList;


    [SerializeField] private int amountBullet = 20;
    [SerializeField] private int amountEnemy = 20;
    [SerializeField] private int amountExplosion = 20;



    #endregion
    #region Event Subscriptions
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        poolDictionary = new Dictionary<PoolEnums, List<GameObject>>();
        factoryList = new List<IPool>();

        factoryList.Add(bulletFactory);
        factoryList.Add(enemyFactory);
        factoryList.Add(explosionFactory);

        InitializePool(PoolEnums.Bullet, amountBullet);
        InitializePool(PoolEnums.Enemy, amountEnemy);
        InitializePool(PoolEnums.Explosion, amountExplosion);
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PoolSignals.onGetObject += OnGetObject;
        CoreGameSignals.onRestart += OnReset;
    }

    private void UnsubscribeEvents()
    {
        PoolSignals.onGetObject -= OnGetObject;
        CoreGameSignals.onRestart -= OnReset;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void InitializePool(PoolEnums type, int size)
    {
        List<GameObject> tempList = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < size; i++)
        {
            tmp = factoryList[(int) type].OnCreate();
            tmp.SetActive(false);
            tmp.transform.parent = transform;
            tempList.Add(tmp);
        }
        poolDictionary.Add(type, tempList);
    }

    public GameObject OnGetObject(PoolEnums type, Vector3 position)
    {
        for (int i = 0; i < poolDictionary[type].Count; i++)
        {
            if (!poolDictionary[type][i].activeInHierarchy)
            {
                poolDictionary[type][i].transform.position = position;
                poolDictionary[type][i].gameObject.SetActive(true);

                return poolDictionary[type][i];
            }
        }
        return ExplandPool(type, position);
    }

    private GameObject ExplandPool(PoolEnums type, Vector3 position)
    {
        GameObject expandObject = factoryList[(int)type].OnCreate();
        expandObject.transform.position = position;
        expandObject.transform.parent = transform;
        poolDictionary[type].Add(expandObject);

        return expandObject;
    }

    private void OnReset()
    {
        //reset
        ResetPool(PoolEnums.Bullet);
        ResetPool(PoolEnums.Enemy);
        ResetPool(PoolEnums.Explosion);
    }

    private void ResetPool(PoolEnums type)
    {
        foreach (var i in poolDictionary[type])
        {
            i.SetActive(false);
        }
    }
}
