using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.UnitOfWork;
using AutoMapper;
using Online_Store.DTOs;
using Online_Store.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure AutoMapper
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Product, ProductDTO>();
    cfg.CreateMap<OrderItem, OrderItemDTO>();
    cfg.CreateMap<Order, OrderDTO>()
        .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));
    cfg.CreateMap<Customer, CustomerDTO>();
    cfg.CreateMap<Category, CategoryDTO>();
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper); // Register AutoMapper in the DI container

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
