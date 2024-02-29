namespace IdentityService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Data.ViewModels;
    using Utilities.Extensions;
    using Services.Contracts;

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

        [HttpPost]
        [Route(nameof(ChangePassword))]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel changePasswordModel)
        {
            var result = await _authService.ChangePasswordAsync(User.GetId()!, changePasswordModel);

            if (!result.IsSuccess) return this.Error(result.ErrorType, result.ErrorMessage!);

            return this.Ok(result.Data);
        }
    }
}
