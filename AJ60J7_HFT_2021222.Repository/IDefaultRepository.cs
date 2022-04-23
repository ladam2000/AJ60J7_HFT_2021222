﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Repository
{
    public interface IDefaultRepository<T>
    {
        void Create(T item);
        T ReadOne(int id);
        IQueryable<T> ReadAll();
        void Update(T item);
        void Delete(int itemID);
    }
}
