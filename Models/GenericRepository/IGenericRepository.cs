using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.GenericRepository
{
    interface IGenericRepository<T> where T : class
    {
        List<T> SelectAll();
        T SelectByID(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        int Save();
    }
}
