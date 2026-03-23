using Drkb.Notification.Application.Interfaces;
using Drkb.Notification.Hubs;
using Drkb.Notification.Infrastructure.Data;
using Drkb.Notification.Infrastructure.DI;
using Drkb.Notification.Infrastructure.LoggerConfiguration;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<INotificationDispatcher, SignalRNotificationDispatcher>();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCorsPolicies();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddBehavior();
builder.Services.AddMediatr();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwagger();
builder.Services.AddUnitOfWork();
builder.Services.AddSignalR();
builder.Services.AddDataProviderServices();
builder.Services.AddQueryObjects();
builder.Services.AddRabbitMQCollection(builder.Configuration);
builder.Services.AddEventHandlerServices();
builder.Services.AddAuxiliaryServices();

builder.Services.AddSerilogLogger();
Log.Logger = SerilogConfiguration.GetSerilogConfiguration(builder.Configuration);
builder.Host.UseSerilog();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger()
        .UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "DrkbNews");
            options.RoutePrefix = string.Empty;
        });
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<NotificationDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowFrontend");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<NotificationHub>("/notifications");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();