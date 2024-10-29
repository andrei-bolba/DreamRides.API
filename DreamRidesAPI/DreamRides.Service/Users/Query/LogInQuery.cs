using System.Security.Cryptography;
using System.Text;
using Ardalis.Result;
using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Data.DTO;
using MediatR;

namespace DreamRides.Service.Users.Query;

public record LogInQuery : IRequest<Result<ResponseType<UserDTO>>>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public sealed class LogInQueryHandler(IUserRepository userRepository) : IRequestHandler<LogInQuery, Result<ResponseType<UserDTO>>>
{
    public async Task<Result<ResponseType<UserDTO>>> Handle(LogInQuery request, CancellationToken cancellationToken)
    {
        var loggedPerson = userRepository.GetUserByEmail(request.Email);
        if (loggedPerson is { IsSuccess: true, Object: not null })
        {
            using var hmac = new HMACSHA512(loggedPerson.Object.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            if (computedHash.Where((t, i) => t != loggedPerson.Object.PasswordHash[i]).Any())
            {
                return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
                {
                    IsSuccess = false,
                    Message = "Password is wrong!"
                });
            }

            return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
            {
                IsSuccess = true,
                Message = $"Welcome back {loggedPerson.Object.FirstName} {loggedPerson.Object.LastName}!"
            });
        }
        return Result<ResponseType<UserDTO>>.Success(new ResponseType<UserDTO>
        {
            IsSuccess = false,
            Message = "Email is wrong!"
        });
    }
}
