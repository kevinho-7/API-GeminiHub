using GeminiHubApi.Services;
using Microsoft.AspNetCore.Components.RenderTree;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

//Console.WriteLine(builder.Environment.EnvironmentName);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<GeminiService>();
builder.Services.AddScoped<GenPdfService>();
builder.Services.AddScoped<AskGeminiService>();

// Development CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("Dev", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("Content-Disposition");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("Dev");
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


