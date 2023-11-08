using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SistemaDeGerenciamentoDeTarefas.Interfaces.Services;
using SistemaDeGerenciamentoDeTarefas.Models;

namespace SistemaDeGerenciamentoDeTarefas.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Users model)
        {
            var enteredUsername = model.Username;
            var enteredPassword = model.Password;

            var defaultUsername = _configuration["Authentication:DefaultUsername"];
            var defaultPassword = _configuration["Authentication:DefaultPassword"];

            if (enteredUsername == defaultUsername && enteredPassword == defaultPassword)
            {
                var token = _authService.GenerateJwtToken(enteredUsername);
                return Ok(new { Token = token });
            }

            return Unauthorized(new { Message = "Credenciais inválidas" });
        }
    }
}
