using System.Net.Http.Json;
using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTracker.Client.Services;

public class TimeEntryService : ITimeEntryService
{
    private readonly HttpClient _http;

    public event Action? OnChange;

    public List<TimeEntryResponse> TimeEntries { get; set; } = new();

    public TimeEntryService(HttpClient http) => _http = http;

    public async Task GetTimeEntriesByProject(int projectId)
    {
        List<TimeEntryResponse>? result = null;
        if (projectId == 0)
        {
            result = await _http.GetFromJsonAsync<List<TimeEntryResponse>>("api/timeentry");
        }
        else
        {
            result = await _http.GetFromJsonAsync<List<TimeEntryResponse>>($"api/timeentry/project/{projectId}");
        }

        if (result is not null)
        {
            TimeEntries = result;
            OnChange?.Invoke();
        }
    }

    public async Task<TimeEntryResponse> GetTimeEntryById(int id)
    {
        return await _http.GetFromJsonAsync<TimeEntryResponse>($"api/timeentry/{id}");
    }
}

