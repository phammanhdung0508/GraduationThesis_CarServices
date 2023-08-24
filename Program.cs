using System.Reflection;
using System.Text;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using GraduationThesis_CarServices.Encrypting;
using GraduationThesis_CarServices.Geocoder;
using GraduationThesis_CarServices.Middleware;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Notification;
using GraduationThesis_CarServices.PaymentGateway;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Repositories.Repository;
using GraduationThesis_CarServices.Repositories.Repository.Authentication;
using GraduationThesis_CarServices.Services.IService;
using GraduationThesis_CarServices.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Register HttpContextAccessor in the Dependency Injection
builder.Services.AddHttpContextAccessor();

//builder.WebHost.ConfigureKestrel(options => options.Listen(System.Net.IPAddress.Parse("172.16.5.7"), 7132));

builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header uisng the Bearer scheme (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();

        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Car Services API",
            Description = "A simple example ASP.NET Core Web API",
            //TermsOfService = new Uri("https://example.com/terms"),
        });

        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        LifetimeValidator = TokenConfiguration.Validate,
        ValidIssuer = issuer,
        ValidAudience = issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    //build.WithOrigins("https://localhost:7091");
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Connect Sql Server
//"https://project20230606170014.azurewebsites.net/"
//"https://localhost:7006/"
var connectionString = builder.Configuration.GetConnectionString("DataContextServerConection") ??
    throw new InvalidOperationException("Connection string 'DataContextLocalConection' not found.");

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(connectionString);
});

FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(builder.Configuration["Firebase:GoogleCredential"]!),
    ProjectId = builder.Configuration["Firebase:ProjectId"]!,
});

builder.Services.AddTransient<StorageMiddleware>();

builder.Services.AddSingleton<TokenConfiguration>();
builder.Services.AddSingleton<EncryptConfiguration>();
builder.Services.AddSingleton<GeocoderConfiguration>();
builder.Services.AddSingleton<FCMSendNotificationMobile>();

builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IVNPayPaymentGateway, VNPayPaymentGateway>();
builder.Services.AddScoped<IGarageRepository, GarageRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
//builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingDetailRepository, BookingDetailRepository>();
builder.Services.AddScoped<IGarageDetailRepository, GarageDetailRepository>();
builder.Services.AddScoped<IServiceDetailRepository, ServiceDetailRepository>();
builder.Services.AddScoped<ILotRepository, LotRepository>();
builder.Services.AddScoped<IMechanicRepository, MechanicRepository>();

builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IGarageService, GarageService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
//builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IMechanicService, MechanicService>();
builder.Services.AddScoped<IGarageDetailService, GarageDetailService>();
builder.Services.AddScoped<IServiceDetailService, ServiceDetailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

if (string.IsNullOrEmpty(app.Configuration.GetValue<String>("WEBSITE_NODE_DEFAULT_VERSION")))
    throw new Exception("Error at Azure Environment Variable.");

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors("MyCors");

app.UseExceptionHandler("/error");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<StorageMiddleware>();

app.Run();
