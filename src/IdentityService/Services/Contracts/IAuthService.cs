﻿namespace IdentityService.Services.Contracts
{
    using Data.ViewModels;
    using Utilities;

    public interface IAuthService
    {
        Task<OperationResult<string>> RegisterAsync(RegisterModel registerModel);

        Task<OperationResult<string>> LoginAsync(LoginModel loginModel);

        Task<OperationResult<string>> ChangePasswordAsync(string userId, ChangePasswordModel changePasswordModel);

        Task<OperationResult<string>> UpdateProfileAsync(string userId, UpdateProfileModel updateProfileModel);
    }
}
