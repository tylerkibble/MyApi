using System.Diagnostics.CodeAnalysis;


namespace MyApi.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public ApiResponse(bool success, string message, [AllowNull] T data = default)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Operation Success.")
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> ErrorResponse(string message)
        {
            return new ApiResponse<T>(false, message, default(T));
        }

        internal static ApiResponse<bool> SuccessResponse(bool updatedUser, string message = "Operation Success.")
        {
            return new ApiResponse<bool>(updatedUser, message, updatedUser);
        }
    }
}