using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    public struct KeyValuePair<T, K> : IEquatable<KeyValuePair<T, K>>
        where T : IEquatable<T>
        where K : IEquatable<K>
    {
        public T? Key;
        public K? Value;

        public KeyValuePair(T? key, K? value)
        {
            Key = key;
            Value = value;
        }

        public bool Equals(KeyValuePair<T, K> other)
        {
            return other.Key.Equals(Key) && other.Value.Equals(Value);
        }
    }

    internal class MyHashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IEquatable<TKey>
        where TValue : IEquatable<TValue>
    {
        // 인덱서
        public TValue this[TKey key]
        {
            get
            {
                var bucket = _buckets[Hash(key)];

                if (bucket == null)
                    throw new Exception($"[MyHashTable<{nameof(TKey)}, {nameof(TValue)}>] : Key {key} doesn't exist");

                for (int i = 0; i < bucket.Count; i++)
                {
                    if (bucket[i].Key.Equals(key))
                        return bucket[i].Value;
                }

                throw new Exception($"[MyHashTable<{nameof(TKey)}, {nameof(TValue)}>] : Key {key} doesn't exist");
            }

            set
            {
                var bucket = _buckets[Hash(key)];

                if (bucket == null)
                    throw new Exception($"[MyHashTable<{nameof(TKey)}, {nameof(TValue)}>] : Key {key} doesn't exist");

                for (int i = 0; i < bucket.Count; i++)
                {
                    if (bucket[i].Key.Equals(key))
                        bucket[i] = new KeyValuePair<TKey, TValue>(key, value);
                }

                throw new Exception($"[MyHashTable<{nameof(TKey)}, {nameof(TValue)}>] : Key {key} doesn't exist");
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                List<TKey> keys = new List<TKey>();

                for (int i = 0; i < _validIndexList.Count; i++)
                {
                    for (int j = 0; j < _buckets[_validIndexList[i]].Count; j++)
                    {
                        keys.Add(_buckets[_validIndexList[i]][j].Key);
                    }
                }

                return keys;
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                List<TValue> values = new List<TValue>();

                for (int i = 0; i < _validIndexList.Count; i++)
                {
                    for (int j = 0; j < _buckets[_validIndexList[i]].Count; j++)
                    {
                        values.Add(_buckets[_validIndexList[i]][j].Value);
                    }
                }

                return values;
            }
        }

        private const int DEFAULT_SIZE = 100;
        private List<KeyValuePair<TKey, TValue>>[] _buckets = new List<KeyValuePair<TKey, TValue>>[DEFAULT_SIZE];
        private List<int> _validIndexList = new List<int>(); // 등록된 key값이 있는 인덱스 목록

        public void Add(TKey key, TValue value)
        {
            int index = Hash(key);
            List<KeyValuePair<TKey, TValue>> bucket = _buckets[index];

            // 해당 인덱스에 버킷이 없다면 새로 생성
            if (bucket == null)
            {
                _buckets[index] = new List<KeyValuePair<TKey, TValue>>();
                _validIndexList.Add(index);
            }

            else
            {
                // 버킷이 있다면 중복 키가 존재하는지 확인 -> 존재한다면 예외 throw
                for(int i = 0; i < bucket.Count; i++)
                {
                    if (bucket[i].Key.Equals(key))
                        throw new Exception($"[MyHashTable<{nameof(TKey)}, {nameof(TValue)}>] : Key {key} already exist");
                }
            }

            bucket.Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public bool TryAdd(TKey key, TValue value)
        {
            int index = Hash(key);
            List<KeyValuePair<TKey, TValue>> bucket = _buckets[index];

            // 해당 인덱스에 버킷이 없다면 새로 생성
            if (bucket == null)
            {
                _buckets[index] = new List<KeyValuePair<TKey, TValue>>();
                _validIndexList.Add(index);
            }

            else
            {
                // 버킷이 있다면 중복 키가 존재하는지 확인 -> 존재한다면 예외 throw
                for (int i = 0; i < bucket.Count; i++)
                {
                    if (bucket[i].Key.Equals(key))
                        return false;
                }
            }

            bucket.Add(new KeyValuePair<TKey, TValue>(key, value));
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int index = Hash(key);

            List<KeyValuePair<TKey, TValue>> bucket = _buckets[index];

            if (bucket != null)
            {
                for (int i = 0; i < bucket.Count; i++)
                {
                    if (bucket[i].Key.Equals(key))
                    {
                        value = bucket[i].Value;
                        return true;
                    }
                }
            }

            value = default;
            return false;
        }

        public bool Remove(TKey key)
        {
            // 구현해보기
            return false;
        }

        public int Hash(TKey key)
        {
            string keyName = key.ToString();
            int result = 0;

            for (int i = 0; i < keyName.Length; i++)
            {
                result += keyName[i];
            }

            result %= DEFAULT_SIZE;

            return result;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
