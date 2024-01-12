using Core.DbContexts;
using Core.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Infrastructure.Repositores
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private Hashtable _repositores;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }


        public async Task<int> Complete()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositores is null)
                _repositores = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositores.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositores.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositores[type];

        }
    }
}
