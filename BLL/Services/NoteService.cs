using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
	public class NoteService : INoteService
	{
		private readonly INoteRepository repository;
		private readonly IMapper mapper;

		public NoteService(INoteRepository repository, IMapper mapper)
		{
			this.repository = repository;
			this.mapper = mapper;
		}

		public async Task AddAsync(NoteModel model)
		{
			if (model == null) throw new BusinessLogicException(nameof(model));

			var id = await repository.GetAmount() + 1;

			model.Id = id;

			var note = mapper.Map<Note>(model);

			await repository.AddAsync(note);
		}

		public async Task DeleteAsync(int modelId)
		{
			try
			{
				await repository.DeleteByIdAsync(modelId);
			} catch (ArgumentException ex)
			{
				throw new BusinessLogicException("Such note doesn't exist", ex);
			}
		}

		public async Task<IEnumerable<NoteModel>> GetAllAsync(int skip, int? count)
		{
			var notes = await repository.GetAllAsync(skip, count);

			var noteModels = notes.OrderBy(x => x.Id).Select(x => mapper.Map<NoteModel>(x));
			return noteModels;
		}

		public async Task<IEnumerable<NoteModel>> GetAllUserNotes(int userId)
		{
			var notes = await repository.GetAllAsync(0, null);

			var noteModels = notes.Where(x => x.UserId == userId).OrderBy(x => x.Id).Select(x => mapper.Map<NoteModel>(x));
			return noteModels;
		}

		public async Task<NoteModel> GetByIdAsync(int id)
		{
			var note = await repository.GetByIdAsync(id);

			var noteModel = mapper.Map<NoteModel>(note);

			return noteModel;
		}

		public async Task UpdateAsync(NoteModel model)
		{
			var note = await repository.GetByIdAsync(model.Id ?? throw new InvalidOperationException());

			note.Title = model.Title;
			note.Text = model.Text;

			await repository.Update(note);
		}
	}
}
