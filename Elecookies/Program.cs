using Elecookies;
using Elecookies.Database;
using Elecookies.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ElecookiesDbContext, RdbContext>();
builder.Services.AddSingleton<AccountRepository>();
builder.Services.AddSingleton<ShopRepository>();
builder.Services.AddSingleton<ShopOrderRepository>();
builder.Services.AddSingleton<CustomerRepository>();
builder.Services.AddSingleton<StaffRepository>();
builder.Services.AddSingleton<ProductRepository>();
builder.Services.AddSingleton<CouponRepository>();
builder.Services.AddSingleton<ShoppingCartRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
