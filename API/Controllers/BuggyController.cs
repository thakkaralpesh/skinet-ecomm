using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    [HttpGet("unauthorized")]
    public IActionResult GetUnAuthorized(){
        return Unauthorized();
    }

    [HttpGet("badrequest")]
    public IActionResult GetBadRequest(){
        return BadRequest("Not a good Request");
    }

    [HttpGet("notfound")]
    public IActionResult GetNotFound(){
        return NotFound();
    }

    [HttpGet("internalerror")]
    public IActionResult GetInternalError(){
        throw new Exception("This is a Test exception");
    }

    [HttpPost("validationerror")]
    public IActionResult GetValidationError(CreateProductDto product){
        return Ok();
    }
}
