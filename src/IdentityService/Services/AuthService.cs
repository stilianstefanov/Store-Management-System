namespace IdentityService.Services
{
    using Microsoft.AspNetCore.Identity;
    using Contracts;
    using Data.Models;
    using Data.ViewModels;
    using Utilities;
    using Utilities.Enums;
    using static Common.ExceptionMessages;
    using static Common.ApplicationConstants;

    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenGeneratorService _tokenGenerator;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<ApplicationUser> userManager, 
            ITokenGeneratorService tokenGenerator,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _configuration = configuration;
        }

        public async Task<OperationResult<string>> RegisterAsync(RegisterModel registerModel)
        {
            var usernameResult = await ValidateUsernameAsync(registerModel.UserName);
            if (usernameResult != null) return usernameResult;

            var emailResult = await ValidateEmailAsync(registerModel.Email);
            if (emailResult != null) return emailResult;

            var newUser = new ApplicationUser()
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email,
                CompanyName = registerModel.CompanyName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var identityResult = await _userManager.CreateAsync(newUser, registerModel.Password);

            if (!identityResult.Succeeded)
            {
                var errorString = string.Join(Environment.NewLine, identityResult.Errors.Select(e => e.Description));

                return OperationResult<string>.Failure(errorString, ErrorType.BadRequest);
            }

            return OperationResult<string>.Success(UserCreated);
        }

        public async Task<OperationResult<string>> LoginAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserNameOrEmail) 
                       ?? await _userManager.FindByEmailAsync(loginModel.UserNameOrEmail);

            if (user == null)
                return OperationResult<string>.Failure(InvalidCredentials, ErrorType.BadRequest);

            var passwordValid = await _userManager.CheckPasswordAsync(user, loginModel.Password);

            if (!passwordValid)
                return OperationResult<string>.Failure(InvalidCredentials, ErrorType.BadRequest);

            var userRoles = await _userManager.GetRolesAsync(user);

            var token = _tokenGenerator.GenerateToken(user, _configuration, userRoles);

            return OperationResult<string>.Success(token);
        }

        public async Task<OperationResult<string>> ChangePasswordAsync(string userId, ChangePasswordModel changePasswordModel)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return OperationResult<string>.Failure(InvalidUserId, ErrorType.BadRequest);

            var identityResult = await _userManager
                .ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);

            if (!identityResult.Succeeded)
            {
                var errorString = string.Join(Environment.NewLine, identityResult.Errors.Select(e => e.Description));

                return OperationResult<string>.Failure(errorString, ErrorType.BadRequest);
            }

            return OperationResult<string>.Success(PasswordChanged);
        }

        public async Task<OperationResult<string>> UpdateProfileAsync(string userId, UpdateProfileModel updateProfileModel)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return OperationResult<string>.Failure(InvalidUserId, ErrorType.BadRequest);

            var usernameResult = await ValidateUsernameAsync(updateProfileModel.UserName, user.UserName!);
            if (usernameResult != null) return usernameResult;

            var emailResult = await ValidateEmailAsync(updateProfileModel.Email, user.Email!);
            if (emailResult != null) return emailResult;

            user.UserName = updateProfileModel.UserName;
            user.Email = updateProfileModel.Email;
            user.CompanyName = updateProfileModel.CompanyName;

            var identityResult = await _userManager.UpdateAsync(user);

            if (!identityResult.Succeeded)
            {
                var errorString = string.Join(Environment.NewLine, identityResult.Errors.Select(e => e.Description));

                return OperationResult<string>.Failure(errorString, ErrorType.BadRequest);
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var token = _tokenGenerator.GenerateToken(user, _configuration, userRoles);

            return OperationResult<string>.Success(token);
        }

        private async Task<OperationResult<string>?> ValidateUsernameAsync(string username, string currentUsername = null!)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null && user.UserName != currentUsername)
                return OperationResult<string>.Failure(UserNameAlreadyExists, ErrorType.BadRequest);

            return null;
        }

        private async Task<OperationResult<string>?> ValidateEmailAsync(string email, string currentEmail = null!)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null && user.Email != currentEmail)
                return OperationResult<string>.Failure(EmailAlreadyExists, ErrorType.BadRequest);

            return null;
        }
    }
}
