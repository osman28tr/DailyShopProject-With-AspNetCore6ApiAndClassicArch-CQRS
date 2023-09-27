using DailyShop.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DailyShop.Shared.DataAccessService.Abstract
{
	public interface IEntityRepositoryBase<T> where T : class, IEntity, new()
	{
		Task<T> AddAsync(T t);
		Task<T> Delete(T t);
		Task<T> UpdateAsync(T t);
		Task<List<T>> GetListAsync();
		Task<T> GetByIdAsync(int id);
		Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> filter);
	}
}
