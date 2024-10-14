using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
	public class User
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("username")]
		public string Username { get; set; }

		[Column("password")]
		public string Password { get; set; }
		public IEnumerable<Note> Notes { get; set; }
	}
}
