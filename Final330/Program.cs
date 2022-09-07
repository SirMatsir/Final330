using Final330;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IUserRepo, UsersRepo>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod();
    });

    options.AddPolicy("GETONLY", builder =>
    {
        builder.AllowAnyOrigin()
               .WithMethods("GET");
    });

});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddMvc(config => {
    //config.Filters.Add<LoggingActionFilter>(); // ADD ME
}).AddXmlSerializerFormatters();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
