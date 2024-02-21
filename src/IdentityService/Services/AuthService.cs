namespace IdentityService.Services
{
    using Contracts;
    using Data.Models;
    using Data.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Utilities;
    using Utilities.Enums;
    using static Common.ExceptionMessages;
    using static Common.ApplicationConstants;

    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<OperationResult<string>> RegisterAsync(RegisterModel registerModel)
        {
            var userExists = await _userManager.FindByNameAsync(registerModel.UserName);

            if (userExists != null) 
                return OperationResult<string>.Failure(UserAlreadyExists, ErrorType.BadRequest);

            var newUser = new ApplicationUser()
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                CompanyName = registerModel.CompanyName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerModel.Password);

            if (!createUserResult.Succeeded)
            {
                var errorString = string.Join(Environment.NewLine, createUserResult.Errors.Select(e => e.Description));

                return OperationResult<string>.Failure(errorString, ErrorType.BadRequest);
            }

            return OperationResult<string>.Success(UserCreated);
        }

        public async Task<OperationResult<string>> LoginAsync(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}
