using DreamRides.Communication.Types;
using DreamRides.Data.DTO;
using DreamRides.Data.RequestData;
using DreamRides.Service.Cars.Command;
using DreamRides.Service.Cars.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamRidesAPI.Controllers;

public sealed class CarController(ISender sender): ApiControllerBase
{
    [AllowAnonymous]
    [HttpGet("/all")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<CarDTO>> GetAllCar()
    {
        return await sender.Send(new GetAllCarsQuery());
    }
    
    [AllowAnonymous]
    [HttpGet("/{carId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ResponseType<CarDTO>> GetOneCar(Guid carId)
    {
        return await sender.Send(new GetOneCarQuery{CarId = carId});
    }
    
    [AllowAnonymous]
    [HttpPost("/add")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ResponseType<CarDTO>> AddCar([FromBody] CarRequest carRequest)
    {
        return await sender.Send(new AddCarCommand{CarRequest = carRequest});
    }
    
    [AllowAnonymous]
    [HttpDelete("/delete/{carId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ResponseType<CarDTO>> DeleteCar(Guid carId)
    {
        return await sender.Send(new DeleteCarCommand(){Id = carId});
    }
    
    [AllowAnonymous]
    [HttpPut("/update/{carId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ResponseType<CarDTO>> UpdateCar(Guid carId, [FromBody]CarRequest car)
    {
        return await sender.Send(new UpdateCarCommand(){CarId = carId, CarRequest = car});
    }
}