using DAL;
using DAL.Context;
using DAL.Repository;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using MVC.Helpers;
using Quartz;
using Services;
using Services.EmailService;
using Services.Quartz;
using Services.ReportGenerator;

var builder = WebApplication.CreateBuilder(args);

var connection_type = builder.Configuration.GetValue<string>("DatabaseType","Sqlite");
var connectionString = builder.Configuration.GetConnectionString(connection_type);
// Add services to the container.
switch (connection_type)
{
    case "Sqlite":
        builder.Services.AddDbContext<AppDb>(options => options.UseSqlite(connectionString, options => options.MigrationsAssembly("DAL.Sqlite")));
        break;
}
builder.Services.Configure<EmailServiceSettings>(builder.Configuration.GetSection("EmailServiceSettings"));
builder.Services.Configure<RazorReportSettings>(builder.Configuration.GetSection("RazorReportSettings"));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<AppDbInitializer>();
builder.Services.AddScoped<INotificationService, EmailService>();
builder.Services.AddScoped<IEmailServiceSettings, EmailServiceSettings>();
builder.Services.AddQuartz(q =>
{ 
    q.UseMicrosoftDependencyInjectionJobFactory();
    var jobKey = new JobKey("SendReportJob");
    q.AddJob<SendReportJob>(c => c.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("SendReportJobTrigger")
        .WithCronSchedule(CronScheduleBuilder.CronSchedule(new CronExpression("*/30 * * * * ?"))));
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
builder.Services.AddScoped<IReport,RazorReport>();

var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<AppDbInitializer>();
    await initializer.InitializeAsync(true);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
