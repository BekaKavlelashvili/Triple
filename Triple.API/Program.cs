using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;
using Triple.API.Shared;
using Triple.Application;
using Triple.Application.Commands.Hubs;
using Triple.Infrastructure;
using Triple.Infrastructure.EventDispatcher;
using Triple.Infrastructure.Identity;
using Triple.Infrastructure.Persistence;
using Triple.Shared.Resources;
using Triple.Shared.Settings;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

var connectionString = configuration.GetSection("ConnectionStrings").GetSection("TripleConnectionString").Value;
builder.Services.AddLocalization(o => o.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new[]
        {
                        new CultureInfo("en-US"),
                        new CultureInfo("ka-GE")
        };

        options.DefaultRequestCulture = new RequestCulture("ka-GE", "ka-GE");

        options.SupportedCultures = supportedCultures;

        options.SupportedUICultures = supportedCultures;

        options.RequestCultureProviders = new[] { new AcceptLanguageHeaderRequestCultureProvider() };
    });

builder.Services.AddDbContext<TripleDbContext>(
    options =>
    {
        options.EnableSensitiveDataLogging();
        options.UseSqlServer(connectionString);
    }
);

builder.Services.AddIdentity<ApplicationUser, UserRole>()
    .AddEntityFrameworkStores<TripleDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    //options.Password.RequiredUniqueChars = 1;

    //options.SignIn.RequireConfirmedEmail = true;
});

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddSingleton<ISharedLocalizer, SharedLocalizer>();
//builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped<IntegrationEventDispatcher>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddCors();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(typeof(MediatREntryPoint).Assembly);


builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddSignalR();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.ConfigureServices();

var app = builder.Build();

app.UseSwagger();
app.UseRouting();

app.UseCors(conf =>
{
    conf.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<MessageHub>("/messageHub");
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

AutoMigration.Initialize(app, configuration);

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions()
//{
//    //This image paths needs to be created to run website!
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine(Directory.GetCurrentDirectory(), @"Images\StatedOrderImages")),
//    RequestPath = new PathString(value: @"/Images/StatedOrderImages")
//});

app.MapRazorPages();

app.MapControllers();

app.UseSwaggerUI(r =>
{
    r.SwaggerEndpoint("/swagger/v1/swagger.json", "Triple API V1");
});

app.Run();