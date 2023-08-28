namespace TimeTracker.API.Repositories.TimeEntryRepository;

public class TimeEntryRepository : ITimeEntryRepository
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

    public List<TimeEntry> CreateTimeEntry(TimeEntry timeEntry)
    {
        _timeEntries.Add(timeEntry);
        return _timeEntries;
    }

    public List<TimeEntry>? DeleteTimeEntry(int id)
    {
        var entryToDelete = _timeEntries.FirstOrDefault(t => t.Id == id);
        if (entryToDelete is null)
        {
            return null;
        }
        _timeEntries.Remove(entryToDelete);
        return _timeEntries;
    }

    public List<TimeEntry> GetAllTimeEntries() => _timeEntries;

    public TimeEntry? GetTimeEntryById(int id) =>
        _timeEntries.FirstOrDefault(t => t.Id == id);


    public List<TimeEntry>? UpdateTimeEntry(int id, TimeEntry timeEntry)
    {
        var entryToUpdateIndex = _timeEntries.FindIndex(t => t.Id == id);
        if (entryToUpdateIndex == -1)
        {
            return null;
        }
        _timeEntries[entryToUpdateIndex] = timeEntry;
        return _timeEntries;
    }
}

