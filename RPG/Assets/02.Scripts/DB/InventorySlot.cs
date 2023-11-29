using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using UnityEngine;

namespace RPG.DB
{
    [Table("InventorySlot")]
    public class InventorySlot : MonoBehaviour
    {
        [Column("slotID")]
        public int slotID { get; set; }
        [Column("itemID")]
        public int itemID { get; set; }
        [Column("itemNum")]
        public int itemNum { get; set; }
    }
}
