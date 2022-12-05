using DAL.Context;
using Interfaces;
using Interfaces.Base.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    private readonly AppDb _context;

    public Repository(AppDb context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(T item, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return item.Id;
    }

    public async Task<T?> DeleteAsync(T item, CancellationToken cancellationToken = default)
    {
        if (!_context.Set<T>().Any(i => i.Id == item.Id))
            return null;
        _context.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return item;
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _context.Set<T>().ToArrayAsync(cancellationToken);

        return items;
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await _context.Set<T>().FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        return item;
    }

    public async Task<T?> UpdateAsync(T item, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Update(item);
        await _context.SaveChangesAsync(cancellationToken);
        return item;
    }
}
