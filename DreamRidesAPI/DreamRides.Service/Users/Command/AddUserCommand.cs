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
        if (string.IsNullOrWhiteSpace(request.UserRequest.FirstName) || 
            string.IsNullOrWhiteSpace(request.UserRequest.LastName) || 
            string.IsNullOrWhiteSpace(request.UserRequest.Email) || 
            string.IsNullOrWhiteSpace(request.UserRequest.Password))
        {
            return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
            {
                IsSuccess = false,
                Message = EmptyFields(request.UserRequest)
            });
        }

        var emailValidation = Utils.Utils.EmailValid(request.UserRequest.Email);
        if (!emailValidation.Key)
        {
            return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
            {
                IsSuccess = false,
                Message = emailValidation.Value
            });
        }
        
        var passwordValidation = Utils.Utils.PasswordValidation(request.UserRequest.Password);
        if (!passwordValidation.Key)
        {
            return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
            {
                IsSuccess = false,
                Message = passwordValidation.Value
            });
        }
        
        var password = Utils.Utils.HashedPassword(request.UserRequest.Password);
        var newUser = new User
        {
            FirstName = request.UserRequest.FirstName,
            LastName = request.UserRequest.LastName,
            Email = request.UserRequest.Email,
            PasswordHash = password.Key,
            PasswordSalt = password.Value,
            CreatedAt = DateTime.Now
        };
        var res = userRepository.Add(newUser);

        if (res is { IsSuccess: true, Object: not null })
        {
            return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
            {
                Object = new UserDTO(res.Object),
                IsSuccess = true,
                Message = res.Message
            }); 
        }
        
        return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
        {
            IsSuccess = false,
            Message = res.Message
        }); 
    }

    private string EmptyFields(UserRequest user)
    {
        var emptyFields = new List<string>();

        if (string.IsNullOrWhiteSpace(user.FirstName))
        {
            emptyFields.Add(nameof(user.FirstName));
        }

        if (string.IsNullOrWhiteSpace(user.LastName))
        {
            emptyFields.Add(nameof(user.LastName));
        }

        if (string.IsNullOrWhiteSpace(user.Email))
        {
            emptyFields.Add(nameof(user.Email));
        }

        if (string.IsNullOrWhiteSpace(user.Password))
        {
            emptyFields.Add(nameof(user.Password));
        }

        if (emptyFields.Any())
        {
            var fieldsMessage = string.Join(", ", emptyFields);
            return $"The following fields are empty: {fieldsMessage}";
        }

        return string.Empty;
    }
}