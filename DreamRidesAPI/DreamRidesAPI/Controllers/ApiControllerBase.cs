using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamRidesAPI.Controllers;

[ApiController]
[Route("api/[controller]/")]
[Produces("application/json")]
[ProducesResponseType(500)]
[Authorize(AuthenticationSchemes = "Bearer")]
public class ApiControllerBase : ControllerBase
{
}