using Microsoft.AspNetCore.Mvc;
using TimeTracker.Shared.Entities;

namespace TimeTracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TimeEntryController : ControllerBase
{

    private static readonly List<TimeEntry> _timeEntries = new()
    {
        new TimeEntry
        {
            Id = 1,
            Project = "Time Tracker App",
            End = DateTime.Now.AddHours(1)
        }
    };

    [HttpGet]
    public ActionResult<List<TimeEntry>> GetAllTimeEntries() => Ok(_timeEntries);

    [HttpPost]
    public ActionResult<List<TimeEntry>> CreateTimeEntry(TimeEntry timeEntry)
    {
        _timeEntries.Add(timeEntry);
        return Ok(_timeEntries);
    }
}
