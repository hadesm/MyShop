using MyShop.Core;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext context;
        internal DbSet<T> db;
        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.db = context.Set<T>();
        }
        public IQueryable<T> Collection()
        {
            return db;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var t = Find(Id);
            if (context.Entry(t).State == EntityState.Detached)
                db.Attach(t);

            db.Remove(t);
        }

        public T Find(string Id)
        {
          return  db.Find(Id);
        }

        public void Insert(T t)
        {
            db.Add(t);
        }

        public void Update(T t)
        {
            db.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }
    }
}
