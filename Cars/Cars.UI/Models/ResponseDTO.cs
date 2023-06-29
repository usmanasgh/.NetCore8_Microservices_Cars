namespace Cars.UI.Models
{
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public bool Success { get; set; } = false; // Default value
        public string Message { get; set; } = string.Empty; // Default empty string

    }
}
