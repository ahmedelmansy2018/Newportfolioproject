using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Interfaces
{
   public interface IGenericRepository<T>where T:class
    {
        IEnumerable<T> GetAll();
        void Delete(object Id);
        T GetByID(object Id);
        void Insert(T entity);
        void Update(T entity);

    }
}
