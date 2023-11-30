using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.DB
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Find(Predicate<T> match);
        void Insert(InventoryModel entity);
        void Insert(T entity);
        void Update(T entity);
    }
}
