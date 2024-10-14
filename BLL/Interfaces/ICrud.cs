using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface ICrud<TModel> where TModel : EntityModel
	{
		Task<IEnumerable<TModel>> GetAllAsync(int skip, int? count);

		Task<TModel> GetByIdAsync(int id);

		Task AddAsync(TModel model);

		Task UpdateAsync(TModel model);

		Task DeleteAsync(int modelId);
	}
}
