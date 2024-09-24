﻿using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repositories
{
    public class GenericRepoistory<TEntity, TKey> : IGenericRepoistory<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepoistory(StoreDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity)
         => await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
         =>  _context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsNoTrackingAsync()
         => await _context.Set<TEntity>().ToListAsync();

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        => await _context.Set<TEntity>().ToListAsync();

        //public async Task<TEntity> GetByIdAsNoTrackingAsync(TKey? id)
        //=> await _context.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Id == id);

        public async Task<TEntity> GetByIdAsync(TKey? id)
         => await _context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)
         => _context.Set<TEntity>().Update(entity);
    }
}
