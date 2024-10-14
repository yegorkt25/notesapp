using BLL.Models;

namespace BLL.Interfaces
{
	public interface IUserService : ICrud<UserModel>
	{
		Task<bool> AuthenticateUser(UserModel user);
		Task<UserModel> GetByUsername(string username);
	}
}
