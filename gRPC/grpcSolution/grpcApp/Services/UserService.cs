using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using grpcApp.Application.Interfaces.IServices;
using grpcApp.Application.Interfaces.Models;
using grpcUserApi;

namespace grpcApp.Services;

public class UserService : GrpcUserApiService.GrpcUserApiServiceBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserService(IUserService userService,
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
        
        var modelId = await _userService.AddAsync(saveModel);
        
        var model = (await _userService.ListAsync()).FirstOrDefault(x => x.Id == modelId);
        
        return _mapper.Map<UserReply>(model);

    }

    public override Task<UserReply> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        return base.DeleteUser(request, context);
    }

    public override Task<UserReply> GetUser(GetUserRequest request, ServerCallContext context)
    {
        return base.GetUser(request, context);
    }

    public override Task<ListReply> ListUsers(Empty request, ServerCallContext context)
    {
        return base.ListUsers(request, context);
    }

    public override Task<UserReply> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        return base.UpdateUser(request, context);
    }
}