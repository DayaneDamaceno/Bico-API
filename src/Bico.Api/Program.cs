using Azure.Identity;
using Bico.Api.Configuration;
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

builder.Services
    .AddDbContext(builder.Configuration)
    .AddBlobClient(builder.Configuration)
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

app.MapControllers();

app.Run();
