using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ManageProperties.Infrastructure
{
    public interface IGenericRepository<T, A, C> where A : class where T : class where C : DbContext
    {
        List<A> GetAll();
        List<A> GetAll(string includeProperties);
        List<A> FindBy(Expression<Func<T, bool>> predicate);
        List<A> FindBy(Expression<Func<T, bool>> predicate, string includeProperties);
        A FindOne(Expression<Func<T, bool>> predicate);
        List<A> GetByPage(Expression<Func<T, bool>> predicate, Expression<Func<T, DateTime>> order, int pageNumber, int pageSize);
        List<A> GetByPage(Expression<Func<T, bool>> predicate, Expression<Func<T, DateTime>> order, string includeProperties, int pageNumber, int pageSize);
        A Add(A contract);
        void AddRange(List<A> contracts);
        bool Delete(Expression<Func<T, bool>> predicate);
        void DeleteRange(Expression<Func<T, bool>> predicate);
        void Edit(Expression<Func<T, bool>> predicate, A contract, Func<T, A, T> selector);
        void UpdateRange(List<A> contracts);
        void Save();
    }
}
