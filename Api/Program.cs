using Domain.Interfaces.Data;
using Infra.CrossCutting;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//IServiceCollection services, IConfiguration configuration
NativeInjector.Setup(builder.Services, builder.Configuration);

var app = builder.Build();
 
UnityOfWork dbcontext = (UnityOfWork)app.Services.GetRequiredService<IUnityOfWork>();
dbcontext.Database.EnsureCreated();

// Configure the HTTP request pipeline.
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Response.Redirect("/swagger/index.html");        
    }
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//CORS configuration
app.UseCors(builder => builder
    .SetIsOriginAllowed((host) => true)
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());

app.Run();


