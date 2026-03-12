using Azure;
using Azure.AI.OpenAI;
using ChatBotV2.Api.Filters;
using ChatBotV2.Application.Interfaces;
using ChatBotV2.Application.Services;
using ChatBotV2.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b =>
    {
        b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddSingleton<ISettingsService, SettingsService>();

builder.Services.AddSingleton<IChatApplicationService, ChatApplicationService>();

builder.Services.AddSingleton<OpenAIClient>(sp =>
{
    var endpoint = builder.Configuration["AzureOpenAI:Endpoint"];
    var key = builder.Configuration["AzureOpenAI:Key"];
    if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(key))
        throw new InvalidOperationException("Azure OpenAI settings not configured");
    return new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
});

builder.Services.AddSingleton<IChatService>(sp =>
{
    var client = sp.GetRequiredService<OpenAIClient>();
    var deployment = builder.Configuration["AzureOpenAI:Deployment"];
    if (string.IsNullOrEmpty(deployment))
        throw new InvalidOperationException("Azure OpenAI deployment not configured");
    return new ChatService(client, deployment);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.MapGet("/", () => Results.Redirect("/swagger/index.html"))
        .ExcludeFromDescription();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
