using Streamline.Domain.Enums;

namespace Streamline.Domain.Entities.Logs
{
    public class Log
    {
        public required string Message { get; set; }
        public ESeverityLog Severity { get; set; } 
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
