﻿namespace CreditService.Utilities
{
    using Enums;
    using Microsoft.AspNetCore.Mvc;
    using static Common.ExceptionMessages;

    public static class ControllerExtensions
    {
        public static IActionResult Error(this ControllerBase controller,
            ErrorType errorType = ErrorType.Internal, string errorMessage = GeneralErrorMessage)
        {
            return errorType switch
            {
                ErrorType.NotFound => controller.NotFound(errorMessage),
                _ => controller.StatusCode(StatusCodes.Status500InternalServerError, new { Error = GeneralErrorMessage })
            };
        }
    }
}
