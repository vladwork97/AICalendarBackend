using System.ClientModel;
using AICalendar.Services;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
using OpenAI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173",
            "https://vladwork97.github.io")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

builder.Services.AddScoped<PromptProcessor>();

builder.Services.AddChatClient(sp =>
{
    var chatClientBuilder = new ChatClientBuilder(new OpenAIClient(new ApiKeyCredential(Environment.GetEnvironmentVariable("AIKey")), new OpenAIClientOptions()
        {
            Endpoint = new Uri(Environment.GetEnvironmentVariable("AIUrl"))
    }).GetChatClient(Environment.GetEnvironmentVariable("AIModel")).AsIChatClient())
        .UseLogging()
        .UseOpenTelemetry()
        .UseFunctionInvocation();

    return chatClientBuilder.Build(sp);
});

var mcpClient = await McpClientFactory.CreateAsync(new SseClientTransport(
    new SseClientTransportOptions()
    {
        Endpoint = new Uri("https://aicalendarbackend.onrender.com/"),
        Name = "AICalendar.ApiService"
    }));
var tools = await mcpClient.ListToolsAsync();
builder.Services.AddSingleton<ChatOptions>(_ => new() { Tools = [.. tools] });

var app = builder.Build();

// Apply CORS policy

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.MapMcp();
app.MapGet("/", () => "AI Calendar backend is running.");

app.Run();