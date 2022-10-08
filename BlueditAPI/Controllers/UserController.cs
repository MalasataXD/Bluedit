using Domain.DTOs;
using Domain.Models;
using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlueditAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController
{
    // # Fields
    private readonly IUserLogic userLogic;
    
    // ¤ Constructor
    public UserController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }
    
    
    // # Create (POST operation)
    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)
    {
        try
        {
            User user = await userLogic.CreateAsync(dto);
            return new OkObjectResult(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    // # Get (GET operation)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string? username)
    {
        try
        {
            SearchUserParametersDto parametersDto = new(username);
            IEnumerable<User> users = await userLogic.GetAsync(parametersDto);
            return new OkObjectResult(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }



}