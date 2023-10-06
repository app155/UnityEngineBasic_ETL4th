// PascalCase : 사용자정의 자료형, 함수, 프로퍼티, public/protected 멤버 변수 이름
// camelCase : 지역 변수 + (Unity API 활용 시 public/protected 멤버 변수 이름)
// _camelCase : private 멤버 변수
// snake_case : 상수 정의시
// UPPER_SNAKE_CASE : 상수 정의시
// m_Hungarian : static 멤버 변수     (요즘엔 잘 안씀)

namespace Collections
{
    internal class MyDynamicArray
    {
        public object this[int index]
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
        private object[] _items = new object[DEFAULT_SIZE];
        
        public void Add(object item)
        {
            if (_count >= _items.Length)
            {
                object[] tmp = new object[_count * 2];
                Array.Copy(_items, tmp, _count);
                _items = tmp;
            }

            _items[_count] = item;
            _count++;
        }

        public object Find(Predicate<object> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return _items[i];
            }

            return default;
        }

        public int FindIndex(Predicate<object> match)
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return i;
            }

            return -1;
        }

        public bool Contains(object item)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_items[i] == item)
                    return true;
            }

            return false;
        }

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

        public bool Remove(object item)
        {
            int index = FindIndex((x) => x == item);

            if (index < 0)
                return false;

            RemoveAt(index);
            return true;
        }
    }
}
