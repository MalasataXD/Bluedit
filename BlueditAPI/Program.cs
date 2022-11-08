using System.Text;
using Application.DAOInterfaces;
using Application.LogicImpl;
using Application.LogicInterfaces;
using BlueditAPI.Service;
using EfcLoginAccess.Implementations;
using EfcPostAccess;
using EfcPostAccess.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Auth;
using Shared.DAOInterface;
using LoginContext = EfcLoginAccess.LoginContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ¤ Added by ME
// builder.Services.AddScoped<FileContext>();
// builder.Services.AddScoped<LoginContext>();
builder.Services.AddDbContext<PostContext>();
builder.Services.AddDbContext<LoginContext>();
builder.Services.AddScoped<IUserDao, UserEfcDao>();
builder.Services.AddScoped<IUserLogic,UserLogicImpl>();
builder.Services.AddScoped<IPostDao, PostEfcDao>();
builder.Services.AddScoped<IPostLogic, PostLogicImpl>();
builder.Services.AddScoped<IUserLoginDao, LoginEfcDao>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
AuthorizationPolicies.AddPolicies(builder.Services);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ¤ Added by ME
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();