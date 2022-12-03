using MeetApi.Data;
using MeetApi.Interfaces.Services;
using MeetApi.Middleware;
using MeetApi.Models;
using MeetApi.Services;
using MeetApi.SignalRContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123"));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services.AddTransient<IRegistrationService, RegistrationService>();
builder.Services.AddTransient<IDataService, DataService>();
builder.Services.AddTransient<IAuthentificationService, AuthentificationService>();
builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v0.1",
        Title = "MeetApi",
        Description = "An Api for chatting and finding new friends",
        Contact = new OpenApiContact
        {
            Name = "Ural Aminev",
            Email = "aminevural50@gmail.com"
        },
    });
    //var basePath = PlatformServices.Default.Application.ApplicationBasePath;

    //Set the comments path for the swagger json and ui.
    var xmlPath = @"bin\Debug\MeetApi.xml"; 
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseCors(appBuilder => appBuilder.AllowAnyOrigin());
app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions() 
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), @"Pictures")),
    RequestPath = new PathString("/Pictures")
});
app.MapHub<ChatHub>("/chat");

app.Run();