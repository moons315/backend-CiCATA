namespace Turnero.API.DTOs
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int LabId { get; set; }
        public string? LabName { get; set; }
        public int ProcessId { get; set; }
        public string? ProcessName { get; set; }
        public int DurationMinutes { get; set; }
        public string? ConfigJson { get; set; }
    }
}
