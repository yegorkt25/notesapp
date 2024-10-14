using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
	public class NoteModel : EntityModel
	{
		public string Title { get; set; }
		public string Text { get; set; }
		public int UserId { get; set; }

		public NoteModel(int? id, string title, string text, int userId) : base(id)
		{
			this.Title = title;
			this.Text = text;
			this.UserId = userId;
		}
	}
}
