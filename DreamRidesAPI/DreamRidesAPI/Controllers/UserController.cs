using DreamRides.Communication.Types;
using DreamRides.Data.DTO;
using DreamRides.Data.RequestData;
using DreamRides.Service.Users.Command;
using DreamRides.Service.Users.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamRidesAPI.Controllers;

public sealed class UserController(ISender sender): ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("/all")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<UserDTO>> GetAllUser()
    {
        return await sender.Send(new GetAllUsersQuery());
    }
    
    [AllowAnonymous]
    [HttpGet("/{userId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<UserDTO>> GetOneUser(Guid userId)
    {
        return await sender.Send(new GetOneUserQuery{UserId = userId});
    }
    
    [AllowAnonymous]
    [HttpPost("/add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<UserDTO>> AddUser([FromBody] UserRequest userRequest)
    {
        return await sender.Send(new AddUserCommand{UserRequest = userRequest});
    }
    
    [AllowAnonymous]
    [HttpPost("/login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<UserDTO>> LogInUser([FromBody] LogInDTO user)
    {
        return await sender.Send(new LogInQuery(){Email = user.Email, Password = user.Password});
    }
    
    [AllowAnonymous]
    [HttpDelete("/delete/{userId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<UserDTO>> DeleteUser(Guid userId)
    {
        return await sender.Send(new DeleteCommand(){Id = userId});
    }
    
    [AllowAnonymous]
    [HttpPut("/update/{userId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<UserDTO>> UpdateUser(Guid userId, [FromBody]UserRequest user)
    {
        return await sender.Send(new UpdateCommand(){UserId = userId, UserRequest = user});
    }
}