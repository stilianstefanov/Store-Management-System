namespace DelayedPaymentService.Utilities
{
    using Enums;

    public class OperationResult<T>
    {
        public bool IsSuccess { get; private set; }
        public T? Data { get; private set; }
        public string? ErrorMessage { get; private set; }

        public ErrorType ErrorType { get; private set; } = ErrorType.None;

        public static OperationResult<T> Success(T data) => new OperationResult<T> { IsSuccess = true, Data = data };

        public static OperationResult<T> Failure(string errorMessage, ErrorType errorType = ErrorType.Internal)
            => new OperationResult<T> { IsSuccess = false, ErrorMessage = errorMessage, ErrorType = errorType };
    }
}
