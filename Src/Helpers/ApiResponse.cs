using System.Collections.Generic;

namespace e_commerce_blackcat_api.Src.Helpers
{
    public class ApiResponse<T>(bool success, string message, T? data = default, List<string>? errors = null)
    {
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
        public T? Data { get; set; } = data;
        public List<string>? Errors { get; set; } = errors;
    }
}
