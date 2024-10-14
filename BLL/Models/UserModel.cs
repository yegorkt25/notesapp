using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace BLL.Models
{
	public class UserModel : EntityModel
	{
		public string Username { get; set; }
		public string Password { get; set; }

		public UserModel(int? id, string username, string password) : base(id)
		{
			this.Username = username;
			this.Password = password;
		}

		public string HashPassword()
		{
			using (var sha256 = new SHA256Managed())
			{
				byte[] passwordBytes = Encoding.UTF8.GetBytes(Password);

				return Convert.ToBase64String(passwordBytes);
			}
		}
	}
}
