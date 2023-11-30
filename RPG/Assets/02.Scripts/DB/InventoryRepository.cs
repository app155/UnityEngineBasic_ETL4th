using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.DB
{
    public class InventoryRepository : IRepository<InventoryModel>
    {
        public GameDbContext context;
        public InventoryRepository(GameDbContext context)
        {
            this.context = context;
            Debug.Log("[InventoryRepository] - constructed");
        }

        public InventoryModel Find(Predicate<InventoryModel> match)
        {
            return context.inventory.Find(match);
        }

        public InventoryModel Get(int id)
        {
            return context.inventory.Find(x => x.id == id);
        }

        public IEnumerable<InventoryModel> GetAll()
        {
            return context.inventory.Entities;
        }

        public void Insert(InventoryModel entity)
        {
            int index = context.inventory.Entities.FindLastIndex(x => x.id <= entity.id);
            index = index < 0 ? 0 : index;
            context.inventory.Entities.Insert(index, entity);
        }

        public void Update(InventoryModel entity)
        {
            int index = context.inventory.Entities.FindLastIndex(x => x.id == entity.id);

            if (index >= 0)
            {
                context.inventory.Entities[index] = entity;
            }

            else
            {
                throw new Exception($"[InventoryRepository] - Update");
            }
        }
    }
}