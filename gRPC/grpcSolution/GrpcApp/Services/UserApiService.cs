using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using grpcApp.Application.Interfaces.IServices;
using grpcApp.Application.Interfaces.Models;
using GrpcUserApi;

namespace grpcApp.Services;

public class UserApiService : GrpcUserApiService.GrpcUserApiServiceBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserApiService(IUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public override async Task<UserReply> CreateUser(CreateUserRequest request, ServerCallContext contex)
    {
        var saveModel = new User()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Age = request.Age
        };

        var newModel = await _userService.AddAsync(saveModel);

        var result = _mapper.Map<UserReply>(newModel);
        
        return await Task.FromResult(result);
    }

    public override async Task<UserReply> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        var userModel = await _userService.DeleteAsync(new Guid(request.Id));

        var result = _mapper.Map<UserReply>(userModel);

        return result;
    }

    public override async Task<UserReply> GetUser(GetUserRequest request, ServerCallContext context)
    {
        return await Task.FromResult(new UserReply
        {
            Id = "222",
            Name = "dddd",
            Age = 5
        });
    }

    public override async Task<ListReply> ListUsers(Empty request, ServerCallContext context)
    {
        return await Task.FromResult(new ListReply()
        {
            Users =
            {
                new UserReply
                {
                    Id = "222",
                    Name = "dddd",
                    Age = 5
                }
            }
        });
    }

    public override async Task<UserReply> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        return await Task.FromResult(new UserReply
        {
            Id = "222",
            Name = "dddd",
            Age = 5
        });
    }
}