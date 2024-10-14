using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Interfaces;
using DAL.Models;
using System.Text.RegularExpressions;

namespace BLL.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository repository;
		private readonly INoteRepository noteRepository;
		private readonly IMapper mapper;

		public UserService(IUserRepository repository, INoteRepository noteRepository, IMapper mapper)
		{
			this.repository = repository;
			this.noteRepository = noteRepository;
			this.mapper = mapper;
		}

		public async Task AddAsync(UserModel model)
		{
			if (model == null) throw new BusinessLogicException(nameof(model));
			if (string.IsNullOrWhiteSpace(model.Username))
			{
				throw new BusinessLogicException("Username cannot be empty");
			}
			var passwordValidation = new Regex("^(?=.*\\d)[A-Za-z\\d!@#$%^&*()_+~`|}{[\\]:;'<>,.?/-]{8,}$");
			if (!passwordValidation.IsMatch(model.Password))
			{
				throw new BusinessLogicException("Password is not valid");
			}

			var id = await repository.GetAmount() + 1;

			model.Id = id;

			var user = mapper.Map<User>(model);

			var allUsers = await repository.GetAllAsync(0, null);
			if (allUsers.FirstOrDefault(x => x.Username == user.Username) != null)
			{
				throw new BusinessLogicException("User with such username already exists");
			}

			await repository.AddAsync(user);
		}

		public async Task<bool> AuthenticateUser(UserModel user)
		{
			var userEntity = mapper.Map<User>(user);
			var allUsers = await repository.GetAllAsync(0, null);

			var supposedUser = allUsers.FirstOrDefault(x => x.Username == userEntity.Username);

			if (supposedUser != null)
			{
				if (supposedUser.Password == userEntity.Password)
				{
					return true;
				}
			} 
			else
			{
				throw new BusinessLogicException("User not found");
			}
			return false;
		}

		public async Task DeleteAsync(int modelId)
		{
			try
			{
				var user = await repository.GetByIdAsync(modelId);
				foreach (var item in user.Notes)
				{
					await noteRepository.Delete(item);
				}
				await repository.DeleteByIdAsync(modelId);
			} catch (ArgumentException ex)
			{
				throw new BusinessLogicException("Such user doesn't exist", ex);
			}
		}

		public async Task<IEnumerable<UserModel>> GetAllAsync(int skip, int? count)
		{
			var users = await repository.GetAllAsync(skip, count);

			var userModels = users.Select(x => mapper.Map<UserModel>(x));
			return userModels;
		}

		public async Task<UserModel> GetByIdAsync(int id)
		{
			try
			{
				var user = mapper.Map<UserModel>(await repository.GetByIdAsync(id));
				return user;
			} catch(ArgumentException ex)
			{
				throw new BusinessLogicException("Such user doesn't exist", ex);
			}
		}

		public async Task<UserModel> GetByUsername(string username)
		{
			var allUsers = await repository.GetAllAsync(0, null);

			var user = allUsers.FirstOrDefault(x => x.Username == username);

			return mapper.Map<UserModel>(user) ?? throw new BusinessLogicException("User with this username doesn't exist");
		}

		public async Task UpdateAsync(UserModel model)
		{
			var user = await repository.GetByIdAsync(model.Id ?? throw new InvalidOperationException());

			user.Username = model.Username;

			user.Password = model.HashPassword();

			await repository.Update(user);
		}
	}
}
