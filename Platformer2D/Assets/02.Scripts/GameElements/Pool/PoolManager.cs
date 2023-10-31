using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Platformer.GameElements.Pool
{
    public class PoolManager
    {
        public static PoolManager instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PoolManager();

                return _instance;
            }
        }

        private static PoolManager _instance;

        private Dictionary<PoolTag, IObjectPool<GameObject>> _pools = new Dictionary<PoolTag, IObjectPool<GameObject>>();

        public void Register(PoolTag tag, IObjectPool<GameObject> pool)
        {
            _pools.Add(tag, pool);
        }

        public IObjectPool<GameObject> GetPool(PoolTag tag)
        {
            return _pools[tag];
        }

        public GameObject Get(PoolTag tag)
        {
            return _pools[tag].Get();
        }

        public T Get<T>(PoolTag tag)
        {
            return _pools[tag].Get().GetComponent<T>();
        }
    }
}