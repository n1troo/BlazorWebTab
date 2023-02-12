using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorWebTab.Server.Services.AuthService;
using BlazorWebTab.Shared;
using BlazorWebTab.Shared.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebTab.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> CreateUser(UserRegister userRegister)
        {
            var user = new User
            {
                Email = userRegister.Email
            };

            var response = await _authService.Register(user, userRegister.Password);
            
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> LoginUser(UserLogin userLogin)
        {
            var response = await _authService.LoginUser(userLogin.Email, userLogin.Password);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }
}
