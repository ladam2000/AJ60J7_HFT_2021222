using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AJ60J7_HFT_2021222.Repository
{
    public interface IDefaultRepository<T>
    {
        IQueryable<T> ReadAll();

        void Create(T item);
        T ReadOne(int id);
        
        void Update(T item);
        void Delete(int itemID);
        
    }
}
