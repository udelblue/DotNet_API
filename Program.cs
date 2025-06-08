using DotNet_API.Data;
using DotNet_API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Register MemberContext for EF Core In-Memory
builder.Services.AddDbContext<MemberContext>(options =>
    options.UseInMemoryDatabase("MemberDb"));


builder.Services.AddScoped<MembersDAO>();
builder.Services.AddScoped<MemberServices>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });


}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
