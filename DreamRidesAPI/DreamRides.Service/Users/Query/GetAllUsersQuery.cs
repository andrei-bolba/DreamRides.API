using Ardalis.Result;
using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Data.DTO;
using MediatR;

namespace DreamRides.Service.Users.Query;

public record GetAllUsersQuery : IRequest<Result<ResponseType<UserDTO>>>
{
}

public sealed class GetAllUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetAllUsersQuery,Result<ResponseType<UserDTO>>>
{
    public async Task<Result<ResponseType<UserDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var result = userRepository.GetAll();

        if (result is { IsSuccess: true, Collection: not null })
        {
            return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>()
            {
                Collection = result.Collection.Select(c=> new UserDTO(c)),
                IsSuccess = true,
                Message = result.Message
            });
        }
        
        return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>()
        {
            IsSuccess = false,
            Message = result.Message
        });
    }
}