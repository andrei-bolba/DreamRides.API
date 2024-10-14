using Ardalis.Result;
using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Data.DTO;
using DreamRides.Data.RequestData;
using DreamRides.Database.Model;
using MediatR;

namespace DreamRides.Service.Users.Command;

public record AddUserCommand : IRequest<Result<ResponseType<UserDTO>>>
{
    public UserRequest UserRequest { get; set; }
}

public sealed class AddUserCommandHandler(IUserRepository userRepository): IRequestHandler<AddUserCommand, Result<ResponseType<UserDTO>>>
{
    public async Task<Result<ResponseType<UserDTO>>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User
        {
            FirstName = request.UserRequest.FirstName,
            LastName = request.UserRequest.LastName,
            Email = request.UserRequest.Email,
            PasswordHash = request.UserRequest.Password,
            CreatedAt = DateTime.Now
        };
        var res = userRepository.Add(newUser);

        if (res is { IsSuccess: true, Object: not null })
        {
            return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>()
            {
                Object = new UserDTO(res.Object),
                IsSuccess = true,
                Message = res.Message
            }); 
        }
        
        return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>()
        {
            IsSuccess = false,
            Message = res.Message
        }); 
    }
}