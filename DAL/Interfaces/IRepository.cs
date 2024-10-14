using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAllAsync(int skip, int? count);

		Task<TEntity> GetByIdAsync(int id);

		Task AddAsync(TEntity entity);

		Task Delete(TEntity entity);

		Task DeleteByIdAsync(int id);

		Task Update(TEntity entity);

		Task<int> GetAmount();
	}
}
