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
	public class NoteRepository : INoteRepository
	{
		private readonly NotesAppContext context;
		
		public NoteRepository(NotesAppContext context)
		{
			this.context = context;
		}

		public async Task AddAsync(Note entity)
		{
			await this.context.Notes.AddAsync(entity);

			await this.context.SaveChangesAsync();
		}

		public async Task Delete(Note entity)
		{
			this.context.Notes.Remove(entity);

			await this.context.SaveChangesAsync();
		}

		public async Task DeleteByIdAsync(int id)
		{
			var entity = await this.GetByIdAsync(id);
			await this.Delete(entity);
		}

		public async Task<IEnumerable<Note>> GetAllAsync(int skip, int? count)
		{
			if (skip < 0)
			{
				throw new ArgumentException(null, nameof(skip));
			}
			if (count <= 0)
			{
				throw new ArgumentException(null, nameof(count));
			}
			return await this.context.Notes.Include(x => x.User).Skip(skip).Take(count ?? await context.Notes.CountAsync()).ToListAsync();
		}

		public async Task<int> GetAmount()
		{
			return await context.Notes.CountAsync();
		}

		public async Task<Note> GetByIdAsync(int id)
		{
			var entity = await this.context.Notes.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);

			return entity ?? throw new ArgumentException(null, nameof(id));
		}

		public async Task Update(Note entity)
		{
			context.Notes.Update(entity);
			await this.context.SaveChangesAsync();
		}
	}
}
