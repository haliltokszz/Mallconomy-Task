using Business.Abstract;
using Business.Concrete;
using Core.Utilities;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region SwaggerConfiguration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MallconomyWebAPI", Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});



#endregion

//builder.Services.AddMongoDbSettings(builder.Configuration);
//builder.Services.AddSingleton(x => new MongoClient(builder.Configuration.GetConnectionString("MongoDbConnection:ConnectionString")));
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDb"));

#region BusinessServices
builder.Services.AddScoped<ILeaderboardService, LeaderboardService>();
builder.Services.AddScoped<IUserRankService, UserRankService>();
builder.Services.AddScoped<IPrizesService ,PrizesService>();

#endregion

#region DataAccessServices
builder.Services.AddScoped<ILeaderboardDal, LeaderboardDal>();
builder.Services.AddScoped<IPrizeDal, PrizeDal>();
builder.Services.AddScoped<IPointsDal, PointsDal>();
builder.Services.AddScoped<IUserRankDal, UserRankDal>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();