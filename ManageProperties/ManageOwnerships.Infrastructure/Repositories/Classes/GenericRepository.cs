using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ManageOwnerships.Infrastructure
{
    public class GenericRepository<T, A, C> : IGenericRepository<T, A, C> where A : class where T : class where C : DbContext
    {
        #region Properties

        private readonly C Entities;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors 

        public GenericRepository(IMapper mapper, C _entities)
        {
            Entities = _entities;
            _mapper = mapper;
            Entities.Database.OpenConnection();
        }

        #endregion

        #region Public overridable Methods

        public virtual List<A> GetAll(string includeProperties)
        {
            IQueryable<T> query = Entities.Set<T>();

            foreach (string includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            List<A> result = _mapper.Map<List<T>, List<A>>(query.ToList());
            Entities.Database.CloseConnection();
            return result;
        }

        public virtual List<A> GetAll()
        {
            IQueryable<T> query = Entities.Set<T>();
            List<A> result = _mapper.Map<List<T>, List<A>>(query.ToList());
            Entities.Database.CloseConnection();
            return result;
        }

        public List<A> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = Entities.Set<T>().Where(predicate);
            List<A> result = _mapper.Map<List<T>, List<A>>(query.ToList());
            Entities.Database.CloseConnection();
            return result;
        }

        public List<A> FindBy(Expression<Func<T, bool>> predicate, string includeProperties)
        {
            IQueryable<T> query = Entities.Set<T>().Where(predicate);

            foreach (string includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            List<A> result = _mapper.Map<List<T>, List<A>>(query.ToList());
            Entities.Database.CloseConnection();
            return result;
        }

        public A FindOne(Expression<Func<T, bool>> predicate)
        {
            T query = Entities.Set<T>().FirstOrDefault(predicate);
            A result = _mapper.Map<T, A>(query);
            Entities.Database.CloseConnection();
            return result;
        }

        public List<A> GetByPage(Expression<Func<T, bool>> predicate, Expression<Func<T, DateTime>> order, int pageNumber, int pageSize)
        {
            IQueryable<T> query = Entities.Set<T>().Where(predicate).OrderByDescending(order)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            List<A> result = _mapper.Map<List<T>, List<A>>(query.ToList());
            Entities.Database.CloseConnection();
            return result;
        }

        public List<A> GetByPage(Expression<Func<T, bool>> predicate, Expression<Func<T, DateTime>> order, string includeProperties, int pageNumber, int pageSize)
        {
            IQueryable<T> query = Entities.Set<T>().Where(predicate).OrderByDescending(order)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            foreach (string includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            List<A> result = _mapper.Map<List<T>, List<A>>(query.ToList());
            Entities.Database.CloseConnection();
            return result;
        }

        public virtual A Add(A contract)
        {
            T entity = _mapper.Map<A, T>(contract);
            T newEntity = Entities.Set<T>().Add(entity).Entity;
            Save();
            A result = _mapper.Map<T, A>(newEntity);
            Entities.Database.CloseConnection();
            return result;
        }

        public virtual void AddRange(List<A> contracts)
        {
            List<T> entities = _mapper.Map<List<A>, List<T>>(contracts);
            Entities.Set<T>().AddRange(entities);
            Save();
            Entities.Database.CloseConnection();
        }

        public virtual bool Delete(Expression<Func<T, bool>> predicate)
        {
            bool status = false;
            T entity = Entities.Set<T>().FirstOrDefault(predicate);
            if (entity is not null)
            {
                Entities.Set<T>().Attach(entity);
                Entities.Set<T>().Remove(entity);
                Save();
                status = true;
            }

            Entities.Database.CloseConnection();
            return status;
        }

        public virtual void DeleteRange(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> entities = Entities.Set<T>().Where(predicate);
            Entities.Set<T>().RemoveRange(entities);
            Save();
            Entities.Database.CloseConnection();
        }

        public virtual void Edit(Expression<Func<T, bool>> predicate, A contract, Func<T, A, T> selector)
        {
            T entity = Entities.Set<T>().Where(predicate).FirstOrDefault();
            if (entity is not null)
            {
                entity = selector(entity, contract);
                Entities.Entry(entity).State = EntityState.Modified;
                Entities.Set<T>().Update(entity);
                Save();
            }
            Entities.Database.CloseConnection();
        }

        public virtual void UpdateRange(List<A> contracts)
        {
            List<T> entities = _mapper.Map<List<A>, List<T>>(contracts);
            Entities.Set<T>().UpdateRange(entities);
            Save();
            Entities.Database.CloseConnection();
        }

        public virtual void Save()
        {
            Entities.SaveChanges();
        }

        #endregion
    }
}
