using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface INoteService : ICrud<NoteModel>
	{
		Task<IEnumerable<NoteModel>> GetAllUserNotes(int userId);
	}
}
