using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    internal class MyLinkedList<T>
    {
        class Node
        {
            T? Value { get; set; }
            Node? Prev;
            Node? Next;
        }
    }
}
