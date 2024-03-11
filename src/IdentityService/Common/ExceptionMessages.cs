namespace IdentityService.Common
{
    public static class ExceptionMessages
    {
        public const string GeneralErrorMessage = "An error occurred while processing your request. Please try again later.";

        public const string UserNameAlreadyExists = "User with the provided username already exists!";

        public const string EmailAlreadyExists = "User with the provided email already exists!";

        public const string InvalidCredentials = "Invalid credentials!";

        public const string PasswordConfirmationError = "Password and confirmation password do not match!";

        public const string InvalidUserId = "Invalid user id";

        public const string CompanyNameLengthError = "Company name must be between 3 and 50 characters long!";

        public const string UserNameLengthError = "Username must be between 3 and 50 characters long!";
    }
}
