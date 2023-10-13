using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    enum ItemID
    {
        RedPotion = 20,
        BluePotion = 21,
    }

    class SlotData : IComparable<SlotData>
    {
        public bool isEmpty => id == 0 && num == 0;

        public int id;
        public int num;

        public void DoSomething<T>()
        {

        }

        public SlotData(int id, int num)
        {
            this.id = id;
            this.num = num;
        }

        public int CompareTo(SlotData? other)
        {
            return this.id == other?.id && this.num == other.num ? 0 : -1;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            #region DynamicArray

            MyDynamicArray inventory = new MyDynamicArray();

            for (int i = 0; i < 32; i++)
            {
                inventory.Add(new SlotData(0, 0));
            }

            inventory[0] = new SlotData((int)ItemID.RedPotion, 40);
            inventory[1] = new SlotData((int)ItemID.BluePotion, 99);
            inventory[2] = new SlotData((int)ItemID.BluePotion, 50);

            // ex) 파란포션 5개 획득.
            // 1. 파란포션 5개가 들어갈 수 있는 공간 찾기
            int availableSlotIndex = inventory.FindIndex(slotData => ((SlotData)slotData).isEmpty ||
                                                                        (((SlotData)slotData).id == (int)ItemID.BluePotion &&
                                                                        ((SlotData)slotData).num <= 99 - 5));

            // 2. 찾은 슬록의 아이템 개수 + 추가하려는 개수인 예상치를 구하기
            int expected = ((SlotData)inventory[availableSlotIndex]).num + 5;

            // 3. 새 아이템 데이터를 생성해 슬롯 데이터 교체
            inventory[availableSlotIndex] = new SlotData((int)ItemID.BluePotion, expected);


            // Todo -> 위 내용 T타입 동적배열로 구현하기.
            MyDynamicArray<SlotData> inventory2 = new MyDynamicArray<SlotData>();

            for (int i = 0; i < 10; i++)
            {
                inventory2.Add(new SlotData(0, 0));
            }

            inventory2[0] = new SlotData((int)ItemID.RedPotion, 40);
            inventory2[1] = new SlotData((int)ItemID.BluePotion, 99);
            inventory2[2] = new SlotData((int)ItemID.BluePotion, 50);

            Console.WriteLine("파란포션 5개 획득 전: ");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"[{(ItemID)inventory2[i].id}] : [{inventory2[i].num}]");
            }

            // object 타입이 아니기 때문에 Unboxing 때문에 SlotData를 캐스팅하지 않아도 된다.
            int availableSlotIndex2 = inventory2.FindIndex(slotData => slotData.isEmpty ||
                                                                    (slotData.id == (int)ItemID.BluePotion &&
                                                                    slotData.num <= 99 - 5));

            int expected2 = inventory2[availableSlotIndex2].num + 5;

            inventory2[availableSlotIndex2] = new SlotData((int)ItemID.BluePotion, expected2);

            Console.WriteLine("파란포션 5개 획득 후: ");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"[{(ItemID)inventory2[i].id}] : [{inventory2[i].num}]");
            }

            MyDynamicArray<SlotData> inventory1 = new MyDynamicArray<SlotData>();

            #region Enumerator 순회 = for-each

            // using 구문 : IDisposable 객체의 Dispose() 호출을 보장하는 구문
            using (IEnumerator<SlotData> e1 = inventory1.GetEnumerator())
            using (IEnumerator<SlotData> e2 = inventory2.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext())
                {
                    
                }

                e1.Reset();
                e2.Reset();
            }
            // e1.Dispose();
            // e2.Dispose();

            // for-each문과 같다.
            foreach (SlotData slotData in inventory2)
            {
                Console.WriteLine($"[{(ItemID)slotData.id}], [{slotData.num}");
            }
            // 즉 for-each문도 읽기 전용 순회이므로, 도중에 write 하면 안됨.

            #endregion

            #region ArrayList, List

            ArrayList arrayList = new ArrayList();
            arrayList.Add("asdf");
            arrayList.Add(1);
            arrayList.Remove("asdf");

            string name = "asdf";
            arrayList.Add(name);
            arrayList.Remove(name);

            List<string> list = new List<string>();
            list.Add("asdf");
            list.Remove("asdf");
            list.Find(x => x == "asdf");
            list.Add("fdsa");
            #endregion
            #endregion

            #region Queue

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(3); // 삽입

            if (queue.Peek() > 0) // 탐색 -> 맨 앞의 값만 탐색 가능.
                queue.Dequeue(); // 삭제

            #endregion

            #region Stack

            Stack<int> stack = new Stack<int>();
            stack.Push(3); // 삽입

            if (stack.Peek() > 0) // 탐색 -> 맨 뒤의 값만 탐색 가능.
                stack.Pop(); // 삭제

            #endregion

            #region LinkedList

            MyLinkedList<int> myLinkedList = new MyLinkedList<int>();

            myLinkedList.AddLast(new MyLinkedListNode<int>(1));
            MyLinkedListNode<int> willDeleteNode = new MyLinkedListNode<int>(3);
            myLinkedList.AddLast(willDeleteNode);
            myLinkedList.AddLast(new MyLinkedListNode<int>(5));
            MyLinkedListNode<int> end = new MyLinkedListNode<int>(7);
            myLinkedList.AddLast(end);

            int k = myLinkedList.Find(3);
            Console.WriteLine(k);

            myLinkedList.AddFirst(new MyLinkedListNode<int>(111));
            myLinkedList.RemoveFirst();
            myLinkedList.RemoveLast();

            MyLinkedList2<int> mLL = new MyLinkedList2<int>();
            mLL.AddLast(2);
            mLL.AddLast(4);
            mLL.AddLast(3);
            mLL.AddLast(11);
            mLL.Find(x => x > 0);

            #endregion

            #region HashTable

            MyHashTable<string, int> myHashTable = new MyHashTable<string, int>();

            myHashTable.Add("asdf", 1);
            myHashTable.Add("a", 2);
            myHashTable.Add("s", 3);
            myHashTable.Add("d", 4);
            myHashTable.Add("f", 5);

            myHashTable.Remove("s");
            myHashTable.Remove("ff");

            Console.WriteLine("HashTable Foreach loop Test");

            foreach (KeyValuePair<string, int> pair in myHashTable)
            {
                Console.WriteLine($"Key : {pair.Key}, Value : {pair.Value}");
            }

            Console.WriteLine("Second Test");

            foreach (KeyValuePair<string, int> pair in myHashTable)
            {
                Console.WriteLine($"Key : {pair.Key}, Value : {pair.Value}");
            }

            #endregion

            IEnumerator routine = GetMakingToastRoutine();

            Console.WriteLine("routine1");
            while (routine.MoveNext())
            {
                Console.WriteLine(routine.Current);
            }

            routine = GetMakingToastRoutine2();
            Console.WriteLine("routine2");
            // routine.Reset(); 단일성 객체라서 yield로 간소화된 객체는 Reset 불가능.
            while (routine.MoveNext())
            {
                Console.WriteLine(routine.Current);
            }

            foreach (var item in NumberEnumerationRoutine())
            {
                Console.WriteLine(item);
            }
        }

        static IEnumerator GetMakingToastRoutine()
        {
            return new MakingToastRoutine();
        }

        static IEnumerator GetMakingToastRoutine2()
        {
            // yield 키워드
            // Enumerator 객체 정의의 간소화 구문.
            // yield return MoveNext() 할 때마다 바뀔 값 작성
            yield return "Induction ON";
            yield return "Pan Ready";
            yield return "Put butter in Pan";
            yield return "Put bread in Pan";
            yield return "Wait until bread toasted";
            yield return "Put jam on bread";
            yield return "Induction OFF";
            yield return "Toast is ready";
        }

        static IEnumerable NumberEnumerationRoutine()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }

        struct MakingToastRoutine : IEnumerator
        {
            public object Current => _routine[_step];

            private string[] _routine =
            {
                "Induction ON",
                "Pan Ready",
                "Put butter in Pan",
                "Put bread in Pan",
                "Wait until bread toasted",
                "Put jam on bread",
                "Induction OFF",
                "Toast is ready"
            };
            private int _step = -1;

            public MakingToastRoutine()
            {

            }

            public bool MoveNext()
            {
                if (_step >= _routine.Length)
                    return false;

                _step++;

                return _step < _routine.Length;
            }

            public void Reset()
            {
                _step = -1;
            }
        }
    }
}