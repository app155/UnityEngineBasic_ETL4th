namespace Homeworks
{
    public struct Pair<T>
    {
        public int id;
        public T? item;

        public Pair(int id, T item)
        {
            this.id = id;
            this.item = item;
        }
    }

    internal class ObservableCollection<T>
    {
        public ObservableCollection()
        {
            OnItemAdded += ItemAdded;
            OnItemChanged += ItemChanged;
            OnItemRemoved += ItemRemoved;
            OnCollectionChanged += Display;
        }

        public Pair<T>[]? items;

        private int _itemCount = 0;
        private int _itemsCapacity = 1;

        public event Action<int, T> OnItemChanged;
        public event Action<int, T> OnItemAdded;
        public event Action<int, T> OnItemRemoved;
        public event Action OnCollectionChanged;

        public void Add(int id, T item)
        {
            if (items == null)
            {
                items = new Pair<T>[_itemsCapacity];
            }

            if (_itemCount >= _itemsCapacity)
            {
                Pair<T>[] tmp = new Pair<T>[_itemsCapacity * 2];

                for (int i = 0; i < _itemsCapacity; i++)
                {
                    tmp[i] = items[i];
                }

                items = tmp;
                _itemsCapacity *= 2;
            }

            items[_itemCount] = new Pair<T>(id, item);
            _itemCount++;

            OnItemAdded?.Invoke(id, item);
            OnCollectionChanged?.Invoke();
        }

        public void Change(int id, T item)
        {
            //???????????? 뭐가 뭘로 어케변하지????????????
            // 잘모르겠어서 그냥 마지막 데이터를 바꿔봄...
            if (items == null)
                return;

            items[_itemCount - 1] = new Pair<T>(id, item);

            OnItemChanged?.Invoke(id, item);
            OnCollectionChanged?.Invoke();
        }

        public bool Remove(int id)
        {
            if (items == null)
                return false;

            for (int i = 0; i < _itemCount; i++)
            {
                if (items[i].id == id)
                {
                    T item = items[i].item;
                    for (int j = i; j < _itemCount - 1; j++)
                    {
                        items[j] = items[j + 1];
                    }
                    _itemCount--;

                    OnItemRemoved?.Invoke(id, item);
                    OnCollectionChanged?.Invoke();

                    return true;
                }
            }

            return false;
        }

        void ItemAdded(int id, T item)
        {
            Console.WriteLine($"Item Added - ID : {id}, ITEM : {item}");
        }

        void ItemChanged(int id, T item)
        {
            Console.WriteLine($"Last Item Changed - ID : {id}, ITEM : {item}");
        }

        void ItemRemoved(int id, T item)
        {
            Console.WriteLine($"Item Removed - ID : {id}, ITEM : {item}");
        }

        void Display()
        {
            Console.WriteLine("----------Collection----------");
            for (int i = 0; i < _itemCount; i++)
            {
                Console.WriteLine($"ID : {items[i].id}, ITEM : {items[i].item}");
                Console.WriteLine("-------------------------------");
            }
            Console.WriteLine();
        }
    }
}
