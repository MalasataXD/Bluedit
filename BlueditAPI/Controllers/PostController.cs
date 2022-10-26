using Domain.DTOs;
using Domain.Models;
using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlueditAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController
{
    // # Fields
    private readonly IPostLogic postLogic;
    
    // ¤ Constructor
    public PostController(IPostLogic postLogic)
    {
        this.postLogic = postLogic;
    }
    
    // # Create (POST operation)
    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync([FromBody] PostCreationDto dto)
    {
        try
        {
            Post created = await postLogic.CreateAsync(dto);
            return new OkObjectResult(created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    
    // # Get (GET operation)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? userName, int? ownerId,
        string? title)
    {
        try
        {
            SearchPostParametersDto parametersDto = new(userName, ownerId, title);
            IEnumerable<Post> posts = await postLogic.GetAsync(parametersDto);
            return new OkObjectResult(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Post>> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            Post postFound = await postLogic.GetByIdAsync(id);
            return new OkObjectResult(postFound);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    
    // # Update (PATCH operation)
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] PostUpdateDto dto)
    {
        try
        {
            await postLogic.UpdateAsync(dto);
            return new OkResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    // # Delete (DELETE operation)
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await postLogic.DeleteAsync(id);
            return new OkResult();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    
    
}