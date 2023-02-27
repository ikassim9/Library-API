using Library_Api.Models;
using Library_Api.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<LibraryDatabaseSettings>(builder.Configuration.GetSection(nameof(LibraryDatabaseSettings)));

builder.Services.AddSingleton<ILibraryDatabaseSettings>(setting => setting.GetRequiredService<IOptions<LibraryDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(client => new MongoClient(builder.Configuration.GetValue<string>("LibraryDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddCors(options => options.AddPolicy("default", policy => {

    policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();

})
    );

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
