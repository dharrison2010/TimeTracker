namespace TimeTracker.API.Repositories.TimeEntryRepository;

public class TimeEntryRepository : ITimeEntryRepository
{
    private readonly DataContext _context;

    public TimeEntryRepository(DataContext context) => _context = context;

    public async Task<List<TimeEntry>> CreateTimeEntry(TimeEntry timeEntry)
    {
        _context.TimeEntries.Add(timeEntry);
        await _context.SaveChangesAsync();

        return await GetAllTimeEntries();
    }

    public async Task<List<TimeEntry>?> DeleteTimeEntry(int id)
    {
        var dbTimeEntry = await _context.TimeEntries.FindAsync(id);
        if (dbTimeEntry is null)
        {
            return null;
        }

        _context.TimeEntries.Remove(dbTimeEntry);
        await _context.SaveChangesAsync();

        return await GetAllTimeEntries();
    }

    public async Task<List<TimeEntry>> GetAllTimeEntries() =>
        await _context.TimeEntries
        .Include(te => te.Project)
        .ThenInclude(p => p!.ProjectDetails)
        .ToListAsync();

    public async Task<TimeEntry?> GetTimeEntryById(int id) =>
        await _context.TimeEntries
        .Include(te => te.Project)
        .ThenInclude(p => p!.ProjectDetails)
        .FirstOrDefaultAsync(te => te.Id == id);

    public async Task<List<TimeEntry>?> UpdateTimeEntry(int id, TimeEntry timeEntry)
    {
        var dbTimeEntry = await _context.TimeEntries.FindAsync(id);

        if (dbTimeEntry is null)
        {
            return null;
        }

        dbTimeEntry.ProjectId = timeEntry.ProjectId;
        dbTimeEntry.Start = timeEntry.Start;
        dbTimeEntry.End = timeEntry.End;
        dbTimeEntry.DateUpdated = DateTime.Now;

        await _context.SaveChangesAsync();

        return await GetAllTimeEntries();
    }
}

