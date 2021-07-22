using BL.Interfaces;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private IGenericRepository<T> _Entity;
        private readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
       

        public IGenericRepository<T> Entity 
        {
            get
            {
                return _Entity ?? (_Entity = new GenericRepository<T>(_context));

            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
