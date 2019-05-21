using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Project.Models.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private VehicleRentalDBEntities _db = null;
        private DbSet<T> table = null;


        public GenericRepository()
        {
            this._db = new VehicleRentalDBEntities();
            table = _db.Set<T>();
        }
        public GenericRepository(VehicleRentalDBEntities db)
        {
            this._db = db;
            table = _db.Set<T>();
        }

        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public int Save()
        {
            return _db.SaveChanges();
        }

        public List<T> SelectAll()
        {
            return table.ToList();
        }

        public T SelectById(object id)
        {
            return table.Find(id);
        }

        public T SelectByID(object id)
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _db.Entry(obj).State = EntityState.Modified;
        }
    }
}