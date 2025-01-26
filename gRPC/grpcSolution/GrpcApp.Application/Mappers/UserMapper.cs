using AutoMapper;
using grpcApp.Application.Interfaces.Models;
using grpcApp.Domain.Entities;

namespace GrpcApp.Application.Mappers;

public class UserMapper:Profile
{
    public UserMapper()
    {
        CreateMap<User, UserEntity>().ReverseMap();
    }
}