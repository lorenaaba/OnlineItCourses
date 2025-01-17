using Microsoft.AspNetCore.Mvc;
using OnlineITCourses.Models;

namespace OnlineITCourses.Controllers;

[ApiController]
[Route("api/logs")]
public class LogsController : ControllerBase
{

    private static readonly List<Log> Logs = new();

    [HttpGet("get/{n}")]
    public ActionResult<IEnumerable<Log>> GetLogs(int n = 10)
    {
        return Logs.TakeLast(n).ToList();
    }

    [HttpGet("count")]
    public ActionResult<int> GetLogsCount()
    {
        return Logs.Count;
    }

public static void LogAction(string message, string level="Info")
    {
        Logs.Add(new Log
            {
                Id = Logs.Count + 1,
                Timestamp = DateTime.Now,
                Level = level,
                Message = message
            }); 
    }
}