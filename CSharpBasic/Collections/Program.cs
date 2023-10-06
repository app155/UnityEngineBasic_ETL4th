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
        }
    }
}