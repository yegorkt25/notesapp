using BLL.Interfaces;
using BLL.Models;

namespace NotesApp.ModelViews
{
	public class MainWindowModelView
	{
		public IEnumerable<NoteModel> Notes { get; private set; }
		public INoteService Service { get; set; }

		public MainWindowModelView(INoteService service)
		{
			this.Service = service;
			Task.Run(() => this.RetrieveInfo()).Wait();
		}

		private async Task RetrieveInfo()
		{
			Notes = await Service.GetAllUserNotes(App.CurrentUser.Id ?? 0);
		}
	}
}

