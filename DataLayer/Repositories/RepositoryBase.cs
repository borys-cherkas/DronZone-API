using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Models;
using DataLayer.DbContext;
using DataLayer.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity>
        where TEntity : ModelBase<TKey>
    {
        protected readonly AppDbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        public RepositoryBase(
            AppDbContext dbContext, DbSet<TEntity> dbSet)
        {
            DbContext = dbContext;
            DbSet = dbSet;
        }

        protected IQueryable<TEntity> Queryable => DbSet.AsQueryable();

        public virtual TEntity GetSingleByPredicate(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null)
        {
            var query = Queryable;

            if (include != null)
            {
                query = include(query);
            }

            return query.SingleOrDefault(predicate);
        }

        public TEntity GetLastByPredicate(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null)
        {
            var query = Queryable;

            if (include != null)
            {
                query = include(query);
            }

            return query.LastOrDefault(predicate);
        }

        public virtual IList<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> include = null)
        {
            var query = Queryable;

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToList();
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? Queryable.Any(predicate) : Queryable.Any();
        }

        public virtual TEntity Add(TEntity entity)
        {
            entity.Created = DateTime.UtcNow;

            var entityEntry = DbSet.Add(entity);
            DbContext.SaveChanges();

            return entityEntry.Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            var entityEntry = DbSet.Update(entity);
            DbContext.SaveChanges();

            return entityEntry.Entity;
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            DbContext.SaveChanges();
        }
    }
}