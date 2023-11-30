using RPG.DB;
using RPG.UI;
using UnityEngine;

namespace RPG.Collections.ObjectModel
{
    public struct InventorySlotData
    {
        public int slotID;
        public int itemID;
        public int itemNum;

        public InventorySlotData(int slotID, int itemID, int itemNum)
        {
            this.slotID = slotID;
            this.itemID = itemID;
            this.itemNum = itemNum;
        }
    }

    public class InventoryPresenter
    {
        public InventoryPresenter()
        {
            inventorySource = new InventorySource();
            Debug.Log("[InventoryPresenter] - constructed");
        }
            
        public class InventorySource : ObservableCollection<InventorySlotData>
        {
            public InventorySource()
            {
                var entities = Repositories.instance.inventory.GetAll();

                foreach (var entity in entities)
                {
                    items.Add(entity.id,
                        new Pair<InventorySlotData>(entity.id, new InventorySlotData(entity.id, entity.itemID, entity.itemNum)));
                }
            }
        }

        public InventorySource inventorySource;
    }
}