using DAL;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL;

public class Repository<T> : IRepository<T> where T : class, IEntity, new()
{
    private DataBaseContext _context;
    private DbSet<T> _objectSet;

    public Repository()
    {
        if (_context == null)
        {
            _context = new DataBaseContext();
            _objectSet = _context.Set<T>();
        }
    }

    public int Add(T entity)
    {
        _objectSet.Add(entity);
        return _context.SaveChanges();
    }

    public T Find(int id)
    {
        return _objectSet.Find(id);
    }

    public T Get()
    {
        throw new NotImplementedException();
    }

    public T Get(Expression<Func<T, bool>> expression)
    {
        return _objectSet.FirstOrDefault(expression);
    }

    public List<T> GetAll()
    {
        return _objectSet.ToList();
    }

    public List<T> GetAll(Expression<Func<T, bool>> expression)
    {
        return _objectSet.Where(expression).ToList();
    }

    public Task<IEnumerable<T>> GetAllByAsync()
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByAsync()
    {
        throw new NotImplementedException();
    }

    public int Remove(T entity)
    {
        _objectSet.Remove(entity);
        return _context.SaveChanges();
    }

    public int Update(T entity)
    {
        _objectSet.Update(entity);
        return _context.SaveChanges();
    }
}
