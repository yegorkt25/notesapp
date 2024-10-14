using BLL.Interfaces;
using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.ModelViews
{
	public class LoginModelView
	{
		public IUserService Service { get; }

		public LoginModelView(IUserService service)
		{
			this.Service = service;
		}
	}
}
