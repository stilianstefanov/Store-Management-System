namespace IdentityService.Services
{
    using Contracts;
    using Data.ViewModels;
    using Utilities;

    public class AuthService : IAuthService
    {
        public async Task<OperationResult<string>> RegisterAsync(RegisterModel registerModel)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<string>> LoginAsync(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }
    }
}
