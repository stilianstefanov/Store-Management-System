namespace ProductService.Utilities.Enums
{
    using Microsoft.AspNetCore.Mvc;
    using static Common.ExceptionMessages;

    public static class ControllerExtensions
    {
        public static IActionResult GeneralError(this ControllerBase controller)
        {
            return controller.StatusCode(StatusCodes.Status500InternalServerError, new { Error = GeneralErrorMessage });
        }
    }
}
