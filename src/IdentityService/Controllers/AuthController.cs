using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    using Data.ViewModels;
    using Services.Contracts;
    using Utilities;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var result = await _authService.RegisterAsync(registerModel);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return this.Ok(result.Data);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var result = await _authService.LoginAsync(loginModel);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return this.Ok(result.Data);
        }
    }
}
