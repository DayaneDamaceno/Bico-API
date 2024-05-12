using Azure.Identity;
using Bico.Api.Configuration;
using Bico.Api.v1.Hubs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var keyVaultEndpoint = builder.Configuration["AzureKeyVault:Endpoint"];

if (!string.IsNullOrEmpty(keyVaultEndpoint))
{
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultEndpoint), new DefaultAzureCredential());
}

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddAuthWithJwt(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddVersion();
builder.Services.AddSwaggerGen();

var cs = "Endpoint=https://bico.service.signalr.net;AccessKey=lUUTHvzJM5qOn5+HGPcolAud2yT1lQmIj77OFHHz3Vo=;Version=1.0;";

//builder.Services.AddSignalR().AddAzureSignalR(cs);
builder.Services.AddSignalR();

builder.Services
    .AddDbContext(builder.Configuration)
    .AddBlobClient(builder.Configuration)
    .AddServiceBusClient(builder.Configuration)
    .RegisterServices(builder.Configuration);


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();


app.MapHub<ChatHub>("/hub/chat");
app.MapControllers();

app.Run();
