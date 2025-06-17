namespace Turnero.API.DTOs
{
    public class CreateDeviceDto
    {
        public string Name { get; set; } = string.Empty;
        public int ProcessId { get; set; }
        public int LabId { get; set; }
        public int DurationMinutes { get; set; }
        public string? ConfigJson { get; set; }
    }
}
