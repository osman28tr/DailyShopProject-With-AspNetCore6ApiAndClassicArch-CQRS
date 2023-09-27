using DailyShop.Shared.DataAccessService.Abstract;
using DailyShop.Shared.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Shared.DataAccessService.Concrete.EntityFramework
{
	public class EfEntityRepositoryBase<T> : IEntityRepositoryBase<T> where T : class,IEntity, new()
	{
		protected readonly DbContext _context;
		public EfEntityRepositoryBase(DbContext context)
		{
			_context = context;
		}
		public async Task<T> Delete(T t)
		{
			_context.Remove(t);
			await _context.SaveChangesAsync();
			return t;
		}

		public async Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> filter)
		{			
			return await _context.Set<T>().Where(filter).ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public async Task<List<T>> GetListAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<T> AddAsync(T t)
		{
			await _context.AddAsync(t);
			await _context.SaveChangesAsync();
			return t;
		}

		public async Task<T> UpdateAsync(T t)
		{
			_context.Update(t);
			await _context.SaveChangesAsync();
			return t;
		}
	}
}
