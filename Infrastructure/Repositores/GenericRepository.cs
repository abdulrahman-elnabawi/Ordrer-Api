using Core.DbContexts;
using Core.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositores
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
            => await _context.AddAsync(entity);

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity);

        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

       

        public async Task<T> GetByIdAsync(int? id)
             =>  await _context.Set<T>().FindAsync(id);

       

        public void Update(T entity)
           => _context.Set<T>().Update(entity);

        public async Task<T> GetEntityWithSpecificationsAsync(ISpecifications<T> specs)
            => await ApplySpecifications(specs).FirstOrDefaultAsync();

        public async Task<IReadOnlyList<T>> GetAllWithSpecificationsAsync(ISpecifications<T> specs)
            =>await  ApplySpecifications(specs).ToListAsync();

        private IQueryable<T> ApplySpecifications(ISpecifications<T> specs)
            => SpecificationEvaluater<T>.GetQuery(_context.Set<T>().AsQueryable(), specs);

        public async Task<int> CountAsync(ISpecifications<T> specifications)
            => await ApplySpecifications(specifications).CountAsync();
    }
}
