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
    public async Task<ResponseType<UserDTO>> GetAllPersons()
    {
        return await sender.Send(new GetAllUsersQuery());
    }
    
    [AllowAnonymous]
    [HttpPost("/add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<UserDTO>> AddPerson([FromBody] UserRequest userRequest)
    {
        return await sender.Send(new AddUserCommand{UserRequest = userRequest});
    }
    
    [AllowAnonymous]
    [HttpPost("/login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<UserDTO>> LogIn([FromBody] string email, string password)
    {
        return await sender.Send(new LogInQuery(){Email = email, Password = password});
    }
}