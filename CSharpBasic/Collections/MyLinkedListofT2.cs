using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class MyLinkedList2Node<T>
    {
        public T? Value;
        public MyLinkedList2Node<T>? Prev;
        public MyLinkedList2Node<T>? Next;

        public MyLinkedList2Node(T? value)
        {
            Value = value;
        }
    }

    internal class MyLinkedList2<T> : IEnumerable<T>
        where T : IEquatable<T>
    {
        public MyLinkedList2Node<T> First => _first;
        public MyLinkedList2Node<T> Last => _last;
        public int Count => _count;

        private MyLinkedList2Node<T>? _first, _last, _tmp;
        private int _count;

        /// <summary>
        /// 가장 앞에 삽입
        /// </summary>
        public void AddFirst(T value)
        {
            _tmp = new MyLinkedList2Node<T>(value);

            // 하나 이상의 노드가 존재하는지 확인. (기존 first 확인)
            if (_first != null)
            {
                // 새로 만든 노드가 새롭게 first노드가 되어야 함.
                _tmp.Next = _first; 
                _first.Prev = _tmp;
            }

            else
            {
                _last = _tmp;
            }

            _first = _tmp;
            _count++;
        }

        /// <summary>
        /// 가장 뒤에 삽입
        /// </summary>
        public void AddLast(T value)
        {
            _tmp = new MyLinkedList2Node<T>(value);

            if (_last != null)
            {
                _tmp.Prev = _last;
                _last.Next = _tmp;
            }

            else
            {
                _first = _tmp;
            }

            _last = _tmp;
            _count++;
        }

        /// <summary>
        /// 특정 노드 앞에 삽입
        /// </summary>
        /// <param name="node"> 기준 노드 </param>
        public void AddBefore(MyLinkedList2Node<T> node, T value)
        {
            _tmp = new MyLinkedList2Node<T>(value);

            // 기준노드 앞에 다른 노드가 존재한다면
            if (node.Prev != null)
            {
                node.Prev.Next = _tmp;
                _tmp.Prev = node.Prev;
            }

            // 기준노드가 first였다면 새로 생성한 노드를 first로 갱신
            else
            {
                _first = _tmp;
            }

            node.Prev = _tmp;
            _tmp.Next = node;
            _count++;
        }

        public void AddAfter(MyLinkedList2Node<T> node, T value)
        {
            _tmp = new MyLinkedList2Node<T>(value);

            if (node.Next != null)
            {
                node.Next.Prev = _tmp;
                _tmp.Next = node.Next;
            }

            else
            {
                _last = _tmp;
            }

            node.Next = _tmp;
            _tmp.Prev = node;
            _count++;
        }

        /// <summary>
        /// first부터 match조건에 맞는 노드를 찾을 때까지 Next탐색
        /// </summary>
        public MyLinkedList2Node<T> Find(Predicate<T> match)
        {
            _tmp = _first;

            while (_tmp != null)
            {
                if (match(_tmp.Value))
                    return _tmp;

                _tmp = _tmp.Next;
            }

            return null;
        }

        /// <summary>
        /// last부터 match조건에 맞는 노드를 찾을 때가지 Prev탐색
        /// </summary>
        public MyLinkedList2Node<T> FindLast(Predicate<T> match)
        {
            _tmp = _last;

            while (_tmp != null)
            {
                if (match(_tmp.Value))
                    return _tmp;

                _tmp = _tmp.Prev;
            }

            return null;
        }

        public bool Remove(MyLinkedList2Node<T> node)
        {
            if (node == null)
                return false;

            if (node.Prev != null)
            {
                node.Prev.Next = _tmp.Next;
            }

            else
            {
                _first = node.Next;
            }

            if (node.Next != null)
            {
                node.Next.Prev = node.Prev;
            }

            else
            {
                _last = node.Prev;
            }

            _count--;
            return true;
        }

        public bool Remove(T value)
        {
            return Remove(Find(x => x.Equals(value)));
        }

        public bool RemoveLast(T value)
        {
            return Remove(FindLast(x => x.Equals(value)));
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
            public T Current => _node.Value;

            object IEnumerator.Current => _node.Value;

            private MyLinkedList2<T> _data;
            private MyLinkedList2Node<T>? _node;
            private MyLinkedList2Node<T>? _error;

            public Enumerator(MyLinkedList2<T> data)
            {
                _data = data;
                _node = _error = new MyLinkedList2Node<T>(default);
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                if (_node == null)
                    return false;

                _node = _node == _error ? _data.First : _node.Next;

                return _node != null;
            }

            public void Reset()
            {
                _node = _error;
            }
        }
    }
}
