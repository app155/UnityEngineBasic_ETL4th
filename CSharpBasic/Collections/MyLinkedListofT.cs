using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{

    public class MyLinkedListNode<T>
    {
        public T? Value => _value;
        public MyLinkedListNode<T>? prev;
        public MyLinkedListNode<T>? next;

        private T? _value;

        public MyLinkedListNode(T value)
        {
            this._value = value;
        }
    }

    internal class MyLinkedList<T>
    {
        public int Count => first == null && last == null ? 0 : _count;

        public MyLinkedListNode<T>? first;
        public MyLinkedListNode<T>? last;

        private int _count;

        public void AddFirst(MyLinkedListNode<T> node)
        {
            if (_count == 0)
                last = node;

            else
            {
                first.prev = node;
                node.next = first;
            }

            first = node;
            _count++;
        }

        public void AddLast(MyLinkedListNode<T> node)
        {
            if (_count == 0)
                first = node;

            else
            {
                node.prev = last;
                last.next = node;
            }

            last = node;
            _count++;
        }

        public void AddAfterPivot(MyLinkedListNode<T> pivot, MyLinkedListNode<T> node)
        {
            if (pivot == last)
                AddLast(node);

            else
            {
                pivot.next.prev = node;
                node.prev = pivot;
                node.next = pivot.next;
                pivot.next = node;
                _count++;
            }
        }

        public void AddBeforePivot(MyLinkedListNode<T> pivot, MyLinkedListNode<T> node)
        {
            if (pivot == first)
                AddFirst(node);

            else
            {
                pivot.prev.next = node;
                node.next = pivot;
                node.prev = pivot.prev;
                pivot.prev = node;
                _count++;
            }
        }

        public bool RemoveFirst()
        {
            if (_count < 1)
                return false;

            else if (_count == 1)
            {
                first = null;
                last = null;
            }

            else
            {
                first = first.next;
                first.prev = null;
            }

            _count--;
            return true;
        }

        public bool RemoveLast()
        {
            if (_count < 1)
                return false;

            else if (_count == 1)
            {
                first = null;
                last = null;
            }

            else
            {
                last = last.prev;
                last.next = null;
            }

            _count--;
            return true;
        }

        public void Remove(MyLinkedListNode<T> node)
        {
            if (node == first)
                RemoveFirst();

            else if (node == last)
                RemoveLast();

            else
            {
                node.next.prev = node.prev;
                node.prev.next = node.next;
                _count--;
            }
        }

        public T Find(T value)
        {
            MyLinkedListNode<T> now = first;

            while (now.next != null)
            {
                if (now.Value.Equals(value))
                    return value;

                now = now.next;
            }

            return default;
        }
    }
}
