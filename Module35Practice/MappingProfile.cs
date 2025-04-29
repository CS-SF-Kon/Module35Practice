using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Module35Practice.Models.Users;
using System.Runtime.InteropServices;
using Module35Practice.ViewModels.Account;

namespace Module35Practice;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterViewModel, User>()
            .ForMember(x => x.BirthDate, opt => opt.MapFrom(c => new DateTime((int)c.Year, (int)c.Month, (int)c.Date)))
            .ForMember(x => x.Email, opt => opt.MapFrom(c => c.EmailReg))
            .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Login));
        CreateMap<LoginViewModel, User>();
    }
}
