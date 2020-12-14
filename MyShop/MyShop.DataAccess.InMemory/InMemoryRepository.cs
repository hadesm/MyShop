using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository <T>  where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string classname;

        public InMemoryRepository()
        {
            classname = typeof(T).Name;
            items = cache[classname] as List<T>;
            
            if(items == null)
            {
                items = new List<T>();
            }

        }

        public void Commit()
        {
            cache[classname] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }
        public void Update(T t)
        {
            T ttoupdate = items.Find(i => i.Id == t.Id);
            if(ttoupdate != null)
            {
                ttoupdate = t;
            }else
            {
                throw new Exception(classname + "Not Found");
            }
        }
        public T Find(string Id)
        {
            T t = items.Find(i => i.Id == Id);
            if(t != null)
            {
                return t;
            }else
            {
                throw new Exception(classname + "Not Found");

            }
        }
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T tDelete = items.Find(i => i.Id == Id);
            if (tDelete != null)
            {
                items.Remove(tDelete);
            }
            else
            {
                throw new Exception(classname + "Not Found");

            }

        }
    }
}
