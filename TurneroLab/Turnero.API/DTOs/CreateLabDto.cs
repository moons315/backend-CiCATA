﻿namespace Turnero.API.DTOs
{
    public class CreateLabDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string? Description { get; set; }
    }
}
