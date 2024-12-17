using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server_Werwolf.Hubs;
using Server_Werwolf.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<RoomContext>(opt => opt.UseInMemoryDatabase("Rooms"));
builder.Services.AddSignalR();
builder.Services.AddSingleton<MyHub>();
// builder.WebHost.UseUrls("http://10.31.148.30:5000");

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<MyHub>("/myhub");

app.Run();