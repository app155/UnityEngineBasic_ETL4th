using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Platformer.GameElements.Pool
{
    public class ParticleSystemPoolManager
    {
        public static ParticleSystemPoolManager instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ParticleSystemPoolManager();

                return _instance;
            }
        }

        private static ParticleSystemPoolManager _instance;

        private Dictionary<PoolTag, IObjectPool<ParticleSystem>> _pools = new Dictionary<PoolTag, IObjectPool<ParticleSystem>>();

        public void Register(PoolTag tag, IObjectPool<ParticleSystem> pool)
        {
            _pools.Add(tag, pool);
        }

        public IObjectPool<ParticleSystem> GetPool(PoolTag tag)
        {
            return _pools[tag];
        }

        public ParticleSystem Get(PoolTag tag)
        {
            return _pools[tag].Get();
        }

        public T Get<T>(PoolTag tag)
        {
            return _pools[tag].Get().GetComponent<T>();
        }
    }
}