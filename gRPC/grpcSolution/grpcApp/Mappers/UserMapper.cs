using AutoMapper;
using grpcApp.Application.Interfaces.Models;
using grpcUserApi;

namespace grpcApp.Mappers;

public class UserMapper:Profile
{
    public UserMapper()
    {
        CreateMap<UserReply, User>()
            .ForMember(x => x.Id, opt =>
                opt.MapFrom(y=>new Guid(y.Id)));
        
        CreateMap<User, UserReply>()
            .ForMember(x => x.Id, opt =>
                opt.MapFrom(y=>y.Id.ToString()));
    }
}