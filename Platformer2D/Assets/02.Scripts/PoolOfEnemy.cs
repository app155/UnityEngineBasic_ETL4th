using Platformer.Controllers;
using Platformer.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using EnemyController = Platformer.Controllers.EnemyController;

public class PoolOfEnemy : MonoBehaviour
{
    public class PooledItem : MonoBehaviour
    {
        public IObjectPool<EnemyController> pool;
        private EnemyController _enemy;

        private void Awake()
        {
            _enemy = GetComponent<EnemyController>();
        }

        private void OnDisable()
        {
            ReturnToPool();
        }

        public void ReturnToPool()
        {
            pool.Release(_enemy);
        }
    }

    public enum PoolType
    {
        Stack,
        LinkedList,
    }

    [SerializeField] private PoolType _collectionType;
    [SerializeField] private bool _collectionCheck;


    public IObjectPool<EnemyController> pool
    {
        get
        {
            if (_pool == null)
            {
                if (_collectionType == PoolType.Stack)
                {
                    _pool = new ObjectPool<EnemyController>(CreatePooledItem,
                                                        OnGetFromPool,
                                                        OnReturnToPool,
                                                        OnDestroyPooledItem,
                                                        _collectionCheck,
                                                        _count,
                                                        _countMax);
                }

                else
                {
                    _pool = new LinkedPool<EnemyController>(CreatePooledItem,
                                                        OnGetFromPool,
                                                        OnReturnToPool,
                                                        OnDestroyPooledItem,
                                                        _collectionCheck,
                                                        _countMax);
                }
            }

            return _pool;
        }
    }

    private IObjectPool<EnemyController> _pool;
    [SerializeField] private EnemyController _prefab;
    [SerializeField] private int _count;
    [SerializeField] private int _countMax;


    private EnemyController CreatePooledItem()
    {
        EnemyController item = Instantiate(_prefab);
        item.gameObject.AddComponent<PooledItem>().pool = pool;

        return item;
    }

    private void OnGetFromPool(EnemyController damagePopUp)
    {
        damagePopUp.gameObject.SetActive(true);
    }

    private void OnReturnToPool(EnemyController damagePopUp)
    {
        damagePopUp.gameObject.SetActive(false);
    }

    private void OnDestroyPooledItem(EnemyController damagePopUp)
    {
        Destroy(damagePopUp.gameObject);
    }
}
