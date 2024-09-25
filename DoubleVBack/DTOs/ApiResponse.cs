namespace DoubleV.DTOs
{
    public class ApiResponse
    {
        public required string Message { get; set; }
        public object? Data { get; set; }
        public string? Error { get; set; }
    }
}
