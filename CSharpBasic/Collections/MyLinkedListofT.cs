using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{

    public class Node<T>
    {
        public T? Value => _value;
        public Node<T>? prev;
        public Node<T>? next;

        private T? _value;

        public Node(T value)
        {
            this._value = value;
        }
    }

    internal class MyLinkedList<T>
    {
        public int Count => first == null && last == null ? 0 : _count;

        public Node<T>? first;
        public Node<T>? last;

        private int _count;

        public void AddFirst(Node<T> node)
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

        public void AddLast(Node<T> node)
        {
            if (_count == 0)
            {
                first = node;
            }


            else
            {
                node.prev = last;
                last.next = node;
            }
            last = node;
            _count++;
        }

        public void AddAfterPivot(Node<T> pivot, Node<T> node)
        {
            pivot.next.prev = node;
            node.prev = pivot;
            node.next = pivot.next;
            pivot.next = node;
            _count++;
        }

        public void AddBeforePivot(Node<T> pivot, Node<T> node)
        {
            pivot.prev.next = node;
            node.next = pivot;
            node.prev = pivot.prev;
            pivot.prev = node;
            _count++;
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

        public void Remove(Node<T> node)
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
            Node<T> now = first;

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
