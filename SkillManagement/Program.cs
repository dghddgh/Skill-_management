using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SkillManagement.Models;    
using Microsoft.Extensions.Configuration;
using AutoMapper;                
using SkillManagement.Mappings;   


var builder = WebApplication.CreateBuilder(args);

// 1. Регистрация сервисов
builder.Services.AddControllers();

// 2. Настройка Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Настройка EF Core и подключение к БД
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// 4. РЕГИСТРАЦИЯ AUTOMAPPER (ключевое дополнение!)
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 5. Дополнительные сервисы (если нужны)
// builder.Services.AddScoped<ISomeService, SomeService>();

var app = builder.Build();

// 6. Настройка middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Для детальных ошибок в разработке
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
