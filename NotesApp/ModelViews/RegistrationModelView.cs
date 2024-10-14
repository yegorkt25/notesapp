using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.ModelViews
{
	public class RegistrationModelView
	{
		public IUserService Service { get; }

		public RegistrationModelView(IUserService service)
		{
			this.Service = service;
		}
	}
}
