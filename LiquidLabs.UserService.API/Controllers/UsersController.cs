using AutoMapper;
using LiquidLabs.UserService.API.Dtos;
using LiquidLabs.UserService.API.Services;
using LiquidLabs.UserService.Domain.Entities;
using LiquidLabs.UserService.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiquidLabs.UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IExternalUserService _externalService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService,IExternalUserService externalUser,IMapper mapper)
        {
            _userService     = userService;
            _externalService = externalUser;
            _mapper          = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var users = await _userService.GetAll();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                var found = await _externalService.GetUserById(id);
                if(found == null)
                    return NotFound($"User with Id {id} not found.");

                user = _mapper.Map<User>(found);

                user = await _userService.AddUser(user);
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
    }
}
