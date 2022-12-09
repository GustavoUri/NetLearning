using Entities.Data;
using Entities.Interfaces.Services;
using Entities.Models;
using Entities.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace MeetApi;

public static class ServiceExtentions
{
    public static void ConfigureCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IMessagesDataService, MessagesDataService>();
        services.AddScoped<IUsersDataService, UsersDataService>();
        services.AddScoped<ICitiesDataService, CitiesDataService>();
        services.AddScoped<IHobbiesDataService, HobbiesDataService>();
        services.AddScoped<IMessageSendingService, MessageSendingService>();
        services.AddScoped<IChatsDataService, ChatsDataService>();
        services.AddScoped<IUserSearchService, UserSearchService>();
        services.AddScoped<IPicturesDataService, PicturesDataService>();
        services.AddScoped<IBlockedUsersDataService, BlockedUsersDataService>();
    }

    public static void ConfigureDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123"));
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1.0",
                Title = "MeetApiServer",
                Description = "An Api for chatting and finding new friends",
                Contact = new OpenApiContact
                {
                    Name = "Ural Aminev",
                    Email = "aminevural50@gmail.com"
                },
            });
            const string xmlPath = @"bin\Debug\MeetApi.xml";
            options.IncludeXmlComments(xmlPath);
        });
    }

    public static void ConfigureEmbeddedServices(this IServiceCollection services)
    {
        services.AddCors();
        services.AddControllers();
        services.AddSignalR();
        services.AddEndpointsApiExplorer();
    }
}