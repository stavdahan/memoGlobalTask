using MongoExample.Models;
using MongoExample.Services;

var builder = WebApplication.CreateBuilder(args);
var policyName = "_myAllowSpecificOrigins";

builder.Services.Configure<MongoDBSetting>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
    policy =>
    {
        policy.WithOrigins(
            "https://localhost",
            "https://www.google.com",
            "https://www.memoglobal.com")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();