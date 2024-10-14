using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly NotesAppContext context;

		public UserRepository(NotesAppContext context)
		{
			this.context = context;
		}

		public async Task AddAsync(User entity)
		{
			await this.context.Users.AddAsync(entity);

			await this.context.SaveChangesAsync();
		}

		public async Task Delete(User entity)
		{
			this.context.Users.Remove(entity);

			await this.context.SaveChangesAsync();
		}

		public async Task DeleteByIdAsync(int id)
		{
			var entity = await this.GetByIdAsync(id);
			await this.Delete(entity);

		}

		public async Task<IEnumerable<User>> GetAllAsync(int skip, int? count)
		{
			if (skip < 0)
			{
				throw new ArgumentException(null, nameof(skip));
			}
            if (count <= 0)
            {
				throw new ArgumentException(null, nameof(count));
            }
			return await this.context.Users.Include(e => e.Notes).Skip(skip).Take(count ?? await context.Users.CountAsync()).ToListAsync();
        }

		public async Task<int> GetAmount()
		{
			return await context.Users.CountAsync();
		}

		public async Task<User> GetByIdAsync(int id)
		{
			var entity = await this.context.Users.Include(e => e.Notes).FirstOrDefaultAsync(e => e.Id == id);

			return entity ?? throw new ArgumentException(null, nameof(id));
		}

		public async Task Update(User entity)
		{
			context.Users.Update(entity);
			await this.context.SaveChangesAsync();
		}
	}
}
