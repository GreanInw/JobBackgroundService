using POC_Quartz_JobManager.JobServices;
using POC_Quartz_JobManager.JobServices.JobConfigurations;
using Template.JobsBackgroundService;
using Template.JobsBackgroundService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddBackgroundServiceWithQuartz();
builder.Services.AddJobBackgroundServiceOptions(new JobBackgroundServiceOptions
{
    EnableBackgroundProcess = true
});

builder.Services
    .AddJobConfigurationsSingleton<FirstJobConfiguration>()
    .AddJobConfigurationsSingleton<SecondJobConfiguration>()
    .AddJobConfigurationsSingleton<ParallelFirstJobConfiguration>()
    .AddJobConfigurationsSingleton<ParallelSecondJobConfiguration>();

builder.Services
    .AddScoped<ISingleJob, FirstJobService>()
    .AddScoped<ISingleJob, SecondJobService>();

//builder.Services
//    .AddScoped<IParallelJob, ParallelFirstJobService>()
//    .AddScoped<IParallelJob, ParallelSecondJobService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
