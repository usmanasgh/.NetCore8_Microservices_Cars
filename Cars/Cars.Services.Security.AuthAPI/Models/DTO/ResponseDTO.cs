﻿namespace Cars.Services.Security.AuthAPI.Models.DTO
{
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public bool Success { get; set; } = true; // Default value
        public string Message { get; set; } = string.Empty; // Default empty string

    }
}
