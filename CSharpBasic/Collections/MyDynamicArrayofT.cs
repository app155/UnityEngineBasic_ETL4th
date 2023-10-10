using System.Collections;

// PascalCase : 사용자정의 자료형, 함수, 프로퍼티, public/protected 멤버 변수 이름
// camelCase : 지역 변수 + (Unity API 활용 시 public/protected 멤버 변수 이름)
// _camelCase : private 멤버 변수
// snake_case : 상수 정의시
// UPPER_SNAKE_CASE : 상수 정의시
// m_Hungarian : static 멤버 변수     (요즘엔 잘 안씀)



namespace Collections
{
    internal class MyDynamicArray<T> : IEnumerable<T>
        where T : IComparable<T> // where 제한자 : 타입을 제한하는 한정자 (T에 넣을 타입은 IComparable<T>에 공변 가능해야함)
    {

        // 인덱스 탐색
        // 시간 복잡도 : O(1)
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > _count - 1)
                    throw new IndexOutOfRangeException();

                return _items[index];
            }
            set
            {
                if (index < 0 || index > _count - 1)
                    throw new IndexOutOfRangeException();

                _items[index] = value;
            }
        }
        public int Count => _count;
        public int Capacity => _items.Length;

        private int _count;
        private const int DEFAULT_SIZE = 1;
        private T[] _items = new T[DEFAULT_SIZE];
        
        // 아이템 삽입
        // 시간 복잡도 : O(N) <- Array.Copy()
        public void Add(T item)
        {
            if (_count >= _items.Length)
            {
                T[] tmp = new T[_count * 2];
                Array.Copy(_items, tmp, _count);
                _items = tmp;
            }

            _items[_count] = item;
            _count++;
        }


        // 매치 조건 탐색
        // 시간 복잡도 : O(N)
        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return _items[i];
            }

            return default;
        }

        public int FindIndex(Predicate<T> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return i;
            }

            return -1;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < _count; i++)
            {
                // Default 비교연산 (C# 기본제공 비교연산)
                // if (Comparer<T>.Default.Compare(_items[i], item) == 0)
                //     return true;

                // IComparable 비교연산 (사용자 정의 비교연산)
                if (item.CompareTo(_items[i]) == 0)
                    return true;
            }

            return false;
        }

        // 삭제
        // 시간 복잡도 : O(N)
        public void RemoveAt(int index)
        {
            if (index < 0 || index > _count - 1)
                throw new IndexOutOfRangeException();

            for (int i = index; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }

            _count--;
        }

        // 시간 복잡도 : O(N)
        public bool Remove(T item)
        {
            int index = FindIndex((x) => item.CompareTo(x) == 0);

            if (index < 0)
                return false;

            RemoveAt(index);
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<T>
        {
            public T Current => _data[_index];

            object IEnumerator.Current => _data[_index];

            private MyDynamicArray<T> _data;
            private int _index;

            public Enumerator(MyDynamicArray<T> data)
            {
                _data = data;
                _index = -1;
            }

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                // 다음 인덱스로 이동 가능하다면 이동하고 true 반환
                if (_index < _data._count - 1)
                {
                    _index++;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                _index = -1;
            }
        }
    }
}