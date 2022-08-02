using Microsoft.EntityFrameworkCore;
using zeiss.DBContext;
using zeiss.Models;
using zeiss.Repositories;
using zeiss.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//DB
builder.Services.AddDbContext<ZeissContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ZeissContext")));
builder.Services.AddScoped<IWorkUnit, WorkUnit>();

builder.Services.AddScoped<IAsyncRepository<Socket, string>, GenericRepository<Socket, string>>();
builder.Services.AddScoped<IAsyncRepository<Machine, string>, GenericRepository<Machine, string>>();

//Services
builder.Services.AddScoped<ISocketService, SocketService>();
builder.Services.AddScoped<IMachineStatusService, MachineStatusService>();


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<WebsocketService>();

var app = builder.Build();

var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromMinutes(1)
};

app.UseWebSockets(webSocketOptions);

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
