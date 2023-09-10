
using Mapster;
using TimeTracker.Shared.Models.Project;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();
builder.Services.AddScoped<ITimeEntryService, TimeEntryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ConfigureMapster();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureMapster()
{
    TypeAdapterConfig<Project, ProjectResponse>.NewConfig()
        .Map(des => des.Description, src => src.ProjectDetails != null ? src.ProjectDetails.Description : null)
        .Map(des => des.StartDate, src => src.ProjectDetails != null ? src.ProjectDetails.StartDate : null)
        .Map(des => des.EndDate, src => src.ProjectDetails != null ? src.ProjectDetails.EndDate : null);
}