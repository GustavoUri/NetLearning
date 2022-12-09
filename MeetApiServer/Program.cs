using Entities.SignalRContext;
using MeetApi;
using MeetApi.Middlewares;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureCustomServices();
builder.Services.ConfigureDatabase();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureEmbeddedServices();

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