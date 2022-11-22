using MeetApi.Models;
using MeetApi.SignalRContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using AppContext = MeetApi.Models.AppContext;
using System.Security.Claims;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123"));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AppContext>();
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
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions() // обрабатывает запросы к каталогу wwwroot/html
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), @"Pictures")),
    RequestPath = new PathString("/Pictures")
});
app.MapHub<ChatHub>("/chat");
app.Map("/", (ClaimsPrincipal x) =>
{
    var user = x.Identity.Name;
    return user;
});

app.Run();