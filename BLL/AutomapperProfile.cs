using AutoMapper;
using BLL.Models;
using DAL.Models;

namespace BLL
{
	public class AutomapperProfile : Profile
	{
		public AutomapperProfile()
		{
			CreateMap<NoteModel, Note>().
				ReverseMap();

			CreateMap<UserModel, User>().
				ForMember(rm => rm.Password, r => r.MapFrom(x => x.HashPassword())).
				ReverseMap();
		}
	}
}
