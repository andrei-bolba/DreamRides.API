using Ardalis.Result;
using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Data.DTO;
using DreamRides.Data.RequestData;
using DreamRides.Database.Model;
using DreamRides.Service.Utils;
using MediatR;

namespace DreamRides.Service.Users.Command;

public class UpdateUserCommand : IRequest<Result<ResponseType<UserDTO>>>
{
    public required Guid UserId { get; set; }
    public required UserRequest UserRequest { get; set; }
}

public sealed class UpdateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, Result<ResponseType<UserDTO>>>
{
    public async Task<Result<ResponseType<UserDTO>>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var requiredFields = ValidationHelperUtils.ValidateOneRequiredFields(request.UserRequest);
        if (!requiredFields.Key)
        {
            return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
            {
                IsSuccess = false,
                Message = requiredFields.Value
            });
        }

        if (!string.IsNullOrWhiteSpace(request.UserRequest.Email))
        {
            var emailValidation = Utils.Utils.EmailValid(request.UserRequest.Email);
            if (!emailValidation.Key)
            {
                return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
                {
                    IsSuccess = false,
                    Message = emailValidation.Value
                });
            }
        }
        
        if (!string.IsNullOrWhiteSpace(request.UserRequest.Password))
        {
            var passwordValidation = Utils.Utils.PasswordValidation(request.UserRequest.Password);
            if (!passwordValidation.Key)
            {
                return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
                {
                    IsSuccess = false,
                    Message = passwordValidation.Value
                });
            }
        }

        var user = userRepository.GetOne(request.UserId);
        if (user is not { IsSuccess: true, Object: not null })
            return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
            {
                IsSuccess = false,
                Message = user.Message
            });
        
        var password = Utils.Utils.HashedPassword(request.UserRequest.Password ?? "");
        var editedUser = new User
        {
            Id=user.Object.Id,
            FirstName = !string.IsNullOrWhiteSpace(request.UserRequest.FirstName) ? request.UserRequest.FirstName : user.Object.FirstName,
            LastName = !string.IsNullOrWhiteSpace(request.UserRequest.LastName) ? request.UserRequest.LastName : user.Object.LastName,
            Email = !string.IsNullOrWhiteSpace(request.UserRequest.Email) ? request.UserRequest.Email : user.Object.Email,
            PasswordHash = !string.IsNullOrWhiteSpace(request.UserRequest.Password) ? password.Key : user.Object.PasswordHash,
            PasswordSalt = !string.IsNullOrWhiteSpace(request.UserRequest.Password) ? password.Value : user.Object.PasswordSalt,
            Favorites = user.Object.Favorites
        };

        var updateResult = userRepository.Update(editedUser);
        if (updateResult is { IsSuccess: true, Object: not null })
        {
            return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
            {
                Object = new UserDTO(updateResult.Object),
                IsSuccess = true,
                Message = updateResult.Message
            }); 
        }
            
        return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
        {
            IsSuccess = false,
            Message = updateResult.Message
        });

    }
}