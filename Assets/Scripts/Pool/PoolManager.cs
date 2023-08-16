using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using Signals;

public class PoolManager : MonoBehaviour
{
    #region Self Variables
    #region Injections
    [Inject] private DiContainer Container;
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }

    #endregion
    #region Serialized Variables
    [SerializeField] private List<PooledObject> pooledObjects;

    [System.Serializable]
    public struct PooledObject
    {
        public PoolEnums PoolEnums;
        public GameObject Prefab;
        public int Amounts;
    }
    #endregion

    #region Private Variables
    private Dictionary<PoolEnums, List<GameObject>> _poolDictionary;
    #endregion
    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        #region Zenject Factory Pool
        _poolDictionary = new Dictionary<PoolEnums, List<GameObject>>();

        foreach (var i in pooledObjects)
        {
            InitializePool(i);
        }

        #endregion
    }
    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PoolSignals.onGetObject += OnGetObject;
        PoolSignals.onGetObjectExpanded += OnGetObject;
        CoreGameSignals.onRestart += OnReset;
    }

    private void UnsubscribeEvents()
    {
        PoolSignals.onGetObject -= OnGetObject;
        PoolSignals.onGetObjectExpanded -= OnGetObject;
        CoreGameSignals.onRestart -= OnReset;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion

    private void InitializePool(PooledObject pooledObject) //Enumlar?n s?ralamas? ?nemlidir.
    {
        List<GameObject> tempList = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < pooledObject.Amounts; i++)
        {
            tmp = Container.InstantiatePrefab(pooledObject.Prefab);

            tmp.SetActive(false);
            tmp.transform.parent = transform;
            tempList.Add(tmp);
        }
        _poolDictionary.Add(pooledObject.PoolEnums, tempList);
    }

    public GameObject OnGetObject(PoolEnums type, Vector3 position)
    {
        for (int i = 0; i < _poolDictionary[type].Count; i++)
        {
            if (!_poolDictionary[type][i].activeInHierarchy)
            {
                _poolDictionary[type][i].transform.position = position;

                return _poolDictionary[type][i];
            }
        }
        return ExplandPool(type, position);
    }

    public GameObject OnGetObject(PoolEnums type, Vector3 position, Quaternion quaternion)
    {
        for (int i = 0; i < _poolDictionary[type].Count; i++)
        {
            if (!_poolDictionary[type][i].activeInHierarchy)
            {
                _poolDictionary[type][i].transform.position = position;
                _poolDictionary[type][i].transform.rotation = quaternion;

                return _poolDictionary[type][i];
            }
        }
        return ExplandPool(type, position);
    }

    private GameObject ExplandPool(PoolEnums type, Vector3 position)
    {
        GameObject expandObject = null;
        foreach (var i in pooledObjects)
        {
            if (i.PoolEnums.Equals(type))
            {
                expandObject = Container.InstantiatePrefab(i.Prefab);
            }
        }

        expandObject.SetActive(false);
        expandObject.transform.position = position;
        expandObject.transform.parent = transform;
        _poolDictionary[type].Add(expandObject);

        return expandObject;
    }

    private void OnReset()
    {
        //reset
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            ResetPool((PoolEnums)i);
        }
    }

    private void ResetPool(PoolEnums type)
    {
        foreach (var i in _poolDictionary[type])
        {
            i.SetActive(false);
        }
    }
}