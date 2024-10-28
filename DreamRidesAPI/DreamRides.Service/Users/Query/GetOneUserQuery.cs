using Ardalis.Result;
using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Data.DTO;
using MediatR;

namespace DreamRides.Service.Users.Query;

public record GetOneUserQuery : IRequest<Result<ResponseType<UserDTO>>>
{
    public Guid UserId { get; set; }
}

public sealed class GetOneUserQueryHandler(IUserRepository userRepository) : IRequestHandler<GetOneUserQuery,Result<ResponseType<UserDTO>>>
{
    public async Task<Result<ResponseType<UserDTO>>> Handle(GetOneUserQuery request, CancellationToken cancellationToken)
    {
        var result = userRepository.GetOne(request.UserId);

        if (result is { IsSuccess: true, Object: not null })
        {
            return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>()
            {
                Object = new UserDTO(result.Object),
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