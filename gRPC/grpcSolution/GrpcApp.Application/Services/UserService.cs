using AutoMapper;
using grpcApp.Application.Interfaces.IRepositories;
using grpcApp.Application.Interfaces.IServices;
using grpcApp.Application.Interfaces.Models;
using grpcApp.Domain.Entities;

namespace grpcApp.Application.Services;

public class UserService:IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public UserService(IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(User user)
    {
        var userEntity = _mapper.Map<UserEntity>(user);
        return await _userRepository.AddAsync(userEntity);
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> ListAsync()
    {
        var entities = await _userRepository.ListAsync();
        var result = _mapper.Map<List<User>>(entities);
        return result;
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }
}