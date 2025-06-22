using Alaska;
using Alaska.Api;
using Alaska.Data;
using Alaska.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.WindowsServices;
using server.Api;
using server.Web;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(db => new DbClient());
builder.Services.AddScoped<TokenValidator>();

builder.Services.AddAuthentication("Bearer")
     .AddJwtBearer(options =>
     {
         options.Events = new JwtBearerEvents
         {
             OnMessageReceived = async context =>
             {
                 var tokenValidator = context.HttpContext.RequestServices.GetRequiredService<TokenValidator>();
                 await tokenValidator.ValidateAsync(context);
                 await Task.CompletedTask;
             }
         };
     });
builder.Services.AddAuthorization();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddWindowsService();
builder.Services.AddHostedService<AlaskaServer>();

var app = builder.Build();

app.UseStaticFiles();
app.MapAuthentications();

app.MapRoleEndPoints();
app.MapUserEndPoints();
app.MapProductEndPoints();
app.MapOutletEndPoints();
app.MapWaiterEndPoints();
app.MapSaleEndPoints();
app.MapCashflowEndPoints();
app.MapHomeEndPoints();

app.InitializeDatabase();

app.Run();