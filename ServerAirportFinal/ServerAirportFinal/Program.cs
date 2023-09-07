using Microsoft.EntityFrameworkCore;
using ServerAirportFinal.BL.AirportBL;
using ServerAirportFinal.Data;
using ServerAirportFinal.Hubs;
using ServerAirportFinal.Models;


var builder = WebApplication.CreateBuilder(args);




string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<AirportContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSingleton<IAirport, Airport>();
builder.Services.AddSingleton<INotificationService<AirportImage>, NotificationAdapter>();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>      //connection to react
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000") //adress of those who want to start the UI
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});

builder.Services.AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseCors();

app.UseEndpoints(endpoints =>
{ endpoints.MapControllers(); endpoints.MapHub<AirportNotifications>("/notifications"); });



app.Run();
