using System.Reflection;
using back_examen.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConexion>(_ => new SqlConexion(builder.Configuration.GetConnectionString("bd")));
builder.Services.AddAntiforgery(option =>
{
    option.Cookie.HttpOnly = true;
    option.Cookie.SameSite = SameSiteMode.Lax;
    option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
    
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddHttpClient();
var app = builder.Build();
var urls = builder.Configuration.GetSection("AllowedHosts").Value?.Split(",");
app.UseCors(corsPolicyBuilder =>
{
    if (urls != null) corsPolicyBuilder.WithOrigins(urls).AllowAnyHeader().AllowAnyMethod();
});

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();