using Microsoft.EntityFrameworkCore;
using UserApplication.ServiceInterfaces;
using UserApplication.Services;
using UserDataAccess;
using UserDataAccess.Repository;
using UserDomain.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//DbContext
builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

#region Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region Services
builder.Services.AddScoped<IUserService, UserService>();
#endregion

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
