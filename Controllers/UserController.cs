using System;
using Microsoft.AspNetCore.Mvc;
using MongoExample.Services;
using MongoExample.Models;

namespace MongoExample.Controllers;

[Controller]
public class UserController : Controller {
    private readonly MongoDBService _mongoDbService;

    public UserController(MongoDBService mongoDBService) {
        _mongoDbService = mongoDBService;
    }

    [HttpGet("getUsers/{page}")]
    public async Task<ActionResult<List<User>>> Get(int page) {
        if (page > 0) { 
            List<User> users = await _mongoDbService.GetAsync(page);
            return Ok(users);
        }else { 
            return NotFound("Page number must be greater than or equal to 1.");
        }
    }

    [HttpGet("getUser/{id}")]
    public async Task<ActionResult<User>> GetUser(int id) {
        User user =  await _mongoDbService.GetUserAsync(id);
        if (user != null) { 
            return Ok(user);
        } else { 
            return NotFound("User does not exist");
        }

    }

    [HttpPost("createUser")]

    public async Task<IActionResult> Post([FromBody] User user) {
        await _mongoDbService.CreateAsync(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.id }, user);
    }

    [HttpPut("updateUser/{id}")]
    public async Task<ActionResult> EditUser(int id, [FromBody] User user) {
        User updatedUser =  await _mongoDbService.EditUserAsync(id, user);
        if (updatedUser != null)
        {
            return CreatedAtAction(nameof(GetUser), new { id = user.id }, user);
        }else { 
            return NotFound("User does not exist");
        }
    }

    [HttpDelete("deleteUser/{id}")]
    public async Task<ActionResult<User>> Delete(int id) {
        User deletedUser =  await _mongoDbService.DeleteUserAsync(id);
        if (deletedUser != null) {
            return deletedUser;
        } else { 
            return NotFound("User does not exist");
        }
    }
}