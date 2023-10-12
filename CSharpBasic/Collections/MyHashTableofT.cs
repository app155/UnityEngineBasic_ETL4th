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
                bucket = _buckets[index] = new List<KeyValuePair<TKey, TValue>>();
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
            //_buckets[index].Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public bool TryAdd(TKey key, TValue value)
        {
            int index = Hash(key);
            List<KeyValuePair<TKey, TValue>> bucket = _buckets[index];

            // 해당 인덱스에 버킷이 없다면 새로 생성
            if (bucket == null)
            {
                bucket = _buckets[index] = new List<KeyValuePair<TKey, TValue>>();
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
            // _buckets[index].Add(new KeyValuePair<TKey, TValue>(key, value));
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
            int index = Hash(key);
            List<KeyValuePair<TKey, TValue>> bucket = _buckets[index];

            if (bucket != null)
            {
                for (int i = 0; i < bucket.Count; i++)
                {
                    if (bucket[i].Key.Equals(key))
                    {
                        bucket.RemoveAt(i);

                        if (bucket.Count == 0)
                            _validIndexList.Remove(index);

                        return true;
                    }
                }
            }

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
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            public KeyValuePair<TKey, TValue> Current => _pair;

            object IEnumerator.Current => _pair;

            private MyHashTable<TKey, TValue> _data;
            private KeyValuePair<TKey, TValue> _pair;
            private int _validListIndex; // validIndexList의 인덱스
            private int _bucketIndex; // validListIndex를 통해 접근한 bucket의 인덱스

            public Enumerator(MyHashTable<TKey, TValue> data)
            {
                _data = data;
                _pair = default;
                _validListIndex = -1;
                _bucketIndex = -1;
            }

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                // bucketIndex가 해당 bucket의 끝에 도달했다면 다른 bucket에 접근해야함.
                // validListIndex를 증가시키면 새 bucket에 접근하게 되므로 bucketIndex를 -1로 초기화함.
                // validListIndex는 validIndexList의 길이를 넘어설 수 없음.
                if (_validListIndex == -1 || 
                    (_validListIndex < _data._validIndexList.Count - 1 && 
                    _bucketIndex >= _data._buckets[_data._validIndexList[_validListIndex]].Count - 1))
                {
                    _validListIndex++;
                    _bucketIndex = -1;
                }
                    
                // validListIndex로 validIndexList에 인덱스 접근하여 데이터가 존재하는 buckets에 인덱스 접근
                // 아직 buckets의 끝에 도달하지 못했다면 이동 + _pair갱신
                // 이동에 성공했으니 true return
                if (_bucketIndex < _data._buckets[_data._validIndexList[_validListIndex]].Count - 1)
                {
                    _bucketIndex++;
                    _pair = _data._buckets[_data._validIndexList[_validListIndex]][_bucketIndex];
                    return true;
                }

                // 위 조건을 모두 빠져나왔다면 이동 불가능이므로 false return
                return false;
            }

            public void Reset()
            {
                _validListIndex = -1;
                _bucketIndex = -1;
            }
        }
    }
}
